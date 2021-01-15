using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using Utilities.Data;
using Utilities.Inspector;
using DG.Tweening;

namespace Utilities.Audio
{
    public partial class AudioManager : MonoBehaviour
    {
        #region FIELDS

        private const float CooldownForSounds = 0.05f;
        private const string AudioPreferencesKey = "audio_preferences";
        public const float MusicMaxVolume = 0.65f;
        public const float SoundMaxVolume = 1f;
        public const float UnmutedMaxVolume = 1f;
        private const float FadeOutDuration = 2f;

        private static readonly string[] MusicVolumeKeys = { AudioPreferencesKey, "music_volume" };
        private static readonly string[] SoundVolumeKeys = { AudioPreferencesKey, "sound_volume" };
        private static readonly string[] MusicMuteKeys = { AudioPreferencesKey, "music_mute" };
        private static readonly string[] SoundMuteKeys = { AudioPreferencesKey, "sound_mute" };

        [Inject] private DataManager dataManager = null;

        [Header("VOLUMES")]
        [ReadOnly] [SerializeField] private float musicVolume = MusicMaxVolume;
        [ReadOnly] [SerializeField] private float soundVolume = SoundMaxVolume;

        private AudioSource musicChannel = null;
        private AudioSource musicChannelCrossFadeHelper = null;
        private AudioSource soundChannel = null;
        private AudioSource unmutedChannel = null;
        private Dictionary<int, AudioSource> persistentSoundsChannels = new Dictionary<int, AudioSource>();
        private List<AudioClip> currentSoundClips = new List<AudioClip>();
        private int persistentCounter = -1;

        #endregion

        #region PROPERTIES

        public bool MusicMuted { get => musicChannel.mute; }
        public float MusicVolume { get => musicVolume; }
        public bool IsMusicPlaying { get => musicChannel.isPlaying; }
        public bool SoundMuted { get => soundChannel.mute; }
        public float SoundVolume { get => soundVolume; }
        public float UnmutedClipLength { get => unmutedChannel.clip == null ? 0f : unmutedChannel.clip.length; }
        public bool IsUnmutedPlaying { get => unmutedChannel.isPlaying; }

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            musicChannel = gameObject.AddComponent<AudioSource>();
            musicChannelCrossFadeHelper = gameObject.AddComponent<AudioSource>();
            soundChannel = gameObject.AddComponent<AudioSource>();
            unmutedChannel = gameObject.AddComponent<AudioSource>();

            LoadVolume();
            LoadMute();
        }

        private void LoadVolume()
        {
            musicChannel.volume = musicVolume = dataManager.GetData<float>(MusicVolumeKeys, MusicMaxVolume);
            soundChannel.volume = soundVolume = dataManager.GetData<float>(SoundVolumeKeys, SoundMaxVolume);
            unmutedChannel.volume = UnmutedMaxVolume;
        }

        public void SetMusicVolume(float volume)
        {
            musicChannel.volume = musicVolume = Mathf.Lerp(0, MusicMaxVolume, volume);
            dataManager.SetData(MusicVolumeKeys, musicVolume);
        }

        public void SetSoundVolume(float volume)
        {
            soundChannel.volume = soundVolume = Mathf.Lerp(0, SoundMaxVolume, volume);
            dataManager.SetData(SoundVolumeKeys, soundVolume);
        }

        private void LoadMute()
        {
            musicChannel.mute = dataManager.GetData<bool>(MusicMuteKeys, false);
            soundChannel.mute = dataManager.GetData<bool>(SoundMuteKeys, false);
        }

        public void ToggleMusicMute()
        {
            musicChannel.mute = !musicChannel.mute;
            dataManager.SetData(MusicMuteKeys, musicChannel.mute);
        }

        public void ToggleSoundMute()
        {
            soundChannel.mute = !soundChannel.mute;
            dataManager.SetData(SoundMuteKeys, soundChannel.mute);
        }

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            StopMusic();
            musicChannel.clip = clip;
            musicChannel.loop = loop;
            musicChannel.Play();
        }

        public void PlayUnmuted(AudioClip clip)
        {
            StopUnmuted();
            unmutedChannel.clip = clip;
            unmutedChannel.Play();
        }

        public void CrossFadeMusic(AudioClip clip, bool loop = true, float duration = FadeOutDuration)
        {
            if (musicChannel.isPlaying)
            {
                musicChannelCrossFadeHelper.clip = musicChannel.clip;
                musicChannelCrossFadeHelper.time = musicChannel.time;
                musicChannelCrossFadeHelper.volume = musicChannel.volume;
                musicChannelCrossFadeHelper.loop = musicChannel.loop;
                musicChannelCrossFadeHelper.Play();
                musicChannelCrossFadeHelper.DOFade(default(int), duration).OnComplete(() => musicChannelCrossFadeHelper.Stop());
            }

            PlayMusic(clip, loop);
            musicChannel.volume = 0;
            musicChannel.DOFade(MusicMaxVolume, duration);
        }

        public void StopMusic()
        {
            musicChannel.clip = null;
            musicChannel.Stop();
        }

        public void StopUnmuted()
        {
            unmutedChannel.clip = null;
            unmutedChannel.Stop();
        }

        public void PlaySound(AudioClip clip)
        {
            if (clip == null || currentSoundClips.Contains(clip))
                return;

            soundChannel.PlayOneShot(clip);
            currentSoundClips.Add(clip);

            StartCoroutine(SoundCooldown(clip, CooldownForSounds));
        }

        public IEnumerator SoundCooldown(AudioClip clip, float cooldown)
        {
            yield return new WaitForSecondsRealtime(cooldown);
            currentSoundClips.Remove(clip);
        }

        public int PlayPersistentSound(AudioClip clip, float duration = 0f)
        {
            AudioSource persistentSoundChannel = gameObject.AddComponent<AudioSource>();
            persistentSoundChannel.volume = soundChannel.volume;
            persistentSoundChannel.mute = soundChannel.mute;
            persistentSoundChannel.loop = true;
            persistentSoundChannel.clip = clip;
            persistentSoundChannel.Play();

            persistentCounter++;
            persistentSoundsChannels.Add(persistentCounter, persistentSoundChannel);

            if (duration > 0)
                StartCoroutine(StopPersistentSoundCoroutine(duration, persistentCounter));

            return persistentCounter;
        }

        public void StopPersistentSound(int persistentSoundId)
        {
            if (!PersistentSoundExists(persistentSoundId))
                return;

            persistentSoundsChannels[persistentSoundId].Stop();
            Destroy(persistentSoundsChannels[persistentSoundId]);
            persistentSoundsChannels.Remove(persistentSoundId);
        }

        public void StopAllPersistentSounds()
        {
            foreach (int id in persistentSoundsChannels.Keys.ToList())
                StopPersistentSound(id);

            persistentSoundsChannels.Clear();
        }

        private IEnumerator StopPersistentSoundCoroutine(float duration, int id)
        {
            yield return new WaitForSeconds(duration);
            StopPersistentSound(id);
        }

        private bool PersistentSoundExists(int persistentSoundId)
        {
            return persistentSoundsChannels.ContainsKey(persistentSoundId);
        }

        #endregion
    }
}
