using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookingAround : MonoBehaviour
{
    private NewControls inputControls;
    private InputAction looking;
    
    //sensitivity
    [SerializeField] private float mouseSensitivity = 100f;
    
    //player
    public Transform playerBody;

    //rotation
    private float xRotation;

    private void Awake()
    {
        inputControls = new NewControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float mouseX = looking.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = looking.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        //looking up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        
        //looking right/left
        playerBody.Rotate(Vector3.up * mouseX);
    }
    
    private void OnEnable()
    {
        looking = inputControls.Player.Look;
        looking.Enable();
    }
    
    private void OnDisable()
    {
        looking.Disable();
    }
}
