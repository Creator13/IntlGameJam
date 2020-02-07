using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    public class PostScreen : Screen {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Button sciPos;
        [SerializeField] private Button sciNeg;
        [SerializeField] private Button consPos;
        [SerializeField] private Button consNeg;

        private PostCategory category;

        public string Category {
            set => category = CategoryLoader.Instance.GetCategory(value);
        }

        protected override void Show() {
            title.text = category.Name;

            sciPos.GetComponentInChildren<TextMeshProUGUI>().text = category.PostOptions[0];
            sciNeg.GetComponentInChildren<TextMeshProUGUI>().text = category.PostOptions[1];
            consPos.GetComponentInChildren<TextMeshProUGUI>().text = category.PostOptions[2];
            consNeg.GetComponentInChildren<TextMeshProUGUI>().text = category.PostOptions[3];

            sciPos.onClick.AddListener(RegisterSciPos);
            sciNeg.onClick.AddListener(RegisterSciNeg);
            consPos.onClick.AddListener(RegisterConsPos);
            consNeg.onClick.AddListener(RegisterConsNeg);
        }

        private void DisableCategory() {
            category.Used = true;
            uiManager.TransitionToScreen("Main");
        }

        private void RegisterSciPos() {
            var followPct = Random.Range(0.02f, 0.04f);
            var credPct = Random.Range(0.05f, 0.15f);

            AddFollowers(followPct);
            AddCredibility(credPct);

            DisableCategory();
        }

        private void RegisterSciNeg() {
            var followPct = Random.Range(-.015f, .005f);
            var credPct = Random.Range(0.1f, 0.2f);

            AddFollowers(followPct);
            AddCredibility(credPct);

            DisableCategory();
        }

        private void RegisterConsPos() {
            var followPct = Random.Range(0.03f, 0.05f);
            var credPct = Random.Range(-.1f, 0.005f);

            AddFollowers(followPct);
            AddCredibility(credPct);

            DisableCategory();
        }

        private void RegisterConsNeg() {
            var followPct = Random.Range(-.005f, 0.02f);
            var credPct = Random.Range(-.5f, -.2f);

            AddFollowers(followPct);
            AddCredibility(credPct);

            DisableCategory();
        }

        private static void AddCredibility(float pct) {
            GameManager.Instance.PlayerInfo.Credibility *= (1 + pct);
        }

        private static void AddFollowers(float pct) {
            GameManager.Instance.PlayerInfo.Followers +=
                Mathf.RoundToInt(GameManager.Instance.PlayerInfo.Followers * pct);
        }
    }
}
