using UnityEngine;
using GameSparks.Core;


namespace GSFramework
{
    public class GsfMaster : MonoBehaviour 
    {

        #region FIELDS
        public static GsfMaster Instance;

        public bool IsAvaillable { get; private set; }


        public bool IsAuthenticated { get; private set; }

        #endregion

        #region Monobehaviours

        private void Awake()
        {
            DontDestroyOnLoad (gameObject);
            Instance = this;

            Register ();
        }
        #endregion

        #region CALLBACKs
        protected virtual void Register()
        {
            GS.GameSparksAvailable += OnGameSparksAvailable;
            GS.GameSparksAuthenticated += OnGameSparksAuthenticated;
        }


        public void OnGameSparksAvailable(bool available)
        {
            IsAvaillable = available;

            if (available && IsAuthenticated == false)
            {
                //We need to connect

            }

            if (!available && IsAuthenticated)
            {
                //We've managed to log in, however we've lost connection to GameSparks due to a bad network
                //TODO display "Reconnecting to server"
            }

        }


        public void OnGameSparksAuthenticated(string available)
        {
            IsAuthenticated = true;
        }
        #endregion
    }
}
