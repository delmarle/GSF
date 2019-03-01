using UnityEngine;
using UnityEngine.AI;

namespace GSFramework.Demo
{
    public class LocalPlayerInput : MonoBehaviour
    {
    	private NavMeshAgent _agent;
	    private Animator _indiceAnimator;
    	
    	void OnEnable ()
    	{
    		InputMultiplatform.OnTouchStart+= OnTouchStart;
    		_agent = GetComponent<NavMeshAgent>();
		    _agent.enabled = true;

		    var go = Resources.Load("Move Indice");
		    GameObject ind = Instantiate(go) as GameObject;
		    if (ind != null) _indiceAnimator = ind.GetComponent<Animator>();
		    _indiceAnimator.gameObject.SetActive(false);
	    }
    
    	void OnDisable()
    	{
    		InputMultiplatform.OnTouchStart-= OnTouchStart;
    	}
    
    	private void OnTouchStart(Vector3 touchPosition)
    	{
    		Ray screenRay = Camera.main.ScreenPointToRay(touchPosition);
    
    		RaycastHit hit;
    		if (Physics.Raycast(screenRay, out hit))
		    {
			    var interactible = hit.collider.GetComponent<Interactible>();
			    if (interactible)
			    {
				    //try interact
			    }
			    else
			    {
				    _agent.SetDestination(hit.point);
				    SetIndice(hit.point);
			    }

			    
			    
    		}
    	}

	    private void SetIndice(Vector3 pos)
	    {
		    _indiceAnimator.transform.position = pos;
		    _indiceAnimator.gameObject.SetActive(true);
		    _indiceAnimator.SetTrigger("trigger");
	    }

    }

}
