using System;
using Simfluencer.Model;
using TMPro;
using UnityEngine;

namespace Simfluencer.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CredibilityBar : MonoBehaviour {
        private TextMeshProUGUI text;

        private GameStateManager StateManager => GameManager.Instance.GameStateManager;

        private void OnEnable() {
            UpdateText(0);

            if (GameManager.Instance != null) {
                StateManager.CredibilityChanged += UpdateText;
            }
        }

        private void OnDisable() {
            StateManager.CredibilityChanged -= UpdateText;
        }

        private void UpdateText(float newValue) {
            if (!text) text = GetComponent<TextMeshProUGUI>();
            text.text = $"Credibility: {Mathf.RoundToInt((GameManager.Instance == null ? 0 : StateManager.Credibility) * 100)}%";
        }
    }
}
