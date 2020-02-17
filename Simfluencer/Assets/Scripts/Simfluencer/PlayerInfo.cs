using System;
using UnityEngine;

namespace Simfluencer {
    public enum Scenario {
        Neutral, Science, Conspiracy
    }
    
    public class PlayerInfo {
        public event Action<int> FollowersChanged;
        public event Action<float> CredibilityChanged;
        public event Action<Scenario> ScenarioTriggered;

        private int followers;
        private float credibility;
        private string name;

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
                CredibilityChanged?.Invoke(credibility);
            }
        }

        public string Name {
            get => name;
            set {
                name = value;
                Debug.Log($"New name: {name}");
            }
        }

        public PlayerInfo(int followersStart, float credibilityStart) {
            followers = followersStart;
            credibility = credibilityStart;
        }

        private void CheckCredibility() {
            if (credibility < .4) {
                ScenarioTriggered?.Invoke(Scenario.Conspiracy);
            }
            else if (credibility > .8) {
                ScenarioTriggered?.Invoke(Scenario.Science);
            }
            else {
                ScenarioTriggered?.Invoke(Scenario.Neutral);
            }
        }
    }
}
