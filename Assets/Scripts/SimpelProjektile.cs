using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SimpelProjektile : MonoBehaviour
{
    public LayerMask layermask;
    [SerializeField]
    private float rayCastRadius = 0.5f;
    [SerializeField]
    private string EnemyTack = "Enemy";
    [SerializeField]
    private float damage = 2f;
    [SerializeField]
    private float speed = 0.5f;
    private float maxMovementPerFrame;
    [SerializeField]
    private float lifeTime;
    private bool mustDie;
    [SerializeField] ParticleSystem hitParticle;
    void Start()
    {
        
    }

    void Update()
    {
        if (mustDie)
        {
            Die();
            return;
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0f)
        {
            Die();
        }
        maxMovementPerFrame = speed * Time.deltaTime;
        HitTraking();

    }

    private void HitTraking()
    {
        RaycastHit raycasthit;
        if (Physics.SphereCast(this.transform.position + (this.gameObject.transform.forward * -1* rayCastRadius),
                                rayCastRadius,
                                this.gameObject.transform.forward,
                                out raycasthit,
                                maxMovementPerFrame,
                                layermask))
        {
            //Debug.LogFormat("SimpelProjektile: HitTraking: " + raycasthit.transform.gameObject.name, raycasthit.transform.gameObject);
            if (raycasthit.transform.tag == EnemyTack)
            {
                Debug.Log("PlayerShooting: HitTraking: Enemy");
                raycasthit.transform.gameObject.GetComponentInParent<EnemyConntroller>().EnemyHit(damage);
            }
            //Move(raycasthit.point - this.gameObject.transform.position);
            mustDie = true;
        }
        Move(this.gameObject.transform.forward * maxMovementPerFrame);
    }

    private void Move(Vector3 recivedTranslation)
    {
        //this.transform.Translate(recivedTranslation);
        this.transform.position += recivedTranslation;
    }
    private void Die()
    {
        Destroy(this.gameObject);
        Instantiate(hitParticle,transform.position,quaternion.identity);
        return;
    }
}
