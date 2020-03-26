using System;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Button))]
    public class Tab : MonoBehaviour {
        internal event Action<Tab> TabClicked;

        [SerializeField] private int value;
        public object Value => value;

        private Button button;
        private Sprite originalSprite; 

        public bool IsSelected {
            set {
                GetComponent<Image>().sprite = value ? button.spriteState.selectedSprite : originalSprite;
                button.enabled = !value;
            }
        }

        private void Awake() {
            button = GetComponent<Button>();
            originalSprite = GetComponent<Image>().sprite;

            button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            TabClicked?.Invoke(this);
        }
    }
}
