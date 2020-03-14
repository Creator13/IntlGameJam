using Simfluencer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI.Screens {
    public class ProfileScreen : Screen {
        [SerializeField] private ProfilePictureChooser chooser;
        [SerializeField] private ScreenTransitionButton submit;
        [SerializeField] private TMP_InputField input;
        

        protected override void Show() {
            //TODO check for existing profile

            submit.ClickAction = () => {
                if (input.text != string.Empty) {
                    SetProfile();
                }
            };
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
