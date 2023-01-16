using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
   public int NumberOfDiamonds
   {
      get;
      private set;
   }

   public UnityEvent<PlayerInventory> OnDiamondCollected;
   
   public void DiamondsCollected()
   {
      NumberOfDiamonds++;
      OnDiamondCollected.Invoke(this);
   }
}
