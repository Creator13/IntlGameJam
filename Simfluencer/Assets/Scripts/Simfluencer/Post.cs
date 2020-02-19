namespace Simfluencer {
    public class Post {
        public Profile Profile { get; }
        public string Content { get; }
        
        public Post(Profile profile, string content) {
            Profile = profile;
            Content = content;
        }
    }
}
