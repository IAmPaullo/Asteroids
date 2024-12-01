using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsConfig", menuName = "Game/AudioClips")]
public class AudioClipsConfig : ScriptableObject
{
    [SerializeField] private AudioClip[] shootSound;
    [SerializeField] private AudioClip[] explosionSound;
    [SerializeField] private AudioClip[] playerDamagedSound;

    public AudioClip GetRandomShootSound()
    {
        int rnd = Random.Range(0, shootSound.Length);
        return shootSound[rnd];
    }
    public AudioClip GetRandomExplosionSound()
    {
        int rnd = Random.Range(0, explosionSound.Length);
        return explosionSound[rnd];
    }
    public AudioClip GetRandomDamageSound()
    {
        int rnd = Random.Range(0, playerDamagedSound.Length);
        return playerDamagedSound[rnd];
    }
}
