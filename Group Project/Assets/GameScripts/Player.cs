using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

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
    
    // SpawnPoint
    [SerializeField] private Transform spawnPoint;

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
            
            //input to move the player
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //moving the player
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            
            //jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            

            //applying gravity to the player
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        
        private void Spawn()
        {
            transform.position = spawnPoint.position;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                Spawn();
            }
        }
}
