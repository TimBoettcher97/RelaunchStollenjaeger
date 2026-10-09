using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerTowerUpgrate))]
[RequireComponent(typeof(CheatMenu))]
public class Gamemanager : MonoBehaviour
{

    public static Gamemanager PublicStaricGamemanager;

    [SerializeField]
    private float Health = 10;
    
    public MaterialWallet materialWallet = new MaterialWallet()
    {
        materialSmaragd = 0, materialAmethyst= 0,materialRubin=0
    };
    /// <summary>
    /// Old
    /// </summary>
    private float oreWallet;
    /// <summary>
    /// Old
    /// </summary>
    [SerializeField]
    private Text oreWalletText;
    public BuildingWallet buildingWallet = new BuildingWallet();

    [SerializeField]
    private float nexusLife = 100;
    private float nexusLifeATMoment;
    [SerializeField]
    private Text nexusLifeText;
    [SerializeField]
    private Text nexusLifeTextPercentage;

    [SerializeField]
    public GameObject EnemyTarget;

    /// <summary>
    /// Old
    /// </summary>
    [SerializeField]
    private Text EnemySpawnText;
    /// <summary>
    /// Old
    /// </summary>
    [SerializeField]
    private Text EnemyMovementalowwed;

    [SerializeField]
    private float neededRubin = 10f;
    [SerializeField]
    private AnvilManager anvielMAnager;

    [SerializeField]
    private MaterialWallet towerT1cost = new MaterialWallet()
    {
        materialSmaragd = 10, 
        materialAmethyst = 0,
        materialRubin = 0
    };
    [SerializeField]
    private MaterialWallet towerT2cost = new MaterialWallet()
    {
        materialSmaragd = 10,
        materialAmethyst = 5,
        materialRubin = 0
    };
    [SerializeField]
    private bool NormalTimeScaling = true;
    [SerializeField]
    private bool howToisActiv;

    [Space(15f)]
    [Header("UI")]
    //[SerializeField]
    //private Text walletSmaragdText;
    //[SerializeField]
    //private Text walletAmethystText;
    //[SerializeField]
    //private Text walletRubinText;
    //[Space(7f)]
    //[SerializeField]
    //private Text TowerT1Text;
    //[SerializeField]
    //private Text upgradeKitText;
    //[Space(7f)]
    //[SerializeField]
    //private Image Win;
    //[SerializeField]
    //private Image Lose;
    [Space(7f)]
    [SerializeField]
    private TMP_Text nexusLifeTextPercentageTextMesh;
    [SerializeField]
    private Image nexusLifeImage;
    [Space(7f)]
    [SerializeField]
    private TMP_Text walletSmaragdTextTextMesh;
    [SerializeField]
    private TMP_Text walletAmethystTextTextMesh;
    [SerializeField]
    private TMP_Text walletRubinTextTextMesh;
    [Space(7f)]
    [SerializeField]
    private TMP_Text TowerT1TextTextMesh;
    [SerializeField]
    private TMP_Text upgradeKitTextTextMesh;
    [Space(7f)]
    [SerializeField]
    private GameObject Win;
    [SerializeField]
    private GameObject Lose;

    public bool allChunksOpen = false;

    public List<EnemyConntroller> EnemyControlers = new List<EnemyConntroller>();

    public bool pathfindingAllowed{ get; private set; }
    public bool enemySpawnAllowed { get; private set; }
    public delegate void OnVariableChangeDelegate(bool newBoolVal);
    public static event OnVariableChangeDelegate PathfindingAllowedDelegate;
    public static event OnVariableChangeDelegate enemySpawnDelegate;


    private void Awake()
    {
        if (PublicStaricGamemanager)
        {
            Debug.LogWarning("<color=yellow> Gamemanager: Awake: PublicStaricGamemanager has a Value| " +
                                "\n This object will be destroyed </color=yellow>", this.gameObject);
            Destroy(this.gameObject);
            return;
        }
        PublicStaricGamemanager = this;
        pathfindingAllowed = true;

        nexusLifeATMoment = nexusLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeUiEnemySpawn();
        ChangeUiEnemyPathfindingAllowed();
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    PathfindingAllowedChange(!pathfindingAllowed);
        //    ChangeUiEnemyPathfindingAllowed();

        //}
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    EnemySpawnAllowedChange(!enemySpawnAllowed);
        //    ChangeUiEnemySpawn();
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetHowTo(!howToisActiv);
        }

        WinOrLose();
    }

    private void Setup()
    {
        ChangeUiNexusLife();
        ChangeMaterialUI();
        ChangeBuildingUI();
        if (!anvielMAnager)
        {
            anvielMAnager = FindObjectOfType<AnvilManager>();
            if (!anvielMAnager)
            {
                Debug.LogErrorFormat("Gamemanager: Setup: no AnvilManager found.");
            }
        }

        SetHowTo(true);
    }

    public void PathfindingAllowedChange(bool recivedBoolState)
    {
        if (pathfindingAllowed == recivedBoolState)
        {
            Debug.LogFormat("Gamemanager: PathfindingAllowedChange: pathfindingAllowed has allready the value {0}.", recivedBoolState);
            return;
        }
        pathfindingAllowed = recivedBoolState;
        //if (PathfindingAllowedDelegate != null)
        //{
        //    PathfindingAllowedDelegate(pathfindingAllowed);
        //}
        PathfindingAllowedDelegate?.Invoke(pathfindingAllowed);
        //OnVariableChange(pathfindingAllowed);
    }
    public void EnemySpawnAllowedChange(bool recivedBoolState)
    {
        if (enemySpawnAllowed == recivedBoolState)
        {
            Debug.LogFormat("Gamemanager: EnemySpawnAllowedChange: enemySpawnAllowed has allready the value {0}.", recivedBoolState);
            return;
        }
        enemySpawnAllowed = recivedBoolState;
        enemySpawnDelegate?.Invoke(enemySpawnAllowed);
    }
    public void OreChange(float recivedOre, MaterialType recivedMaterialType)
    {
        Debug.LogFormat("Gamemanager: OreChange: Ore ({0}) change ({1})", recivedMaterialType, recivedOre);
        if (recivedMaterialType == MaterialType.Smaragd )
        {
            materialWallet.materialSmaragd += recivedOre;
        }
        else if (recivedMaterialType == MaterialType.Amethyst)
        {
            materialWallet.materialAmethyst += recivedOre;
        }
        else if (recivedMaterialType == MaterialType.Rubin)
        {
            materialWallet.materialRubin += recivedOre;
        }
        else
        {
            Debug.LogErrorFormat("Gamemanager: OreChange: Ore note found ({0})", recivedMaterialType);
            return;
        }
        ChangeMaterialUI();
    }

    public float GetOreCount(MaterialType recivedMaterialType)
    {
        if (recivedMaterialType == MaterialType.Smaragd)
        {
            return materialWallet.materialSmaragd;
        }
        else if (recivedMaterialType == MaterialType.Amethyst)
        {
            return materialWallet.materialAmethyst;
        }
        else if (recivedMaterialType == MaterialType.Rubin)
        {
            return materialWallet.materialRubin;
        }
        else
        {
            Debug.LogErrorFormat("Gamemanager: GetOreCount: Ore note found ({0})", recivedMaterialType);
            return -1;
        }
        
    }

    public void BuildingWalletChange(float recivedCount, BuildingWallet.BuildingWalletType recivedBuildingType)
    {
        Debug.LogFormat("Gamemanager: BuildingWalletChange: Type ({0}) count ({1})", recivedBuildingType, recivedCount);
        if (recivedBuildingType == BuildingWallet.BuildingWalletType.TowerT1)
        {
            buildingWallet.towerT1 += recivedCount;
            Debug.LogFormat("Gamemanager: BuildingWalletChange: VAlueAtMoment ({0}) count ({1})", recivedBuildingType, buildingWallet.towerT1);
        }
        else if (recivedBuildingType == BuildingWallet.BuildingWalletType.UpgateKit)
        {
            buildingWallet.towerUpgradeKit += recivedCount;
        }
        else
        {
            Debug.LogErrorFormat("Gamemanager: BuildingWalletChange: BuildingType ({0})", recivedBuildingType);
            return;
        }
        ChangeBuildingUI();
    }

    public float GetBuildingCount(BuildingWallet.BuildingWalletType recivedBuildingType)
    {
        if (recivedBuildingType == BuildingWallet.BuildingWalletType.TowerT1)
        {
            return buildingWallet.towerT1;
        }
        else if (recivedBuildingType == BuildingWallet.BuildingWalletType.UpgateKit)
        {
            return buildingWallet.towerUpgradeKit;
        }
        else
        {
            Debug.LogErrorFormat("Gamemanager: GetBuildingCount: BuildingType note found ({0})", recivedBuildingType);
            return -1;
        }

    }

    public void TowerPickup(float recivedHealtPercantage, BuildingWallet.BuildingWalletType recicedBuildingType)
    {
        float smaragd = 0;
        float ametist = 0;
        float rubin = 0;
        if (recicedBuildingType == BuildingWallet.BuildingWalletType.TowerT1)
        {
            smaragd = Mathf.FloorToInt( towerT1cost.materialSmaragd * recivedHealtPercantage);
            ametist = Mathf.FloorToInt(towerT1cost.materialAmethyst * recivedHealtPercantage);
            rubin = Mathf.FloorToInt(towerT1cost.materialRubin * recivedHealtPercantage);
            
        }
        else if (recicedBuildingType == BuildingWallet.BuildingWalletType.TowerT2)
        {
            smaragd = Mathf.FloorToInt(towerT2cost.materialSmaragd * recivedHealtPercantage);
            ametist = Mathf.FloorToInt(towerT2cost.materialAmethyst * recivedHealtPercantage);
            rubin = Mathf.FloorToInt(towerT2cost.materialRubin * recivedHealtPercantage);

        }
        else
        {
            return;
        }

        if (smaragd > 0)
        {
            OreChange(smaragd, MaterialType.Smaragd);
        }
        if (ametist > 0)
        {
            OreChange(ametist, MaterialType.Amethyst);
        }
        if (rubin > 0)
        {
            OreChange(rubin, MaterialType.Rubin);
        }

    }

    public void LebenAbziehen(float recivedDamage)
    {
        Health -= recivedDamage;
    }

    public void NexusLifeChange(float recivedLifeChange)
    {
        nexusLifeATMoment += recivedLifeChange;
        ChangeUiNexusLife();
    }

    public void CheckForAnvilEnd()
    {
        if (GetOreCount(MaterialType.Rubin) >= neededRubin)
        {
            OreChange(-neededRubin, MaterialType.Rubin);
            TriggerEnd();
        }
        else
        {
            Debug.LogErrorFormat("Gamemanager: CheckForAnvilEnd: not enough ({0})", MaterialType.Rubin);
        }
    }
    public void TriggerEnd()
    {      
        if (anvielMAnager)
        {
            anvielMAnager.TriggerEnd();
        }
        PathfindingAllowedChange(false);
        ChangeUiEnemyPathfindingAllowed();
        EnemySpawnAllowedChange(false);
        ChangeUiEnemySpawn();
    }
    //public void PauseTimeline()
    //{
    //    playableDirectorAnvilEnd.Pause();
    //}

    //public void ResumeTimeline()
    //{
    //    playableDirectorAnvilEnd.Resume();
    //}

    //public void PlayTimeline()
    //{
    //    playableDirectorAnvilEnd.Play();
    //}


    public void AddEnemxControler(EnemyConntroller recivedEnemyController)
    {
        EnemyControlers.Add(recivedEnemyController);
    }
    public void RemoveEnemxControler(EnemyConntroller recivedEnemyController)
    {
        EnemyControlers.Remove(recivedEnemyController);
    }

    //public EnemyConntroller GetColosestContoler(Vector3 recivedPosition)
    //{

    //}

    public void RestartScene()
    {
        Debug.Log("Gamemanager: RestartScene");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Gamemanager: QuitGame");
        Application.Quit();
    }

    public void TimeScaling()
    {
        Debug.Log("Gamemanager: Gamemanager: !" + NormalTimeScaling);
        TimeScaling(!NormalTimeScaling);
    }
    public void TimeScaling(bool reciveNormalTimeScaling = true)
    {
        if (reciveNormalTimeScaling)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0.01f;
        }
    }

    public void SetHowTo(bool reciveHowToStat)
    {
        howToisActiv = reciveHowToStat;
        TimeScaling(!reciveHowToStat);
        HowToManager.PublicStaricHowToManager.cangeHowToState(reciveHowToStat);
        PlayerManager.mainStaticPlayerManager.SetMousLock(!reciveHowToStat);
    }



    #region UI

    private void ChangeUiNexusLife()
    {
        //if (nexusLifeText)
        //{
        //    nexusLifeText.text = Mathf.FloorToInt(nexusLifeATMoment).ToString();
        //}
        //if (nexusLifeTextPercentage)
        //{
        //    nexusLifeTextPercentage.text = Mathf.FloorToInt(nexusLifeATMoment * 100 / nexusLife).ToString() + " %";
        //}
        if (nexusLifeTextPercentageTextMesh)
        {
            nexusLifeTextPercentageTextMesh.text = Mathf.FloorToInt(nexusLifeATMoment * 100 / nexusLife).ToString() + " %";
        }
        if (nexusLifeImage)
        {
            nexusLifeImage.fillAmount = /*Mathf.FloorToInt(*/nexusLifeATMoment /** 100*/ / nexusLife/*)*/;
        }
        
    }

    public void ChangeUiEnemySpawn()
    {
        EnemySpawnText.text = enemySpawnAllowed.ToString();
    }
    public void ChangeUiEnemyPathfindingAllowed()
    {
        EnemyMovementalowwed.text = pathfindingAllowed.ToString();
    }

    private void UIDisplayWin()
    {
        Win.gameObject.SetActive(true);
    }

    private void UIDisplayLose()
    {
        Lose.gameObject.SetActive(true);
        TimeScaling(false);
        PlayerManager.mainStaticPlayerManager.SetMousLock(false);
    }

    public void ChangeMaterialUI()
    {
        //if (walletSmaragdText)
        //{
        //    walletSmaragdText.text = Mathf.FloorToInt(materialWallet.materialSmaragd).ToString();
        //}
        if (walletSmaragdTextTextMesh)
        {
            walletSmaragdTextTextMesh.text = Mathf.FloorToInt(materialWallet.materialSmaragd).ToString();
        }
        else
        {
            Debug.LogWarningFormat("Gamemanager: ChangeMaterialUI: no UI found (walletSmaragdText)");
        }

        //if (walletAmethystText)
        //{
        //    walletAmethystText.text = Mathf.FloorToInt(materialWallet.materialAmethyst).ToString();
        //}
        if (walletAmethystTextTextMesh)
        {
            walletAmethystTextTextMesh.text = Mathf.FloorToInt(materialWallet.materialAmethyst).ToString();
        }
        else
        {
            Debug.LogWarningFormat("Gamemanager: ChangeMaterialUI: no UI found (walletAmethystText)");
        }

        //if (walletRubinText)
        //{
        //    walletRubinText.text = Mathf.FloorToInt(materialWallet.materialRubin).ToString();
        //}
        if (walletRubinTextTextMesh)
        {
            walletRubinTextTextMesh.text = Mathf.FloorToInt(materialWallet.materialRubin).ToString();
        }
        else
        {
            Debug.LogWarningFormat("Gamemanager: ChangeMaterialUI: no UI found (walletRubinText)");
        }
    }
    public void ChangeBuildingUI()
    {
        //if (TowerT1Text)
        //{
        //    TowerT1Text.text = buildingWallet.towerT1.ToString();
        //}
        if (TowerT1TextTextMesh)
        {
            TowerT1TextTextMesh.text = buildingWallet.towerT1.ToString();
        }
        else
        {
            Debug.LogWarningFormat("Gamemanager: ChangeMaterialUI: no UI found (TowerT1Text)");
        }

        //if (upgradeKitText)
        //{
        //    upgradeKitText.text = buildingWallet.towerUpgradeKit.ToString();
        //}
        if (upgradeKitTextTextMesh)
        {
            upgradeKitTextTextMesh.text = buildingWallet.towerUpgradeKit.ToString();
        }
        else
        {
            Debug.LogWarningFormat("Gamemanager: ChangeMaterialUI: no UI found (upgradeKitText)");
        }
    }
    public void WinOrLose()
    {
        if (nexusLifeATMoment <= 0)
        {
            UIDisplayLose();
        }
        if (EnemyControlers.Count < 1 && allChunksOpen)
        {
            UIDisplayWin();
        }
    }

    public void ForceWinORLoss(bool reciveWin)
    {
        if (reciveWin)
        {
            UIDisplayWin();
        }
        else
        {
            UIDisplayLose();
        }
        TimeScaling(false);
        PlayerManager.mainStaticPlayerManager.SetMousLock(false);
    }

    #endregion UI
}
