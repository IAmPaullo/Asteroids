using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips Config")]
    [SerializeField] private AudioClipsConfig audioClipsConfig;

    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;

    [Header("Audio Source Pool")]
    [SerializeField] private int poolSize = 3;
    private Queue<AudioSource> audioSourcePool;

    private void Awake()
    {
        audioSourcePool = new Queue<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSourcePool.Enqueue(audioSource);
        }
    }

    private void OnEnable()
    {
        gameEvents.OnShootSound += PlayShootSound;
        gameEvents.OnExplosionSound += PlayExplosionSound;
        gameEvents.OnPlayerDamagedSound += PlayPlayerDamagedSound;
    }

    private void OnDisable()
    {
        gameEvents.OnShootSound -= PlayShootSound;
        gameEvents.OnExplosionSound -= PlayExplosionSound;
        gameEvents.OnPlayerDamagedSound -= PlayPlayerDamagedSound;
    }

    private void PlayShootSound()
    {
        PlaySound(audioClipsConfig.GetRandomShootSound());
    }

    private void PlayExplosionSound()
    {
        PlaySound(audioClipsConfig.GetRandomExplosionSound());
    }

    private void PlayPlayerDamagedSound()
    {
        PlaySound(audioClipsConfig.GetRandomDamageSound());
    }

    private void PlaySound(AudioClip clip, bool randomPitch = false)
    {
        if (clip == null) return;

        if (audioSourcePool.Count > 0)
        {
            AudioSource audioSource = audioSourcePool.Dequeue();
            audioSource.pitch = randomPitch ? Random.Range(0f, 1.5f) : 1f;
            audioSource.PlayOneShot(clip);
            StartCoroutine(ReturnAudioSourceToPool(audioSource, clip.length));
        }
        else
        {
            Debug.LogWarning("All AudioSources are busy!");
        }
    }

    private IEnumerator ReturnAudioSourceToPool(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSourcePool.Enqueue(audioSource);
    }
}
