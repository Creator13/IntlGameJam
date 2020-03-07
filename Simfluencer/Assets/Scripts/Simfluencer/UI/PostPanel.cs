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
            var profile = processedPost.Profile;
            if (profile == null) {
                profile = ScriptableObject.CreateInstance<Profile>();
                profile.Username = "<null>";
            }
            
            username.text = profile.Username;
            picture.sprite = profile.Picture;
            content.text = processedPost.Post.Content;
        }
    }
}
