using UnityEngine;
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

        protected virtual void Show() { }

        protected virtual void Hide() { }

        public virtual void GoToScreen(string name) {
            uiManager.TransitionToScreen(name);
        }

        public void GoBack() {
            uiManager.ReturnToLastScreen();
        }
        // TODO add methods for sliding 
    }
}
