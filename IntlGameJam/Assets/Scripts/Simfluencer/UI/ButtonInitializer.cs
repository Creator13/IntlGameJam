using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    public class ButtonInitializer : MonoBehaviour {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private int count;
        
        private void Start() {
            // Generate buttons
            for (var i = 0; i < count; i++) {
                CreateButton(i);
            }
        }

        private void CreateButton(int i) {
            var b = Instantiate(buttonPrefab, transform, false);
            b.GetComponentInChildren<TextMeshProUGUI>().text = $"Button {i + 1}";
        }
    }
}
