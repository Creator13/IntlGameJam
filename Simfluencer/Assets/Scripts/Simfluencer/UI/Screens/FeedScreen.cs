namespace Simfluencer.UI.Screens {
    public class FeedScreen : Screen {
        private PostHistoryPanel feed;
        
        protected override void Show() {
            feed = GetComponentInChildren<PostHistoryPanel>();
            
            feed.Clear();
            feed.SetPosts(GameManager.Instance.PostHistory.Posts);
        }
    }
}
