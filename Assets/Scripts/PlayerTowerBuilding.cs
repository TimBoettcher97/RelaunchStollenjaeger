using System.Collections;
using System.Collections.Generic;
using System.Drawing;
//using UnityEditor.Search;
using UnityEngine;

public class PlayerTowerBuilding : MonoBehaviour
{
    public LayerMask layermask;
    [SerializeField]
    private GameObject towerPlacingShootingPoint;
    [SerializeField]
    private float towerPlacingRayCastRadius = 0.5f;
    [SerializeField]
    private float towerPlacingRange = 10f;

    public GameObject TowerParent;
    public GameObject TowerPrefab;
    public GameObject Spawnpoint;
    [SerializeField]
    private MaterialType buildingMaterial = MaterialType.Smaragd;
    [SerializeField]
    private float buildingCost = 5f;
    public GameObject holoPrefabHolder;
    public GameObject holoPrefabGreen;
    public GameObject holoPrefabRed;
    [SerializeField]
    private bool buildingAlowed = true;
    public PlayerTowerBuildChecker Checker;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            holoPrefabHolder.SetActive(true);
            MoveBuildingpoint();
            CheckAllowedBuilding();
            ShowHolo(true);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            MoveBuildingpoint();
            CheckAllowedBuilding();

            if (Input.GetButtonDown("Fire1"))
            {
                MoveBuildingpoint();
                CheckAllowedBuilding();
                if (buildingAlowed)
                {
                    BuildTower();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            ShowHolo(false);
            holoPrefabHolder.SetActive(false);
        }
        
    }

    private void MoveBuildingpoint()
    {
        Vector3 spawnpoint = new Vector3( towerPlacingShootingPoint.transform.position.x,0f, towerPlacingShootingPoint.transform.position.z);
        spawnpoint += (new Vector3(towerPlacingShootingPoint.transform.forward.x, 0, towerPlacingShootingPoint.transform.forward.z).normalized * towerPlacingRange);
        //Debug.Log("PlayerTowerBuilding: MoveBuildingpoint");
        RaycastHit raycasthit;
        if (Physics.SphereCast(towerPlacingShootingPoint.transform.position,
                                towerPlacingRayCastRadius,
                                towerPlacingShootingPoint.transform.forward,
                                out raycasthit,
                                towerPlacingRange,
                                layermask))
        {
            Debug.LogFormat("PlayerTowerBuilding: MoveBuildingpoint: " + raycasthit.transform.gameObject.name, raycasthit.transform.gameObject);
            spawnpoint = new Vector3(raycasthit.point.x, 0, raycasthit.point.z);
        }
        Spawnpoint.transform.position = spawnpoint;
    }

    private void BuildTower()
    {
        float returenedOreValue = Gamemanager.PublicStaricGamemanager.GetBuildingCount(BuildingWallet.BuildingWalletType.TowerT1);
        if (returenedOreValue >= 1)
        {
            Debug.LogFormat("PlayerTowerBuilding: BuildTower: TowerSpawn");
            GameObject spawenedTower = Instantiate(TowerPrefab, new Vector3(Spawnpoint.transform.position.x, 0f, Spawnpoint.transform.position.z), new Quaternion());
            spawenedTower.transform.parent = TowerParent.transform;
            Gamemanager.PublicStaricGamemanager.BuildingWalletChange(-1, BuildingWallet.BuildingWalletType.TowerT1);
        }
        else
        {
            Debug.LogFormat("PlayerTowerBuilding: BuildTower: No TowerSpawn");
        }

        //float returenedOreValue = Gamemanager.PublicStaricGamemanager.GetOreCount(buildingMaterial);
        //if (returenedOreValue >= 0 && returenedOreValue >= buildingCost)
        //{
        //    Debug.LogFormat("PlayerTowerBuilding: BuildTower: TowerSpawn");
        //    GameObject spawenedTower = Instantiate(TowerPrefab, new Vector3(Spawnpoint.transform.position.x,0f, Spawnpoint.transform.position.z) , new Quaternion());
        //    spawenedTower.transform.parent = TowerParent.transform;
        //    Gamemanager.PublicStaricGamemanager.OreChange(-buildingCost, buildingMaterial);
        //}
        //else
        //{
        //    Debug.LogFormat("PlayerTowerBuilding: BuildTower: No TowerSpawn");
        //}
    }
    private void ShowHolo(bool recivedState)
    {

        if (holoPrefabHolder)
        {
            holoPrefabHolder.SetActive(recivedState);
        }
    }
    private void CheckAllowedBuilding()
    {
        int collisions = Checker.GetCollisionNumbre();
        if (collisions == 0)
        {
            buildingAlowed = true;
            holoPrefabGreen.SetActive(true);
            holoPrefabRed.SetActive(false);
        }
        else if(collisions >= 0)
        {
            buildingAlowed = false;
            holoPrefabGreen.SetActive(false);
            holoPrefabRed.SetActive(true);
        }
        else
        {
            buildingAlowed = false;
            Debug.LogFormat("PlayerTowerBuilding: CheckAllowedBuilding: Collision is corrupted ", this.gameObject.name);
            //Checker.enabled = false;

        }

    }
}
