using Simfluencer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI.Screens {
    public class ProfileScreen : Screen {
        [SerializeField] private string nextScreen;
        [SerializeField] private ProfilePictureChooser chooser;
        [SerializeField] private Button submit;
        [SerializeField] private TMP_InputField input;
        

        protected override void Show() {
            //TODO check for existing profile
            
            submit.onClick.RemoveAllListeners();
            submit.onClick.AddListener(() => {
                if (input.text != string.Empty) {
                    SetProfile();
                    GoToScreen(nextScreen);
                }
            });
        }

        private void SetProfile() {
            GameManager.Instance.PlayerInfo.Avatar = chooser.GetSelected();
            GameManager.Instance.PlayerInfo.Name = input.text;
        }
    }
}
