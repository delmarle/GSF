using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;

namespace GSFramework.UI
{
    public class UiTeamButton : MonoBehaviour 
    {
	
        [SerializeField]private Text _clanName;
        [SerializeField]private Text _playerLeader;
	
        public void SetupTeamItem(ListTeamsResponse._Team content)
        {
            _clanName.text = content.TeamName;
            _playerLeader.text = "Leader: "+content.Owner.DisplayName;
        }
	
        public void RequestJoin()
        {
            UiTeam.Instance.RequestJoinTeam (_clanName.text);
        }
    }
}
