using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Image))]
    public class BackgroundSwitcher : MonoBehaviour {
        [SerializeField] private Sprite neutralBackground;

        private new Image renderer;

        private void Awake() {
            renderer = GetComponent<Image>();
            renderer.sprite = neutralBackground;
        }

        private void OnValidate() {
            // Detect changes in edit mode to not have a big empty space instead of an image
            renderer = GetComponent<Image>();
            renderer.sprite = neutralBackground ? neutralBackground : null;
        }

        private void Start() {
            // GameManager.Instance.PlayerInfo.ScenarioTriggered += SwitchScenario;
            //TODO implement for GameStateManager
        }

        private void OnEnable() {
            // if (GameManager.Instance != null) GameManager.Instance.PlayerInfo.ScenarioTriggered += SwitchScenario;
            //TODO implement for GameStateManager
        }

        private void OnDisable() {
            // GameManager.Instance.PlayerInfo.ScenarioTriggered -= SwitchScenario;
            //TODO implement for GameStateManager
            
        }

        private void OnDestroy() {
            // GameManager.Instance.PlayerInfo.ScenarioTriggered -= SwitchScenario;
            //TODO implement for GameStateManager
        }

        // private void SwitchScenario(Scenario scenario) {
        //     switch (scenario) {
        //         case Scenario.Conspiracy:
        //             renderer.sprite = badBackground;
        //             break;
        //         case Scenario.Science:
        //             renderer.sprite = goodBackground;
        //             break;
        //         case Scenario.Neutral:
        //             renderer.sprite = neutralBackground;
        //             break;
        //         default:
        //             renderer.sprite = neutralBackground;
        //             break;
        //     }
        //     //TODO implement for GameStateManager
        // }
    }
}
