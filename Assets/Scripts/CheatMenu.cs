using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.Text TimeScaleText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTimeScaleUI();
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
        //{
        //    if (Input.GetKeyDown(keycode))
        //    {
        //        Debug.Log("Qwerty key is : " + keycode);
        //    }
        //}
        if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Gamemanager.PublicStaricGamemanager.RestartScene();
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Time.timeScale += 0.1f;
                UpdateTimeScaleUI();
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                Time.timeScale -= 0.1f;
                UpdateTimeScaleUI();
            }
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Time.timeScale = 1f;
                UpdateTimeScaleUI();
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Gamemanager.PublicStaricGamemanager.OreChange(100, MaterialType.Smaragd);
                Gamemanager.PublicStaricGamemanager.OreChange(100, MaterialType.Amethyst);
                Gamemanager.PublicStaricGamemanager.OreChange(100, MaterialType.Rubin);
                Gamemanager.PublicStaricGamemanager.ChangeMaterialUI();
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Gamemanager.PublicStaricGamemanager.BuildingWalletChange(100, BuildingWallet.BuildingWalletType.TowerT1);
                Gamemanager.PublicStaricGamemanager.BuildingWalletChange(100, BuildingWallet.BuildingWalletType.UpgateKit);
                Gamemanager.PublicStaricGamemanager.ChangeBuildingUI();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Gamemanager.PublicStaricGamemanager.PathfindingAllowedChange(!Gamemanager.PublicStaricGamemanager.pathfindingAllowed);
                Gamemanager.PublicStaricGamemanager.ChangeUiEnemyPathfindingAllowed();

            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Gamemanager.PublicStaricGamemanager.EnemySpawnAllowedChange(!Gamemanager.PublicStaricGamemanager.enemySpawnAllowed);
                Gamemanager.PublicStaricGamemanager.ChangeUiEnemySpawn();
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Gamemanager.PublicStaricGamemanager.TriggerEnd();
            }
        }
    }
    private void UpdateTimeScaleUI()
    {
        if (TimeScaleText)
        {
            TimeScaleText.text = Time.timeScale.ToString();
        }
    }
}
