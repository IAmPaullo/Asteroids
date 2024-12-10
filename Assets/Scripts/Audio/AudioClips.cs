using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AudioClipsConfig", menuName = "Game/AudioClips")]
public class AudioClipsConfig : ScriptableObject
{
    [SerializeField] private SoundCategory shootSounds;
    [SerializeField] private SoundCategory explosionSounds;
    [SerializeField] private SoundCategory playerDamagedSounds;
    [SerializeField] private SoundCategory playerDeathSounds;
    [SerializeField] private SoundCategory thrusterSounds;
    [SerializeField] private SoundCategory blackHoleSounds;
    [SerializeField] private SoundCategory enemyShipSounds;
    [SerializeField] private SoundCategory levelStartSounds;

    public AudioClip GetRandomShootSound() => shootSounds?.GetRandomClip();
    public AudioClip GetRandomExplosionSound() => explosionSounds?.GetRandomClip();
    public AudioClip GetRandomDamageSound() => playerDamagedSounds?.GetRandomClip();
    public AudioClip GetRandomDeathSound() => playerDeathSounds?.GetRandomClip();
    public AudioClip GetThrusterSound() => thrusterSounds?.GetRandomClip();
    public AudioClip GetRandomBlackHoleSound() => blackHoleSounds?.GetRandomClip();
    public AudioClip GetRandomEnemyShipSound() => enemyShipSounds?.GetRandomClip();
    public AudioClip GetRandomLevelStartSound() => levelStartSounds?.GetRandomClip();

    public AudioClip GetRandomSound(SoundCategory category) => category?.GetRandomClip();

    public AudioClip GetSpecificSound(SoundCategory category, int index) =>
        category?.GetSpecificClip(index);

}

[Serializable]
public class SoundCategory
{
    [SerializeField] private string categoryName;
    [SerializeField] private AudioClip[] clips;

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

    public int ClipCount => clips?.Length ?? 0;
}