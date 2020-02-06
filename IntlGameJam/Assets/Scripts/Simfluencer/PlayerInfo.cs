using System;

namespace Simfluencer {
    public class PlayerInfo {
        public event Action<int> FollowersChanged;
        public event Action<float> CredibilityChanged;

        private int followers;
        private float credibility;

        public int Followers {
            get => followers;
            set {
                var oldValue = followers;
                followers = value;
                FollowersChanged?.Invoke(value - oldValue);
            }
        }
        
        public float Credibility {
            get => credibility;
            set {
                credibility = value;
                CredibilityChanged?.Invoke(value);
            }
        }

        public PlayerInfo(int followersStart, float credibilityStart) {
            followers = followersStart;
            credibility = credibilityStart;
        }
    }
}
