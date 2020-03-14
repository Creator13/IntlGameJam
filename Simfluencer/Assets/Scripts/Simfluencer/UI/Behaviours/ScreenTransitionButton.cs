using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Screen = Simfluencer.UI.Screens.Screen;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Button))]
    public class ScreenTransitionButton : MonoBehaviour {
        [SerializeField] private Screen nextScreen;

        private Screen parentScreen;
        private Button buttonComponent;

        public Action ClickAction { private get; set; }

        private void Start() {
            parentScreen = GetComponentInParent<Screen>();
            Assert.IsNotNull(parentScreen);
            // No assertion for button component is needed as this is already enforced through RequireComponent
            buttonComponent = GetComponent<Button>();

            buttonComponent.onClick.AddListener(HandleClick);
        }

        private void HandleClick() {
            // Perform custom action if set
            ClickAction?.Invoke();
            // Ask the screen to transition to the next
            parentScreen.GoToScreen(nextScreen);
        }
    }
}
