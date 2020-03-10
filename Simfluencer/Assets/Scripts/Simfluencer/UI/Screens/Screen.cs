﻿using UnityEngine;
using UnityEngine.Assertions;

namespace Simfluencer.UI.Screens {
    [RequireComponent(typeof(RectTransform))]
    public class Screen : MonoBehaviour {
        protected UIManager uiManager;
        [SerializeField] private string screenName;
        public string Name => screenName;

        public bool Active {
            get => gameObject.activeInHierarchy;
            set {
                if (value) {
                    Show();
                }
                else {
                    Hide();
                }

                gameObject.SetActive(value);
            }
        }

        private void Awake() {
            uiManager = GetComponentInParent<UIManager>();
            Assert.IsNotNull(uiManager);
        }

        /// <summary>
        /// Called when the screen is activated.
        /// </summary>
        protected virtual void Show() { }

        /// <summary>
        /// Called when the screen is deactivated.
        /// </summary>
        protected virtual void Hide() { }

        /// <summary>
        /// Go to a screen. Screen is obtained through string lookup.
        /// </summary>
        /// <param name="name">The name of the screen to go to.</param>
        public virtual void GoToScreen(string name) {
            uiManager.TransitionToScreen(name);
        }

        /// <summary>
        /// Go back to previous screen.
        /// </summary>
        public virtual void GoBack() {
            uiManager.ReturnToLastScreen();
        }
        // TODO add methods for sliding 
    }
}
