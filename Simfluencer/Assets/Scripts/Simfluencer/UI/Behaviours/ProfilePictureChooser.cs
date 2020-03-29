using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Simfluencer.UI {
    [RequireComponent(typeof(ToggleGroup))]
    public class ProfilePictureChooser : MonoBehaviour {
        private ToggleGroup toggleGroup;

        public Sprite GetSelected() {
            toggleGroup = GetComponent<ToggleGroup>();
            var toggle = toggleGroup.ActiveToggles().FirstOrDefault();
            var profilePicture = toggle.GetComponent<ProfileToggle>();
            return profilePicture.Sprite;
        }
    }
}
