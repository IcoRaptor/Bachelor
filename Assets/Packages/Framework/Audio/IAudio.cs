using UnityEngine;

namespace Framework.Audio
{
    /// <summary>
    /// Audio interface for Locator
    /// </summary>
    public interface IAudio
    {
        void PlayMusic(int clipIndex);

        void PlayMusic(string clipName);

        void PlayMusic(AudioClip clip);

        void PlayRandomMusic();

        void PlaySFX(int clipIndex);

        void PlaySFX(int clipIndex, AudioSource other);

        void PlaySFX(string clipName);

        void PlaySFX(string clipName, AudioSource other);

        void PlaySFX(AudioClip clip);

        void PlaySFX(AudioClip clip, AudioSource other);

        void FadeIn(float time);

        void FadeInAndPlay(int clipIndex, float time);

        void FadeInAndPlay(string clipName, float time);

        void FadeInAndPlay(AudioClip clip, float time);

        void FadeOut(float time);
    }
}