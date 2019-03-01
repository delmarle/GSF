using UnityEngine;
using Dispatcher;

namespace GSFramework
{
	public class Module : MonoBehaviour 
	{
		#region FIELDS
		[SerializeField] private bool _debugBaseEvents;
		#endregion

		#region Monobehaviours
		protected void OnEnable()
		{
			Register ();
		}

		protected void OnDisable()
		{
			UnRegister ();
		}
		#endregion

		#region GSF EVENTS
		protected virtual void Register()
		{
			EventManager.Subscribe<EventData.AvaillableEvent>(OnAvaillable);
			EventManager.Subscribe<EventData.AuthenticatedEvent>(OnAuthenticated);
			EventManager.Subscribe<EventData.ConnectionDropped>(OnConnectionDropped);
		}

		protected virtual void UnRegister()
		{
			EventManager.Unsubscribe<EventData.AvaillableEvent>(OnAvaillable);
			EventManager.Unsubscribe<EventData.AuthenticatedEvent>(OnAuthenticated);
			EventManager.Unsubscribe<EventData.ConnectionDropped>(OnConnectionDropped);
		}

		/// <summary>
		/// Raised when Gamespark is Availlable.
		/// </summary>
		protected virtual void OnAvaillable(EventData.AvaillableEvent result)
		{
			if(_debugBaseEvents)
				print ("<b>["+GetType()+"]</b> OnAvaillable |"+ result.AvaillableKey);
		}

		/// <summary>
		/// Raised after receiving the result of an authentication request.
		/// </summary>
		protected virtual void OnAuthenticated(EventData.AuthenticatedEvent result)
		{
			if(_debugBaseEvents)
				print ("<b>["+GetType()+"]</b> OnAuthenticated | has error ? "+ result.Response.HasErrors);
		}

		/// <summary>
		/// Raises After we lost connection.
		/// </summary>
		/// <param name="result">Result.</param>
		protected virtual void OnConnectionDropped(EventData.ConnectionDropped result)
		{

		}


		#endregion
	}
}

