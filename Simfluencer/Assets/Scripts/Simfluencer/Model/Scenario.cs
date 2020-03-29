using System.Collections.Generic;
using Simfluencer.UI;
using UnityEngine;

namespace Simfluencer.Model {
    public enum ScenarioEnding {
        ConspiracyPositive,
        ConspiracyNegative,
        SciencePositive,
        ScienceNegative
    }

    [CreateAssetMenu(menuName = "Simfluencer/Scenario")]
    public class Scenario : ScriptableObject {
        [SerializeField] private string scenarioName;
        public string ScenarioName => scenarioName;
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField] private AudioClip neutralMusic;
        [SerializeField] private AudioClip negativeMusic;
        [SerializeField] private AudioClip positiveMusic;
        public AudioClip NeutralMusic => neutralMusic;
        public AudioClip NegativeMusic => negativeMusic;
        public AudioClip PositiveMusic => positiveMusic;

        [SerializeField] private BackgroundObject[] midwayBackgrounds = new BackgroundObject[4];
        [SerializeField] private BackgroundObject[] endBackgrounds = new BackgroundObject[4];

        [SerializeField, TextArea] private string[] endingMessages;

        [SerializeField] private List<Post> posts;
        public List<Post> Posts => posts;

        public void Init() {
            foreach (var post in posts) {
                post.scenario = this;
            }
        }

        public BackgroundObject GetMidwayBackground(ScenarioEnding ending) {
            return midwayBackgrounds[(int) ending];
        }

        public BackgroundObject GetEndBackground(ScenarioEnding ending) {
            return endBackgrounds[(int) ending];
        }

        public string GetEndingMessage(ScenarioEnding ending) {
            return endingMessages[(int) ending];
        }
    }
}
