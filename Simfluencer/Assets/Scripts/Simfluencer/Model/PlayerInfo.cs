using System;
using UnityEngine;

namespace Simfluencer {
    [Serializable]
    public class PlayerInfo {
        [field: NonSerialized] public event Action<int> FollowersChanged;

        private int followers;

        public int Followers {
            get => followers;
            set {
                var oldValue = followers;
                followers = value;
                FollowersChanged?.Invoke(value - oldValue);
            }
        }

        public Profile Profile { get; private set; }

        public string Name {
            set {
                if (!Profile) Profile = ScriptableObject.CreateInstance<Profile>();

                if (value == Profile.Username) return;

                Profile.Username = value;
            }
        }

        public PlayerInfo(int followersStart) {
            followers = followersStart;
        }
    }
}
