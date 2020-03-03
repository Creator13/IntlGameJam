namespace Simfluencer.Model {
    public abstract class GameState {
    }

    public class FreeState : GameState { }

    public class ScenarioState : GameState { }

    public class ScenarioLockState : GameState { }
}
