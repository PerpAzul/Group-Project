using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float lives;
    
    //UI
    [SerializeField] private TMPro.TextMeshProUGUI livesUI;
    [SerializeField] private TMPro.TextMeshProUGUI dashCooldownUI;
    [Header("DashUI")] public Image dashImage;
    [SerializeField] private GameObject redScreen;

    //Spawn
    [SerializeField] private Vector3 spawnPoint1;
    [SerializeField] private Vector3 spawnPoint2;
    
    private Vector3 spawnPoint;

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

    public int playerIndex;
    
    //Audio Source
    [SerializeField] private AudioSource tickSource;

    //Paused
    private PauseMenuControl pauseMenu;

    private void Start()
    {
        dashImage.fillAmount = 0;
        tickSource = GetComponent<AudioSource>();
        pauseMenu = FindObjectOfType<PauseMenuControl>();
    }

    public void Init(int id)
    {
        if (id == 0)
        {
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }
        shooting.Init(id);
        GetComponent<Renderer>().material = id == 0 ? playerMateria1 : playerMateria2;
        spawnPoint = id == 0 ? spawnPoint1 : spawnPoint2;
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
        dashCooldownUI.text = "Dash: ";
        if (Time.time > nextDashTime)
        {
            isCooldown = false;
        }
        if (isCooldown)
        {
            dashImage.fillAmount += 1 / cooldownTime * Time.deltaTime;
            timer -= Time.deltaTime;
            if (dashImage.fillAmount >= 1)
            {
                dashImage.fillAmount = 1;
            }
        }
        else
        {
            dashImage.fillAmount = 1;
        }

        if (redScreen.GetComponent<Image>().color.a > 0)
        {
            var color = redScreen.GetComponent<Image>().color;
            color.a -= 0.01f;
            redScreen.GetComponent<Image>().color = color;
        }

        //Is paused
        pauseMenu = FindObjectOfType<PauseMenuControl>();
        
        //adjust lifes
        
    }
    
    private void Spawn()
    {
        transform.position = spawnPoint;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            if (lives > 1)
            {
                lives = 1;
            }

            if (shooting.ammo > 10)
            {
                shooting.returnAmmo();
            }
            
            Spawn();
        }
        
        if (other.CompareTag("Finish"))
        {
            Spawn();
        }
        
        if(other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            shooting.pickUpAmmo();
        }
        if(other.CompareTag("EditorOnly"))
        {
            Destroy(other.gameObject);
            if (lives < 3)
            {
                lives++;
            }
        }
        if(other.CompareTag("InvisPowerUp"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Invis());
        }
        if(other.CompareTag("DoubleDamagePowerUp"))
        {
            Destroy(other.gameObject);
            shooting.UseDoubleDamagePowerUp();
        }
    }

    IEnumerator Invis()
    {
        MeshRenderer[] RendererArray = GetComponentsInChildren<MeshRenderer>();
        foreach (var item in RendererArray)
        {
            item.enabled = false;
        }
        yield return new WaitForSeconds(5);
        foreach (var item in RendererArray)
        {
            item.enabled = true;
        }
    }

    public void TakeDamage()
    {
        gotHurt();
        lives--;
        tickSource.Play();
        if (lives <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            if (playerIndex == 0)
            {
                SceneManager.LoadScene(3);
            }

            if (playerIndex == 1)
            {
                SceneManager.LoadScene(2);
            }
            
            lives = 3;
            shooting.returnAmmo();
            nextDashTime = Time.time;
            dashImage.fillAmount = 1;
        }
    }
    
    public void TakeDamage2()
    {
        lives--;
        if (lives <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            if (playerIndex == 0)
            {
                SceneManager.LoadScene(3);
            }

            if (playerIndex == 1)
            {
                SceneManager.LoadScene(2);
            }
            
            lives = 3;
            shooting.returnAmmo();
            nextDashTime = Time.time;
            dashImage.fillAmount = 1;
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
        if (isGrounded && pauseMenu.isPaused == false)
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
            dashImage.fillAmount = 0;
            Vector3 move = orientation.forward * dashForce + orientation.up * dashUpwardForce;
            controller.Move(move);
            nextDashTime = Time.time + cooldownTime;
        }
    }
}
