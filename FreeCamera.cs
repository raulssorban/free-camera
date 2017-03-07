using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [Header("Target")]
    public bool useTarget;
    public float distance;
    public Transform targetTransform;

    [Header("Speeds")]
    public float normalSpeed = 1.0f;
    public float sprintSpeed = 5.0f;

    [Header("Lerp")]
    public bool useLerp = true;
    public float positionLerp = 7.5f;
    public float rotationLerp = 7.5f;

    [Header("Keys")]
    public KeyCode mainKey = KeyCode.Mouse1;
    public KeyCode fieldOfViewKey = KeyCode.Mouse0;
    public KeyCode resetFieldOfViewKey = KeyCode.Mouse2;

    private Vector3 position = new Vector3();
    private Quaternion rotation = new Quaternion();
    private float x, y, currentFieldOfView, zoom;
    private bool freeCamLock, targetLock;
    private Camera targetCamera;
    private float currentMovementSensitivity = 1;
    private float currentCameraSensitivity = 1;

    private void Start ()
    {
        targetCamera = GetComponent<Camera> ();

        position = transform.position;
        rotation = transform.rotation;
        x = transform.rotation.x;
        y = transform.rotation.y;
        zoom = 0.55f;
    }
    private void Update ()
    {
        Motor ();

        FreeCamInput ();
        TargetMotorInput ();
    }

    public void Motor ()
    {
        //
        // Movement and camera rotation
        //
        transform.position = useLerp ? Vector3.Lerp (transform.position, position, Time.deltaTime * positionLerp) : position;
        transform.rotation = useLerp ? Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * rotationLerp) : rotation;
        targetCamera.fieldOfView = useLerp ? Mathf.Lerp (targetCamera.fieldOfView, Mathf.Lerp (20.0f, 120.0f, zoom), Time.deltaTime * 10) : Mathf.Lerp (20.0f, 120.0f, zoom);
    }
    public void FreeCamInput ()
    {
        freeCamLock = Input.GetKey (mainKey) && !useTarget;

        //
        // Cursor lockage
        //
        if (freeCamLock && !useTarget)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!freeCamLock && !useTarget)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        //
        // Field of view
        //
        if (freeCamLock && Input.GetKey (fieldOfViewKey))
            zoom = Mathf.Clamp (zoom + Input.GetAxisRaw ("Mouse Y") * -0.033f, 0.01f, 1.0f);
        if (freeCamLock && Input.GetKeyDown (resetFieldOfViewKey))
            zoom = 0.55f;

        //
        // Input and motor
        //
        if (freeCamLock)
        {
            if(!Input.GetKey (fieldOfViewKey))
            {
                x += Input.GetAxis ("Mouse X") * currentCameraSensitivity * 2.5f;
                y -= Input.GetAxis ("Mouse Y") * currentCameraSensitivity * 2.5f;
            }

            rotation = Quaternion.Euler (y, x, 0);

            if (Input.GetKey (KeyCode.W))
                position += transform.forward * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (KeyCode.S))
                position -= transform.forward * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (KeyCode.D))
                position += transform.right * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (KeyCode.A))
                position -= transform.right * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (KeyCode.X))
                position += transform.up * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (KeyCode.C))
                position -= transform.up * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (KeyCode.LeftShift))
                currentMovementSensitivity = sprintSpeed;
            else
                currentMovementSensitivity = normalSpeed;
        }
    }
    public void TargetMotorInput ()
    {
        targetLock = Input.GetKey (mainKey) && useTarget;

        //
        // Cursor lockage
        //
        if (targetLock && useTarget)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!targetLock && useTarget)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        //
        // Field of view
        //
        if (targetLock && Input.GetKey (fieldOfViewKey))
            zoom = Mathf.Clamp (zoom + Input.GetAxisRaw ("Mouse Y") * -0.033f, 0.01f, 1.0f);
        if (targetLock && Input.GetKeyDown (resetFieldOfViewKey))
            zoom = 0.55f;

        if (targetLock)
        {
            position = rotation * new Vector3 (0, 0, -distance) + targetTransform.position;
            rotation = Quaternion.Euler (y, x, 0);

            if (!Input.GetKey (fieldOfViewKey))
            {
                x += Input.GetAxis ("Mouse X") * currentCameraSensitivity * 2.5f;
                y -= Input.GetAxis ("Mouse Y") * currentCameraSensitivity * 2.5f;
            }
        }
    }
}
