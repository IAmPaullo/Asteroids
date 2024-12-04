using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Dedicated Audio Sources")]
    [SerializeField] private AudioSource thrusterAudioSource;

    [Header("Audio Source Pool")]
    [SerializeField] private int poolSize = 3;
    private Queue<AudioSource> audioSourcePool;

    [Header("Audio Clips Config")]
    [SerializeField] private AudioClipsConfig audioClipsConfig;

    [Header("Game Events")]
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private PlayerConfig playerConfig;


    private readonly float shootCooldown = 0.1f;
    private float lastShootTime = -Mathf.Infinity;

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
        gameEvents.OnThrusterStart += PlayThrusterSound;
        gameEvents.OnThrusterStop += StopThrusterSound;
        gameEvents.OnPlayerDeath += PlayPlayerDeathSound;
        gameEvents.OnSpawnBlackHole += PlayBlackHoleSound;
        gameEvents.OnSpawnEnemyShip += PlaySpawnEnemyShipSound;
    }

    private void OnDisable()
    {
        gameEvents.OnShootSound -= PlayShootSound;
        gameEvents.OnExplosionSound -= PlayExplosionSound;
        gameEvents.OnPlayerDamagedSound -= PlayPlayerDamagedSound;
        gameEvents.OnThrusterStart -= PlayThrusterSound;
        gameEvents.OnThrusterStop -= StopThrusterSound;
        gameEvents.OnPlayerDeath -= PlayPlayerDeathSound;
        gameEvents.OnSpawnBlackHole -= PlayBlackHoleSound;
        gameEvents.OnSpawnEnemyShip -= PlaySpawnEnemyShipSound;

    }



    private void PlayThrusterSound()
    {
        if (!playerConfig.isSoundActivated) return;
        thrusterAudioSource.volume = playerConfig.gameAudioVolume;
        if (!thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.clip = audioClipsConfig.GetThrusterSound();
            thrusterAudioSource.loop = true;
            thrusterAudioSource.Play();
        }
    }

    private void StopThrusterSound()
    {
        if (thrusterAudioSource.isPlaying)
        {
            thrusterAudioSource.Stop();
        }
    }
    private void PlayShootSound()
    {
        if (Time.time - lastShootTime >= shootCooldown)
        {
            PlaySound(audioClipsConfig.GetRandomShootSound());
            lastShootTime = Time.time;
        }
    }

    private void PlayExplosionSound()
    {
        PlaySound(audioClipsConfig.GetRandomExplosionSound(), true);
    }

    private void PlayPlayerDamagedSound()
    {
        PlaySound(audioClipsConfig.GetRandomDamageSound());
    }
    private void PlayPlayerDeathSound()
    {
        PlaySound(audioClipsConfig.GetRandomDeathSound());
    }
    private void PlayBlackHoleSound()
    {
        PlaySound(audioClipsConfig.GetRandomBlackHoleSound());
    }
    private void PlaySpawnEnemyShipSound()
    {
        PlaySound(audioClipsConfig.GetRandomEnemyShipSound());
    }
    private void PlaySound(AudioClip clip, bool randomPitch = false, bool isHighPriority = false)
    {
        if (clip == null || !playerConfig.isSoundActivated) return;

        AudioSource audioSource;
        if (audioSourcePool.Count > 0)
        {
            audioSource = audioSourcePool.Dequeue();
        }
        else if (isHighPriority)
        {
            audioSource = StopAndReuseAudioSource();
        }
        else
        {
            Debug.LogWarning("All AudioSources are busy!");
            return;
        }
        audioSource.volume = playerConfig.gameAudioVolume;
        audioSource.pitch = randomPitch ? Random.Range(0f, 1.5f) : 1f;
        audioSource.PlayOneShot(clip);
        StartCoroutine(ReturnAudioSourceToPool(audioSource, clip.length));
    }

    private AudioSource StopAndReuseAudioSource()
    {
        AudioSource audioSource = audioSourcePool.Peek();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        return audioSourcePool.Dequeue();
    }

    private IEnumerator ReturnAudioSourceToPool(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSourcePool.Enqueue(audioSource);
    }

    private void SwitchAudioVolume()
    {
        float volume = playerConfig.isSoundActivated ? 1f : 0f;
    }
}
