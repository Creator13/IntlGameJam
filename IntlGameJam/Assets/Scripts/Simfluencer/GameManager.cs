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
            PlayerInfo = new PlayerInfo(1000, .62f);
            Instance = this;
        }

        private void Start() { }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }
    }
}
