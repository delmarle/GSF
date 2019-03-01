using System.Collections.Generic;
using Dispatcher;
using GameSparks.Api.Requests;
using GameSparks.Api.Messages;

namespace GSFramework
{
	public class MailContent
	{
		public string MailId;
		public string Sender;
		public string Receiver;
		public string Title;
		public string Message;
		public string Date;
		public int Currency1;
		public int Currency2;
		public bool HasAttachment;
	}

	public class GsfMail : Module 
	{

		#region FIELDS
		private List<MailContent> _mails = new List<MailContent>();

		//KEYS
		private const string EventKeySendEmail = "gsf_email_send";
		private const string EventKeyListEmail = "gsf_email_query";
		private const string EventKeyDeleteEmail = "gsf_email_delete";
		private const string EventKeyRetreiveAttachment = "gsf_email_retrieve_attachment";
		private const string Sender = "sender";
		private const string Receiver = "receiver";
		private const string Title = "title";
		private const string Message = "message";
		private const string Date = "datetime";
		private const string MailId = "mail_id";
		private const string Currency1 = "currency1";
		private const string Currency2 = "currency2";
		#endregion
		protected override void Register ()
		{
			base.Register ();
			EventManager.Subscribe<EventData.RequestSendEmailEvent> (OnSendNewMail);
			EventManager.Subscribe<EventData.RequestMailListEvent> (OnRequestMailList);
			EventManager.Subscribe<EventData.RequestMailDeleteEvent> (OnRequestMailDelete);
			EventManager.Subscribe<EventData.RequestMailAttachmentEvent>(OnRequestMailAttachment);
			ScriptMessage_new_email.Listener += OnReceiveMailNotification;

		}

		protected override void UnRegister ()
		{
			base.UnRegister ();
			EventManager.Unsubscribe<EventData.RequestSendEmailEvent> (OnSendNewMail);
			EventManager.Unsubscribe<EventData.RequestMailListEvent> (OnRequestMailList);
			EventManager.Unsubscribe<EventData.RequestMailDeleteEvent> (OnRequestMailDelete);
			EventManager.Unsubscribe<EventData.RequestMailAttachmentEvent>(OnRequestMailAttachment);
			ScriptMessage_new_email.Listener -= OnReceiveMailNotification;
		}


		#region Requests
		private void OnSendNewMail(EventData.RequestSendEmailEvent emailToSend)
		{
			new LogEventRequest()
				.SetEventKey(EventKeySendEmail)
				.SetEventAttribute(Sender, emailToSend.Mail.Sender)
				.SetEventAttribute(Receiver, emailToSend.Mail.Receiver)
				.SetEventAttribute(Title, emailToSend.Mail.Title)
				.SetEventAttribute(Message, emailToSend.Mail.Message)
				.SetEventAttribute(Date,System.DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
				.SetEventAttribute(Currency1, emailToSend.Mail.Currency1)
				.SetEventAttribute(Currency2, emailToSend.Mail.Currency2)
				.Send((response) =>
				{
					EventManager.SendEvent(new EventData.ResponseSendMailEvent(!response.HasErrors));

				}  );
		}

		private void OnRequestMailList(EventData.RequestMailListEvent listInfo)
		{
			new LogEventRequest()
				.SetEventKey(EventKeyListEmail)
				.SetEventAttribute(Receiver, listInfo.Receiver)
				.Send((response) =>
					{
						if (response.HasErrors)
						{
							print(response.Errors.JSON.ToString());
						}
						else
						{
							var jsonMails = response.ScriptData.GetGSDataList("emailmessages");
							_mails.Clear();

							for(var i =0; i< jsonMails.Count; i++)
							{
								var cacheMail = new MailContent
								{
									MailId = jsonMails[i].GetGSData("_id").GetString("$oid"),
									Sender = jsonMails[i].GetString("_sender"),
									Title = jsonMails[i].GetString("_title"),
									Receiver = jsonMails[i].GetString("_receiver"),
									Message = jsonMails[i].GetString("_message"),
									Date = jsonMails[i].GetString("_date"),
									Currency1 = (int)jsonMails[i].GetNumber("_currency1").Value,
									Currency2 = (int)jsonMails[i].GetNumber("_currency2").Value,
									HasAttachment = jsonMails[i].GetBoolean("_hasAttachment").Value,
								};
								_mails.Add(cacheMail);

							}

							EventManager.SendEvent(new EventData.ResponseMailListEvent(_mails.ToArray()));
						}
					}  );
		}

		private void OnRequestMailDelete(EventData.RequestMailDeleteEvent deleteInfo)
		{
			print (deleteInfo.MailId);
			new LogEventRequest()
				.SetEventKey(EventKeyDeleteEmail)
				.SetEventAttribute(MailId, deleteInfo.MailId)
				.Send((response) =>
					{
						if (response.HasErrors)
						{
							print(response.Errors.JSON.ToString());
							EventManager.SendEvent(new EventData.ResponseDeleteMailEvent(false,_mails.ToArray()));
						}
						else
						{
							print(response.JSONString);

							foreach(var entry in _mails)
							{
								if(entry.MailId == deleteInfo.MailId)
								{
									_mails.Remove(entry);
									EventManager.SendEvent(new EventData.ResponseDeleteMailEvent(true,_mails.ToArray()));
									return;
								}
							}
							EventManager.SendEvent(new EventData.ResponseDeleteMailEvent(true,_mails.ToArray()));
						}
					}  );
		}

		private void OnRequestMailAttachment(EventData.RequestMailAttachmentEvent request)
		{
			
			new LogEventRequest()
				.SetEventKey(EventKeyRetreiveAttachment)
				.SetEventAttribute(MailId, request.MailId)
				.Send((response) =>
				{
					if (response.HasErrors)
					{
						print(response.Errors.JSON.ToString());
						EventManager.SendEvent(new EventData.ResponseDeleteMailEvent(false,_mails.ToArray()));
					}
					else
					{
						print(response.JSONString);
						var success = response.ScriptData.GetBoolean("success");
						
						if (success.Value)
						{
							var updatedMail = response.ScriptData.GetGSData("updated_email");
							var cacheMail = new MailContent
							{
								MailId = updatedMail.GetGSData("_id").GetString("$oid"),
								Sender = updatedMail.GetString("_sender"),
								Title = updatedMail.GetString("_title"),
								Receiver = updatedMail.GetString("_receiver"),
								Message = updatedMail.GetString("_message"),
								Date = updatedMail.GetString("_date"),
								Currency1 = (int)updatedMail.GetNumber("_currency1").Value,
								Currency2 = (int)updatedMail.GetNumber("_currency2").Value,
								HasAttachment = updatedMail.GetBoolean("_hasAttachment").Value,
							};
							var currency1 = response.ScriptData.GetNumber("currency1").Value;
							var currency2 = response.ScriptData.GetNumber("currency2").Value;
							
							EventManager.SendEvent(new EventData.ResponseRetreiveMailAttachmentEvent
								(
									true,
									cacheMail,
									currency1,
									currency2
								));
							
							EventManager.SendEvent(new EventData.CurrencyUpdateEvent
							(
								(int)currency1,
								(int)currency2,
								DataManager.Instance.Get_INT(Keys.Currency3)
							));
							
						}
						else
						{
							EventManager.SendEvent(new EventData.ResponseRetreiveMailAttachmentEvent
							(
								false,
								null,
								0,
								0
							));
						}
						
						
						
						
					}
				}  );
		}
		#endregion

		public void OnReceiveMailNotification(ScriptMessage message)
		{
			EventManager.SendEvent(new EventData.NoticeMessage(
				"[ "+message.Title+" ]",
				message.SubTitle
			));
		}
	}

}