using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Simfluencer.UI.Screens {
    public class UpdateScreen : Screen {
        [SerializeField] private Transform buttonPanel;

        protected override void Show() {
            var buttons = buttonPanel.GetComponentsInChildren<Button>();
            var categories = CategoryLoader.Instance.Categories;

            Assert.AreEqual(buttons.Length, categories.Count);

            for (var i = 0; i < buttons.Length; i++) {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = categories[i].Name;

                if (categories[i].Used) {
                    buttons[i].interactable = false;
                }
                else {
                    // FIXME when using more than one listener (eg for audio) on the button, they will get removed as well
                    buttons[i].onClick.RemoveAllListeners();
                    var i1 = i;
                    buttons[i].onClick
                        .AddListener(() => uiManager.TransitionToScreen($"PostScreen.{categories[i1].Name}"));
                }
            }
        }
    }
}
