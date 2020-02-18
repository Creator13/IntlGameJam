using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    public class PostPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI username;
        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private Image picture;
        
        public Post Post {
            set => SetPostValues(value);
        }
        
        private void SetPostValues(Post post) {
            username.text = post.Profile.Username;
            content.text = post.Content;
            picture.sprite = post.Profile.Picture;
        }
    }
}
