using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjektilMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <0f)
        {
            Die();
        }
        Move();
    }

    //private void HitTraking()
    //{
    //    RaycastHit raycasthit;
    //    if (Physics.SphereCast(this.transform.position + (this.gameObject.transform.forward * -1 * rayCastRadius),
    //                            rayCastRadius,
    //                            this.gameObject.transform.forward,
    //                            out raycasthit,
    //                            maxMovementPerFrame,
    //                            layermask))
    //    {
    //        Debug.Log("PlayerProjektile: HitTraking: RaycastHit");
    //        if (raycasthit.transform.tag == EnemyTack)
    //        {
    //            Debug.Log("PlayerShooting: HitTraking: Enemy");
    //            raycasthit.transform.gameObject.GetComponentInParent<EnemyConntroller>().EnemyHit(damage);
    //        }
    //        else
    //        {
                
    //        }
    //        //Debug.LogFormat("PlayerShooting: TriggerShooting: " + raycasthit.transform.gameObject.name, raycasthit.transform.gameObject);
    //        mustDie = true;
    //    }
    //    Move(this.gameObject.transform.forward * maxMovementPerFrame);
    //}

    private void Move()
    {
        //this.transform.Translate(this.transform.forward * (speed * Time.deltaTime));
        this.transform.position += this.transform.forward * (speed * Time.deltaTime);
    }
    private void Die()
    {
        Destroy(this.gameObject);
        return;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogFormat("ProjektilMovement: OnCollisionEnter: " + collision.transform.gameObject.name, collision.transform.gameObject);
        Die();
    }
}
