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
   }
   
   public void OnPlayerJoined(PlayerInput playerInput)
   {
      playerList.Add(playerInput);
      int currentId = playerList.Count - 1;
      Player player = playerList[currentId].GetComponent<Player>();
      player.Init(currentId);
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
