using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiNotice : Module 
	{
		#region Fields
		[SerializeField] private UiNoticeObject _objectPrefab;
		[SerializeField] private VerticalLayoutGroup _layout;

		private float _timeLeft;
		private readonly List<EventData.NoticeMessage> _queue = new List<EventData.NoticeMessage> ();
		private const int MaxConcurentEntries = 5;
		#endregion

		protected override void Register()
		{
			base.Register();
			EventManager.Subscribe<EventData.NoticeMessage> (ReceiveNotification);
		}

		protected override void UnRegister()
		{
			base.UnRegister();
			EventManager.Unsubscribe<EventData.NoticeMessage> (ReceiveNotification);
		}

		private void ReceiveNotification(EventData.NoticeMessage entry)
		{
			_queue.Add (entry);
		}

		private void Update()
		{
			if(_queue.Count>0)
			{
				if (_timeLeft <= 0 && _layout.transform.childCount <MaxConcurentEntries) 
				{
					var entry = _queue[0];
					_queue.RemoveAt (0);
					var instance = Instantiate(_objectPrefab,_layout.transform);
					instance.Setup (entry.Message1,entry.Message2);
					_timeLeft = 0.5f;
				}
			}
			_timeLeft -= Time.deltaTime;
		}

	}
}

