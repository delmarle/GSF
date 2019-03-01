using UnityEngine;
using UnityEngine.EventSystems;

namespace GSFramework.Demo
{
    public class InputMultiplatform : MonoBehaviour 
    {
    	public delegate void InputEvent(Vector3 touchPosition);
    	public static event InputEvent OnTouchStart;

	    private void Update () 
    	{
    		TouchInputs();
    		#if UNITY_EDITOR || PLATFORM_STANDALONE
    		MouseInput();
    		#endif
    	}
    	#region Touch
    	private void TouchInputs()
    	{
    		int nbTouches = Input.touchCount;
    
    		if(nbTouches > 0)
    		{
    			for (int i = 0; i < nbTouches; i++)
    			{
    				Touch touch = Input.GetTouch(i);
    
    				TouchPhase phase = touch.phase;
    
    				switch(phase)
    				{
    					case TouchPhase.Began:
    						//print("New touch detected at position " + touch.position + " , index " + touch.fingerId);
						    if (IsOverUi()) return;
    						OnFingerDown(touch);
    						break;
    					case TouchPhase.Moved:
    						print("Touch index " + touch.fingerId + " has moved by " + touch.deltaPosition);
    						break;
    					case TouchPhase.Stationary:
    						print("Touch index " + touch.fingerId + " is stationary at position " + touch.position);
    						break;
    					case TouchPhase.Ended:
    						print("Touch index " + touch.fingerId + " ended at position " + touch.position);
    						break;
    					case TouchPhase.Canceled:
    						print("Touch index " + touch.fingerId + " cancelled");
    						break;
    				}
    			}
    		}
    	}
    
    	public  void OnFingerDown(Touch touchPosition)
    	{
    		if (OnTouchStart != null)
    			OnTouchStart(touchPosition.position);
    	}
    	#endregion
    	
    	#region Desktop
    
    	private void MouseInput()
    	{
    		if (Input.GetMouseButtonDown(0))
    		{
    			OnMouseButtonDown(Input.mousePosition);
    		}
    	}
    
    	void OnMouseButtonDown(Vector3 clickPosition)
    	{
		    if (IsOverUi()) return;
    		if (OnTouchStart != null)
    			OnTouchStart(clickPosition);
    	}


	    bool IsOverUi()
	    {
		    bool isPointerOverGameObject = false;

		    if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		    {
			    for(int i=0; i<Input.touchCount; i++) {
				    Touch touch = Input.touches[i];
				    if( touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended) {
					    if(EventSystem.current.IsPointerOverGameObject( Input.touches[i].fingerId )) {
						    isPointerOverGameObject = true;
						    break;
					    }
				    }
			    }
		    } 
		    else 
		    {
			    isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
		    }
		    
		    return isPointerOverGameObject;
	    }

	    #endregion
    }

}
