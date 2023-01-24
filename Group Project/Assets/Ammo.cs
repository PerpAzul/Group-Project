using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float speedY;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 360 * speedY * Time.deltaTime, 0);
    }
}
