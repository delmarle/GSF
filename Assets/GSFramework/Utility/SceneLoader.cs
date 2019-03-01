using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	#region FIELDS
	public static SceneLoader Instance;

	private string _mLoadSceneName = "MainMenu";
	static private string _mLastSceneName;
	public UIPanel uiElement;
	public Slider loadingSlider;
	#endregion
	#region Condition check
	public bool IsLoading = false;

	public string LoadSceneName {
		get {
			return _mLoadSceneName;
		}
		set {
			_mLastSceneName = SceneManager.GetActiveScene ().name;
			_mLoadSceneName = value;
		}
	}
	public string LastSceneName {
		get {
			return _mLastSceneName;
		}
	}

	public int LoadedScene
	{
		get{ return SceneManager.GetActiveScene ().buildIndex; }
	}
	#endregion
	#region Monobehaviours
	void Awake ()
	{
		Instance = this;
	}


	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	#endregion

	#region Load scenes

	void LoadSceneByName (string sceneName)
	{
		if (string.IsNullOrEmpty (LoadSceneName)) {
			LoadCompleted ();
			return;
		}
			
		AsyncOperation op = SceneManager.LoadSceneAsync (sceneName);
		StartCoroutine (DoLoading (op));
	}

	IEnumerator DoLoading (AsyncOperation op)
	{	
		SetLoadPercentUi (0);
		op.allowSceneActivation = false;

		yield return new WaitForSeconds (0.1f);
		SetLoadPercentUi (0.1f);
		op.allowSceneActivation = true;
		while (!op.isDone) {
			SetLoadPercentUi (op.progress);
			yield return new WaitForEndOfFrame ();
		}

		LoadCompleted ();
		SetLoadPercentUi (1f);
		yield return op;
	}

	void LoadCompleted ()
	{
		// Unloads assets that are not used.
		Resources.UnloadUnusedAssets ();
		// Forces an immediate garbage collection of all generations.
		System.GC.Collect ();
		IsLoading = false;
	}

	public void LoadScene(string sceneName)
	{
		if (IsLoading)
			return;

		LoadSceneName = sceneName;
		IsLoading = true;
		StartCoroutine ("LoadControl");
	}

	IEnumerator LoadControl()
	{
		uiElement.Show (false);
		yield return new WaitForSeconds (0.5f);
		LoadSceneByName (LoadSceneName);
		while (IsLoading)
			yield return new WaitForEndOfFrame ();
		yield return new WaitForSeconds (0.5f);
		uiElement.Hide (false);
	}
	#endregion

	#region UI CONTROL

	void SetLoadPercentUi(float percent)
	{
		loadingSlider.value = percent;
	}
	#endregion
	#region Teleport
	public void TeleportPlayer(Vector3 destination)
	{
		StartCoroutine ("TeleportSequence",destination);
	}
	IEnumerator TeleportSequence(Vector3 v3)
	{
		uiElement.Show (false);
		//SceneInputs.Instance.Input.IsEnabled = false;
		yield return new WaitForEndOfFrame ();

		//	SceneInputs.Instance.sceneController.SetPlayerPosition (v3);
		yield return new WaitForSeconds (2);
		uiElement.Hide (false);
		//SceneInputs.Instance.Input.IsEnabled = true;
	}
	#endregion

	#region Callbacks


	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//GameSparkLogic.Instance.LevelLoaded (scene.buildIndex);
	}
	#endregion
}