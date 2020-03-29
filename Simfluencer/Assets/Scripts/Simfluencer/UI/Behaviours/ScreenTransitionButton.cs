using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Screen = Simfluencer.UI.Screens.Screen;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Button))]
    public class ScreenTransitionButton : MonoBehaviour {
        [SerializeField] private bool back;
        [SerializeField] private Screen nextScreen;

        private Screen parentScreen;
        private Button buttonComponent;

        public Func<bool> PreCondition { get; set; }
        public Action ClickAction { get; set; }

        private void Start() {
            parentScreen = GetComponentInParent<Screen>();
            Assert.IsNotNull(parentScreen);
            // No assertion for button component is needed as this is already enforced through RequireComponent
            buttonComponent = GetComponent<Button>();

            buttonComponent.onClick.AddListener(DoClick);
        }

        public void DoClick() {
            if (PreCondition != null && !PreCondition.Invoke()) return;

            // Perform custom action if set
            ClickAction?.Invoke();

            // Perform transition
            if (back) {
                parentScreen.GoBack();
            }
            else {
                parentScreen.GoToScreen(nextScreen);
            }
        }
    }
}
