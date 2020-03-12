using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(ToggleGroup))]
    public class ProfilePictureChooser : MonoBehaviour {
        private ToggleGroup toggleGroup;
        
        private void Awake() {
            toggleGroup = GetComponent<ToggleGroup>();
        }

        public Sprite GetSelected() {
            var toggle = toggleGroup.ActiveToggles().First();
            var profilePicture = toggle.GetComponent<ProfileToggle>();
            return profilePicture.Sprite;
        }
    }
}
