using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
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
    
    //PlayerIndex
    private int index = 1;
    private int splitScreenIndex = 0;

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
        transform.position = new Vector3(0.048f, 1.819f, -0.157f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            lives = 3;
            // Shooting.returnAmmo();
            Spawn();
        }

        if (other.CompareTag("Player"))
        {
            lives--;
        }

        if (lives <= 0)
        {
            lives = 3;
            // Shooting.returnAmmo();
            Spawn();
        }
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

