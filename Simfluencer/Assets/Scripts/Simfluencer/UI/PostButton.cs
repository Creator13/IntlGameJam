using System;
using Simfluencer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(Button))]
    public class PostButton : MonoBehaviour {
        private Button buttonComp;
        private TextMeshProUGUI text;
        private Post post;

        public Post Post {
            get => post;
            set {
                post = value;
                Text.text = post.Tagline;
            }
        }

        private TextMeshProUGUI Text => text ? text : text = GetComponentInChildren<TextMeshProUGUI>();
        public Button ButtonComponent => buttonComp ? buttonComp : buttonComp = GetComponent<Button>();

        // private void Awake() {
        //     GetComponent<Button>().onClick.AddListener(DoPost);
        // }
        //
        // private void OnDisable() {
        //     GetComponent<Button>().onClick.RemoveListener(DoPost);
        // }
        //
        // private void OnDestroy() {
        //     GetComponent<Button>().onClick.RemoveListener(DoPost);
        // }
        //
        // private void DoPost() {
        //     
        // }
    }
}
