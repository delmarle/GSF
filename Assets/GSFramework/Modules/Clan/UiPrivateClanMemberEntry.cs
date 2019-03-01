using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
    public class UiPrivateClanMemberEntry : MonoBehaviour 
    {
        #region FIELDS
        [SerializeField] private Text _playerNameText;
        [SerializeField] private Text _playerLevelText;
        [SerializeField] private Text _playerRankText;
        
        [SerializeField] private Button[] _requireRank2Buttons;

        private ClanMemberData _cachedData = new ClanMemberData();
        #endregion

        public void Setup(ClanMemberData data)
        {
            _cachedData = data;
            _playerNameText.text = data.PlayerDisplayName;
            _playerRankText.text = "Rank: " + data.Rank.ToString();
        }

        public void Kick()
        {
            print("kick "+_cachedData.PlayerId);
        }

    }
}

