using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public LayerMask layermask;
    [SerializeField]
    private GameObject shootingPoint;
    [SerializeField]
    private float ShootingRange = 10f;
    [SerializeField]
    private float rayCastRadius = 0.5f;
    [SerializeField]
    private float shootingDamage = 10f;
    [SerializeField]
    private string oreTack = "Ore";
    [SerializeField]
    private string EnemyTack = "Enemy";
    [SerializeField]
    private string borderTack = "Border";
    [SerializeField]
    private string towerTack = "Tower";
    [SerializeField]
    private string anvilTack = "Anvil";

    public bool newShootingSystem;
    public bool axeIsActive;
    public bool AcrossbowIsActive;

    public GameObject axeGameObject;
    public GameObject firstSwingPosition;
    public GameObject secoundSwingPosition;
    public float swingTime = 1f;
    public float swingWaitingTime = 0.25f;
    public bool swingIsActive;
    public float swingTimeAtMoment = 1f;
    private float swingProcentage;

    [SerializeField]
    private float realShootingDamage = 10f;
    public GameObject shootAxeGameObject;
    public GameObject shootingpointAxeGameObject;
    [SerializeField]
    private float shootingWaitingMax = 0.5f;
    private float shootingWaitingAtMoment;
    [SerializeField]
    private int ammunitionMax = 7;
    private int ammunitionAtMoment;
    [SerializeField]
    private float reloadWaitingMax = 2f;
    private float reloadWaitingAtMoment;
    [SerializeField]
    private bool AxeMode = true;
    [SerializeField]
    private bool ShootMode;
    public GameObject bulletPrefab;

    [SerializeField]
    private float repareCost = 1f;
    [SerializeField]
    private float repareHealth = 2f;
    public float TowerEigendamage = 20;

    [Space(3)]
    [Header("Animation")]
    [SerializeField] Animator pickaxeAnimator;
    [Header("Audio")]
    [SerializeField] AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (true)
        {
            ammunitionMax = -1;
        }
        ReloadFinished();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ButtenCkeck();
            TowerPickUpCheck();
            AnvilCheck();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TowerRepareCkeck();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            TowerUpdateCkeck();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeShootMode(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeShootMode(false);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ChangeShootMode(true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            ChangeShootMode(false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (AxeMode)
            {
                if (newShootingSystem)
                {
                    SwingForward();
                }
                else
                {
                    TriggerSlashing();
                }
            }
            else
            {
                TriggerShooting();
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopMyCorotien();
        }

        if (shootingWaitingAtMoment > 0f)
        {
            shootingWaitingAtMoment -= Time.deltaTime;
        }

        if (ammunitionMax > 0)
        {


            if (Input.GetKeyDown(KeyCode.R) && reloadWaitingAtMoment <= 0f)
            {
                StartReload();
            }
            if (reloadWaitingAtMoment > 0f)
            {
                reloadWaitingAtMoment -= Time.deltaTime;
                if (reloadWaitingAtMoment <= 0f)
                {
                    ReloadFinished();
                }
            }
        }
    }

    private bool ButtenCkeck()
    {
        Debug.Log("PlayerShooting: Buttoncheck");
        RaycastHit raycasthit;
        if (Physics.SphereCast(shootingPoint.transform.position + (shootingPoint.transform.forward * rayCastRadius * -1f),
                                 rayCastRadius,
                                 shootingPoint.transform.forward,
                                 out raycasthit,
                                 ShootingRange,
                                 layermask))
        {
            Debug.Log("PlayerShooting: Raycast Hit"+ raycasthit.transform.gameObject.name);
            if (raycasthit.transform.gameObject.GetComponent<DTDButton>())
            {
                Debug.Log("PlayerShooting: if Statement");
                raycasthit.transform.gameObject.GetComponent<DTDButton>().Click();
                return true;
            }
        }
        return false;
    }
    private bool TowerPickUpCheck()
    {
        Debug.Log("PlayerShooting: TowerPickUpCheck");
        RaycastHit raycasthit;
        if (Physics.SphereCast(shootingPoint.transform.position + (shootingPoint.transform.forward * rayCastRadius * -1f),
                              rayCastRadius,
                              shootingPoint.transform.forward,
                              out raycasthit,
                              ShootingRange,
                              layermask))
        {
            if (raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>())
            {
                raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>().TowerPickUp();
                return true;
            }
        }
        return false;
    }

    private void AnvilCheck()
    {
        Debug.Log("PlayerShooting: AnvilCheck");
        RaycastHit raycasthit;
        if (Physics.SphereCast(shootingPoint.transform.position + (shootingPoint.transform.forward * rayCastRadius * -1f),
                                 rayCastRadius,
                                 shootingPoint.transform.forward,
                                 out raycasthit,
                                 ShootingRange,
                                 layermask))
        {
            Debug.Log("PlayerShooting: AnvilCheck Raycast Hit" + raycasthit.transform.gameObject.name);
            if (raycasthit.transform.gameObject.tag == anvilTack)
            {
                // if AnvilDamaged -> Repair
                Debug.Log("PlayerShooting: if Statement");
                Gamemanager.PublicStaricGamemanager.CheckForAnvilEnd();
            }
        }
    }

    private void TowerRepareCkeck()
    {
        RaycastHit raycasthit;
        if (Physics.SphereCast(shootingPoint.transform.position + (shootingPoint.transform.forward * rayCastRadius * -1f),
                                 rayCastRadius,
                                 shootingPoint.transform.forward,
                                 out raycasthit,
                                 ShootingRange,
                                 layermask))
        {
            if (raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>())
            {
                raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>().TowerRepare(repareCost, repareHealth);
            }
        }
    }
    private void TowerUpdateCkeck()
    {
        RaycastHit raycasthit;
        if (Physics.SphereCast(shootingPoint.transform.position + (shootingPoint.transform.forward * rayCastRadius * -1f),
                                 rayCastRadius,
                                 shootingPoint.transform.forward,
                                 out raycasthit,
                                 ShootingRange,
                                 layermask))
        {
            if (raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>())
            {
                PlayerTowerUpgrate.PublicStaricPlayerTowerUpgrate.TowerUpdate(raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>());
            }
        }
    }

    private void TriggerSlashing()
    {
        Debug.Log("PlayerShooting: TriggerSlashing");
        RaycastHit raycasthit;
        if (Physics.SphereCast(shootingPoint.transform.position+(shootingPoint.transform.forward* rayCastRadius*-1f), 
                                rayCastRadius, 
                                shootingPoint.transform.forward, 
                                out raycasthit, 
                                ShootingRange,
                                layermask))
        {
            pickaxeAnimator.SetTrigger("Hit");
            Debug.Log("PlayerShooting: TriggerSlashing: RaycastHit");
            if (raycasthit.transform.tag == EnemyTack)
            {
                Debug.Log("PlayerShooting: TriggerSlashing: Enemy");
                raycasthit.transform.gameObject.GetComponentInParent<EnemyConntroller>().EnemyHit(shootingDamage/10f);
            }
            else if(raycasthit.transform.tag == oreTack)
            {
                Debug.Log("PlayerShooting: TriggerSlashing: Ore");
                raycasthit.transform.gameObject.GetComponent<OreField>().OreHit();
                PLayAudio("HitCrystal");
            }
            else if (raycasthit.transform.tag == borderTack)
            {
                Debug.Log("PlayerShooting: TriggerSlashing: Border");
                raycasthit.transform.gameObject.GetComponentInParent<ChunkBorderControler>().BorderPiceHit(raycasthit.transform.gameObject, shootingDamage);
                PLayAudio("HitStone");
            }
            else if (raycasthit.transform.tag == towerTack)
            {
                Debug.Log("PlayerShooting: TriggerSlashing: Tower");
                raycasthit.transform.gameObject.GetComponentInParent<TowerShooting>().EnemyHit(TowerEigendamage);
                PLayAudio("HitStone");
            }
            else
            {
                Debug.LogFormat("PlayerShooting: TriggerSlashing: " + raycasthit.transform.gameObject.name, raycasthit.transform.gameObject);
                PLayAudio("HitStone");
            }
        }
    }
    private void TriggerShooting()
    {
        Debug.Log("PlayerShooting: TriggerShooting");
        

        if (shootingWaitingAtMoment <=0f)
        {
            if (ammunitionMax <= 0 || (ammunitionMax >0 && ammunitionAtMoment >0))
            {
                Shoot();
            }
            else if (ammunitionMax > 0 && ammunitionAtMoment <= 0)
            {
                Debug.Log("PlayerShooting: TriggerShooting: No ammunition");
                //ToDo: FeedbackSound
                return;
            }
            //else
            //{
            //    Shoot();
            //}

        }
        else
        {
            Debug.LogFormat("PlayerShooting: shootingWaitingAtMoment ({0}) > 0", shootingWaitingAtMoment);
        }
        //projektile.transform.parent = towerShootingPoint.transform;

    }
    private void Shoot()
    {
        Debug.Log("PlayerShooting: Shoot");
        if (ammunitionMax >0)
        {
            ammunitionAtMoment--;
        }
        GameObject projektile = Instantiate(bulletPrefab, shootingpointAxeGameObject.transform.position, shootingpointAxeGameObject.transform.rotation);
        PLayAudio("Shot");
        shootingWaitingAtMoment = shootingWaitingMax;
    }
    private void StartReload()
    {
        reloadWaitingAtMoment = reloadWaitingMax;
    }
    private void ReloadFinished()
    {
        ammunitionAtMoment = ammunitionMax;
    }

    public void SwingForward()
    {
        if (!swingIsActive)
        {
            swingIsActive = true;
            StartCoroutine(SwingAnimation());
        }

    }
    public void SwingWait()
    {

    }
    public void SwingBAckward()
    {

    }

    public void StopMyCorotien()
    {

        StopAllCoroutines();
        swingIsActive = false;
        //swingMovment(firstSwingPosition, secoundSwingPosition, 0f);
    }
    
    public IEnumerator SwingAnimation() 
    {
        pickaxeAnimator.SetTrigger("StartSwing");
        
        yield return new WaitForSeconds(1f + swingWaitingTime);
        swingIsActive = false;
    }

    //public IEnumerator SwingAnimation()
    //{
    //    float recivedPercentage = 0f;
    //    float calculationFloat = 1 / swingTime;

    //    while (recivedPercentage <1f)
    //    {
    //        swingMovment(firstSwingPosition, secoundSwingPosition, recivedPercentage);
    //        recivedPercentage += (calculationFloat * Time.deltaTime);

    //       yield return null;
    //    }
    //    TriggerSlashing();
    //    yield return new WaitForSeconds(swingWaitingTime);
    //    while (recivedPercentage > 0f)
    //    {
    //        swingMovment(firstSwingPosition, secoundSwingPosition, recivedPercentage);
    //        recivedPercentage -= (calculationFloat * 1.5f * Time.deltaTime);

    //        yield return null;
    //    }
    //    swingIsActive = false;

    //}

    //private void swingMovment(GameObject recivedStartingPosition, GameObject recivedEndPosition, float recivedPercentage)
    //{
    //    axeGameObject.transform.rotation = Quaternion.Slerp(recivedStartingPosition.transform.rotation, recivedEndPosition.transform.rotation, recivedPercentage);
    //    axeGameObject.transform.position = Vector3.Slerp(recivedStartingPosition.transform.position, recivedEndPosition.transform.position, recivedPercentage);
    //}

    
    private void ChangeShootMode(bool reciedAxeMode)
    {
        if (reciedAxeMode)
        {
            StopMyCorotien();
            AxeMode = false;
            ShootMode = true;
            axeGameObject.SetActive(false);
            shootAxeGameObject.SetActive(true);
        }
        else
        {
            AxeMode = true;
            ShootMode = false;
            axeGameObject.SetActive(true);
            shootAxeGameObject.SetActive(false);
        }
    }

    
    void PLayAudio(string name)
    {
        audioManager.Play(name);
    }
    


    private void OnDrawGizmos()
    {
        // Draw a red sphere at the transform's position
        Gizmos.color = new Color(0f, 0.5f, 0.5f, 0.25f);
        Gizmos.DrawSphere(shootingPoint.transform.position, rayCastRadius);
        Gizmos.color = new Color(0f, 0.5f, 0.75f, 0.125f);
        Gizmos.DrawSphere((shootingPoint.transform.position +(shootingPoint.transform.forward* ShootingRange)), rayCastRadius);
    }
}
