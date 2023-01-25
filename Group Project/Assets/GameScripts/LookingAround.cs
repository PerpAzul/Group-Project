using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookingAround : MonoBehaviour
{
    private Vector2 looking;
    
    //sensitivity
    [SerializeField] private float mouseSensitivity = 100f;
    
    //player
    public Transform playerBody;

    //rotation
    private float xRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = looking.x * mouseSensitivity * Time.deltaTime;
        float mouseY = looking.y * mouseSensitivity * Time.deltaTime;

        //looking up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        
        //looking right/left
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void DoLook(InputAction.CallbackContext context)
    {
        looking = context.ReadValue<Vector2>();
    }
}
