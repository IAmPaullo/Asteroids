using UnityEngine;

public interface IAudioClipProvider
{
    AudioClip GetRandomClip();
    AudioClip GetSpecificClip(int index);
    int ClipCount { get; }
}