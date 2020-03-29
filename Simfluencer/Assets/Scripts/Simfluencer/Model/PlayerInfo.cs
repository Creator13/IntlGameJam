using System;
using Simfluencer.Model;
using UnityEngine;

namespace Simfluencer {
    public class PlayerInfo {
        public event Action<int> FollowersChanged;

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

        public Sprite Avatar {
            set {
                if (!Profile) Profile = ScriptableObject.CreateInstance<Profile>();

                if (value == Profile.Picture) return;

                Profile.Picture = value;
            }
        }

        public PlayerInfo(int followersStart) {
            followers = followersStart;
        }
    }
}
