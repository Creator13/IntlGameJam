using System.Collections.Generic;
using System.Linq;
using Simfluencer.Model;
using UnityEngine;

namespace Simfluencer.UI {
    public class PostHistoryPanel : MonoBehaviour {
        [SerializeField] private PostPanel postPrefab;

        private Stack<ProcessedPost> posts;

        public void SetPosts(Stack<ProcessedPost> posts) {
            this.posts = new Stack<ProcessedPost>(posts.Reverse());

            foreach (var post in this.posts) {
                var panel = Instantiate(postPrefab, transform, false);
                panel.ProcessedPost = post;
            }
        }

        public void Clear() {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
