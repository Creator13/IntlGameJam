namespace Simfluencer.Model {
    public abstract class GameState {
        private GameStateManager manager;
        
        protected GameState(GameStateManager manager) {
            this.manager = manager;
        }
    }

    public class FreeState : GameState {
        public FreeState(GameStateManager manager) : base(manager) { }
    }

    public class ScenarioState : GameState {
        public ScenarioState(GameStateManager manager) : base(manager) { }
    }

    public class ScenarioLockState : GameState {
        public ScenarioLockState(GameStateManager manager) : base(manager) { }
    }
}
