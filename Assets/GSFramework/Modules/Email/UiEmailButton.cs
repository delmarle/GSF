using UnityEngine;
using UnityEngine.UI;

namespace GSFramework.UI
{
	public class UiEmailButton : MonoBehaviour 
	{
		[SerializeField]private Text _sender;
		[SerializeField]private Text _title;
		private MailContent _content = new MailContent();


		public void SetupMail(MailContent content)
		{
			_content = content;
			_content.MailId = content.MailId;
			_sender.text = content.Sender;
			_title.text = content.Title;
		}

		public void OpenMail()
		{
			UiMail.Instance.OpenEmailContent (_content);
		}
	}

}