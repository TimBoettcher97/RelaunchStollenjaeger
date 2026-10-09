using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTDButton : MonoBehaviour
{
    [SerializeField]
    private bool buyTower;
    [SerializeField]
    private bool buyUpgrad;
    [SerializeField]
    private ForgeManagement forgeManagement;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        Debug.LogFormat("DTDButton: Click: buyTower:({0}); buyUpgrad:({1}) ", buyTower, buyUpgrad, this.gameObject);

        if (buyTower)
        {
            forgeManagement.TowerBuy();
        }
        else if (buyUpgrad) 
        {
            forgeManagement.UpgrateBuy();
        }
    }
}
