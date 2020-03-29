using UnityEngine;

namespace cvanbattum.Audio {
    public class SoundEffectPlayer : MonoBehaviour {
        [SerializeField] protected string soundName;

        public void PlaySound() {
            SoundManager.Instance.PlayEffect(soundName);
        }
    }
}
