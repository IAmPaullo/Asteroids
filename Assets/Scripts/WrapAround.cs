using UnityEngine;

public class WrapAround : MonoBehaviour
{
    [SerializeField] private bool enableWrapAround = true;
    [SerializeField] private float wrapBuffer = 0.5f;

    [SerializeField] private AudioClip wrapSound;

    [SerializeField] private GameEvents gameEvents;

    private Camera mainCamera;
    private Vector2 screenBounds;



    private void Start()
    {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    private void Update()
    {
        if (enableWrapAround && TryWrap())
        {
            PlayWrapSound();
        }
    }

    private void PlayWrapSound()
    {
        if (wrapSound != null)
        {
            gameEvents.WrapAround();
        }
    }

    private bool TryWrap()
    {
        Vector3 position = transform.position;
        bool wrapped = false;

        position.x = WrapCoordinate(position.x, screenBounds.x, ref wrapped);
        position.y = WrapCoordinate(position.y, screenBounds.y, ref wrapped);

        if (wrapped)
        {
            transform.position = position;
        }
        return wrapped;
    }

    private float WrapCoordinate(float coord, float bound, ref bool wrapped)
    {
        if (coord > bound + wrapBuffer)
        {
            wrapped = true;
            return -bound - wrapBuffer;
        }
        else if (coord < -bound - wrapBuffer)
        {
            wrapped = true;
            return bound + wrapBuffer;
        }
        return coord;
    }
}