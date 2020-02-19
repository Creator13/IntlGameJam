using System.Collections.Generic;

namespace Simfluencer {
    public class PostHistory {
        public Stack<Post> Posts { get; }

        public PostHistory() {
            Posts = new Stack<Post>();
        }

        public void AddPost(Post post) {
            Posts.Push(post);
        }
    }
}
