using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Input/Input Reader", fileName = "Input Reader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _asset;


    public event UnityAction<Vector2> RotateEvent;
    public event UnityAction ShootEvent;
    public event UnityAction ThrustEvent;
    public event UnityAction StopThrustEvent;

    private InputAction rotateAction;
    private InputAction shootAction;
    private InputAction thrustAction;
    private InputAction stopThrustAction;

  

    private void OnEnable()
    {
        rotateAction = _asset.FindAction("Rotate", true);
        shootAction = _asset.FindAction("Shoot", true);
        thrustAction = _asset.FindAction("Thrust", true);

        // REMOVER OS STOP THRUST


        rotateAction.started += OnRotate;
        rotateAction.performed += OnRotate;
        rotateAction.canceled += OnRotate;

        shootAction.started += OnShoot;
        shootAction.performed += OnShoot;
        shootAction.canceled += OnShoot;

        thrustAction.started += OnThrust;
        thrustAction.performed += OnThrust;
        thrustAction.canceled += OnThrust;

        rotateAction.Enable();
        shootAction.Enable();
        thrustAction.Enable();
    }
    private void OnDisable()
    {

        rotateAction.started -= OnRotate;
        rotateAction.performed -= OnRotate;
        rotateAction.canceled -= OnRotate;

        shootAction.started -= OnShoot;
        shootAction.performed -= OnShoot;
        shootAction.canceled -= OnShoot;

        thrustAction.started -= OnThrust;
        thrustAction.performed -= OnThrust;
        thrustAction.canceled -= OnThrust;

        rotateAction.Disable();
        shootAction.Disable();
        thrustAction.Disable();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        RotateEvent?.Invoke(context.ReadValue<Vector2>());
        Debug.LogWarning(context.ReadValue<Vector2>());
    }
    private void OnShoot(InputAction.CallbackContext context)
    {
        if (ShootEvent != null && context.performed)
            ShootEvent?.Invoke();
    }
    private void OnThrust(InputAction.CallbackContext context)
    {
        if (ThrustEvent != null && context.started)
            ThrustEvent?.Invoke();
        if (StopThrustEvent != null && context.canceled)
            StopThrustEvent?.Invoke();
    }
}
