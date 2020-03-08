using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace cvanbattum.Audio {
    public interface ISoundManager {
        void PlayMusic();

        void StopMusic();

        void PlayEffect(string name);
    }

    public class SoundManager : MonoBehaviour, ISoundManager {
        public static ISoundManager Instance { get; private set; }

        public static List<AudioClip> EffectClips => LoadEffectClips();

        private AudioSource musicSource;
        private AudioSource fxSource;

        [SerializeField] private bool enableMusic = true;
        [SerializeField] private bool enableFx = true;
        
        [SerializeField] private AudioClip music;
        [SerializeField] private AudioClip posMusic;
        [SerializeField] private AudioClip negMusic;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }

            GetComponents<AudioSource>().ToList().ForEach(Destroy);
            
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.enabled = enableMusic;

            fxSource = gameObject.AddComponent<AudioSource>();
            fxSource.playOnAwake = false;
            fxSource.enabled = enableFx;
        }

        private void OnValidate() {
            if (musicSource) musicSource.enabled = enableMusic;
            if (fxSource) fxSource.enabled = enableFx;
        }

        public void PlayMusic() {
            if (!musicSource.clip) SwitchNeutral();
            musicSource.loop = true;

            if (!musicSource.isPlaying) musicSource.Play();
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

        public void PlayEffect(string name) {
            fxSource.PlayOneShot(EffectClips.Find(clip => clip.name == name));
        }

        private static List<AudioClip> LoadEffectClips() {
            return Resources.LoadAll<AudioClip>("Sound/Effects").ToList();
        }
    }
}
