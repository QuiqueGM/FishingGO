using UnityEngine;

namespace VFG.Core.Audio
{
	public enum Channel 
	{
		Music = 0, 
		Effects = 1
	}

    [RequireComponent(typeof(AudiosData))]
    public class AudioManager : MonoBehaviour
    {
        public bool initFade;
		public float initFadeDuration;
		[Space]
        public AudioSource[] audioSource;
        public static AudioManager Instance;

        private bool isFadeDown, isFadeUp;
        private float counter = 0;
        private float fadeDuration;
        private float finalVolume;
        private AudioClip newClip;
        private AudiosData audiosList;
        
        void Awake()
        {
            Instance = this;
            audiosList = GetComponent<AudiosData>();
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            InitFadeUp();
            FadeClip();
        }

        #region MUSIC
        public void PlayMusic()
        {
			audioSource[(int)Channel.Music].Play();
        }

        public void PlayMusic(string clip)
        {
            audioSource[(int)Channel.Music].clip = GetAudioClipFromDatabase(clip);
            audioSource[(int)Channel.Music].Play();
        }

        public void PlayMusic(AudioClip clip)
        {
            audioSource[(int)Channel.Music].clip = clip;
            audioSource[(int)Channel.Music].Play();
        }

        public void PlayMusic(AudioClip clip, int source)
		{
            audioSource[source].clip = clip;
            audioSource[source].Play();
        }

        public void PlayMusic(AudioClip clip, float volume)
        {
            audioSource[(int)Channel.Music].volume = volume;
            audioSource[(int)Channel.Music].clip = clip;
            audioSource[(int)Channel.Music].Play();
        }

        public void PlayMusic(AudioClip clip, int source, float volume)
        {
            audioSource[source].volume = volume;
            audioSource[source].clip = clip;
            audioSource[source].Play();
        }

        public void PlayMusic(int source)
        {
            audioSource[source].Play();
        }

        public void PlayMusic(float volume)
        {
            audioSource[(int)Channel.Music].volume = volume;
            audioSource[(int)Channel.Music].Play();
        }

        public void PlayMusic(int source, float volume)
        {
            audioSource[source].volume = volume;
            audioSource[source].Play();
        }

        public void ChangeMusicWithFade(string newClip, float fadeDuration, float finalVolume=1)
        {
            counter = audioSource[(int)Channel.Music].volume;
            this.fadeDuration = fadeDuration;
            this.newClip = GetAudioClipFromDatabase(newClip);
            this.finalVolume = finalVolume;

            isFadeDown = true;
        }

        private void ChangeClip(AudioClip clip)
        {
            audioSource[(int)Channel.Music].clip = clip;
            audioSource[(int)Channel.Music].Play();
        }

        private void FadeClip()
        {
            if (isFadeDown)
            {
                counter -= (Time.deltaTime / fadeDuration);
                audioSource[(int)Channel.Music].volume = counter;

                if (counter < 0)
                {
                    isFadeDown = false;
                    isFadeUp = true;
                    ChangeClip(newClip);
                }
            }

            if (isFadeUp)
            {
                counter += (Time.deltaTime / fadeDuration);
                audioSource[(int)Channel.Music].volume = counter;

                if (counter > finalVolume) isFadeUp = false;
            }
        }

        private void InitFadeUp(float initVolume=1.0f, float fadeDuration=1.0f)
        {
            if (!initFade) return;

            counter += (Time.deltaTime / initFadeDuration);
            audioSource[(int)Channel.Music].volume = counter;
            if (counter > initVolume) initFade = false;
        }

        public void StopMusic()
        {
            audioSource[(int)Channel.Music].Stop();
        }

        public void StopMusic(int source)
        {
            audioSource[source].Stop();
        }

        public void Mute(bool state)
        {
            audioSource[(int)Channel.Music].mute = state;
        }

        public void Mute(int source, bool state)
        {
            audioSource[source].mute = state;
        }

        #endregion

        #region EFFECT

        public void PlayEffect(AudioClip clip)
        {
            audioSource[(int)Channel.Effects].clip = clip;
            audioSource[(int)Channel.Effects].PlayOneShot(clip);
        }

        public void PlayEffect(AudioClip clip, int source)
        {
            audioSource[source].clip = clip;
            audioSource[source].PlayOneShot(clip);
        }

        public void PlayEffect(AudioClip clip, float volume)
        {
            audioSource[(int)Channel.Effects].volume = volume;
            audioSource[(int)Channel.Effects].clip = clip;
            audioSource[(int)Channel.Effects].PlayOneShot(clip);
        }

        public void PlayEffect(AudioClip clip, int source, float volume)
        {
            audioSource[source].volume = volume;
            audioSource[source].clip = clip;
            audioSource[source].PlayOneShot(clip);
        }

        public void PlayEffect(AudioClip clip, float volume, float pitch)
        {
            audioSource[(int)Channel.Effects].volume = volume;
            audioSource[(int)Channel.Effects].pitch = pitch;
            audioSource[(int)Channel.Effects].clip = clip;
            audioSource[(int)Channel.Effects].PlayOneShot(clip);
        }

        public void PlayEffect(AudioClip clip, int source, float volume, float pitch)
        {
            audioSource[source].volume = volume;
            audioSource[source].pitch = pitch;
            audioSource[source].clip = clip;
            audioSource[source].PlayOneShot(clip);
        }

        public void PlayEffect(string clip)
        {
            audioSource[(int)Channel.Effects].volume = 1;
            audioSource[(int)Channel.Effects].pitch = 1;
            audioSource[(int)Channel.Effects].clip = GetAudioClipFromDatabase(clip);
            audioSource[(int)Channel.Effects].PlayOneShot(audioSource[(int)Channel.Effects].clip);
        }

        public void PlayEffect(string clip, int source)
        {
            audioSource[source].clip = GetAudioClipFromDatabase(clip);
            audioSource[source].PlayOneShot(audioSource[source].clip);
        }

        public void PlayEffect(string clip, float volume)
        {
            audioSource[(int)Channel.Effects].volume = volume;
            audioSource[(int)Channel.Effects].clip = GetAudioClipFromDatabase(clip);
            audioSource[(int)Channel.Effects].PlayOneShot(audioSource[(int)Channel.Effects].clip);
        }

        public void PlayEffect(string clip, int source, float volume)
        {
            audioSource[source].volume = volume;
            audioSource[source].clip = GetAudioClipFromDatabase(clip);
            audioSource[source].PlayOneShot(audioSource[source].clip);
        }

        public void PlayEffect(string clip, float volume, float pitch)
        {
            audioSource[(int)Channel.Effects].volume = volume;
            audioSource[(int)Channel.Effects].pitch = pitch;
            audioSource[(int)Channel.Effects].clip = GetAudioClipFromDatabase(clip);
            audioSource[(int)Channel.Effects].PlayOneShot(audioSource[(int)Channel.Effects].clip);
        }

        public void PlayEffect(string clip, int source, float volume, float pitch)
        {
            audioSource[source].volume = volume;
            audioSource[source].pitch = pitch;
            audioSource[source].clip = GetAudioClipFromDatabase(clip);
            audioSource[source].PlayOneShot(audioSource[source].clip);
        }

        private AudioClip GetAudioClipFromDatabase(string clip)
        {
            return audiosList.audio.Find(a => a.name == clip);
        }

        #endregion
    }
}
