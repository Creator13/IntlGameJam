using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Simfluencer.UI {
    public class PostHistoryPanel : MonoBehaviour {
        [SerializeField] private PostPanel postPrefab;

        private Stack<Post> posts;

        public void SetPosts(Stack<Post> posts) {
            this.posts = new Stack<Post>(posts.Reverse());

            foreach (var post in this.posts) {
                var panel = Instantiate(postPrefab, transform, false);
                panel.Post = post;
            }
        }

        public void Clear() {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
