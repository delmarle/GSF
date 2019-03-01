﻿using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Dispatcher
{
	/// <summary>
	/// The base class exists in case we want to generically call a function
	/// without caring about the type.
	/// </summary>
	public abstract class UnityEventSystemBase
	{
		// Global usage, LocalEventSystem handles per GameObject
		public static System.Action OnGlobalReset;
		public static System.Action OnResetAll;
		public static System.Action<object> OnUnsubscribeTarget;

		public abstract void UnsubscribeTarget(object target);
		public abstract void Reset();
		public abstract void CleanUp();
		public abstract void SubscribeSoft(object nodeObj);
		public abstract void UnsubscribeSoft(object nodeObj);
		public abstract void AddSubscription(object nodeObj, bool isActive);
		public abstract void RemoveSubscription(object nodeObj);
	}

	public class UnityEventSystem<T> : UnityEventSystemBase where T : struct
	{
		public bool LockSubscriptions;

		private Queue<Subscription> _queuedSubscriptions = new Queue<Subscription>();
		private LinkedList<EventCallback<T>> _callbacks = new LinkedList<EventCallback<T>>();
		private bool _reset;
		private bool _internalLockSubscriptions;
		private LinkedListNode<EventCallback<T>> _nextNode;

		private static Stack<LinkedListNode<EventCallback<T>>> _nodePool;
		private const int InitialNodePopulation = 100;

		private bool Locked { get { return LockSubscriptions || _internalLockSubscriptions; } }

		public bool Debug { get; set; }

		public UnityEventSystem()
		{
			OnResetAll += Reset;
		}

		private struct Subscription
		{
			public LinkedListNode<EventCallback<T>> node;

			public Subscription(LinkedListNode<EventCallback<T>> node)
			{
				this.node = node;
			}
		}

		public bool SendEvent(T ev)
		{
			_internalLockSubscriptions = true;
			bool wasTerminated = SendSingleEvent(ev);
			_internalLockSubscriptions = false;
			HandlePostEvent();

			return wasTerminated;
		}

		private bool SendSingleEvent(T ev)
		{
			LinkedListNode<EventCallback<T>> node = _callbacks.First;
			bool terminated = false;

			while (node != null)
			{
				_nextNode = node.Next;

				if (node.Value.Callback != null &&
					node.Value.Callback.Target == null ||
					node.Value.TerminableCallback != null &&
					node.Value.TerminableCallback.Target == null)
				{
					// Remove, must be gone.
					_callbacks.Remove(node);
					_nodePool.Push(node);
				}

				if (!terminated && node.Value.IsActive)
				{
					if (node.Value.Callback != null)
					{
						if (Debug)
						{
							UnityEngine.Debug.Log("Sending event: " + node.Value.Callback.Target + " " + node.Value);
						}

						node.Value.Callback(ev);
					}
					else
					{
						if (Debug)
						{
							if (node.Value.TerminableCallback != null)
								UnityEngine.Debug.Log("Sending Terminatable event: " + node.Value.TerminableCallback.Target + " " + node.Value);
						}

						bool shouldTerminate = node.Value.TerminableCallback != null && node.Value.TerminableCallback(ev);

						if (shouldTerminate)
						{
							terminated = true;
						}
					}
				}

				node = _nextNode;
			}

			return terminated;
		}

		private void HandlePostEvent()
		{
			if (_reset)
			{
				_reset = false;
				Reset();
			}
			else
			{
				while (_queuedSubscriptions.Count > 0)
				{
					Subscription sub = _queuedSubscriptions.Dequeue();

					if (Debug)
					{
						UnityEngine.Debug.Log("Handling Queued Subscribe: " + sub.node.Value.Callback.Target + " " + sub.node.Value);
					}

					AddToSubscribeList(sub.node);
				}
			}
		}

		/// <summary>
		/// Once Reset all active SusbscriptionHandles are no longer valid.
		/// They are still safe to unsubscribe with but have no effect.
		/// </summary>
		public override void Reset()
		{
			if (Locked)
			{
				_reset = true;
				return;
			}

			_queuedSubscriptions = new Queue<Subscription>();
			_callbacks = new LinkedList<EventCallback<T>>();
		}

		public LinkedListNode<EventCallback<T>> RegisterCallback(System.Action<T> callback)
		{
			LinkedListNode<EventCallback<T>> node = GetNode(callback);
			RegisterNode(node);
			return node;
		}

		public LinkedListNode<EventCallback<T>> RegisterCallback(System.Func<T, bool> terminableCallback)
		{
			LinkedListNode<EventCallback<T>> node = GetNode(terminableCallback);
			RegisterNode(node);
			return node;
		}

		private void RegisterNode(LinkedListNode<EventCallback<T>> node)
		{
			node.Value.IsActive = false;
			_callbacks.AddLast(node);
		}

		public SubscriptionHandle<T> GetSubscriptionHandle(System.Action<T> callback)
		{
			return new SubscriptionHandle<T>(this, callback);
		}

		public SubscriptionHandle<T> GetSubscriptionHandle(System.Func<T, bool> terminableCallback)
		{
			return new SubscriptionHandle<T>(this, terminableCallback);
		}

		public void Subscribe(System.Action<T> callback)
		{
			SubscribeGetNode(callback);
		}

		public void Subscribe(System.Func<T, bool> terminateCallback)
		{
			SubscribeGetNode(terminateCallback);
		}

		public LinkedListNode<EventCallback<T>> SubscribeGetNode(System.Action<T> callback)
		{
			LinkedListNode<EventCallback<T>> node = GetNode(callback);
			SubscribeNode(node);

			return node;
		}

		public LinkedListNode<EventCallback<T>> SubscribeGetNode(System.Func<T, bool> terminableCallback)
		{
			LinkedListNode<EventCallback<T>> node = GetNode(terminableCallback);
			SubscribeNode(node);

			return node;
		}

		public override void AddSubscription(object nodeObj, bool isActive)
		{
			LinkedListNode<EventCallback<T>> node = (LinkedListNode<EventCallback<T>>)nodeObj;

			SubscribeNode(node);
			node.Value.IsActive = isActive;
		}

		private void SubscribeNode(LinkedListNode<EventCallback<T>> node)
		{
			if (Locked)
			{
				if (Debug)
				{
					UnityEngine.Debug.Log("Queued Subscribe: " + node.Value.Callback.Target + " " + node.Value);
				}

				_queuedSubscriptions.Enqueue(new Subscription(node));
			}
			else
			{
				if (Debug)
				{
					UnityEngine.Debug.Log("Subscribe: " + node.Value.Callback.Target + " " + node.Value);
				}

				AddToSubscribeList(node);
			}
		}

		public override void SubscribeSoft(object nodeObj)
		{
			LinkedListNode<EventCallback<T>> node = (LinkedListNode<EventCallback<T>>) nodeObj;

			Assert.IsTrue(node != null);

			if (Debug)
			{
				UnityEngine.Debug.Log("Subscribe soft: " + node.Value.Callback.Target + " " + node.Value.Callback);
			}

			node.Value.IsActive = true;
		}

		private void AddToSubscribeList(LinkedListNode<EventCallback<T>> node)
		{
			if (node.List == null)
			{
				node.Value.IsActive = true;
				_callbacks.AddLast(node);
			}
		}

		public void Unsubscribe(System.Action<T> callback)
		{
			LinkedListNode<EventCallback<T>> node = FindLastNode(callback);

			if (node == null)
			{
				return;
			}

			if (Debug)
			{
				UnityEngine.Debug.Log("Unsubscribe: " + callback.Target + " " + callback);
			}

			RemoveFromCallbacks(node);
		}

		public void Unsubscribe(System.Func<T, bool> terminableCallback)
		{
			LinkedListNode<EventCallback<T>> node = FindLastNode(terminableCallback);

			if (node == null)
			{
				return;
			}

			if (Debug)
			{
				UnityEngine.Debug.Log("Unsubscribe: " + terminableCallback.Target + " " + terminableCallback);
			}

			RemoveFromCallbacks(node);
		}

		public override void RemoveSubscription(object nodeObj)
		{
			RemoveFromCallbacks((LinkedListNode<EventCallback<T>>)nodeObj, false);
		}

		public void UnsubscribeWithNode(LinkedListNode<EventCallback<T>> node)
		{
			if (node == null)
			{
				return;
			}

			RemoveFromCallbacks(node);
		}

		public override void UnsubscribeSoft(object nodeObj)
		{
			LinkedListNode<EventCallback<T>> node = (LinkedListNode<EventCallback<T>>) nodeObj;

			if (node == null)
			{
				return;
			}

			if (Debug)
			{
				UnityEngine.Debug.Log("Unsubscribe soft: " + node.Value.Callback.Target + " " + node.Value.Callback);
			}

			node.Value.IsActive = false;
		}

		public bool HasSubscribers()
		{
			LinkedListNode<EventCallback<T>> node = _callbacks.First;

			while (node != null)
			{
				if (node.Value.IsActive)
				{
					return true;
				}

				node = node.Next;
			}

			return false;
		}

		private void RemoveFromCallbacks(LinkedListNode<EventCallback<T>> node, bool addBackToPool = true)
		{
			if (node == _nextNode)
			{
				_nextNode = _nextNode.Next;
			}

			if (node.List == _callbacks)
			{
				_callbacks.Remove(node);
			}

			if (addBackToPool)
			{
				node.Value.IsActive = false;
				node.Value.Callback = null;
				node.Value.TerminableCallback = null;
				_nodePool.Push(node);
			}
		}

		private LinkedListNode<EventCallback<T>> GetNode(System.Action<T> callback)
		{
			LinkedListNode<EventCallback<T>> node = GetBlankNode();
			node.Value.Callback = callback;

			return node;
		}

		private LinkedListNode<EventCallback<T>> GetNode(System.Func<T, bool> terminableCallback)
		{
			LinkedListNode<EventCallback<T>> node = GetBlankNode();
			node.Value.TerminableCallback = terminableCallback;

			return node;
		}

		public static LinkedListNode<EventCallback<T>> GetBlankNode()
		{
			if (_nodePool == null)
			{
				_nodePool = new Stack<LinkedListNode<EventCallback<T>>>();

				for (int i = 0; i < InitialNodePopulation; i++)
				{
					_nodePool.Push(new LinkedListNode<EventCallback<T>>(new EventCallback<T>()));
				}
			}

			LinkedListNode<EventCallback<T>> node;

			if (_nodePool.Count > 0)
			{
				node = _nodePool.Pop();
			}
			else
			{
				node = new LinkedListNode<EventCallback<T>>(new EventCallback<T>());
			}

			return node;
		}

		private LinkedListNode<EventCallback<T>> FindLastNode(System.Action<T> callback)
		{
			LinkedListNode<EventCallback<T>> node = _callbacks.Last;

			while (node != null)
			{
				if (node.Value.Callback == callback)
				{
					return node;
				}

				node = node.Previous;
			}

			return null;
		}

		private LinkedListNode<EventCallback<T>> FindLastNode(System.Func<T, bool> terminableCallback)
		{
			LinkedListNode<EventCallback<T>> node = _callbacks.Last;

			while (node != null)
			{
				if (node.Value.TerminableCallback == terminableCallback)
				{
					return node;
				}

				node = node.Previous;
			}

			return null;
		}


		public override void CleanUp()
		{
			OnResetAll -= Reset;
			OnGlobalReset -= Reset;
		}

		public static UnityEventSystem<T> Global
		{
			get
			{
				if (_global == null)
				{
					_global = new UnityEventSystem<T>();
					OnGlobalReset += _global.Reset;
					OnUnsubscribeTarget += _global.UnsubscribeTarget;
				}

				return _global;
			}
		}

		private static UnityEventSystem<T> _global;

		public override void UnsubscribeTarget(object target)
		{
			// PERF: This could be made more efficient by using
			// a dictionary with the key being target and the list
			// of subscriptions being the value.
			LinkedListNode<EventCallback<T>> node = _callbacks.First;

			while (node != null)
			{
				LinkedListNode<EventCallback<T>> nextNode = node.Next;

				if (node.Value.Callback.Target == target)
				{
					RemoveFromCallbacks(node);
				}

				node = nextNode;
			}
		}
	}
}


