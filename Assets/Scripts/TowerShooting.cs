using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerShooting : MonoBehaviour
{
    [SerializeField]
    private float Health = 10;
    private float startingHealth;
    [SerializeField]
    private GameObject towerHead;
    private Quaternion towerHeadStartingRotation;
    [SerializeField]
    private GameObject towerHeadVertical;
    private Quaternion towerHeadStartingRotationVertical;
    [SerializeField]
    private GameObject towerShootingPoint;

    [SerializeField]
    private GameObject ProjektilPrefab;
    [SerializeField]
    private float shootingRange = 10;
    [SerializeField]
    private float shootingRate = 1;
    private float shootingWaitingMax;
    private float shootingWaitingAtMoment;
    [SerializeField]
    private float shootingDamage = 1;
    [SerializeField]
    private string EnemyTack = "Enemy";

    private SphereCollider shootingTriggeringSphere;
    private List<GameObject> EnemyObjects = new List<GameObject>();

    [SerializeField]
    private EnemyConntroller targetAtMoment;
    [SerializeField]
    private TMP_Text healthText;
    public enum TowerTier
    {
        T1,
        T2,
        T3
    }
    public TowerTier myTowerTier = TowerTier.T1;

    // Start is called before the first frame update
    void Start()
    {
        //shootingTriggeringSphere = this.gameObject.GetComponent<SphereCollider>();
        //shootingTriggeringSphere.radius = shootingRange;
        towerHeadStartingRotation = towerHead.transform.rotation;
        towerHeadStartingRotationVertical = towerHeadVertical.transform.rotation;
        TowerSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetAtMoment && Gamemanager.PublicStaricGamemanager.EnemyControlers.Count > 0f)
        {
            FindNearstEnemyV2();
            //if (EnemyObjects.Count > 0)
            //{
            //    for (int i = EnemyObjects.Count; i < 0; i--)
            //    {
            //        Debug.LogFormat("TowerShooting: Update: EnemyObjects {0}| i -> {1}| EnemyObjects[i] {2}|", EnemyObjects.Count, i, EnemyObjects[i] != null);
            //        if (!EnemyObjects[i])
            //        {
            //            EnemyObjects.RemoveAt(i);
            //        }
            //    }
            //}

            //if (EnemyObjects.Count > 0)
            //{
            //    FindNearstEnemy();
            //}
            //else
            //{
            //    towerHead.transform.rotation = towerHeadStartingRotation;
            //    towerHeadVertical.transform.rotation = towerHeadStartingRotationVertical;
            //    shootingWaitingAtMoment = shootingWaitingMax;
            //}
        }

        if (targetAtMoment)
        {
            AimAtTarget();
            ShootAtTarget();
            if (Vector3.Distance(this.gameObject.transform.position, targetAtMoment.transform.position)> shootingRange)
            {
                targetAtMoment = null;
            }
        }
    }

    private void TowerSetup()
    {
        shootingWaitingMax = 1 / shootingRate;
        shootingWaitingAtMoment = shootingWaitingMax;
        startingHealth = Health;
        UpdateHealtUI();


    }

    private void FindNearstEnemyV2()
    {
        EnemyConntroller target = null;
        float targetDistanc = float.PositiveInfinity;
        foreach (EnemyConntroller forechObjekt in Gamemanager.PublicStaricGamemanager.EnemyControlers)
        {
            float calculatedDistance = Vector3.Distance(this.gameObject.transform.position, forechObjekt.transform.position);
            if (calculatedDistance < targetDistanc && calculatedDistance <= shootingRange)
            {
                targetDistanc = calculatedDistance;
                target = forechObjekt;
            }
            //Debug.LogFormat("TowerShooting: FindNearstEnemyV2: calculatedDistance {0}| targetDistanc {1}| forechObjekt {2}|", calculatedDistance, targetDistanc, forechObjekt.gameObject.name);
            targetAtMoment = target;
        }
    }

    private void FindNearstEnemy()
    {
        List<EnemyConntroller> EnemysInRange = new List<EnemyConntroller>();
        
        foreach (GameObject forechObjekt in EnemyObjects)
        {
            if (true)
            {

            }
            EnemyConntroller foundEnemy = forechObjekt.GetComponentInParent<EnemyConntroller>();
            if (!EnemysInRange.Contains(foundEnemy))
            {
                EnemysInRange.Add(foundEnemy);
            }
            
        }
        Debug.LogFormat("TowerShooting: FindNearstEnemy: EnemysInRange {0}|", EnemysInRange.Count);

        EnemyConntroller target = null;
        float targetDistanc = float.PositiveInfinity;
        foreach (EnemyConntroller forechObjekt in EnemysInRange)
        {
            float calculatedDistance = Vector3.Distance(this.gameObject.transform.position, forechObjekt.transform.position);
            if (calculatedDistance < targetDistanc)
            {
                targetDistanc = calculatedDistance;
                target = forechObjekt;
            }
            Debug.LogFormat("TowerShooting: FindNearstEnemy: calculatedDistance {0}| targetDistanc {1}| forechObjekt {2}|", calculatedDistance, targetDistanc, forechObjekt.gameObject.name);
        }
        //for (int i = 0; i < EnemysInRange.Count; i++)
        //{
        //    float calculatedDistance = Vector3.Distance(this.gameObject.transform.position, EnemysInRange[i].transform.position);
        //    if (calculatedDistance < targetDistanc)
        //    {
        //        calculatedDistance = targetDistanc;
        //        target = EnemysInRange[i].gameObject.GetComponent<EnemyConntroller>(); ;
        //    }
        //    Debug.LogFormat("TowerShooting: FindNearstEnemy: calculatedDistance {0}| targetDistanc {1}| forechObjekt {2}| target {3}|", calculatedDistance, targetDistanc, EnemysInRange[i].gameObject.name, target.gameObject.name);
        //}
        targetAtMoment = target;
    }

    private void AimAtTarget()
    {
        Vector3 targetH =  new Vector3(targetAtMoment.transform.position.x,towerHead.transform.position.y, targetAtMoment.transform.position.z);
        //targetH.position = new Vector3(targetAtMoment.transform.position.x, towerHead.transform.position.y, targetAtMoment.transform.position.z);
        towerHead.transform.LookAt(targetH, Vector3.up);
        towerHeadVertical.transform.LookAt(targetAtMoment.transform, Vector3.up);

    }
    [Header("Audio")] [SerializeField] private AudioSource shootSound;
    private void ShootAtTarget()
    {
        if (shootingWaitingAtMoment >0)
        {
            shootingWaitingAtMoment -= Time.deltaTime;
        }
        else
        {
            targetAtMoment.EnemyHit(shootingDamage);
            GameObject projektile = Instantiate(ProjektilPrefab, towerShootingPoint.transform.position, towerShootingPoint.transform.rotation);
            shootSound.Play();
            //projektile.transform.parent = towerShootingPoint.transform;
            shootingWaitingAtMoment = shootingWaitingMax;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("TowerShooting: OnTriggerEnter: Object {0} with Tag {1}", other.gameObject.name, other.gameObject.tag, other.gameObject);
        if (other.gameObject.tag == EnemyTack)
        {
            EnemyObjects.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        print("exit");
        if (other.gameObject.tag == EnemyTack)
        {
            print("exit+");
            EnemyObjects.Remove(other.gameObject);
            if (other.gameObject.GetComponent<EnemyConntroller>() == targetAtMoment)
            {
                print("exit++");
                targetAtMoment = null;
            }
        }
    }
    private void Destroy()
    {
        Debug.LogFormat("TowerShooting: Destroy", this.gameObject);
        Destroy(this.gameObject);
        return;
    }
    public void EnemyHit(float recivedDamge)
    {
        Debug.LogFormat("TowerShooting: EnemyHit({0})", recivedDamge, this.gameObject);
        Health -= recivedDamge;
        if (Health <= 0)
        {
            Destroy();
        }
        UpdateHealtUI();
    }

    public void TowerRepare(float recivedRepareCost,float recivedRepareHealth)
    {
        if (Health >= startingHealth)
        {
            Debug.LogFormat("TowerShooting: TowerRepare: nothing to repair {0}/{1}", Health, startingHealth);
            // ToDoFeedback:
            return;
        }
        float walletvalue = Gamemanager.PublicStaricGamemanager.GetOreCount(MaterialType.Smaragd);
        if (walletvalue >= recivedRepareCost)
        {
            Debug.LogFormat("TowerShooting: TowerRepare: Tower:({0}); Towercost:({1}) ", BuildingWallet.BuildingWalletType.TowerT1, recivedRepareCost, this.gameObject);
            Gamemanager.PublicStaricGamemanager.OreChange(-recivedRepareCost, MaterialType.Smaragd);

            Health = Mathf.Clamp(Health + recivedRepareHealth, -1, startingHealth); 
            UpdateHealtUI();
        }
        else
        {
            Debug.LogFormat("TowerShooting: TowerRepare: Not enough {0}. Towercost:{1} Wallet:{2} ", MaterialType.Smaragd, recivedRepareCost, walletvalue, this.gameObject);
            // ToDoFeedback:
        }
    }

    public void TowerPickUp()
    {
        if (myTowerTier == TowerTier.T1)
        {
            Gamemanager.PublicStaricGamemanager.TowerPickup((Health / startingHealth), BuildingWallet.BuildingWalletType.TowerT1);
        }
        else if (myTowerTier == TowerTier.T2)
        {
            Gamemanager.PublicStaricGamemanager.TowerPickup((Health / startingHealth), BuildingWallet.BuildingWalletType.TowerT2);
        }
        else
        {
            Debug.LogFormat("TowerShooting: TowerPickUp: TowerTier not found ", this.gameObject);
            return;
        }
        Destroy();
    }


    public bool HasFullHelath()
    {
        return Health >= startingHealth;
    }

    private void UpdateHealtUI()
    {
        healthText.text = Mathf.FloorToInt(Health).ToString() + "/" + startingHealth.ToString();
    }

    private void OnDrawGizmos()
    {
        // Draw a red sphere at the transform's position
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawSphere(this.gameObject.transform.position, shootingRange);
    }
}
