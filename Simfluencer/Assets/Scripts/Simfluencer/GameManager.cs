using Simfluencer.UI;
using UnityEngine;

namespace Simfluencer {
    public interface IGameManager {
        PlayerInfo PlayerInfo { get; }
    }

    public class GameManager : MonoBehaviour, IGameManager {
        public static IGameManager Instance { get; private set; }

        public PlayerInfo PlayerInfo { get; private set; }

        [SerializeField] private int startFollowers = 48629;
        [SerializeField, Range(0, 1)] private float startCredibility = 58f;

        private void Awake() {
            // SettingTools.FitTargetResolution();
            PlayerInfo = new PlayerInfo(startFollowers, startCredibility);
            Instance = this;
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
