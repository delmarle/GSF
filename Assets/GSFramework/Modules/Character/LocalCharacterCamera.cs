using Cinemachine;
using Dispatcher;
using UnityEngine;

namespace GSFramework.Utils
{
    public class LocalCharacterCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        private void OnEnable()
        {
            EventManager.Subscribe<CharacterEventData.OnLocalCharacterInstantiated>(OnLocalCharacterCreated);
        }
        
        private void OnDisable()
        {
            EventManager.Unsubscribe<CharacterEventData.OnLocalCharacterInstantiated>(OnLocalCharacterCreated);
        }
        
        private void OnLocalCharacterCreated(CharacterEventData.OnLocalCharacterInstantiated evt)
        {
            _virtualCamera.Follow = evt.PlayerTransform;
            _virtualCamera.LookAt = evt.PlayerTransform;
        }

    }
    
}
