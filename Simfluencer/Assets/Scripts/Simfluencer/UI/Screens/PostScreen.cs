using System.Collections.Generic;
using Simfluencer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI.Screens {
    public class PostScreen : Screen {
        [SerializeField] private TMP_InputField textField;
        [SerializeField] private Transform buttonGroup;
        [SerializeField] private PostButton buttonPrefab;
        [SerializeField] private Button submitButton;

        private List<PostButton> buttons;
        private Post selectedPost;

        private Post SelectedPost {
            set {
                selectedPost = value;
                textField.text = selectedPost.Content;
            }
        }

        protected override void Show() {
            if (buttonGroup.childCount > 0) {
                foreach (Transform child in buttonGroup) {
                    Destroy(child.gameObject);
                }
            }

            buttons = new List<PostButton>();
            CreateButtons();
            submitButton.onClick.AddListener(SubmitPost);
        }

        protected override void Hide() {
            DestroyButtons();
            submitButton.onClick.RemoveListener(SubmitPost);
        }

        private void SubmitPost() {
            if (!selectedPost) {
                // TODO give warning to user
                return;
            }

            GameManager.Instance.GameStateManager.ProcessPost(selectedPost, GameManager.Instance.PlayerInfo.Profile);
            
            uiManager.TransitionToScreen("Main");
        }

        private void CreateButtons() {
            var posts = GameManager.Instance.PostPool.GetPosts(ScenarioEnding.ConspiracyNegative);

            // Instantiate the four buttons
            for (var i = 0; i < 4; i++) {
                var button = Instantiate(buttonPrefab, buttonGroup, false);
                // Assign a post to each button
                // Technically not necessary as this can also be implicitly assigned in the listener on the next line
                button.Post = posts[i];

                // Assign a listener to this button that will set this post as the selected post
                button.ButtonComponent.onClick.AddListener(() => SelectedPost = button.Post);

                buttons.Add(button);
            }
        }

        private void DestroyButtons() {
            if (buttons == null) return;

            foreach (var button in buttons) {
                Destroy(button.gameObject);
            }

            buttons.Clear();
        }
    }
}
