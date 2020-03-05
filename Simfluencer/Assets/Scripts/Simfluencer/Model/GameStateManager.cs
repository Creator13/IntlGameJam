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
            private set => credibility = Mathf.Clamp(value, 0, 1);
        }

        public GameStateManager(List<Scenario> scenarios, float startCredibility, float startPositivity) {
            Credibility = startCredibility;
            Positivity = startPositivity;
            Scenarios = scenarios;

            scenarioScores = new List<float>();
            InitScenarioScores();

            currentState = new FreeState(this);
        }

        public List<Scenario> TopScenarios(int count) {
            var top = scenarioScores.OrderBy(i => i)
                                    .Take(count)
                                    .Select((f, i) => i);
            return top.Select(i => Scenarios[i]).ToList();
        }

        public void ProcessPost(Post post, Profile profile) {
            // Value changes
            Positivity += post.Positivity;
            Credibility += post.Credibility;

            // Check if the post has an assigned scenario. If not, this means the post does not belong to any specific
            // scenario. Hence, the post will not affect any of the scenario-specific scores.
            if (post.scenario) {
                IncreaseScenarioScore(post.scenario, GameManager.Instance.GameSettings.basePostImpact * post.Impact);
            }

            // Add this post to the history
            PostHistory.Push(new ProcessedPost(post, profile));

            // Remove from pool
            GameManager.Instance.PostPool.Consume(post);

            DoTransitionCheck();
        }

        private void DoTransitionCheck() {
            // stage 1 > 2: 3 turns highest scenarios
            
            // if 3x not posted: stage 2 > 1
            
            // stage 2 > 3 (lock): 2 more turns
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
