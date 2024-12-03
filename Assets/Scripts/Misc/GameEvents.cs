using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Game/GameEvents")]
public class GameEvents : ScriptableObject
{

    public event Action OnGameReset;
    #region Level Events
    public event Action<int> OnSetAsteroidsToSpawn;
    public event Action OnLevelStart;
    public event Action OnLevelComplete;
    #endregion

    #region Player Events
    public event Action OnPlayerDamaged;
    public event Action OnPlayerDeath;
    public event Action<int> OnAsteroidDestroyed;
    public event Action<int> OnAddAsteroids;
    public event Action OnLevelUp;
    public event Action HUDUpdate;
    public event Action OnThrusterStart;
    public event Action OnThrusterStop;
    #endregion
    public event Action OnWrapAround;

    #region Sound Events
    public event Action OnShootSound;
    public event Action OnExplosionSound;
    public event Action OnPlayerDamagedSound;
    #endregion
    public event Action OnGameOver;


    #region Player Methods
    public void AsteroidDestroyed(int points)
    {
        if (points <= 0) return;
        OnAsteroidDestroyed?.Invoke(points);
        PlayExplosionSound();
    }
    public void OnShoot()
    {
        OnShootSound?.Invoke();
    }
    public void PlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }
    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    public void LevelUp()
    {
        OnLevelUp?.Invoke();
    }
    public void HUDUpdated()
    {
        HUDUpdate?.Invoke();
    }
    public void WrapAround()
    {
        OnWrapAround?.Invoke();
    }
    public void ThrusterStart()
    {
        OnThrusterStart?.Invoke();
    }


    public void ThrusterStop()
    {
        OnThrusterStop?.Invoke();
    }
    #endregion
    public void AddAsteroids(int count)
    {
        OnAddAsteroids?.Invoke(count);
    }
    public void SetAsteroidsToSpawn(int count)
    {
        OnSetAsteroidsToSpawn?.Invoke(count);
    }
    #region Sound Methods

    public void PlayExplosionSound()
    {
        OnExplosionSound?.Invoke();
    }
    public void PlayPlayerDamagedSound()
    {
        OnPlayerDamagedSound?.Invoke();
    }
    #endregion
    #region Level Manager Methods
    public void LevelStart()
    {
        OnLevelStart?.Invoke();
    }
    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }
    #endregion

    public void GameReset()
    {
        OnGameReset?.Invoke();
    }
    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

}
