using Simfluencer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    public class PostPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI username;
        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private Image picture;
        
        public ProcessedPost ProcessedPost {
            set => SetPostValues(value);
        }
        
        private void SetPostValues(ProcessedPost processedPost) {
            username.text = processedPost.Profile.Username;
            content.text = processedPost.Post.Content;
            picture.sprite = processedPost.Profile.Picture;
        }
    }
}
