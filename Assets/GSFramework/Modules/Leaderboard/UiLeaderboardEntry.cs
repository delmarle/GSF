using UnityEngine;
using UnityEngine.UI;

namespace GSFramework
{
	public class UiLeaderboardEntry : MonoBehaviour 
	{
		[SerializeField]private Text _rank;
		[SerializeField]private Text _playerName;
		[SerializeField]private Text _scoreValue;



		public void SetupEntry(LeaderboardEntry content)
		{
			_rank.text = content.Rank.ToString();
			_playerName.text = content.PlayerName;
			_scoreValue.text = content.ScoreValue.ToString();
		}

	}
}


