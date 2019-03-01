using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dispatcher;
using GSFramework;

/// <summary>
///     Dialog box
/// </summary>
public class UiDialogBox : MonoBehaviour
{
	[SerializeField] private UIPanel _panel;
	private DialogBoxData _data;

	private Queue<DialogBoxData> _queue = new Queue<DialogBoxData>();

	public Button Button1;
	public Button Button2;
	public Button Button3;

	public InputField Input;
	public Text PlaceholderText;
	public Text Text;
	private bool _wasDialogShown;

	private bool _hasSubscribedToEvents;

	private void Awake()
	{
		SubscribeToEvents ();
		_queue = new Queue<DialogBoxData>();

		// Disable itself
		if (!_wasDialogShown)
			_panel.Hide ();

	}

	public void SubscribeToEvents()
	{
		if (_hasSubscribedToEvents)
			return;

		_hasSubscribedToEvents = true;

		EventManager.Subscribe<EventData.OnDialogEvent> (OnDialogEvent);
	}

	private void AfterHandlingClick()
	{
		if (_queue.Count > 0)
		{
			ShowDialog(_queue.Dequeue());
			return;
		}

		CloseDialog();
	}

	public void OnLeftClick()
	{
		if (_data.LeftButtonAction != null)
			_data.LeftButtonAction.Invoke();

		AfterHandlingClick();
	}

	public void OnMiddleClick()
	{
		if (_data.MiddleButtonAction != null)
			_data.MiddleButtonAction.Invoke();

		AfterHandlingClick();
	}

	public void OnRightClick()
	{
		if (_data.RightButtonAction != null)
			_data.RightButtonAction.Invoke();

		if (_data.ShowInput)
			_data.InputAction.Invoke(Input.text);

		AfterHandlingClick();
	}

	public void ShowDialog(DialogBoxData data)
	{
		_wasDialogShown = true;
		ResetAll();

		_data = data;

		if ((data == null) || (data.Message == null))
			return;

		// Show the dialog box
		_panel.Show();

		Text.text = data.Message;

		var buttonCount = 0;

		if (!string.IsNullOrEmpty(data.LeftButtonText))
		{
			// Setup Left button
			Button1.gameObject.SetActive(true);
			Button1.GetComponentInChildren<Text>().text = data.LeftButtonText;
			buttonCount++;
		}

		if (!string.IsNullOrEmpty(data.MiddleButtonText))
		{
			// Setup Middle button
			Button2.gameObject.SetActive(true);
			Button2.GetComponentInChildren<Text>().text = data.MiddleButtonText;
			buttonCount++;
		}

		if (!string.IsNullOrEmpty(data.RightButtonText))
		{
			// Setup Right button
			Button3.gameObject.SetActive(true);
			Button3.GetComponentInChildren<Text>().text = data.RightButtonText;
		}
		else if (buttonCount == 0)
		{
			// Add a default "Close" button if there are no other buttons in the dialog
			Button3.gameObject.SetActive(true);
			Button3.GetComponentInChildren<Text>().text = "Close";
		}

		if (data.ShowInput)
		{
			Input.gameObject.SetActive(true);
			Input.text = data.DefaultInputText;
		}

		transform.SetAsLastSibling();
	}

	private void OnDialogEvent(EventData.OnDialogEvent _event)
	{

		if (_panel.IsVisible)
		{
			// We're already showing something
			// Display later by adding to queue
			_queue.Enqueue(_event.Data);
			return;
		}

		ShowDialog(_event.Data);
	}

	private void ResetAll()
	{
		Button1.gameObject.SetActive(false);
		Button2.gameObject.SetActive(false);
		Button3.gameObject.SetActive(false);
		Input.gameObject.SetActive(false);
	}

	private void CloseDialog()
	{
		_panel.Hide ();
	}

	public static void ShowError(string error)
	{
		// Fire an event to display a dialog box.
		// We're not opening it directly, in case there's a custom 
		// dialog box event handler
		// DialogBoxData.CreateError(error);
	}
}

public class DialogBoxData
{
	public string DefaultInputText = "";
	public Action<string> InputAction;
	public Action LeftButtonAction;

	public string LeftButtonText;
	public Action MiddleButtonAction;

	public string MiddleButtonText;
	public Action RightButtonAction;

	public string RightButtonText = "Close";

	public bool ShowInput;

	public DialogBoxData(string message)
	{
		Message = message;
	}

	public string Message { get; private set; }

	public static DialogBoxData CreateError(string message)
	{
		return new DialogBoxData(message);
	}

	public static DialogBoxData CreateInfo(string message)
	{
		return new DialogBoxData(message);
	}

	public static DialogBoxData CreateTextInput(string message, Action<string> onComplete,
		string rightButtonText = "OK")
	{
		var data = new DialogBoxData(message)
		{
			ShowInput = true,
			RightButtonText = rightButtonText,
			InputAction = onComplete,
			LeftButtonText = "Close"
		};
		return data;
	}

	public static DialogBoxData CreateActionBox(string message,Action onClickLeft, Action onClickRight,
		string leftButtonText ,string rightButtonText = "Cancel")
	{
		var data = new DialogBoxData(message)
		{
			ShowInput = false,
			RightButtonText = rightButtonText,
			RightButtonAction = onClickRight,
			LeftButtonText = leftButtonText,
			LeftButtonAction = onClickLeft
		};
		return data;
	}
}
