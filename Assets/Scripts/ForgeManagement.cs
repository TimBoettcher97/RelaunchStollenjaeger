using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeManagement : MonoBehaviour
{
    [SerializeField]
    private float Towercost = 5;
    [SerializeField]
    private float UpgrateCost = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TowerBuy()
    {
        float walletvalue = Gamemanager.PublicStaricGamemanager.GetOreCount(MaterialType.Smaragd);
        if (walletvalue >= Towercost)
        {
            Debug.LogFormat("ForgeManagement: TowerBuy: Tower:({0}); Towercost:({1}) ", BuildingWallet.BuildingWalletType.TowerT1, Towercost, this.gameObject);
            Gamemanager.PublicStaricGamemanager.OreChange(-Towercost, MaterialType.Smaragd);
            Gamemanager.PublicStaricGamemanager.BuildingWalletChange(1, BuildingWallet.BuildingWalletType.TowerT1);
        }
        else
        {
            Debug.LogFormat("ForgeManagement: TowerBuy: Not enough {0}. Towercost:{1} Wallet:{2} ", MaterialType.Smaragd, Towercost, walletvalue, this.gameObject);
            // ToDoFeedback:
        }
        
    }
    public void UpgrateBuy()
    {
        float walletvalue = Gamemanager.PublicStaricGamemanager.GetOreCount(MaterialType.Amethyst);
        if (walletvalue >= UpgrateCost)
        {
            Debug.LogFormat("ForgeManagement: UpgrateBuy: Upgrade:({0}); UpgrateCost:({1}) ", BuildingWallet.BuildingWalletType.UpgateKit, UpgrateCost, this.gameObject);
            Gamemanager.PublicStaricGamemanager.OreChange(-UpgrateCost, MaterialType.Amethyst);
            Gamemanager.PublicStaricGamemanager.BuildingWalletChange(1, BuildingWallet.BuildingWalletType.UpgateKit);
        }
        else
        {
            Debug.LogFormat("ForgeManagement: UpgrateBuy: Not enough {0}. UpgrateCost:{1} Wallet:{2} ", MaterialType.Amethyst, UpgrateCost, walletvalue, this.gameObject);
            // ToDoFeedback:
        }
    }
}
