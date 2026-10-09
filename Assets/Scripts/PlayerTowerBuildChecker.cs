using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerBuildChecker : MonoBehaviour
{
    [SerializeField]
    private int collisionsNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("PlayerTowerBuildChecker: OnTriggerEnter: " + other.transform.gameObject.name+"|" + other.transform.gameObject.transform.position, other.transform.gameObject);
        collisionsNumber++;
    }

    private void OnTriggerExit(Collider other)
    {
        collisionsNumber--;
    }

    private void OnEnable()
    {
        collisionsNumber = 0;
    }

    public int GetCollisionNumbre()
    {
        return collisionsNumber;
    }
}
