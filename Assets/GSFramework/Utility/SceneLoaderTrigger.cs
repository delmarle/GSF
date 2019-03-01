using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// will load the scene called as parameter, can be used on an ui utton
/// </summary>
public class SceneLoaderTrigger : MonoBehaviour 
{


	public void LoadScene (string sceneName) 
	{
		if (SceneLoader.Instance.IsLoading)
			return;

		SceneLoader.Instance.LoadScene (sceneName);
	}
	

}
