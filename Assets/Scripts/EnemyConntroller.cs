using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyConntroller : MonoBehaviour
{
    [SerializeField]
    private float Health = 10;
    [SerializeField]
    private float movemendSpeed = 2f;

    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackCountdown = 1f;
    private float attackCountdownAtMoment = 1f;
    [SerializeField]
    private float attackDamage = 1f;

    private EnemyMovement myEnemyMovement;
    [SerializeField]
    private bool pathfindingAllowedByController = false;
    [SerializeField]
    private bool pathfindingAllowed;
    private Transform Target;

    private List<TowerShooting> posibleTowerTargets = new List<TowerShooting>();
    private bool needAttekTarget;
    private TowerShooting towerAttackTarget;

    private void Awake()
    {
        myEnemyMovement = this.gameObject.GetComponent<EnemyMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCountdownAtMoment > 0)
        {
            CalculateAttackCountdown();
        }

        if (myEnemyMovement.navMeshPathStatus == UnityEngine.AI.NavMeshPathStatus.PathPartial &&
            myEnemyMovement.hasPath == false)
        {
            needAttekTarget = true;
            ChaeckTowerTargetRage();
            AttakTower();

        }
        else
        {
            needAttekTarget = false;
            posibleTowerTargets.Clear();
        }

        //if (myEnemyMovement.navMeshPathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete &&
        //    myEnemyMovement.hasPath == false)
        //{
        //    if (attackCountdownAtMoment <= 0)
        //    {
        //        CalculateAttackCountdown(true);
        //        Attack();
        //    }
        //}

        ChaeckTargetRage();
    }
    private void Setup()
    {
        Debug.Log("EnemyConntroller: Setup");
        pathfindingAllowed = Gamemanager.PublicStaricGamemanager.pathfindingAllowed;
        Target = Gamemanager.PublicStaricGamemanager.EnemyTarget.transform;
        //if (pathfindingAllowed)
        //{
        //    myEnemyMovement.MoveToTarget();
        //}

    }
    [Header("Particle Effekt")]
    [SerializeField] GameObject LarvaDyingParticle;
    [SerializeField] GameObject LarvaHit;
    public void EnemyHit(float recivedDamge)
    {
        Health -= recivedDamge;
        Instantiate(LarvaHit,transform.position,Quaternion.identity,transform);
        if (Health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        PathfindingAllowedByControllerChange(false);
        Instantiate(LarvaDyingParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        return;
    }

    private void ChaeckTowerTargetRage()
    {
        TowerShooting target = null;
        float targetDistanc = float.PositiveInfinity;
        foreach (TowerShooting forechObjekt in posibleTowerTargets)
        {
            float calculatedDistance = Vector3.Distance(this.gameObject.transform.position, forechObjekt.transform.position);
            if (calculatedDistance < targetDistanc /*&& calculatedDistance <= attackRange*/)
            {
                targetDistanc = calculatedDistance;
                target = forechObjekt;
            }
            Debug.LogFormat("TowerShooting: FindNearstEnemyV2: calculatedDistance {0}| targetDistanc {1}| forechObjekt {2}|", calculatedDistance, targetDistanc, forechObjekt.gameObject.name);
            towerAttackTarget = target;
        }
    }
    private void AttakTower()
    {
        if (!towerAttackTarget)
        {
            return;
        }
        if (attackCountdownAtMoment <= 0)
        {
            CalculateAttackCountdown(true);
            towerAttackTarget.EnemyHit(attackDamage);
        }
    }

    private void CalculateAttackCountdown(bool reciveSetup = false)
    {
        if (reciveSetup)
        {
            attackCountdownAtMoment = attackCountdown;
            return;
        }
        attackCountdownAtMoment -= Time.deltaTime;
    }

    private void ChaeckTargetRage()
    {
        float distance = Vector3.Distance(Target.position, this.gameObject.transform.position);
        if (distance<= attackRange)
        {
            Debug.LogFormat("EnemyConntroller: ChaeckTargetRage: distance({0})<= attackRange({1})", distance, attackRange);
            PathfindingAllowedByControllerChange(false);
        }
        else
        {
            PathfindingAllowedByControllerChange(true);
            return;
        }
        
        if (attackCountdownAtMoment<= 0)
        {
            CalculateAttackCountdown(true);
            Attack();
        }
    }
    private void Attack()
    {
        Gamemanager.PublicStaricGamemanager.NexusLifeChange(-attackDamage);
    }

    private void PathfindingAllowedWasChanged(bool recivedBoolState)
    {
        Debug.LogFormat("EnemyConntroller: PathfindingAllowedWasChanged: pathfindingAllowed was changed to {0}.", recivedBoolState);
        pathfindingAllowed = recivedBoolState;
        ComandEnemyMovement(pathfindingAllowedByController);
    }
    private void PathfindingAllowedByControllerChange(bool recivedBoolState)
    {
        if (recivedBoolState == pathfindingAllowedByController)
        {
            return;
        }
        pathfindingAllowedByController = recivedBoolState;
        ComandEnemyMovement(pathfindingAllowedByController);
    }

    private void ComandEnemyMovement(bool recivedAllowedStateController)
    {
        if (!pathfindingAllowed || !recivedAllowedStateController)
        {
            myEnemyMovement.StopMovingToTarget();
        }
        else
        {
            myEnemyMovement.MoveToTarget();
        }
    }

    private void OnEnable()
    {
        Gamemanager.PathfindingAllowedDelegate += PathfindingAllowedWasChanged;
        Setup();
        Gamemanager.PublicStaricGamemanager.AddEnemxControler(this);

    }
    private void OnDisable()
    {
        Gamemanager.PathfindingAllowedDelegate -= PathfindingAllowedWasChanged;
        Gamemanager.PublicStaricGamemanager.RemoveEnemxControler(this);
    }
    private void OnDrawGizmos()
    {
        // Draw a red sphere at the transform's position
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawSphere(this.gameObject.transform.position, attackRange);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!needAttekTarget)
        {
            return;
        }
        if (other.GetComponentInParent<TowerShooting>())
        {
            if (!posibleTowerTargets.Contains(other.GetComponentInParent<TowerShooting>()))
            {
                posibleTowerTargets.Add(other.GetComponentInParent<TowerShooting>());
            }
        }
    }
}
