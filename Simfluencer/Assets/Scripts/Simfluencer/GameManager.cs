using UnityEngine;

namespace Simfluencer {
    public interface IGameManager {
        PlayerInfo PlayerInfo { get; }
        PostHistory PostHistory { get; }
    }

    public class GameManager : MonoBehaviour, IGameManager {
        public static IGameManager Instance { get; private set; }

        public PlayerInfo PlayerInfo { get; private set; }
        public PostHistory PostHistory { get; private set; }

        [SerializeField] private int startFollowers = 48629;
        [SerializeField, Range(0, 1)] private float startCredibility = 58f;
        //TODO test code plz remove
        [SerializeField] private Profile testProfile;

        private void Awake() {
            // SettingTools.FitTargetResolution();
            PlayerInfo = new PlayerInfo(startFollowers, startCredibility);
            PostHistory = new PostHistory();
            Instance = this;
            
            //TODO test code please remove
            for (var i = 0; i < 15; i++) {
                PostHistory.AddPost(new Post(testProfile, $"Lorem ipsum dolor sit amet post {i}"));
            }
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
