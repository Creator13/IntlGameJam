using TMPro;
using UnityEngine;

namespace Simfluencer.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FollowerText : MonoBehaviour {
        private TextMeshProUGUI text;

        private void OnEnable() {
            UpdateText(0);
            
            if (GameManager.Instance != null) {
                GameManager.Instance.PlayerInfo.FollowersChanged += UpdateText;
            }
        }

        private void OnDisable() {
            GameManager.Instance.PlayerInfo.FollowersChanged -= UpdateText;
        }

        private void UpdateText(int added) {
            if (!text) text = GetComponent<TextMeshProUGUI>();
            text.text = $"Followers: {(GameManager.Instance == null ? 0 : GameManager.Instance.PlayerInfo.Followers)}";
        }
    }
}
