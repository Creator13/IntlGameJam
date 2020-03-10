using cvanbattum.Audio;
using UnityEngine;

namespace Simfluencer.UI.Screens {
    public class MainScreen : Screen {
        [SerializeField] private PostHistoryPanel feed;
        protected override void Show() {
            SoundManager.Instance.PlayMusic();

            if (!feed) feed = GetComponentInChildren<PostHistoryPanel>(true);

            feed.Clear();
            var posts = GameManager.Instance.GameStateManager.PostHistory;
            feed.SetPosts(posts);
        }
    }
}
