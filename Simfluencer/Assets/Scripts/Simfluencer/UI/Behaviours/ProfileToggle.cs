using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Toggle))]
    public class ProfileToggle : MonoBehaviour {
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

        private void OnValidate() {
            if (sprite) {
                var toggle = GetComponent<Toggle>();
                toggle.image.sprite = sprite;
            }
        }
    }
}
