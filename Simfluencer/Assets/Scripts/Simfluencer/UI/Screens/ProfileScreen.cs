using TMPro;
using UnityEngine;

namespace Simfluencer.UI.Screens {
    public class ProfileScreen : Screen {
        [SerializeField] private ProfilePictureChooser chooser;
        [SerializeField] private ScreenTransitionButton submit;
        [SerializeField] private TMP_InputField input;

        protected override void Show() {
#if UNITY_ANDROID
            TouchScreenKeyboard.hideInput = true;
#endif

            if (GameManager.Instance.PlayerInfo.Profile != null) {
                submit.DoClick();
                return;
            }

            submit.PreCondition = HasText;
            submit.ClickAction = ClickAction;
        }

        protected override void Hide() {
            submit.PreCondition = null;
            submit.ClickAction = null;
        }

        private bool HasText() {
            return input.text != string.Empty;
        }

        private void ClickAction() {
            if (HasText()) {
                SetProfile();
            }
        }

        private void SetProfile() {
            GameManager.Instance.PlayerInfo.Avatar = chooser.GetSelected();
            GameManager.Instance.PlayerInfo.Name = input.text;
        }

        public override void GoToScreen(Screen screen) {
            uiManager.TransitionToScreen(screen, false);
        }
    }
}
