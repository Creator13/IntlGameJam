using System;
using UnityEngine;

namespace Simfluencer {
    public enum Scenario {
        Neutral, Science, Conspiracy
    }
    
    public class PlayerInfo {
        public event Action<int> FollowersChanged;
        public event Action<float> CredibilityChanged;
        public event Action<Scenario> ScenerioTriggered;

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
                credibility = Mathf.Clamp(value, 0, 1);
                CheckCredibility();
                CredibilityChanged?.Invoke(value);
            }
        }

        public PlayerInfo(int followersStart, float credibilityStart) {
            followers = followersStart;
            credibility = credibilityStart;
        }

        private void CheckCredibility() {
            if (credibility < .4) {
                ScenerioTriggered?.Invoke(Scenario.Conspiracy);
            }
            else if (credibility > .8) {
                ScenerioTriggered?.Invoke(Scenario.Science);
            }
            else {
                ScenerioTriggered?.Invoke(Scenario.Neutral);
            }
        }
    }
}
