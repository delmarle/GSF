using UnityEngine;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiHud : Module 
	{
		#region FIELDS
		[SerializeField] private UIPanel _panel;
		#endregion

		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.ShowHud> (OnShowHudEvent);
		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Subscribe<EventData.ShowHud> (OnShowHudEvent);
		}

		private void OnShowHudEvent(EventData.ShowHud message)
		{
			if (message.Show)
				_panel.Show ();
			else
				_panel.Hide ();
		}
	}
}

