using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformNew : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition; 
    [SerializeField] private float speed;

    private Vector3 startPosition; 
    private bool toEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        toEnd = true;
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = toEnd ? startPosition + endPosition : startPosition;
        Vector3 amtToMove = (nextPosition - transform.position).normalized;
        amtToMove *= Time.deltaTime * speed;
        transform.Translate(amtToMove, Space.World);

        if (Vector3.Distance(nextPosition, transform.position) < amtToMove.magnitude)
        {
            toEnd = !toEnd;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(startPosition, startPosition + endPosition);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + endPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
