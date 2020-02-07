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

        private void Awake() {
            instance = this;

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            fxSource = gameObject.AddComponent<AudioSource>();
            fxSource.playOnAwake = false;
        }

        public void PlayMusic() {
            if (!musicSource.clip) musicSource.clip = music;
            musicSource.loop = true;
            
            musicSource.Play();
        }

        public void StopMusic() {
            musicSource.Pause();
        }
    }
}
