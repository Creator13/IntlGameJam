using UnityEngine;
using UnityEngine.UI;

namespace cvanbattum.Audio {
    [RequireComponent(typeof(Button))]
    public class ButtonSoundEffect : SoundEffectPlayer {
        private Button button;
        
        private void Awake() {
            button = GetComponent<Button>();
            
            button.onClick.AddListener(PlaySound);
        }
    }
}
