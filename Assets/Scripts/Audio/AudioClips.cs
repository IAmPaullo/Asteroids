using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Audio;

[CreateAssetMenu(fileName = "AudioClipsConfig", menuName = "Game/AudioClips")]
public class AudioClipsConfig : ScriptableObject
{
    [SerializeField] private List<SoundCategory> soundCategories = new();

    public void AddSoundCategory(SoundCategory category)
    {
        soundCategories.Add(category);
    }

    public SoundCategory GetSoundCategory(string categoryName)
    {
        return soundCategories.FirstOrDefault(c =>
            string.Equals(c.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase));
    }
    public AudioClip GetRandomClip(string categoryName)
    {
        var category = GetSoundCategory(categoryName);
        return category?.GetRandomClip();
    }

    public List<string> GetCategoryNames()
    {
        return soundCategories.Select(c => c.CategoryName).ToList();
    }

}
