using Dispatcher;
using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
    public class UiPublicClanEntry : MonoBehaviour
    {
    	[SerializeField] private Text _clanName;
    	[SerializeField] private Text _clanLevel;
    	[SerializeField] private Text _clanMembersCount;
    	
    	private PublicClanData _data;
    	
    	public void Setup(PublicClanData data)
    	{
    		_data = data;
    		_clanName.text = data.ClanName;
    		gameObject.name = data.ClanName;
    		_clanLevel.text = "Level " + data.ClanLevel;
    		_clanMembersCount.text = data.ClanMembers+"/10";
    	}

	    public void ClickDetail()
	    {
		    EventManager.SendEvent(new ClanEventData.OpenClanDetail(_data));
	    }

    }

}
