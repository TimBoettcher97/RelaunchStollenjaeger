using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinsionExplosionEnemyDamager : MonoBehaviour
{
    [SerializeField]
    private string EnemyTack = "Enemy";

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
        if (other.transform.tag == EnemyTack)
        {
            Debug.Log("ExtinsionExplosionEnemyDamager: OnTriggerEnter: Enemy");
            other.transform.gameObject.GetComponentInParent<EnemyConntroller>().EnemyHit(float.MaxValue);
        }
    }
}
