using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Game/GameEvents")]
public class GameEvents : ScriptableObject
{
    public event Action<int> OnPlayerHeal;
    public event Action OnShieldIncrease;
    #region Level Events
    public event Action OnGameReset;
    public event Action OnGameOver;
    public event Action<int> OnSetAsteroidsToSpawn;
    public event Action OnWrapAround;
    public event Action OnLevelStart;
    public event Action OnLevelComplete;
    public event Action OnSpawnBlackHole;
    public event Action OnSpawnEnemyShip;
    #endregion

    #region Player Events
    public event Action OnPlayerShoot;
    public event Action OnPlayerDamaged;
    public event Action OnPlayerDeath;
    public event Action<int> OnAsteroidDestroyed;
    public event Action<int> OnEnemyShipDestroyed;
    public event Action<int> OnAddAsteroids;
    public event Action<Vector2> OnRotateEvent;
    public event Action OnLevelUp;
    public event Action HUDUpdate;
    public event Action OnThrusterStart;
    public event Action OnThrusterStop;
    #endregion

    #region Sound Events
    public event Action OnShootSound;
    public event Action OnExplosionSound;
    public event Action OnPlayerDamagedSound;
    #endregion

    #region Player Methods
    public void AsteroidDestroyed(int points)
    {
        if (points <= 0) return;
        OnAsteroidDestroyed?.Invoke(points);
        PlayExplosionSound();
    }

    public void EnemyShipDestroyed(int points)
    {
        if (points <= 0) return;
        OnEnemyShipDestroyed?.Invoke(points);
        PlayExplosionSound();
    }

    public void OnShoot()
    {
        OnShootSound?.Invoke();
    }
    public void PlayerShoot()
    {
        OnPlayerShoot?.Invoke();
    }
    public void PlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }

    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public void RotateEvent(Vector2 input)
    {
        OnRotateEvent?.Invoke(input);
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

    #region Level Manager Methods
    public void LevelStart()
    {
        OnLevelStart?.Invoke();
    }

    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public void SpawnBlackHole()
    {
        OnSpawnBlackHole?.Invoke();
    }
    #endregion

    #region General Methods
    public void AddAsteroids(int count)
    {
        OnAddAsteroids?.Invoke(count);
    }

    public void SetAsteroidsToSpawn(int count)
    {
        OnSetAsteroidsToSpawn?.Invoke(count);
    }

    public void GameReset()
    {
        OnGameReset?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void SpawnEnemyShip()
    {
        OnSpawnEnemyShip?.Invoke();
    }
    #endregion

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
}
