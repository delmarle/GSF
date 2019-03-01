using System.Collections.Generic;


namespace Dispatcher
{
	/// <summary>
	/// This is used for optimization, by using this handle the event
	/// system can avoid dictionary look ups and list searching.
	/// 
	/// If optimization isn't a worry then these can be disregarded.
	/// </summary>
	/// <typeparam name="T">The event associated with the handle.</typeparam>
	public struct EventHandle<T> where T : struct
	{
		private readonly UnityEventSystem<T> _eventSystem;

		public EventHandle(UnityEventSystem<T> system)
		{
			_eventSystem = system;
		}

		public override int GetHashCode()
		{
			return (_eventSystem != null ? _eventSystem.GetHashCode() : 0);
		}

		public bool Equals(EventHandle<T> other)
		{
			return Equals(_eventSystem, other._eventSystem);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is EventHandle<T> && Equals((EventHandle<T>) obj);
		}

		public static bool operator ==(EventHandle<T> a, EventHandle<T> b)
		{
			return a._eventSystem == b._eventSystem;
		}

		public static bool operator !=(EventHandle<T> a, EventHandle<T> b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Send the event to the system stored by the handle.
		/// </summary>
		/// <param name="ev">The event to send.</param>
		public void SendEvent(T ev)
		{
			_eventSystem.SendEvent(ev);
		}
	}

	/// <summary>
	/// Used for more efficient subscriptions and unsubscriptions.
	/// </summary>
	/// <typeparam name="T">The event associated with the handle.</typeparam>
	public struct SubscriptionHandle<T> where T : struct
	{
		private UnityEventSystem<T> _system;
		private System.Action<T> _callback;
		private System.Func<T, bool> _terminableCallback; 
		private LinkedListNode<EventCallback<T>> _node;

		public SubscriptionHandle(
			UnityEventSystem<T> system,
			System.Action<T> callback)
		{
			_system = system;
			_callback = callback;
			_terminableCallback = null;
			_node = null;
		}

		public SubscriptionHandle(
			UnityEventSystem<T> system,
			System.Func<T, bool> terminableCallback)
		{
			_system = system;
			_callback = null;
			_terminableCallback = terminableCallback;
			_node = null;
		}

		/// <summary>
		/// Subscribe the function to the event associated with this handle.
		/// </summary>
		public void Subscribe()
		{
			if (_callback != null)
			{
				_node = _system.SubscribeGetNode(_callback);
			}
			else
			{
				_node = _system.SubscribeGetNode(_terminableCallback);
			}
		}

		/// <summary>
		/// Unsubscribe the function from the event associated with this handle.
		/// </summary>
		public void Unsubscribe()
		{			
			_system.UnsubscribeWithNode(_node);
		}
	}

	/// <summary>
	/// The send modes that determine when subscribes have their functions invoked.
	/// </summary>
	public enum EventSendMode
	{
		/// <summary>
		/// Use whatever is currently set on EventManager.defaultSendMode. The default is Immediate.
		/// </summary>
		Default,

		/// <summary>
		/// Invoke the functions immediately.
		/// </summary>
		Immediate,

		/// <summary>
		/// Invoke the functions on the next fixed update.
		/// </summary>
		OnNextFixedUpdate,

		/// <summary>
		/// Invoke the functions at the end of this frame during LateUpdate.
		/// </summary>
		OnLateUpdate
	}

	/// <summary>
	/// Marks a function for automatic subscription and unsubscription
	/// to the global event system when a GameObject is enabled and
	/// disabled. REQUIRES an EventAttributeHandler component on 
	/// the GameObject.
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Method)]
	public class GlobalEventListener : System.Attribute
	{
	
	}

	/// <summary>
	/// Marks a function for automatic subscription and unsubscription
	/// to the local event system when a GameObject is enabled and
	/// disabled. REQUIRES an EventAttributeHandler component on 
	/// the GameObject.
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Method)]
	public class LocalEventListener : System.Attribute
	{
	}

	/// <summary>
	/// Marks a function for automatic subscription and unsubscription
	/// to the a parent who has a specific component. REQUIRES an EventAttributeHandler component on 
	/// the GameObject.
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Method)]
	public class ParentCompEventListener : System.Attribute
	{
		/// <summary>
		/// The type of the component to search parents for.
		/// </summary>
		public System.Type CompToLookFor;

		/// <summary>
		/// Should we check ourselves for the component?
		/// </summary>
		public bool SkipSelf;

		public ParentCompEventListener(System.Type compType, bool skipSelf = false)
		{
			CompToLookFor = compType;
			SkipSelf = skipSelf;
		}
	}
}

namespace Dispatcher
{
	public class EventCallback<T>
	{
		public System.Action<T> Callback;
		public System.Func<T, bool> TerminableCallback; 
		public bool IsActive;
	}


	public class AttributeSubscription
	{
		public UnityEventSystemBase System;
		public object Node;
	}
}