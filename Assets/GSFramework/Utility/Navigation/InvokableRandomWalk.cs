using System.Collections;
using UnityEngine.AI;
using UnityEngine;

namespace GSFramework.Utils
{
	public class InvokableRandomWalk : Invokable
	{
		#region Fields
		const float MaxRange = 10;
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Animator _animator;
		private Vector3 _cachedPosition;
		#endregion
		
		public override void InvokeLogic()
		{
			_cachedPosition = transform.position;
			_agent.enabled = true;
			OnReachedDestination();
		}

		void Update()
		{
			_animator.SetFloat("Speed", _agent.velocity.magnitude);
		}

		IEnumerator MoveToNextPoint()
		{
			Vector3 destination = GetRandomPosition();
			_agent.SetDestination(destination);
			yield return new WaitForEndOfFrame();
			
			while (_agent.isOnNavMesh && _agent.remainingDistance > 0.5f)
			{
				yield return null;
			}
			Invoke("OnReachedDestination",Random.Range(2,10));
		}

		private void OnReachedDestination()
		{
			StartCoroutine(MoveToNextPoint());
		}
		
		private Vector3 GetRandomPosition()
		{
			Vector3 pos = _cachedPosition;
			pos.x += Random.Range((MaxRange * -1), MaxRange);
			pos.z += Random.Range((MaxRange * -1), MaxRange);

			return pos;
		}
		
	}
}

