using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [Header("Target")]
    public bool targetMode;
    public float distance = 2.5f;
    public Transform targetTransform;

    [Header("Speeds")]
    public float normalSpeed = 1.0f;
    public float sprintSpeed = 5.0f;

    [Header("Lerp")]
    public bool useLerp = true;
    public float positionLerp = 7.5f;
    public float rotationLerp = 7.5f;
    public float fieldOfViewLerp = 10.0f;

    [Header("Field Of View")]
    public float minFieldOfView = 20.0f;
    public float maxFieldOfView = 120.0f;
    public float middle = 0.55f;

    [Header("Mouse KeyCodes")]
    public KeyCode mainKey = KeyCode.Mouse1;
    public KeyCode fieldOfViewKey = KeyCode.Mouse0;
    public KeyCode resetFieldOfViewKey = KeyCode.Mouse2;

    [Header("Keyboard KeyCodes")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode panUpKey = KeyCode.Q;
    public KeyCode panDownKey = KeyCode.E;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private Vector3 position = new Vector3();
    private Quaternion rotation = new Quaternion();
    private float x, y, currentFieldOfView, zoom;
    private bool activated, freeCamLock, targetLock;
    private Camera targetCamera;
    private float currentMovementSensitivity = 1;
    private float currentCameraSensitivity = 1;

    private void Start ()
    {
        targetCamera = GetComponent<Camera> ();

        if (targetCamera == null)
        {
            Debug.LogError ("[Free Camera] The object that this script is on does not have a Camera component.");
            activated = false;
        }
        else
            activated = true;

        position = transform.position;
        rotation = transform.rotation;
        x = transform.localEulerAngles.y;
        y = transform.localEulerAngles.x;
        zoom = middle;
    }
    private void Update ()
    {
        if (!activated)
            return;

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
        targetCamera.fieldOfView = useLerp ? Mathf.Lerp (targetCamera.fieldOfView, Mathf.Lerp (minFieldOfView, maxFieldOfView, zoom), Time.deltaTime * fieldOfViewLerp) : Mathf.Lerp (minFieldOfView, maxFieldOfView, zoom);
    }
    public void FreeCamInput ()
    {
        freeCamLock = Input.GetKey (mainKey) && !targetMode;

        //
        // Cursor lockage
        //
        if (freeCamLock && !targetMode)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!freeCamLock && !targetMode)
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
            zoom = middle;

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

            if (Input.GetKey (forwardKey))
                position += transform.forward * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (backwardKey))
                position -= transform.forward * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (rightKey))
                position += transform.right * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (leftKey))
                position -= transform.right * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (panUpKey))
                position += transform.up * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (panDownKey))
                position -= transform.up * Time.deltaTime * currentMovementSensitivity;
            if (Input.GetKey (sprintKey))
                currentMovementSensitivity = sprintSpeed;
            else
                currentMovementSensitivity = normalSpeed;
        }
    }
    public void TargetMotorInput ()
    {
        targetLock = Input.GetKey (mainKey) && targetMode;

        //
        // Error masking
        //
        if (targetTransform == null)
            targetMode = false;

        //
        // Cursor lockage
        //
        if (targetLock && targetMode)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!targetLock && targetMode)
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
