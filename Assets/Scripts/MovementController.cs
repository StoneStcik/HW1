using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("Variables for input")]
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction fire;

    [Header("Variables for position and speed control")]
    [Tooltip("Speed for vertical and horizontal input. *vertical will be double of this amount")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxX = 3f;
    [SerializeField] private float maxY = 8f;
    [SerializeField] private float minY = -4f;

    [SerializeField] private float pitchFactor = -5f;
    [SerializeField] private float controlPitchFactor = 5f;
    [SerializeField] private float controlRollFactor = 15f;
    [SerializeField] private float yawFactor = 4f;

    [SerializeField] private ParticleSystem[] lasers;

    private Vector3 shipLocalPos;
    private float speedY;

    float _horizontalMovement;
    float _verticalMovement;

    private void Start()
    {
        speedY = speed * 2;
    }

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    private void Update()
    {
        ProcessThrow();
        ProcessRotation();
        ProcessFire();
    }

    private void ProcessFire()
    {
        if (fire.ReadValue<float>() > 0.5f)
        {
            // DRY -> Dont repeat yourself
            SwitchLasersEmission(true);
        }
        else
        {
            SwitchLasersEmission(false);
        }
    }

    void SwitchLasersEmission(bool state)
    {
        foreach (var laser in lasers)
        {
            var laserEmission = laser.emission;
            laserEmission.enabled = state;
        }
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * pitchFactor + _verticalMovement * controlPitchFactor;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = _horizontalMovement * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessThrow()
    {
        _horizontalMovement = movement.ReadValue<Vector2>().x;
        _verticalMovement = movement.ReadValue<Vector2>().y;

        shipLocalPos.x = Mathf.Clamp(transform.localPosition.x + _horizontalMovement * Time.deltaTime * speed, -maxX, maxX);
        shipLocalPos.y = Mathf.Clamp(transform.localPosition.y + _verticalMovement * Time.deltaTime * speedY, minY, maxY);

        transform.localPosition = shipLocalPos;
    }
}
