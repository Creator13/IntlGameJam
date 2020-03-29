using cvanbattum.Audio;
using Simfluencer.Model;
using UnityEngine;

namespace Simfluencer.UI.Screens {
    public class MainScreen : Screen {
        [SerializeField] private PostHistoryPanel feed;

        // TODO move this somewhere it belongs. state check is not a property of this screen.
        private bool transitionToEnd;

        protected override void Awake() {
            base.Awake();
            // TODO move this somewhere it belongs. state check is not a task for this class.
            GameManager.Instance.GameStateManager.StateChanged += CheckGameStateChange;
        }
        
        protected override void Show() {
            // TODO move this somewhere it belongs. state check is not a task for this class.
            if (transitionToEnd) {
                uiManager.TransitionToScreen("Ending");
                return;
            }
            
            SoundManager.Instance.PlayMusic();

            if (!feed) feed = GetComponentInChildren<PostHistoryPanel>(true);

            feed.Clear();
            var posts = GameManager.Instance.GameStateManager.PostHistory;
            feed.SetPosts(posts);
        }

        private void CheckGameStateChange(GameState state) {
            if (state is EndState) {
                transitionToEnd = true;
            }
        }
    }
}
