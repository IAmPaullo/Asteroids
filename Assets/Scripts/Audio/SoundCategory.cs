using UnityEngine;
using System;

namespace Game.Audio
{
    [Serializable]
    public class SoundCategory : IAudioClipProvider
    {
        [SerializeField] private string categoryName;
        [SerializeField] private SoundType soundType;

        public SoundType SoundType => soundType;
        public string CategoryName => categoryName;
        [SerializeField] private AudioClip[] clips;
        private int currentIndex = 0;


        public AudioClip GetRandomClip()
        {
            if (clips == null || clips.Length == 0)
            {
                Debug.LogWarning($"No clips found in category: {categoryName}");
                return null;
            }
            return clips[UnityEngine.Random.Range(0, clips.Length)];
        }

        public AudioClip GetSpecificClip(int index)
        {
            if (clips == null || index < 0 || index >= clips.Length)
            {
                Debug.LogWarning($"Invalid clip index in category: {categoryName}");
                return null;
            }
            return clips[index];
        }
        public AudioClip GetNextClipSequentially()
        {
            if (clips == null || clips.Length == 0)
                return null;

            AudioClip selectedClip = clips[currentIndex];
            currentIndex = (currentIndex + 1) % clips.Length;
            return selectedClip;
        }

        public int ClipCount => clips?.Length ?? 0;
    }

    [Serializable]
    public enum SoundType
    {
        Shoot,
        Explosion,
        Damage,
        Spawn,
        Death,
        Thruster,
        BlackHole,
        Event
    }


}