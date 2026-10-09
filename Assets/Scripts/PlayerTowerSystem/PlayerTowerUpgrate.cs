using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerUpgrate : MonoBehaviour
{
    public static PlayerTowerUpgrate PublicStaricPlayerTowerUpgrate;

    [SerializeField]
    private GameObject towerT1;
    [SerializeField]
    private GameObject towerT2;
    [SerializeField]
    private GameObject towerT3;

    [Space(7f)]
    [SerializeField]
    private BuildingWallet.BuildingWalletType upgradeType = BuildingWallet.BuildingWalletType.UpgateKit;
    private int upgradeCost = 1;

    private void Awake()
    {
        if (PublicStaricPlayerTowerUpgrate)
        {
            Debug.LogWarning("<color=yellow> PlayerTowerUpgrate: Awake: PublicStaricPlayerTowerUpgrate has a Value| " +
                                "\n This object will be destroyed </color=yellow>", this.gameObject);
            Destroy(this.gameObject);
            return;
        }
        PublicStaricPlayerTowerUpgrate = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TowerUpdate(TowerShooting recivedTowerShooting)
    {
        if (!recivedTowerShooting.HasFullHelath())
        {
            Debug.LogFormat("PlayerTowerUpgrate: TowerUpdate: Tower({0}) doesn't have full health", recivedTowerShooting.name, recivedTowerShooting.gameObject);
            // ToDoFeedback:
            return;
        }

        float walletvalue = Gamemanager.PublicStaricGamemanager.GetBuildingCount(upgradeType);
        if (walletvalue >= upgradeCost && recivedTowerShooting.myTowerTier == TowerShooting.TowerTier.T1)
        {
            Debug.LogFormat("PlayerTowerUpgrate: TowerUpdate: Tower:({0})", recivedTowerShooting.gameObject);
            Gamemanager.PublicStaricGamemanager.BuildingWalletChange(-upgradeCost, BuildingWallet.BuildingWalletType.UpgateKit);

            Transform transforms = recivedTowerShooting.gameObject.transform;
            Destroy(recivedTowerShooting.gameObject);

            GameObject newTower = Instantiate(towerT2);
            newTower.transform.position = transforms.position;
            newTower.transform.rotation = transforms.rotation;
            newTower.transform.parent = transforms.parent;
        }
        else
        {
            Debug.LogFormat("TowerShooting: TowerRepare: Not enough {0}. Upgrade:{1} Wallet:{2} ", BuildingWallet.BuildingWalletType.UpgateKit, upgradeCost, walletvalue);
            // ToDoFeedback:
        }
    }
}
