using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
	public class UiAchievementEntry : MonoBehaviour 
	{
		[SerializeField]private Text _name;
		[SerializeField]private Text _description;
		[SerializeField]private Text _earned;



		public void SetupEntry(AchievementEntry content)
		{
			_name.text = content.Name;
			_description.text = content.Description;
			_earned.text = "Earned: " + content.Earned;
			gameObject.SetActive (true);
		}
	}
}