using UnityEngine.Events;
using GameSparks.Api.Requests;
using UnityEngine;

namespace GSFramework
{
    public class GsfVersionCheck:Module
    {
    	public const string Version = "0.81";
	    [Header("Editor Events")]
	    public UnityEvent OnVersionValid;

	    public UnityEvent OnVersionInvalid;
	    
    	#region overrides

	    protected override void OnAuthenticated(EventData.AuthenticatedEvent result)
	    {
		  new LogEventRequest_gsf_version_check()
			  .Set_version(Version)
			  .Send((response) =>
			  {
				  if (response.ScriptData.GetBoolean("valid").Value)
				  {
					  Debug.Log("version valid with server");
					  OnVersionValid.Invoke();
				  }
				  else
				  {
					  Debug.LogError("[Version invalid with server] your client version is: "
					                 +Version+
					                 ". Please update your client and use last version of the server");
					  OnVersionInvalid.Invoke();        
				  }

			  }  );
	    }

	    #endregion
    
    }

}
