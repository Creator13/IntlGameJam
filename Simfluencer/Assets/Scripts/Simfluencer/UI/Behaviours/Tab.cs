using System;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Button))]
    public class Tab : MonoBehaviour {
        internal event Action<Tab> TabClicked;

        [SerializeField] private int value;
        public object Value => value;

        private Image imageComponent;
        private Button button;
        private Sprite originalSprite;

        private Image ImageComponent => imageComponent ? imageComponent : imageComponent = GetComponent<Image>();

        public bool IsSelected {
            set {
                // if (!isActiveAndEnabled) return;
                if (!ImageComponent || !button) return;

                ImageComponent.sprite = value ? button.spriteState.selectedSprite : originalSprite;
                button.enabled = !value;
            }
        }

        private void Awake() {
            button = GetComponent<Button>();
            originalSprite = ImageComponent.sprite;

            button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            TabClicked?.Invoke(this);
        }
    }
}
