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
            renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = neutralBackground;
        }
        
        
    }
}
