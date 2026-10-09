using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitLineDistance : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] LayerMask layer;
    [SerializeField] float distance = 100f;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer= GetComponent<LineRenderer>();
    }
    float pointerLength;
    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit info, distance, layer))
        {
            pointerLength = info.distance;
        }
        else
        {
            pointerLength = distance;
        } 
        Vector3 collisionPoint = new Vector3 (0,0,pointerLength);
        lineRenderer.SetPosition(1, collisionPoint);
    }
}
