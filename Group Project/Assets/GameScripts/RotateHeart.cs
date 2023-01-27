using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHeart : MonoBehaviour
{
    [SerializeField] float speedZ;

    private void Awake()
    {
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * speedZ * Time.deltaTime);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
