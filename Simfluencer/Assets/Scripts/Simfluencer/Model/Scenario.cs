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

        [SerializeField] private BackgroundObject[] midwayBackgrounds = new BackgroundObject[4];
        [SerializeField] private BackgroundObject[] endBackgrounds = new BackgroundObject[4];
        
        [SerializeField] private List<Post> posts;
        public List<Post> Posts => posts;
        
        public void Init() {
            foreach (var post in posts) {
                post.scenario = this;
            }
        }

        public BackgroundObject GetEndBackground(ScenarioEnding scen) {
            return endBackgrounds[(int) scen];
        }
        
        public BackgroundObject GetMidwayBackground(ScenarioEnding scen) {
            return midwayBackgrounds[(int) scen];
        }
    }
}
