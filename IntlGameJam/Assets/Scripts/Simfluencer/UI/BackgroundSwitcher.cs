using System;
using UnityEngine;

namespace Simfluencer.UI {
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundSwitcher : MonoBehaviour {
        [SerializeField] private Sprite neutralBackground;
        [SerializeField] private Sprite badBackground;
        [SerializeField] private Sprite goodBackground;

        private new SpriteRenderer renderer;

        private void Awake() {
            renderer.sprite = neutralBackground;
        }

        private void OnValidate() {
            renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = neutralBackground ? neutralBackground : null;
        }

        private void Start() {
            GameManager.Instance.PlayerInfo.ScenerioTriggered += SwitchScenario;
        }

        private void OnDisable() {
            GameManager.Instance.PlayerInfo.ScenerioTriggered -= SwitchScenario;
            
        }

        private void OnDestroy() {
            GameManager.Instance.PlayerInfo.ScenerioTriggered -= SwitchScenario;
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
