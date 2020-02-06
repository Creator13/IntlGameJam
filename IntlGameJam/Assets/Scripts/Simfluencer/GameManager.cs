using System;
using Simfluencer.FSM;
using UnityEngine;

namespace Simfluencer {
    public interface IGameManager {
        PlayerInfo PlayerInfo { get; }
    }

    public class GameManager : MonoBehaviour, IGameManager {
        private static IGameManager instance;
        public static IGameManager Instance => instance;

        public PlayerInfo PlayerInfo { get; private set; }

        [SerializeField] private int startFollowers;
        [SerializeField] private float startCredibility;

        private void Awake() {
            PlayerInfo = new PlayerInfo();
            instance = this;
        }

        private void Start() { }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                PlayerInfo.Followers += 4000;
            }
        }
    }
}
