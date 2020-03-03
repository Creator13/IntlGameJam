using System.Linq;

namespace Simfluencer.UI.Screens {
    public class FeedScreen : Screen {
        private PostHistoryPanel feed;
        
        protected override void Show() {
            feed = GetComponentInChildren<PostHistoryPanel>();
            
            feed.Clear();

            var posts = GameManager.Instance.GameStateManager.PostHistory;
            feed.SetPosts(posts);
        }
    }
}
