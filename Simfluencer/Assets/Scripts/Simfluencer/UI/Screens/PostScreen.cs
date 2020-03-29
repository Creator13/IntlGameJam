using System.Collections.Generic;
using Simfluencer.Model;
using TMPro;
using UnityEngine;

namespace Simfluencer.UI.Screens {
    public class PostScreen : Screen {
        [SerializeField] private TMP_InputField textField;
        [SerializeField] private Transform buttonGroup;
        [SerializeField] private PostButton buttonPrefab;
        [SerializeField] private ScreenTransitionButton submitButton;
        [SerializeField] private TabGroup tabGroup;

        private List<PostButton> buttons;
        private Post selectedPost;

        private Post SelectedPost {
            set {
                selectedPost = value;
                textField.text = selectedPost.Content;
            }
        }

        /// <inheritdoc />
        protected override void Show() {
            if (buttonGroup.childCount > 0) {
                foreach (Transform child in buttonGroup) {
                    Destroy(child.gameObject);
                }
            }

            textField.text = string.Empty;
            buttons = new List<PostButton>();
            CreateButtons((ScenarioEnding) tabGroup.SelectedTab.Value);

            submitButton.PreCondition = HasPost;
            submitButton.ClickAction = SubmitPost;
            tabGroup.TabChanged += OnTabSwitch;
        }

        /// <inheritdoc />
        protected override void Hide() {
            selectedPost = null;
            submitButton.PreCondition = null;
            submitButton.ClickAction = null;
            tabGroup.TabChanged -= OnTabSwitch;
            DestroyButtons();
        }

        private bool HasPost() {
            return selectedPost;
        }

        private void SubmitPost() {
            GameManager.Instance.GameStateManager.ProcessPost(selectedPost);
            selectedPost = null;
        }

        private void CreateButtons(ScenarioEnding ending) {
            var posts = GameManager.Instance.PostPool.GetPosts(ending);

            // Instantiate the four buttons
            foreach (var t in posts) {
                var button = Instantiate(buttonPrefab, buttonGroup, false);
                // Assign a post to each button
                // Technically not necessary as this can also be implicitly assigned in the listener on the next line
                button.Post = t;

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

        private void OnTabSwitch(object val) {
            DestroyButtons();
            CreateButtons((ScenarioEnding) (int) val);
        }
    }
}
