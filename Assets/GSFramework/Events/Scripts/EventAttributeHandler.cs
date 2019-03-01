using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dispatcher
{
	/// <summary>
	/// It's probably best that this script executes before any other script that uses the event system.
	/// </summary>
	public class EventAttributeHandler : MonoBehaviour
	{
		private bool _initialized;
		private LocalEventSystem _eventSystem;
		private List<AttributeSubscription> _subscriptions = new List<AttributeSubscription>();

		private List<ParentListenerBase> _parentSubscriptions = new List<ParentListenerBase>();
		private Transform _curParent;

		private abstract class ParentListenerBase
		{
			public AttributeSubscription AttributeSubscription;
			public Type ParentType;
			public bool IsSubscribed;
			public bool SkipSelf;

			public abstract void UpdateSystem(LocalEventSystem system);
		}

		private class ParentListener<T> : ParentListenerBase where T : struct
		{
			public override void UpdateSystem(LocalEventSystem system)
			{
				if (system == null)
				{
					AttributeSubscription.System = null;
				}
				else
				{
					system.UpdateAttributeSubscription<T>(AttributeSubscription);
				}
			}

			public ParentListener(Action<T> callback)
			{
				LinkedListNode<EventCallback<T>> node = UnityEventSystem<T>.GetBlankNode();
				node.Value.Callback = callback;

				AttributeSubscription = new AttributeSubscription();
				AttributeSubscription.Node = node;
			}

			public ParentListener(Func<T, bool> terminableCallback)
			{
				LinkedListNode<EventCallback<T>> node = UnityEventSystem<T>.GetBlankNode();
				node.Value.TerminableCallback = terminableCallback;

				AttributeSubscription = new AttributeSubscription {Node = node};
			}
		}

		private void LateUpdate()
		{
			if (_curParent != transform.parent)
			{
				UpdateParentSubscriptions();
			}
		}

		public void UpdateParentSubscriptions()
		{
			// A little optimization
			Type previousParent = null;
			LocalEventSystem previousSystem = null;
			bool previousSkip = false;

			for (int i = 0; i < _parentSubscriptions.Count; i++)
			{
				AttributeSubscription attSub = _parentSubscriptions[i].AttributeSubscription;

				// Unsubscribe
				if (_parentSubscriptions[i].IsSubscribed)
				{
					_parentSubscriptions[i].IsSubscribed = false;
					attSub.System.RemoveSubscription(attSub.Node);
				}

				// New subscribe!
				if (previousParent != null &&
					previousSkip == _parentSubscriptions[i].SkipSelf && 
					previousParent == _parentSubscriptions[i].ParentType)
				{
					_parentSubscriptions[i].UpdateSystem(previousSystem);
				}
				else
				{
					Component mb;

					if (_parentSubscriptions[i].SkipSelf)
					{
						mb = GetComponentInParentInactive(transform.parent, _parentSubscriptions[i].ParentType);
					}
					else
					{
						mb = GetComponentInParentInactive(transform, _parentSubscriptions[i].ParentType);
					}

					if (mb != null)
					{
						previousSystem = mb.GetComponent<LocalEventSystem>();

						if (previousSystem == null)
						{
							previousSystem = mb.gameObject.AddComponent<LocalEventSystem>();
						}

						previousParent = _parentSubscriptions[i].ParentType;
						previousSkip = _parentSubscriptions[i].SkipSelf;
						_parentSubscriptions[i].UpdateSystem(previousSystem);
					}
					else
					{
						_parentSubscriptions[i].AttributeSubscription.System = null;
					}
					
				}

				if (attSub.System != null)
				{
					attSub.System.AddSubscription(attSub.Node, gameObject.activeSelf);
					_parentSubscriptions[i].IsSubscribed = true;
				}
			}

			_curParent = transform.parent;
		}

		public static void CheckAddHandler(GameObject objToCheck)
		{
			if (objToCheck.GetComponent<EventAttributeHandler>() != null)
			{
				return;
			}

			MonoBehaviour[] behaviours = objToCheck.GetComponents<MonoBehaviour>();

			for (int k = 0; k < behaviours.Length; k++)
			{
				MethodInfo[] methods = behaviours[k].GetType().GetMethods(
					BindingFlags.Public |
					BindingFlags.NonPublic |
					BindingFlags.Instance |
					BindingFlags.Static);

				for (int i = 0; i < methods.Length; i++)
				{
					Attribute[] attributes = Attribute.GetCustomAttributes(methods[i]);

					for (int j = 0; j < attributes.Length; j++)
					{
						if (attributes[j] is GlobalEventListener ||
							attributes[j] is LocalEventListener ||
							attributes[j] is ParentCompEventListener)
						{
							objToCheck.AddComponent<EventAttributeHandler>();
							return;
						}
					}
				}
			}
		}

		private void Awake()
		{
			RegisterEvents();
		}

		/// <summary>
		/// If used in junction with UnityPooler then this function will be called.
		/// </summary>
		private void OnCreate()
		{
			RegisterEvents();
		}

		private void OnEnable()
		{
			for (int i = 0; i < _subscriptions.Count; i++)
			{
				if (_subscriptions[i].System != null)
				{
					_subscriptions[i].System.SubscribeSoft(_subscriptions[i].Node);
				}
			}
		}

		private void OnDisable()
		{
			for (int i = 0; i < _subscriptions.Count; i++)
			{
				if (_subscriptions[i].System != null)
				{
					_subscriptions[i].System.UnsubscribeSoft(_subscriptions[i].Node);
				}
			}
		}

		private void RegisterEvents()
		{
			if (!_initialized)
			{
				_initialized = true;

				_eventSystem = GetComponent<LocalEventSystem>();

				if (_eventSystem == null)
				{
					_eventSystem = gameObject.AddComponent<LocalEventSystem>();
				}

				MonoBehaviour[] behaviours = GetComponents<MonoBehaviour>();

				for (int i = 0; i < behaviours.Length; i++)
				{
					if (behaviours[i].enabled)
					{
						RegisterBehaviour(behaviours[i]);
					}
				}
			}
		}

		private void RegisterBehaviour(MonoBehaviour mb)
		{
			MethodInfo[] methods = mb.GetType().GetMethods(
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Instance |
				BindingFlags.Static);

			for (int i = 0; i < methods.Length; i++)
			{
				Attribute[] attributes = Attribute.GetCustomAttributes(methods[i]);
				object methodTarget = methods[i].IsStatic ? null : mb;

				for (int j = 0; j < attributes.Length; j++)
				{

					if (attributes[j] is GlobalEventListener)
					{
						RegisterCallback(methods[i], methodTarget, typeof(EventManager), null);
					}
					else if (attributes[j] is LocalEventListener)
					{
						RegisterCallback(methods[i], methodTarget, _eventSystem.GetType(), _eventSystem);
					}
					else if (attributes[j] is ParentCompEventListener)
					{
						ParentCompEventListener compListener = (ParentCompEventListener)attributes[j];

						ParameterInfo[] args = methods[i].GetParameters();
						Type parentListenerType = typeof(ParentListener<>).MakeGenericType(args[0].ParameterType);

						ParentListenerBase parentListenerBase = (ParentListenerBase)Activator.CreateInstance(
							parentListenerType,
							GetCallbackDelegate(methods[i], methodTarget));

						parentListenerBase.SkipSelf = compListener.SkipSelf;
						parentListenerBase.ParentType = compListener.CompToLookFor;

						_parentSubscriptions.Add(parentListenerBase);
						_subscriptions.Add(parentListenerBase.AttributeSubscription);
					}
				}
			}

			UpdateParentSubscriptions();
		}

		private void RegisterCallback(
			MethodInfo methodInfo, 
			object methodTarget, 
			Type type, 
			object target)
		{
			ParameterInfo[] args = methodInfo.GetParameters();

			MethodInfo method;

			if (methodInfo.ReturnParameter.ParameterType == typeof(void))
			{
				method = type.GetMethod(
					"RegisterCallback",
					BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			}
			else
			{
				method = type.GetMethod(
					"RegisterCallbackTerminable",
					BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
			}

			MethodInfo genericMethod = method.MakeGenericMethod(args[0].ParameterType);

			Delegate del = GetCallbackDelegate(methodInfo, methodTarget);

			object sub = genericMethod.Invoke(
				target,
				new object[] { del });

			_subscriptions.Add((AttributeSubscription)sub);
		}

		private Delegate GetCallbackDelegate(MethodInfo methodInfo, object methodTarget)
		{
			ParameterInfo[] args = methodInfo.GetParameters();

			Assert.IsTrue(args.Length == 1);
			Assert.IsTrue(args[0].ParameterType.IsValueType && !args[0].ParameterType.IsEnum);

			Type delegateType;

			if (methodInfo.ReturnParameter.ParameterType == typeof(void))
			{
				delegateType = Expression.GetActionType(args[0].ParameterType);
			}
			else
			{
				delegateType = Expression.GetFuncType(args[0].ParameterType, typeof(bool));
			}

			return Delegate.CreateDelegate(delegateType, methodTarget, methodInfo);
		}

		private Component GetComponentInParentInactive(Transform start, Type type)
		{
			Transform checking = start;
			Component ret = null;

			while (checking != null && ret == null)
			{
				ret = checking.GetComponent(type);
				checking = checking.parent;
			}

			return ret;
		}
	}
}
