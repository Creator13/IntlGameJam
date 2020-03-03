using System.Collections.Generic;
using Simfluencer.Model;
using UnityEngine;
using UnityEngine.Assertions;

namespace Simfluencer {
    public interface IGameManager {
        PlayerInfo PlayerInfo { get; }
        GameStateManager GameStateManager { get; }
        GameSettings GameSettings { get; }
    }

    [System.Serializable]
    public class GameSettings {
        [SerializeField] internal int startFollowers = 48629;
        [SerializeField, Range(0, 1)] internal float startCredibility = .58f;
        [SerializeField, Range(0, 1)] internal float basePostImpact = .2f;
    }

    public class GameManager : MonoBehaviour, IGameManager {
        public static IGameManager Instance { get; private set; }

        public PlayerInfo PlayerInfo { get; private set; }
        public GameStateManager GameStateManager { get; private set; }
        public PostPool PostPool { get; private set; }

        [SerializeField] private GameSettings gameSettings;

        public GameSettings GameSettings {
            get => gameSettings;
            private set => gameSettings = value;
        }

        public int CurrentRound => GameStateManager.PostHistory.Count;

        [SerializeField] private List<Model.Scenario> scenarios;

        private void Awake() {
            // SettingTools.FitTargetResolution();
            Assert.IsNotNull(Instance);

            PlayerInfo = new PlayerInfo(gameSettings.startFollowers, gameSettings.startCredibility);
            GameStateManager = new GameStateManager(scenarios);
            // PostPool = new PostPool();

            Instance = this;
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
