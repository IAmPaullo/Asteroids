using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Manager", menuName = "Managers/SoundManager", order = 1)]
public class SoundManagerSO : ScriptableObject
{
    private static SoundManagerSO instance;
    public static SoundManagerSO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<SoundManagerSO>("Sound Manager");
            }
            return instance;
        }
    }

    public AudioSource SoundObject;
    private float volumeChangeMultipler = .2f;
    private float pitchChangeMultipler = .1f;

    [SerializeField] private List<SoundPoolConfig> soundPools;
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private AudioClipsConfig audioClipsConfig;


    private Dictionary<string, Queue<AudioSource>> audioSourcePools = new();


    public void PlaySoundFXClip(AudioClip clip, Vector3 position, float volume)
    {
        float randomVolume = Random.Range(volume - volumeChangeMultipler, volume + volumeChangeMultipler);
        float randomPitch = Random.Range(1f - pitchChangeMultipler, 1f + pitchChangeMultipler);


        AudioSource source = Instantiate(Instance.SoundObject, position, Quaternion.identity);
        source.clip = clip;
        source.volume = randomVolume;
        source.pitch = randomPitch;
        source.Play();
    }
    public void PlaySoundFXClip(AudioClip[] clips, Vector3 position, float volume)
    {
        float randomVolume = Random.Range(volume - volumeChangeMultipler, volume + volumeChangeMultipler);
        float randomPitch = Random.Range(1f - pitchChangeMultipler, 1f + pitchChangeMultipler);
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        AudioSource source = Instantiate(Instance.SoundObject, position, Quaternion.identity);
        source.clip = clip;
        source.volume = randomVolume;
        source.pitch = randomPitch;
        source.Play();
    }

}

[System.Serializable]
public class SoundPoolConfig
{
    public string poolName;
    public AudioSource audioSourcePrefab;
    public int poolSize = 20;
}
