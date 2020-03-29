using Simfluencer.Model;
using TMPro;
using UnityEngine;

namespace Simfluencer.UI.Screens {
    public class EndingScreen : Screen {
        [SerializeField] private TextMeshProUGUI messageTextbox;

        protected override void Show() {
            Scenario scenario;
            if (GameManager.Instance.GameStateManager.CurrentState is ScenarioBaseState state) {
                scenario = state.scenario;
            }
            else {
                scenario = null;
            }

            if (scenario != null) {
                messageTextbox.text = scenario.GetEndingMessage(GameManager.Instance.GameStateManager.CurrentScenarioEndingPath);
            }
            
            uiManager.ClearHistory();
        }
    }
}
