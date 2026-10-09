using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager mainStaticPlayerManager;
    [SerializeField] private KeyCode MouselockKey;
    [SerializeField]
    private bool currentState;

    private void Awake()
    {
        if (mainStaticPlayerManager)
        {
            Destroy(this);
            return;
        }
        mainStaticPlayerManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(MouselockKey))
    //    {
    //        ToggelMousLock();
    //    }
    //}
    private void ToggelMousLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void SetMousLock(bool recivedStat)
    {
        currentState = recivedStat;
        if (!currentState)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    public void SetMousLock()
    {
        SetMousLock(!currentState);
    }


}
