﻿using UnityEngine;
using UnityEngine.Assertions;

namespace Simfluencer.UI.Screens {
    [RequireComponent(typeof(RectTransform))]
    public class Screen : MonoBehaviour {
        [SerializeField] protected UIManager uiManager;
        [SerializeField] private string screenName;
        public string Name => screenName;

        public bool Active {
            get => gameObject.activeInHierarchy;
            set {
                if (value) {
                    Show();
                    // Debug.Log($"Now displaying screen {screenName}");
                }
                else {
                    Hide();
                }

                gameObject.SetActive(value);
            }
        }

        protected virtual void Show() { }

        protected virtual void Hide() { }

        // TODO add methods for sliding
    }
}