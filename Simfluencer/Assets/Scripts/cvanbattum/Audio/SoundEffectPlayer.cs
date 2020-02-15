using UnityEngine;

namespace cvanbattum.Audio {
    public class SoundEffectPlayer : MonoBehaviour {
        [SerializeField] protected string soundName;

        protected void PlaySound() {
            SoundManager.Instance.PlayEffect(soundName);
        }
    }
}
