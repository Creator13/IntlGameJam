using System;
using UnityEngine;

namespace Simfluencer.UI {
    public class AddFollowers : MonoBehaviour {
        [SerializeField] private FollowerBubble prefab;

        private void Start() {
            GameManager.Instance.PlayerInfo.FollowersChanged += ShowBubble;
        }

        private void ShowBubble(int amt) {
            Instantiate(prefab, transform, false).Activate(amt);
        }

        private void OnDisable() {
            GameManager.Instance.PlayerInfo.FollowersChanged -= ShowBubble;
        }

        private void OnDestroy() {
            GameManager.Instance.PlayerInfo.FollowersChanged -= ShowBubble;
        }
    }
}
