using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    private NewControls inputControls;
    private InputAction movement;

    //speed of player
    public float speed = 12f;
    Vector3 velocity;
    
    //jump height
    public float jumpHeight = 3f;
    
    //gravity
    public float gravity = -9.81f;
    
    //variabels to see if player is on the ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    //Life
    [SerializeField] private int lives;
    
    //UI
    [SerializeField] private TMPro.TextMeshProUGUI livesUI;

    private void Awake()
    {
        inputControls = new NewControls();
    }

    void Start()
    {
        Spawn();
    }
    
    void Update()
    {
        //check if player is on the ground or not
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //moving the player
        Vector3 move = movement.ReadValue<Vector2>().x * transform.right + movement.ReadValue<Vector2>().y * transform.forward;
        controller.Move( move * speed * Time.deltaTime);
        

        //applying gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        //UI for ammo and life
        livesUI.text = "Lives: " + lives;
    }
    
    private void Spawn()
    {
        transform.position = new Vector3(0.004f, 1.71f, 85.128f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            lives = 3;
            Shooting.returnAmmo();
            Spawn();
        }

        if (other.gameObject.CompareTag("EditorOnly"))
        {
            lives--;
        }

        if (lives <= 0)
        {
            lives = 3;
            Shooting.returnAmmo();
            Spawn();
        }
        if(other.CompareTag("Ammo"))
        {
            Destroy(other.gameObject);
            Shooting.pickUpAmmo();
        }
        if(other.CompareTag("Healz"))
        {
            Destroy(other.gameObject);
            lives++;
        }
        if(other.CompareTag("SkyTP"))
        {
            Destroy(other.gameObject);
            OnDisable();
            Invoke("actualTP", 0.1f);
            Invoke("OnEnable", 0.2f);
            StartCoroutine(TpCoroutine());
        }
        if(other.CompareTag("SkyTpPlatform"))
        {
            Debug.Log("Collided with SkyPlatform");
            StartCoroutine(TpCoroutine());
        }
    }
    
    IEnumerator TpCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Spawn();
    }

    private void actualTP()
    {
        transform.position = new Vector3(-2, 106, 38);
    }

    private void OnEnable()
    {
        movement = inputControls.Player.Move;
        movement.Enable();

        inputControls.Player.Jump.performed += doJump;
        inputControls.Player.Jump.Enable();
    }
    

    private void doJump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnDisable()
    {
        movement.Disable();
        inputControls.Player.Jump.Disable();
    }
    
}
