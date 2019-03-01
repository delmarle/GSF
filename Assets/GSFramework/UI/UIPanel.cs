using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Dispatcher;
using GSFramework;
  
    [RequireComponent(typeof(Animator))]
    public class UIPanel : MonoBehaviour
    {
        #region Variables
		[Header("State On Start")]
        public bool ForceHide = true;
        public bool ForceResetPosition = true;

		[Header("Animation ( Optional )")]
        [SerializeField]
		private AnimationClip showAnimation;
        public int ShowAnimID { get; protected set; }

        [SerializeField]
		private AnimationClip hideAnimation;
        public int HideAnimID { get; protected set; }

		[Header("Sounds ( Optional )")]
        public AudioClip ShowClip;
		public AudioClip HideClip;

        public bool IsVisible { get; protected set; }

        private IEnumerator _animationCoroutine;

        private Animator _animator;
        public Animator animator
        {
            get
            {
                if (_animator == null)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        private RectTransform _rectTransform;
        protected RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }
        #endregion

		#region EVENTS
		[Header("Editor Events")]
		public UnityEvent OnHide;

		public UnityEvent OnShow;

		#endregion


        public virtual void Awake()
        {
            if(showAnimation)
                ShowAnimID = Animator.StringToHash(showAnimation.name);

            if(hideAnimation)
                HideAnimID = Animator.StringToHash(hideAnimation.name);

            OnLevelLoaded();

            if (ForceResetPosition)
            {
                RectTransform.anchoredPosition = Vector2.zero;
            }
        }
        
        public void OnLevelLoaded(int level)
        {
            OnLevelLoaded();
        }

        protected virtual void OnLevelLoaded()
        {
            if (ForceHide)
            {
                HideFirst();
				InvokeHideEvent ();
            }
            else
            {
                IsVisible = true;
            }
        }


        #region Invoke Events
        public void InvokeHideEvent()
        {
			if (OnHide != null)
				OnHide.Invoke();

        }

        public void InvokeShowEvent()
        {
			if (OnShow != null)
				OnShow.Invoke();

        }
        #endregion
        protected virtual void SetChildrenActive(bool active)
        {
            foreach (Transform trans in transform)
				trans.gameObject.SetActive(active);
            

			Image img = gameObject.GetComponent<Image>();
            if (img)
                img.enabled = active;
        }

        private void PlayAnimation(AnimationClip clip, int hash, Action callback)
        {
            if (clip != null)
            {
                if (_animationCoroutine != null)
                    StopCoroutine(_animationCoroutine);

                _animationCoroutine = AnimationCorroutine(clip.length + 0.1f, hash, callback);
                StartCoroutine(_animationCoroutine);
            }
            else
            {
                animator.enabled = false;
                if (callback != null)
                    callback();

            }
        }

        public virtual void Toggle()
        {
            if (IsVisible)
                Hide();
            else
                Show();
        }

        public virtual void Show()
        {
            Show(true);
        }

        public virtual void Show(bool triggerEvents)
        {
            if (IsVisible)
                return;

            IsVisible = true;
            SetChildrenActive(true);
            PlayAnimation(showAnimation, ShowAnimID, null);

			if (ShowClip) 
				EventManager.SendEvent<EventData.PlaySoundEvent>(new EventData.PlaySoundEvent(ShowClip));

            if (triggerEvents)
                InvokeShowEvent();
        }

        public void Show(float waitTime)
        {
            Show(waitTime, true);
        }

        public void Show(float waitTime, bool triggerEvents)
        {
            if (waitTime > 0.0f)
                StartCoroutine(_Show(waitTime, triggerEvents));
            else
                Show(triggerEvents);
        }

        protected virtual IEnumerator _Show(float waitTime, bool triggerEvents = true)
        {
            yield return StartCoroutine(WaitCorroutine(waitTime));
            Show(triggerEvents);
        }
		
        public virtual void HideFirst()
        {
            IsVisible = false;
            animator.enabled = false;

            SetChildrenActive(false);
        }
		
        public virtual void Hide()
        {
            Hide(true);
        }

        public virtual void Hide(bool triggerEvents)
        {
            if (IsVisible == false)
                return;

            IsVisible = false;
            PlayAnimation(hideAnimation, HideAnimID, () =>
                {
                    if (IsVisible == false)
                        SetChildrenActive(false);

                });

			if (HideClip) 
			{
			EventManager.SendEvent<EventData.PlaySoundEvent>(new EventData.PlaySoundEvent(HideClip));
			}


            if (triggerEvents)
                InvokeHideEvent();
        }

        public void Hide(float waitTime)
        {
            Hide(waitTime, true);
        }

        public void Hide(float waitTime, bool invokeEvents)
        {
            if (waitTime > 0)
                StartCoroutine(prepareHide(waitTime, invokeEvents));
            else
                Hide(invokeEvents);
        }

        protected virtual IEnumerator prepareHide(float waitTime, bool triggerEvents = true)
        {
            yield return StartCoroutine(WaitCorroutine(waitTime));
            Hide(triggerEvents);
        }


       
        protected virtual IEnumerator AnimationCorroutine(float waitTime, int hash, Action callback)
        {
            yield return null; 

            var before = _animationCoroutine;
            animator.enabled = true;
            animator.Play(hash);

            yield return StartCoroutine(WaitCorroutine(waitTime));

            animator.enabled = false;
            if (callback != null)
                callback();

            if (before == _animationCoroutine)
            {
                _animationCoroutine = null;
            }
        }

        protected IEnumerator WaitCorroutine(float waitTime)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + waitTime)
            {
                yield return null;
            }
        }
    }

