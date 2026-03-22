// TEMPORARY FIX: DOTween disabled - animations won't work until DOTween is installed from Asset Store
// All animation code replaced with instant transitions for testing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Bhambhoo
{
    public class MainMenuScreen : MonoBehaviour
    {
        public UnityEvent BeforeLoadingThisScreen;

        [Header("Fade Animation Properties (DISABLED - DOTween not installed)")]
        public CanvasGroup[] FadableObjects;
        public float fadeDuration = 0.15f;
        public int fadeMoveDistance = 200;
        public float delaysAmongObjects = 0.05f;

        /// <summary>
        /// TEMPORARY: Instant transition (no animation) until DOTween is installed
        /// </summary>
        public void SwitchTo(MainMenuScreen newScreen, Action OnFadeOutComplete)
        {
            // Disable this screen immediately
            gameObject.SetActive(false);

            // Invoke callback
            OnFadeOutComplete?.Invoke();

            // Load new screen
            if (newScreen != null)
                newScreen.LoadThisScreen();
        }

        /// <summary>
        /// TEMPORARY: Instant load (no animation) until DOTween is installed
        /// </summary>
        public void LoadThisScreen()
        {
            UIManager.Instance.CurrentScreen = this;
            gameObject.SetActive(true);

            BeforeLoadingThisScreen.Invoke();

            // Set all canvas groups to visible immediately
            foreach (CanvasGroup oneItem in FadableObjects)
            {
                if (oneItem == null) continue;
                oneItem.alpha = 1;
            }

            UIManager.Instance.AllowUITouches(true);
        }
    }
}
