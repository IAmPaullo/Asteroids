using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Ship currentShip;
    private Rigidbody2D rigidBody;

    private float maxThrustValue;
    private float thrustSpeed;
    private float turnSpeed;
    private float accelerationRate;
    private float decelerationRate;
    private float decelerationThreshold;

    private float thrustInputValue;
    private float targetRotation;
    private bool isThrusting;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Load Ship stats
    /// </summary>
    /// <param name="data">Ship's scriptable object you want to load!.</param>
    public void ConfigureMovement(Ship data)
    {
        maxThrustValue = data.MaxThrustValue;
        thrustSpeed = data.MaxThrustValue;
        turnSpeed = data.TurnSpeed;
        accelerationRate = data.AccelerationRate;
        decelerationRate = data.DecelerationRate;
        decelerationThreshold = data.DecelerationThreshold;
        currentShip = data;
        Debug.LogWarning("Ship Movement loaded");
    }
    public void ConfigureMovement()
    {
        if (currentShip == null)
        {
            Debug.LogError("No Ship configuration was set in Ship Data");
            return;
        }
        maxThrustValue = currentShip.MaxThrustValue;
        thrustSpeed = currentShip.MaxThrustValue;
        turnSpeed = currentShip.TurnSpeed;
        accelerationRate = currentShip.AccelerationRate;
        decelerationRate = currentShip.DecelerationRate;
        decelerationThreshold = currentShip.DecelerationThreshold;
    }

    private void OnEnable()
    {
        inputReader.RotateEvent += HandleRotationInput;
        inputReader.ThrustEvent += StartThrust;
        inputReader.StopThrustEvent += StopThrust;
    }

    private void OnDisable()
    {
        inputReader.RotateEvent -= HandleRotationInput;
        inputReader.ThrustEvent -= StartThrust;
        inputReader.StopThrustEvent -= StopThrust;
    }


    private void Update()
    {
        ApplyRotation();

    }
    private void FixedUpdate()
    {
        ApplyThrust();
    }

    private void HandleRotationInput(Vector2 input)
    {
        targetRotation = -input.x * turnSpeed;
    }

    private void StartThrust()
    {
        isThrusting = true;
    }

    private void StopThrust()
    {
        isThrusting = false;
    }

    private void ApplyThrust()
    {
        float lerpValue = isThrusting ? 1f : 0f;
        float lerpSpeed = isThrusting ? accelerationRate : decelerationRate;
        thrustInputValue = Mathf.Lerp(thrustInputValue, lerpValue, Time.deltaTime * lerpSpeed);

        if (!isThrusting && thrustInputValue < decelerationThreshold)
        {
            thrustInputValue = 0f;
        }

        float thrust = Mathf.Clamp(thrustInputValue * thrustSpeed * Time.deltaTime, 0f, maxThrustValue);
        rigidBody.linearVelocity = transform.up * thrust;
    }

    private void ApplyRotation()
    {
        transform.Rotate(0, 0, targetRotation);
    }
}
