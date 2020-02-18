using UnityEngine;

namespace Simfluencer {
    [CreateAssetMenu(fileName = "NewUserProfile", menuName = "Simfluencer/User Profile", order = 0)]
    public class Profile : ScriptableObject {
        [SerializeField] private string username;
        [SerializeField] private Sprite picture;

        public string Username => username;
        public Sprite Picture => picture;
    }
}
