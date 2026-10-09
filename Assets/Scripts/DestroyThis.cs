using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    
    private void Update()
    {
        if (!ChildrenActive())
        {
            Destroy(gameObject);
        }
    }
    bool ChildrenActive()
    {
        int inactive = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return true;
            }
            else
            {
                inactive++;
            }
        }
        if (inactive == gameObject.transform.childCount)
        {
            return false;
        }
        else return true;
    }
}
