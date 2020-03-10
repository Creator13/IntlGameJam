using System;
using System.Collections.Generic;
using System.Linq;
using Simfluencer.UI;
using UnityEngine;

namespace Simfluencer.Model {
    [Serializable]
    public class GameStateManager {
        public event Action<GameState> StateChanged;
        public event Action<float> CredibilityChanged;
        public event Action<float> PositivityChanged;

        public List<Scenario> Scenarios { get; private set; }
        private List<float> scenarioScores;
        private float positivity;
        private float credibility;
        private GameState currentState;

        private GameState CurrentState {
            get => currentState;
            set {
                if (value == null) return;

                currentState = value;
                StateChanged?.Invoke(value);
            }
        }

        public Stack<ProcessedPost> PostHistory { get; } = new Stack<ProcessedPost>();

        public float Positivity {
            get => positivity;
            private set {
                positivity = Mathf.Clamp(value, -1, 1);
                
                PositivityChanged?.Invoke(value);
            }
        }

        public float Credibility {
            get => credibility;
            private set {
                credibility = Mathf.Clamp(value, 0, 1);
                
                CredibilityChanged?.Invoke(value);
            }
        }

        public ScenarioEnding CurrentScenarioEndingPath {
            get {
                if (Positivity < 0) {
                    return Credibility < .5 ? ScenarioEnding.ConspiracyNegative : ScenarioEnding.ScienceNegative;
                }
                else {
                    return Credibility < .5 ? ScenarioEnding.ConspiracyPositive : ScenarioEnding.SciencePositive;
                }
            }
        }

        public BackgroundObject CurrentBackground {
            get {
                // TODO integrate this logic in game states
                if (CurrentState is ScenarioBaseState state) {
                    switch (state) {
                        case ScenarioState _:
                            return state.scenario.GetMidwayBackground(CurrentScenarioEndingPath);
                        case ScenarioLockState _:
                            return state.scenario.GetEndBackground(CurrentScenarioEndingPath);
                    }
                }

                return null;
            }
        }

        public GameStateManager(List<Scenario> scenarios, float startCredibility, float startPositivity) {
            Credibility = startCredibility;
            Positivity = startPositivity;
            Scenarios = scenarios;

            scenarioScores = new List<float>();
            InitScenarioScores();

            CurrentState = new FreeState(this);
        }

        public List<Scenario> TopScenarios(int count) {
            var top = scenarioScores.OrderBy(i => i)
                                    .Take(count)
                                    .Select((f, i) => i);
            return top.Select(i => Scenarios[i]).ToList();
        }

        public Scenario TopScenario() {
            return TopScenarios(1)[0];
        }

        public void ProcessPost(Post post) {
            // Value changes
            Positivity += post.Positivity;
            Credibility += post.Credibility;
            // TODO change follower count

            // Check if the post has an assigned scenario. If not, this means the post does not belong to any specific
            // scenario. Hence, the post will not affect any of the scenario-specific scores.
            if (post.scenario) {
                IncreaseScenarioScore(post.scenario, GameManager.Instance.GameSettings.basePostImpact * post.Impact);
            }

            // Add this post to the history
            PostHistory.Push(new ProcessedPost(post, GameManager.Instance.PlayerInfo.Profile));

            // Remove from pool
            GameManager.Instance.PostPool.Consume(post);

            DoTransitionCheck(post);
        }

        private void DoTransitionCheck(Post post) {
            // No scenario means neutral post, hence no transition will happen
            if (post.scenario == null) return;

            CurrentState = CurrentState.CheckTransition(post);

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
