using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
	public class UiNoticeObject : MonoBehaviour 
	{
		public Text Field1,Field2;

		private void OnEnable()
		{
			transform.SetAsFirstSibling ();
		}

		public void Setup(string message,string message2)
		{
			Field1.text = message;
			Field2.text = message2;
			gameObject.SetActive (true);
		}
	}
}
