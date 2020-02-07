using System;
using UnityEngine;

namespace Simfluencer {
    public interface ISoundManager {
        void PlayMusic();
    }
    
    public class SoundManager : MonoBehaviour, ISoundManager {
        private static ISoundManager instance;
        public static ISoundManager Instance => instance;

        private AudioSource musicSource;
        private AudioSource fxSource;
        [SerializeField] private AudioClip music;
        [SerializeField] private AudioClip posMusic;
        [SerializeField] private AudioClip negMusic;

        private void Awake() {
            instance = this;

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            fxSource = gameObject.AddComponent<AudioSource>();
            fxSource.playOnAwake = false;
        }

        public void PlayMusic() {
            if (!musicSource.clip) SwitchNeutral();
            musicSource.loop = true;
            
            musicSource.Play();
        }

        public void SwitchNeutral() {
            musicSource.clip = music;
        }

        public void SwitchPositive() {
            musicSource.clip = posMusic;
        }

        public void SwitchNegative() {
            musicSource.clip = negMusic;
        }
        
        
        public void StopMusic() {
            musicSource.Pause();
        }
    }
}
