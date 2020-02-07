using System;
using Simfluencer.FSM;
using UnityEngine;

namespace Simfluencer {
    public interface IGameManager {
        PlayerInfo PlayerInfo { get; }
    }

    public class GameManager : MonoBehaviour, IGameManager {
        public static IGameManager Instance { get; private set; }

        public PlayerInfo PlayerInfo { get; private set; }

        [SerializeField] private int startFollowers;
        [SerializeField] private float startCredibility;

        private void Awake() {
            SettingTools.FitTargetResolution();
            PlayerInfo = new PlayerInfo(48629, .58f);
            Instance = this;
        }

        private void Start() { }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
