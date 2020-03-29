using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI.Screens {
    public class NameScreen : Screen {
        [SerializeField] private string nextScreen;
        private Button submitButton;
        private TMP_InputField nameField;
        
        protected override void Show() {
            submitButton = GetComponentInChildren<Button>();
            nameField = GetComponentInChildren<TMP_InputField>();
            
            submitButton.onClick.AddListener(() => {
                if (nameField.text != string.Empty) {
                    GameManager.Instance.PlayerInfo.Name = nameField.text;
                    uiManager.TransitionToScreen(nextScreen);
                }
                else {
                    Debug.Log("No username entered");
                }
            });
        }
    }
}
