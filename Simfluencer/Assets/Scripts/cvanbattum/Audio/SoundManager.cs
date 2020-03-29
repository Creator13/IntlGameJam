using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace cvanbattum.Audio {
    public interface ISoundManager {
        AudioClip Music { get; set; }
        AudioClip OverrideMusic { get; set; }

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
        public AudioClip Music {
            get => music;
            set => music = value;
        }

        private AudioClip overrideMusic;
        public AudioClip OverrideMusic {
            get => overrideMusic;
            set {
                overrideMusic = value;
                
                var time = musicSource.time;
                musicSource.clip = overrideMusic ? overrideMusic : music;
                musicSource.time = time;
            }
        }

        private void Awake() {
            Instance = this;

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
            if (!musicSource.clip) {
                musicSource.clip = overrideMusic ? overrideMusic : music;
            }
            musicSource.loop = true;

            if (!musicSource.isPlaying) musicSource.Play();
        }

        public void StopMusic() {
            musicSource.Pause();
        }

        public void PlayEffect(string name) {
            if (enableMusic) {
                fxSource.PlayOneShot(EffectClips.Find(clip => clip.name == name));
            }
        }

        private static List<AudioClip> LoadEffectClips() {
            return Resources.LoadAll<AudioClip>("Sound/Effects").ToList();
        }
    }
}
