using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Simfluencer.UI {
    public class ButtonController : MonoBehaviour {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private int count;

        private List<Button> buttons;
        
        private void Start() {
            buttons = new List<Button>();
            
            // Instantiate the buttons
            for (var i = 0; i < count; i++) {
                var b = Instantiate(buttonPrefab, transform, false);
                b.name = $"Reply_Button_{i + 1}";
                b.GetComponentInChildren<TextMeshProUGUI>().text = "";
                buttons.Add(b);
            }
        }

        public void SetQuestion(Question q) {
            // Check that the scene is set up so that each question has exactly the same number as responses as any
            // incoming question
            Assert.AreEqual(buttons.Count, q.Replies.Length);
            
            for (var i = 0; i < buttons.Count; i++) {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.Replies[i].Content;
            }
        }
    }
}
