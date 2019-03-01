using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dispatcher;

namespace GSFramework.UI
{
	public class UiMail : Module 
	{

		#region FIELDS
		[SerializeField] private UIPanel _panel;
		[Header("Send Mail")]
		[SerializeField] private GameObject _sendEmailTab;
		[SerializeField] private InputField _receiverInput;
		[SerializeField] private InputField _titleInput;
		[SerializeField] private InputField _messageInput;
		[SerializeField] private InputField _currency1Input;
		[SerializeField] private InputField _currency2Input;

		[Header("Mail List")]
		[SerializeField] private GameObject _listMailTab;
		[SerializeField] private GameObject _listMailOverlay;
		[SerializeField] private UiEmailButton _mailPrefab;
		private UiEmailButton[] _buttons;

		[Header("Read Mail")]
		[SerializeField] private GameObject _readMailTab;
		[SerializeField] private Text _fromText;
		[SerializeField] private Text _titleText;
		[SerializeField] private Text _messageText;
		[SerializeField] private Text _dateText;
		[SerializeField] private LayoutGroup _layoutGroup;
		[SerializeField] private Text _receivedCurrency1;
		[SerializeField] private Text _receivedCurrency2;
		[SerializeField] private Button _receivedAttachement;

		public static UiMail Instance;
		private GenericUIList<MailContent> _emails;
		private string _mailId;
		#endregion
		public void Awake()
		{
			_emails = new GenericUIList<MailContent>(_mailPrefab.gameObject, _layoutGroup);
			Instance = this;
		}

		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.ResponseMailListEvent> (OnReceivedMailList);
			EventManager.Subscribe<EventData.ResponseSendMailEvent> (OnConfirmMailSent);
			EventManager.Subscribe<EventData.ResponseDeleteMailEvent> (OnConfirmMailDeleted);
			EventManager.Subscribe<EventData.ResponseRetreiveMailAttachmentEvent> (OnReceiveAttachment);
		}


		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.ResponseMailListEvent> (OnReceivedMailList);
			EventManager.Unsubscribe<EventData.ResponseSendMailEvent> (OnConfirmMailSent);
			EventManager.Unsubscribe<EventData.ResponseDeleteMailEvent> (OnConfirmMailDeleted);
			EventManager.Unsubscribe<EventData.ResponseRetreiveMailAttachmentEvent> (OnReceiveAttachment);
		}

		#region UI UPDATES

		private void OnReceiveAttachment(EventData.ResponseRetreiveMailAttachmentEvent response)
		{
			print(response.Success);
			if (response.Success)
			{
				//update email opened
				OpenEmailContent(response.UpdatedMail);
				
			}
			else
			{
				//cannot find attachment
			}
		}

		private void SetEmailList(IEnumerable<MailContent> data)
		{
			_emails.Generate<UiEmailButton>(data, (entry, item) => { item.SetupMail(entry); });

		}

		private void OnReceivedMailList(EventData.ResponseMailListEvent list)
		{
			//feed the ui

			SetEmailList (list.Mails);

			_listMailOverlay.SetActive (false);
			_sendEmailTab.SetActive (false);
			_readMailTab.SetActive (false);
		}

		private void OnConfirmMailSent(EventData.ResponseSendMailEvent confirmation)
		{
			print ("OnConfirmMailSent: "+confirmation.Result);
			_sendEmailTab.SetActive(false);
			_listMailTab.SetActive (true);
			_receiverInput.text = String.Empty;
			_titleInput.text = String.Empty;
			_messageInput.text = String.Empty;
			_currency1Input.text = String.Empty;
			_currency2Input.text = String.Empty;
			
		}

		private void OnConfirmMailDeleted(EventData.ResponseDeleteMailEvent confirmation)
		{
			print ("OnConfirmMailDeleted: "+confirmation.Result);
			SetEmailList (confirmation.Mails);
			_listMailTab.SetActive (true);
		}
		#endregion



		#region UI STATES
		public void OnStartOpenMailList()
		{
			EventManager.SendEvent(new EventData.RequestMailListEvent(DataManager.Instance.Get_string(Keys.DisplayName)));
			_panel.Show ();
			_listMailOverlay.SetActive (true);
			_listMailTab.SetActive (true);

			// wait for  "ResponseMailListEvent" which call OnReceivedMailList
		}


		#endregion
		public void OpenEmailContent(MailContent content)
		{
			_fromText.text = "From: "+content.Sender;
			_titleText.text = "Title: "+content.Title;
			_messageText.text = content.Message;
			_dateText.text = content.Date;
			_mailId = content.MailId;
			_receivedCurrency1.text = content.Currency1+" currency 1";
			_receivedCurrency2.text = content.Currency2+" currency 2";
			_receivedAttachement.interactable = content.Currency1 > 0 || content.Currency2 > 0;
			_readMailTab.SetActive (true);
			_listMailTab.SetActive (false);


		}

		public void BackToList()
		{
			_readMailTab.SetActive (false);
			_listMailTab.SetActive (true);
		}

		public void SendNewEmail()
		{
			//Check its valid before
			if (_receiverInput.text == ""
				|| _titleInput.text == ""
				|| _messageInput.text == "") 
			{
				print ("cant send mail");
				return;
			}
			MailContent mail = new MailContent ();
			mail.Sender = DataManager.Instance.Get_string (Keys.UserName);
			mail.Receiver = _receiverInput.text;
			mail.Title = _titleInput.text;
			mail.Message = _messageInput.text;
			mail.Currency1 = _currency1Input.text == "" ? 0 : int.Parse(_currency1Input.text);
			mail.Currency2 = _currency2Input.text == "" ? 0 : int.Parse(_currency2Input.text);
			mail.HasAttachment = mail.Currency1 > 0 || mail.Currency2 > 0;
			
			EventManager.SendEvent(new EventData.RequestSendEmailEvent(mail));
		}

		public void DeleteEmail()
		{
			print (_mailId);
			EventManager.SendEvent(new EventData.RequestMailDeleteEvent(_mailId));
			_readMailTab.SetActive (false);
		}

		public void RequestMailAttachment()
		{
			EventManager.SendEvent(new EventData.RequestMailAttachmentEvent(_mailId));
		}
	}
}
