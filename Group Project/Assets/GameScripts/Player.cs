using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    private Vector2 movement;

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
    [SerializeField] private TMPro.TextMeshProUGUI dashCooldownUI;
    [SerializeField] private GameObject redScreen;
    

    //Spawn
    [SerializeField] private Vector3 spawnPoint1;
    [SerializeField] private Vector3 spawnPoint2;
    
    private Vector3 spawnPoint;
    private int index = 0;
    
    //Tag from bullet
    private string enemyProjectileTag;
    
    //Dash
    [SerializeField] private Transform orientation;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashUpwardForce;
    [SerializeField] private float cooldownTime = 2;
    private float nextDashTime = 0;
    private bool isCooldown;
    private float timer;
    
    //Init
    [Header("Init Settings")]
    public Shooting shooting;

    [SerializeField] private Material playerMateria1;
    [SerializeField] private Material playerMateria2;

    private void Start()
    {
        dashCooldownUI.gameObject.SetActive(true);
    }

    public void Init(int id)
    {
        if (id == 0)
        {
            enemyProjectileTag = "EditorOnly";
        }
        else
        {
            enemyProjectileTag = "Player";
        }
        shooting.Init(id);
        GetComponent<Renderer>().material = id == 0 ? playerMateria1 : playerMateria2;
        spawnPoint = id == 0 ? spawnPoint1 : spawnPoint2;
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
        Vector3 move = movement.x * transform.right + movement.y * transform.forward;
        controller.Move( move * speed * Time.deltaTime);
        

        //applying gravity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //UI for life and dash
        livesUI.text = "Lifes: " + lives;
        dashCooldownUI.text = "Dash";
        if (Time.time > nextDashTime)
        {
            isCooldown = false;
        }
        if (isCooldown)
        {
            dashCooldownUI.gameObject.SetActive(false);
            timer -= Time.deltaTime;
        }
        else
        {
            dashCooldownUI.gameObject.SetActive(true);
        }

        if (redScreen.GetComponent<Image>().color.a > 0)
        {
            var color = redScreen.GetComponent<Image>().color;
            color.a -= 0.01f;
            redScreen.GetComponent<Image>().color = color;
        }
    }
    
    private void Spawn()
    {
        transform.position = spawnPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            lives = 3;
            shooting.returnAmmo();
            nextDashTime = Time.time;
            dashCooldownUI.gameObject.SetActive(true);
            Spawn();
        }
    }

    public void TakeDamage()
    {
        gotHurt();
        lives--;
        if (lives <= 0)
        {
            lives = 3;
            shooting.returnAmmo();
            nextDashTime = Time.time;
            dashCooldownUI.gameObject.SetActive(true);
            Spawn();
        }
        
    }

    private void gotHurt()
    {
        var color = redScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        redScreen.GetComponent<Image>().color = color;
    }

    public void doMove(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }
    
    public void doJump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    
    public void doDash(InputAction.CallbackContext obj)
    {
        if (Time.time > nextDashTime)
        {
            timer = cooldownTime;
            isCooldown = true;
            Vector3 move = orientation.forward * dashForce + orientation.up * dashUpwardForce;
            controller.Move(move);
            nextDashTime = Time.time + cooldownTime;
        }
    }
}
