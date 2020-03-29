using System.Collections.Generic;
using System.Linq;
using Simfluencer.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Simfluencer {
    public interface IGameManager {
        PlayerInfo PlayerInfo { get; }
        GameStateManager GameStateManager { get; }
        GameSettings GameSettings { get; }
        PostPool PostPool { get; }
    }

    [System.Serializable]
    public struct ScenarioSettings {
        [SerializeField, Range(-1, 1)] private float positivityImpact;
        [SerializeField, Range(-.5f, .5f)] private float credibilityImpact;
        [SerializeField, Range(-1, 1)] private float followerChangeMultiplier;

        public float PositivityImpact => positivityImpact;
        public float CredibilityImpact => credibilityImpact;
        public float FollowerChangeMultiplier => followerChangeMultiplier;
    }

    [System.Serializable]
    public class GameSettings {
        [SerializeField] internal int startFollowers = 48629;
        [SerializeField, Range(0, 1)] internal float startCredibility = .58f;
        [SerializeField, Range(-1, 1)] internal float startPositivity = 0f;
        [SerializeField, Range(0, 1)] internal float basePostImpact = .2f;

        [SerializeField] internal ScenarioSettings[] scenarioSettings;
    }

    public class GameManager : MonoBehaviour, IGameManager {
        public static IGameManager Instance { get; private set; }

        public PlayerInfo PlayerInfo { get; private set; }
        public GameStateManager GameStateManager { get; private set; }
        public PostPool PostPool { get; private set; }

        [SerializeField] private GameSettings settings;

        public GameSettings GameSettings {
            get => settings;
            private set => settings = value;
        }

        public int CurrentRound => GameStateManager.PostHistory.Count;

        [SerializeField] private List<Scenario> scenarios;
        [SerializeField] private List<Post> neutralPosts;

        private void Awake() {
            SettingTools.FitTargetResolution();
            Assert.IsNull(Instance);

            // FIXME list shouldn't contain null elements in the first place
            scenarios.RemoveAll(elem => elem == null);

            scenarios.ForEach(s => s.Init());

            // Remove duplicates from post list (also enforced in object's inspector, but it never hurts to validate on
            // the back-end amirite?)
            neutralPosts = neutralPosts.Distinct().ToList();

            PlayerInfo = new PlayerInfo(settings.startFollowers);
            GameStateManager = new GameStateManager(scenarios, settings.startCredibility, settings.startPositivity);
            PostPool = new PostPool(GameStateManager, neutralPosts);

            Instance = this;

            // TODO temporary test code
            GameStateManager.StateChanged += LogStateChange;
        }

        private void LogStateChange(GameState state) {
            string message;
            switch (state) {
                case ScenarioState sState:
                    message = $"Switched to {state.GetType()}-{sState.scenario.ScenarioName}";
                    break;
                case ScenarioLockState slState:
                    message = $"Switched to {state.GetType()}-{slState.scenario.ScenarioName}";
                    break;
                default:
                    message = $"Switched to {state.GetType()}";
                    break;
            }

            Debug.Log(message);
        }

        public static void QuitGame() {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            // Do nothing
#else
            Application.Quit();
#endif
        }
    }
}
