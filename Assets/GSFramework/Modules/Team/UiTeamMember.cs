using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;

namespace GSFramework.UI
{
    public class UiTeamMember : MonoBehaviour 
    {
        [SerializeField]private Text _memberName;

        public void SetupMemberItem(GetMyTeamsResponse._Team._Player content)
        {
            _memberName.text = content.DisplayName;
        }

        public void SetupMemberItem(JoinTeamResponse._Player content)
        {
            _memberName.text = content.DisplayName;
        }
    }

}

