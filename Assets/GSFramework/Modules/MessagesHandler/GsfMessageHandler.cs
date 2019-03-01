using UnityEngine;
using GameSparks.Api.Messages;
using GameSparks.Core;

public class GsfMessageHandler : MonoBehaviour
{
	protected void Awake()
	{
		ScriptMessage.Listener += OnReceiveMessages;
	}


	public void OnReceiveMessages(ScriptMessage message)
	{
		GSData data = message.Data; 
		string extCode = message.ExtCode; 
		string messageId = message.MessageId; 
		bool? notification = message.Notification; 
		GSData scriptData = message.ScriptData; 
		string subTitle = message.SubTitle; 
		string summary = message.Summary; 
		string title = message.Title; 
		print (message.JSONString);
	}
}
