using System.Collections;
using TMPro;
using UnityEngine;

namespace Simfluencer.UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FollowerBubble : MonoBehaviour {
        [SerializeField] private float duration = 2;
        private TextMeshProUGUI text;
        private float activationTime;
        private Color originalColor;

        private void Awake() {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void Activate(int followerNumber) {
            text.text = $"{(followerNumber < 0 ? "" : "+")}{followerNumber}";
            activationTime = Time.time;
            originalColor = text.color;

            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve() {
            var passedTime = Time.time - activationTime;
            while (passedTime < duration) {
                passedTime = Time.time - activationTime;
                var percent = passedTime / duration;

                transform.localPosition += Vector3.up * (Time.deltaTime * 40);

                var newColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - percent);
                text.color = newColor;
                
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
