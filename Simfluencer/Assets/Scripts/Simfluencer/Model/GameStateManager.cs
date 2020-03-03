using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer.Model {
    public delegate bool TransitionCheck();
    
    [System.Serializable]
    public class GameStateManager {
        public List<Scenario> Scenarios { get; private set; }
        private List<float> scenarioScores;
        private float positivity;
        private float credibility;
        private GameState currentState;

        public Stack<ProcessedPost> PostHistory { get; } = new Stack<ProcessedPost>();

        public float Positivity {
            get => positivity;
            private set => positivity = Mathf.Clamp(value, -1, 1);
        }

        public float Credibility {
            get => credibility;
            private set => credibility = Mathf.Clamp(value, -1, 1);
        }

        public GameStateManager(List<Scenario> scenarios) {
            Scenarios = scenarios;
            
            scenarioScores = new List<float>();
            InitScenarioScores();
            
            currentState = new FreeState();
        }

        public void ProcessPost(Post post, Profile profile) {
            // Value changes
            Positivity += post.Positivity;
            Credibility += post.Credibility;

            IncreaseScenarioScore(post.scenario, GameManager.Instance.GameSettings.basePostImpact * post.Impact);

            // Add this post to the history
            PostHistory.Push(new ProcessedPost(post, profile));
            
            //TODO remove post from pool

            DoTransitionCheck();
        }

        private void DoTransitionCheck() {
            
        }
        
        private void InitScenarioScores() {
            var startScore = 1f / Scenarios.Count;
            
            foreach (var _ in Scenarios) {
                scenarioScores.Add(startScore);
            }
        }

        private void IncreaseScenarioScore(Scenario s, float delta) {
            var index = Scenarios.IndexOf(s);
            scenarioScores[index] += delta;
            NormalizeScores();
        }

        private void NormalizeScores() {
            var total = scenarioScores.Sum();
            for (var i = 0; i < scenarioScores.Count; i++) {
                scenarioScores[i] /= total;
            }
        }
    }
}
