using Framework.Debugging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Audio
{
    /// <summary>
    /// Plays music and SFX
    /// </summary>
    public sealed class AudioManager : MonoBehaviour, IAudio
    {
        #region Variables

        private const float _MIN_FADE_TIME = 0.5f;
        private const float _MAX_FADE_TIME = 5f;
        private const float _DEFAULT_VALUE = -1f;

        private static float _masterVolume = 0.1f;
        private static float _musicVolume = 1f;
        private static float _sfxVolume = 1f;

        [Header("Sound")]
        [SerializeField]
        private List<AudioClip> _music = new List<AudioClip>();

        [SerializeField]
        private List<AudioClip> _sfx = new List<AudioClip>();

        [Header("Fade")]
        [SerializeField]
        [Range(_MIN_FADE_TIME, _MAX_FADE_TIME)]
        private float _fadeTime = _MIN_FADE_TIME;

        private Dictionary<string, int> _nameDict = new Dictionary<string, int>();
        private List<AudioSource> _sources;

        private AudioSource _musicSource;
        private AudioSource _sfxSource;

        #endregion

        #region Properties

        public float MasterVolume
        {
            get { return _masterVolume; }
            set
            {
                _masterVolume = Mathf.Clamp01(value);
                _musicSource.volume = _masterVolume * _musicVolume;
                _sfxSource.volume = _masterVolume * _sfxVolume;
            }
        }

        public float MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                _musicVolume = Mathf.Clamp01(value);
                _musicSource.volume = _masterVolume * _musicVolume;
            }
        }

        public float SFXVolume
        {
            get { return _sfxVolume; }
            set
            {
                _sfxVolume = Mathf.Clamp01(value);
                _sfxSource.volume = _masterVolume * _sfxVolume;
            }
        }

        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Locator.Provide(this);

            CheckSources(out _sources);

            _musicSource = _sources[0];
            _sfxSource = _sources[1];
            _musicSource.loop = true;

            for (int i = 0; i < _music.Count; ++i)
            {
                if (!_nameDict.ContainsKey(_music[i].name))
                    _nameDict.Add(_music[i].name, i);
                else
                {
                    Debugger.LogFormat(LOG_TYPE.WARNING, "Duplicate name: {0}!\n",
                        _music[i]);
                }
            }

            for (int i = 0; i < _sfx.Count; ++i)
            {
                if (!_nameDict.ContainsKey(_sfx[i].name))
                    _nameDict.Add(_sfx[i].name, i);
                else
                {
                    Debugger.LogFormat(LOG_TYPE.WARNING, "Duplicate name: {0}!\n",
                        _music[i]);
                }
            }
        }

        #region Music

        /// <summary>
        /// Plays music
        /// </summary>
        /// <param name="clipIndex">Clip to play by index</param>
        public void PlayMusic(int clipIndex)
        {
            if (clipIndex < 0 || clipIndex >= _music.Count)
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "There is no index {0} in Music!\n",
                    clipIndex);
                return;
            }

            AudioClip clip = _music[clipIndex];
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        /// <summary>
        /// Plays music
        /// </summary>
        /// <param name="clipName">Clip to play by name</param>
        public void PlayMusic(string clipName)
        {
            if (!_nameDict.ContainsKey(clipName))
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "{0} was not found!\n",
                    clipName);
                return;
            }

            AudioClip clip = _music[_nameDict[clipName]];
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        /// <summary>
        /// Plays music
        /// </summary>
        /// <param name="clip">Clip to play</param>
        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        /// <summary>
        /// Plays random music
        /// </summary>
        public void PlayRandomMusic()
        {
            AudioClip clip = _music[Random.Range(0, _music.Count)];
            _musicSource.clip = clip;
            _musicSource.Play();
        }

        #endregion

        #region SFX

        /// <summary>
        /// Plays SFX on other source
        /// </summary>
        /// <param name="clipIndex">Clip to play by index</param>
        /// <param name="other">Other source</param>
        public void PlaySFX(int clipIndex, AudioSource other)
        {
            if (clipIndex < 0 || clipIndex >= _sfx.Count)
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "There is no index {0} in SFX!\n",
                    clipIndex);
                return;
            }

            AudioClip clip = _sfx[clipIndex];
            other.PlayOneShot(clip, MasterVolume * SFXVolume);
        }

        /// <summary>
        /// Plays SFX
        /// </summary>
        /// <param name="clipIndex">Clip to play by index</param>
        public void PlaySFX(int clipIndex)
        {
            if (clipIndex < 0 || clipIndex >= _sfx.Count)
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "There is no index {0} in SFX!\n",
                    clipIndex);
                return;
            }

            AudioClip clip = _sfx[clipIndex];
            _sfxSource.PlayOneShot(clip, MasterVolume * SFXVolume);
        }

        /// <summary>
        /// Plays SFX on other source
        /// </summary>
        /// <param name="clipName">Clip to play by name</param>
        /// <param name="other">Other source</param>
        public void PlaySFX(string clipName, AudioSource other)
        {
            if (!_nameDict.ContainsKey(clipName))
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "{0} was not found!\n",
                    clipName);
                return;
            }

            AudioClip clip = _sfx[_nameDict[clipName]];
            other.PlayOneShot(clip, MasterVolume * SFXVolume);
        }

        /// <summary>
        /// Plays SFX
        /// </summary>
        /// <param name="clipName">Clip to play by name</param>
        public void PlaySFX(string clipName)
        {
            if (!_nameDict.ContainsKey(clipName))
            {
                Debugger.LogFormat(LOG_TYPE.WARNING, "{0} was not found!\n",
                    clipName);
                return;
            }

            AudioClip clip = _sfx[_nameDict[clipName]];
            _sfxSource.PlayOneShot(clip, MasterVolume * SFXVolume);
        }

        /// <summary>
        /// Plays SFX on other Source
        /// </summary>
        /// <param name="clip">Clip to play</param>
        /// <param name="other">Other source</param>
        public void PlaySFX(AudioClip clip, AudioSource other)
        {
            other.PlayOneShot(clip, MasterVolume * SFXVolume);
        }

        /// <summary>
        /// Plays SFX
        /// </summary>
        /// <param name="clip">Clip to play</param>
        public void PlaySFX(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip, MasterVolume * SFXVolume);
        }

        #endregion

        #region Fade

        /// <summary>
        /// Fades music in
        /// </summary>
        /// <param name="time">Time in seconds to fade in</param>
        public void FadeIn(float time = _DEFAULT_VALUE)
        {
            if (time != _DEFAULT_VALUE)
                _fadeTime = Mathf.Clamp(time, _MIN_FADE_TIME, _MAX_FADE_TIME);

            StopAllCoroutines();
            StartCoroutine(FadeInRoutine());
        }

        /// <summary>
        /// Fades music in and plays the clip
        /// </summary>
        /// <param name="clipIndex">Clip to play by index</param>
        /// <param name="time">Time in seconds to fade</param>
        public void FadeInAndPlay(int clipIndex, float time = _DEFAULT_VALUE)
        {
            FadeIn(time);
            PlayMusic(clipIndex);
        }

        /// <summary>
        /// Fades music in and plays the clip
        /// </summary>
        /// <param name="clipName">Clip to play by name</param>
        /// <param name="time">Time in seconds to fade</param>
        public void FadeInAndPlay(string clipName, float time = _DEFAULT_VALUE)
        {
            FadeIn(time);
            PlayMusic(clipName);
        }

        /// <summary>
        /// Fades music in and plays the clip
        /// </summary>
        /// <param name="clip">Clip to play</param>
        /// <param name="time">Time in seconds to fade</param>
        public void FadeInAndPlay(AudioClip clip, float time = _DEFAULT_VALUE)
        {
            FadeIn(time);
            PlayMusic(clip);
        }

        /// <summary>
        /// Fades music out
        /// </summary>
        /// <param name="time">Time in seconds to fade out</param>
        public void FadeOut(float time = _DEFAULT_VALUE)
        {
            if (time != _DEFAULT_VALUE)
                _fadeTime = Mathf.Clamp(time, _MIN_FADE_TIME, _MAX_FADE_TIME);

            StopAllCoroutines();
            StartCoroutine(FadeOutRoutine());
        }

        #endregion

        private IEnumerator FadeInRoutine()
        {
            float oldVolume = MusicVolume;
            MusicVolume = 0;

            while (MusicVolume < oldVolume)
            {
                MusicVolume += Time.unscaledDeltaTime /
                    _fadeTime * oldVolume;
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator FadeOutRoutine()
        {
            float oldVolume = MusicVolume;

            while (MusicVolume > 0)
            {
                MusicVolume -= Time.unscaledDeltaTime /
                    _fadeTime * oldVolume;
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Checks if there are 2 AudioSources attached.
        ///  If not, adds them
        /// </summary>
        private void CheckSources(out List<AudioSource> sources)
        {
            sources = new List<AudioSource>(GetComponents<AudioSource>());

            while (sources.Count < 2)
                sources.Add(gameObject.AddComponent<AudioSource>());

            if (sources.Count > 2)
            {
                Debugger.Log(LOG_TYPE.WARNING, "Only two AudioSources are supported!\n");

                while (sources.Count > 2)
                    sources.RemoveLast().enabled = false;
            }
        }
    }
}