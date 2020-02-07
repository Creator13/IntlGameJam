using System;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Image))]
    public class BackgroundSwitcher : MonoBehaviour {
        [SerializeField] private Sprite neutralBackground;
        [SerializeField] private Sprite badBackground;
        [SerializeField] private Sprite goodBackground;

        private new Image renderer;

        private void Awake() {
            renderer = GetComponent<Image>();
            renderer.sprite = neutralBackground;
        }

        private void OnValidate() {
            renderer = GetComponent<Image>();
            renderer.sprite = neutralBackground ? neutralBackground : null;
        }

        private void Start() {
            GameManager.Instance.PlayerInfo.ScenarioTriggered += SwitchScenario;
        }

        private void OnEnable() {
            if (GameManager.Instance != null) GameManager.Instance.PlayerInfo.ScenarioTriggered += SwitchScenario;
        }

        private void OnDisable() {
            GameManager.Instance.PlayerInfo.ScenarioTriggered -= SwitchScenario;
            
        }

        private void OnDestroy() {
            GameManager.Instance.PlayerInfo.ScenarioTriggered -= SwitchScenario;
        }

        private void SwitchScenario(Scenario scenario) {
            switch (scenario) {
                case Scenario.Conspiracy:
                    renderer.sprite = badBackground;
                    break;
                case Scenario.Science:
                    renderer.sprite = goodBackground;
                    break;
                case Scenario.Neutral:
                    renderer.sprite = neutralBackground;
                    break;
                default:
                    renderer.sprite = neutralBackground;
                    break;
            }
        }
    }
}
