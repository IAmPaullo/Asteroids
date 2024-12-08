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



    private InputAction rotateAction;
    private InputAction shootAction;
    private InputAction thrustAction;


    private void OnEnable()
    {
        rotateAction = _asset.FindAction("Rotate", true);
        thrustAction = _asset.FindAction("Thrust", true);
        shootAction = _asset.FindAction("Shoot", true);


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
        thrustAction.Enable();
        shootAction.Enable();
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
        thrustAction.Disable();
        shootAction.Disable();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        RotateEvent?.Invoke(context.ReadValue<Vector2>());
    }
    private void OnShoot(InputAction.CallbackContext context)
    {
        ShootEvent?.Invoke();
    }
    private void OnThrust(InputAction.CallbackContext context)
    {
        if (ThrustEvent != null && context.started)
            ThrustEvent?.Invoke();
    }



}
