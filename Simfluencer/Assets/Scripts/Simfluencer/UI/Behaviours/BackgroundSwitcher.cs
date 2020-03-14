using Simfluencer.Model;
using UnityEngine;

namespace Simfluencer.UI {
    public class BackgroundSwitcher : MonoBehaviour {
        [SerializeField] private BackgroundObject neutralBackground;
        [SerializeField] private RectTransform backgroundPlaceholder;

        private GameStateManager StateMachine => GameManager.Instance.GameStateManager;

        private void Start() {
            StateMachine.StateChanged += OnStateSwitch;
            StateMachine.PositivityChanged += OnPositivityChange;
        }

        // private void OnDisable() {
        //     StateMachine.StateChanged -= OnStateSwitch;
        //     StateMachine.PositivityChanged -= OnPositivityChange;
        // }

        private void OnDestroy() {
            StateMachine.StateChanged -= OnStateSwitch;
            StateMachine.PositivityChanged -= OnPositivityChange;
        }

        private void OnStateSwitch(GameState newState) {
            SwitchBackground(StateMachine.CurrentBackground ? StateMachine.CurrentBackground : neutralBackground);
        }

        private void OnPositivityChange(float newValue) {
            SwitchBackground(StateMachine.CurrentBackground ? StateMachine.CurrentBackground : neutralBackground);
        }

        private void SwitchBackground(BackgroundObject newBgPrefab) {
            // Destroy current background
            foreach (Transform child in backgroundPlaceholder) {
                Destroy(child.gameObject);
            }

            // Load the new background
            Instantiate(newBgPrefab, backgroundPlaceholder, false);
        }
    }
}
