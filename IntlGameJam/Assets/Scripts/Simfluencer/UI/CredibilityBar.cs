using TMPro;
using UnityEngine;

namespace Simfluencer.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CredibilityBar : MonoBehaviour {
        private TextMeshProUGUI text;

        private void OnEnable() {
            UpdateText(0);
            
            if (GameManager.Instance != null) {
                GameManager.Instance.PlayerInfo.CredibilityChanged += UpdateText;
            }
        }

        private void OnDisable() {
            GameManager.Instance.PlayerInfo.CredibilityChanged -= UpdateText;
        }

        private void UpdateText(float newValue) {
            if (!text) text = GetComponent<TextMeshProUGUI>();
            text.text = $"Credibility: {Mathf.RoundToInt((GameManager.Instance == null ? 0 : GameManager.Instance.PlayerInfo.Credibility) * 100)}%";
        }
    }
}
