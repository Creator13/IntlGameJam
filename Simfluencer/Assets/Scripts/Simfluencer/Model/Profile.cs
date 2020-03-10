using UnityEngine;

namespace Simfluencer.Model {
    [CreateAssetMenu(fileName = "NewUserProfile", menuName = "Simfluencer/User Profile", order = 0)]
    public class Profile : ScriptableObject {
        [SerializeField] private string username;
        [SerializeField] private Sprite picture;

        public string Username {
            get => username;
            set => username = value;
        }

        public Sprite Picture {
            get => picture;
            set => picture = value;
        }
    }
}
