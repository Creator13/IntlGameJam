namespace Simfluencer.Model {
    public class ProcessedPost {
        public Profile Profile { get; }
        public Post Post { get; }
        
        public ProcessedPost(Post post, Profile profile) {
            Profile = profile;
            Post = post;
        }
    }
}
