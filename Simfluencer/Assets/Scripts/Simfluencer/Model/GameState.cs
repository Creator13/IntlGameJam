using JetBrains.Annotations;

namespace Simfluencer.Model {
    public abstract class GameState {
        protected readonly GameStateManager stateMachine;

        protected GameState(GameStateManager stateMachine) {
            this.stateMachine = stateMachine;
        }

        public abstract GameState CheckTransition(Post post);
    }

    public class FreeState : GameState {
        private Scenario currentTop;
        private int scenarioPostCount;

        public FreeState(GameStateManager stateMachine) : base(stateMachine) {
            scenarioPostCount = 0;
            currentTop = null;
        }

        public override GameState CheckTransition(Post post) {
            currentTop = stateMachine.TopScenario();
            if (post.scenario == currentTop) {
                scenarioPostCount++;
            }
            else {
                scenarioPostCount = 1;
            }

            if (scenarioPostCount == 3) {
                return new ScenarioState(stateMachine, currentTop);
            }

            return null;
        }
    }

    public abstract class ScenarioBaseState : GameState {
        public readonly Scenario scenario;

        protected ScenarioBaseState(GameStateManager stateMachine, Scenario scenario) : base(stateMachine) {
            this.scenario = scenario;
        }
    }

    public class ScenarioState : ScenarioBaseState {
        private Scenario currentTop;
        private int scenarioPostCount;
        private int notPostedCount;

        public ScenarioState(GameStateManager stateMachine, Scenario scenario) : base(stateMachine, scenario) {
            notPostedCount = 0;
            scenarioPostCount = 0;
            currentTop = null;
        }

        public override GameState CheckTransition(Post post) {
            currentTop = stateMachine.TopScenario();
            // Count how many times in a row the player has posted about a different topic than the current scenario
            if (post.scenario != scenario) {
                notPostedCount++;

                if (notPostedCount == 3) {
                    return new FreeState(stateMachine);
                }
            }
            else {
                notPostedCount = 0;
            }

            // Count how many times in a row the player ha posted about the current scenario, if that scenario is still
            // at the top
            // TODO consider if it should fall back to FreeState as soon as the current scenario is not the top scenario anymore
            if (post.scenario == currentTop && currentTop == scenario) {
                scenarioPostCount++;

                if (scenarioPostCount == 2) {
                    return new ScenarioLockState(stateMachine, scenario);
                }
            }
            else {
                scenarioPostCount = 0;
            }

            return null;
        }
    }

    public class ScenarioLockState : ScenarioBaseState {
        public ScenarioLockState(GameStateManager stateMachine, Scenario scenario) : base(stateMachine, scenario) { }

        public override GameState CheckTransition(Post post) {
            // Go to ending when player posts about the topic once more
            if (post.scenario == scenario) {
                return new EndState(stateMachine, scenario);
            }
            
            return null;
        }
    }

    public class EndState : ScenarioBaseState {
        public EndState(GameStateManager stateMachine, Scenario scenario) : base(stateMachine, scenario) { }

        public override GameState CheckTransition(Post post) {
            return null;
        }
    }
}
