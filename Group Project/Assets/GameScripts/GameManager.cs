using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
   public List<PlayerInput> playerList = new List<PlayerInput>();

   [SerializeField] private InputAction joinAction;
   [SerializeField] private InputAction leaveAction;
   
   //Instances
   public static GameManager instance = null;
   
   //Events
   public event System.Action<PlayerInput> PlayerJoinedGame;
   public event System.Action<PlayerInput> PlayerLeftGame;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else if (instance != null)
      {
         Destroy(gameObject);
      }
      
      joinAction.Enable();
      joinAction.performed += context => JoinAction(context);
      
      leaveAction.Enable();
      leaveAction.performed += context => LeaveAction(context);
   }

   private void Start()
   {
      // PlayerInputManager.instance.JoinPlayer(0, -1, "Control1");
   }

   void OnPlayerJoined(PlayerInput playerInput)
   {
      // playerList.Add(playerInput);
      // if (PlayerJoinedGame != null)
      // {
      //    PlayerJoinedGame(playerInput);
      // }
   }

   void OnPlayerLeft(PlayerInput playerInput)
   {
      
   }

   void JoinAction(InputAction.CallbackContext context)
   {
      PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
   }
   
   void LeaveAction(InputAction.CallbackContext context)
   {
      
   }
}
