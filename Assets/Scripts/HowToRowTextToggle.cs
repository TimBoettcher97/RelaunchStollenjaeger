using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToRowTextToggle : MonoBehaviour
{
    [SerializeField]
    private bool rawTextState;
    [SerializeField]
    private List<GameObject> rawTexts = new List<GameObject>();
    [SerializeField]
    private List<GameObject> otherRawTexts = new List<GameObject>();

    private void Awake()
    {
        
    }

    public void ToggleRawText()
    {
        rawTextState = !rawTextState;

        if (rawTexts.Count > 0)
        {
            for (int i = 0; i < rawTexts.Count; i++)
            {
                rawTexts[i].SetActive(rawTextState);
            }
        }
        if (rawTextState)
        {
            if (otherRawTexts.Count > 0)
            {
                for (int i = 0; i < otherRawTexts.Count; i++)
                {
                    otherRawTexts[i].SetActive(!rawTextState);
                }
            }
        }
        
    }

    private void OnEnable()
    {
        rawTextState = true;
        ToggleRawText();
    }

}
