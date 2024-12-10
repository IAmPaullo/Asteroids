using UnityEngine;
[CreateAssetMenu(fileName = "Gun Configuration", menuName = "Ship/New Gun Configuration")]
public class GunConfiguration : ScriptableObject
{
    public GameObject bulletPrefab;
    public Sprite[] projectileSprites;
    public float projectileSpeed;
    public int maxProjectillesPerSecond;
}