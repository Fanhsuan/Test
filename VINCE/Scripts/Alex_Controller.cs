using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex_Controller : MonoBehaviour
{
    private static bool isDisabled = false;
    private Rigidbody rigidBody;

    #region MovementSpeed
    // Move speed
    public float moveSpeed = 750.0f;
    public float Default_Speed = 750.0f;

    /// <summary>
    /// Resets the player's movement speed to the default
    /// </summary>
    public void ResetMoveSpeed() { moveSpeed = Default_Speed; }
    /// <summary>
    /// Updates the player's movement speed
    /// </summary>
    /// <param name="speed"></param>
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    #endregion

    #region CameraSensitivity
    // Camera 
    public float cameraSensitivity = 0.5f;
    public float Default_Sensitivity = 0.5f;
    /// <summary>
    /// Resets the camera's sensitivity to the default
    /// </summary>
    public void ResetCameraSensitivity() { SetCameraSensitivity(Default_Sensitivity); }
    /// <summary>
    /// Sets the camera sensitivity to a new value
    /// </summary>
    /// <param name="sensitivity"></param>
    public void SetCameraSensitivity(float sensitivity)
    {
        cameraSensitivity = sensitivity;
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    [SerializeField] private Vector3 debugVelocityVectors;
    [SerializeField] private float debugVelocity;

    // Update is called once per frame
    void Update()
    {
        debugVelocityVectors = rigidBody.velocity;
        debugVelocity = rigidBody.velocity.magnitude;
    }
    // Update, but for phyiscs
    private void FixedUpdate()
    {
        DoMovement();
    }

    // Movement controller, accounts for collisions
    private void DoMovement()
    {
        // If unimpeded, normal movement
        if (!isDisabled)
        {
            float translation = Input.GetAxis("Move_Vertical") * moveSpeed;
            float straffe = Input.GetAxis("Move_Horizontal") * moveSpeed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            rigidBody.AddRelativeForce(straffe, 0, translation);

            if (Input.GetButtonDown("Cancel"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        // Force movement to stop if it would continue sliding slowly
        if (rigidBody.velocity.magnitude < 0.2f &&
            Input.GetAxis("Move_Horizontal") == 0.0f &&
            Input.GetAxis("Move_Vertical") == 0.0f)
        {
            rigidBody.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// True = movement enabled, false = movement disabled
    /// </summary>
    /// <param name="setting"></param>
    public static void Disable(bool setting)
    {
        isDisabled = !setting;
    }

    /// <summary>
    /// Toggles movement, returns true/false if enabled/disabled
    /// </summary>
    /// <returns></returns>
    public static bool ToggleMovement() {
        Disable(isDisabled);
        CamMouseLook.SetRotation(isDisabled);
        if (!isDisabled) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }
        return isDisabled;
    }

    /// <summary>
    /// Toggle movement, rotation, and locks or unlocks the mouse based upon the state of stopped
    /// <para>Enabled = true, disabled = false</para>
    /// </summary>
    /// <param name="setting"></param>
    /// <returns></returns>
    public static bool SetMovement(bool setting) {
        return SetMovement(setting, setting);
    }

    /// <summary>
    /// Enabled = true, disabled = false
    /// </summary>
    /// <param name="movement">Set movement enabled</param>
    /// <param name="rotation">Set rotation enabled</param>
    /// <returns></returns>
    public static bool SetMovement(bool movement, bool rotation)
    {
        if (isDisabled != movement)
        {
            Disable(!movement);
        }
        if (CamMouseLook.GetEnabled != rotation)
        {
            CamMouseLook.SetRotation(!rotation);
        }
        // If it worked, return true
        if (isDisabled == !movement && CamMouseLook.GetEnabled == rotation)
        {
            return true;
        }
        return false;
    }

    
}