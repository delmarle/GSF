using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Utils
{
    public class FaceCamera : MonoBehaviour 
    {
    	void Update ()
	    {
		    if (Camera.main == null) return;
		    
		    transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
	    }
    }

}
