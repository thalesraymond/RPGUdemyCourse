using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private float soundEffectMinimumDistance;
        
        [SerializeField] private AudioSource[] soundEffects;

        [SerializeField] private AudioSource[] backgroundMusic;

        [SerializeField] public bool shouldPlayBackgroundMusic;
        
        [SerializeField] private int currentBackgroundMusicIndex = 0;

        private bool _canPlaySoundEffects = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(Instance.gameObject);
            else
                Instance = this;
            
            Invoke(nameof(this.AllowSoundEffects), 1f);
        }

        private void Update()
        {
            if (!this.shouldPlayBackgroundMusic)
            {
                this.StopAllBackgroundMusic();
                return;
            }
            
            if(this.backgroundMusic[currentBackgroundMusicIndex].isPlaying)
                return;
            
            this.PlayRandomBackgroundMusic();
        }

        /// <summary>
        /// Plays the specified sound effect, optionally filtering by source.
        /// </summary>
        /// <param name="effect">The sound effect to play.</param>
        /// <param name="source">The source of the sound effect. If the distance
        /// between the player and the source is greater than
        /// <see cref="soundEffectMinimumDistance"/>, the sound effect will not
        /// be played. If the source is null, the sound effect will always be
        /// played.</param>
        public void PlaySoundEffect(SoundEffect effect, Transform source = null, bool overlap = false)
        {
            if (!this._canPlaySoundEffects) return;
            
            if (this.soundEffects[(int)effect].isPlaying && !overlap) return;
            
            if ((int)effect >= this.soundEffects.Length) return;

            var playerPosition = PlayerManager.Instance.Player.transform.position;
            if(source && Vector2.Distance(playerPosition, source.position) > this.soundEffectMinimumDistance) return;
            
            this.soundEffects[(int)effect].pitch = UnityEngine.Random.Range(0.8f, 1.1f);
            
            this.soundEffects[(int)effect].Play();
        }


        /// <summary>
        /// Stops the specified sound effect if it is playing.
        /// </summary>
        /// <param name="effect">The sound effect to stop.</param>
        /// <remarks>
        /// If the sound effect is not playing, this method does nothing.
        /// </remarks>
        public void StopSoundEffect(SoundEffect effect)
        {
            if ((int)effect >= this.soundEffects.Length) return;

            this.soundEffects[(int)effect].Stop();
        }

        public void GraduallyStopSoundEffect(SoundEffect effect)
        {   
            if ((int)effect >= this.soundEffects.Length) return;
            
            StartCoroutine(DecreaseVolumeCoroutine(this.soundEffects[(int)effect]));
        }

        IEnumerator DecreaseVolumeCoroutine(AudioSource audio)
        {
            var defaultVolume = audio.volume;

            while (audio.volume > .1f)
            {
                audio.volume -= audio.volume * .2f;
                
                yield return new WaitForSeconds(.25f);
            }
            
            audio.Stop();
            audio.volume = defaultVolume;
        }

        /// <summary>
        /// Plays the background music at the specified index.
        /// </summary>
        /// <param name="index">The index of the background music to play.</param>
        /// <remarks>
        /// If the specified index is out of range, this method does nothing.
        /// This method will stop any currently playing background music.
        /// </remarks>
        public void PlayBackgroundMusic(int index)
        {
            if (index >= this.backgroundMusic.Length) return;

            this.StopAllBackgroundMusic();

            this.backgroundMusic[index].Play();

            this.currentBackgroundMusicIndex = index;
        }

        /// <summary>
        /// Stops all background music.
        /// </summary>
        /// <remarks>
        /// This method iterates over the array of background music
        /// and calls <see cref="AudioSource.Stop"/> on each one.
        /// </remarks>
        public void StopAllBackgroundMusic()
        {
            foreach (var audioSource in this.backgroundMusic)
                audioSource.Stop();
        }

        /// <summary>
        /// Plays a random background music from the backgroundMusic array.
        /// </summary>
        /// <remarks>
        /// This method selects a random index from the backgroundMusic array and calls
        /// <see cref="PlayBackgroundMusic"/> with that index.
        /// </remarks>
        public void PlayRandomBackgroundMusic()
        {    
            this.currentBackgroundMusicIndex = UnityEngine.Random.Range(0, this.backgroundMusic.Length);
            
            this.PlayBackgroundMusic(this.currentBackgroundMusicIndex);
        }
        
        /// <summary>
        /// Allows sound effects to be played.
        /// </summary>
        /// <remarks>
        /// This method is used by the <see cref="AudioManager"/> to control whether sound effects are allowed to be played.
        /// </remarks>
        private void AllowSoundEffects() => this._canPlaySoundEffects = true;
    }
}