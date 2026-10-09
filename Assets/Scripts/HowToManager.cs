using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToManager : MonoBehaviour
{
    public static HowToManager PublicStaricHowToManager;


    [SerializeField]
    private bool howToState;
    [SerializeField]
    private GameObject howToPannel;
    [SerializeField]
    private List<GameObject> HowToSits = new List<GameObject>();
    [SerializeField]
    private int currentSite = 0;
    [SerializeField]
    private GameObject arrowLeft;
    [SerializeField]
    private GameObject arrowRight;

    private void Awake()
    {
        if (PublicStaricHowToManager)
        {
            Debug.LogWarning("<color=yellow> HowToManager: Awake: PublicStaricHowToManager has a Value| " +
                                "\n This object will be destroyed </color=yellow>", this.gameObject);
            Destroy(this.gameObject);
            return;
        }
        PublicStaricHowToManager = this;

        SetCurrentSite(0);
        cangeHowToState(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCurrentSite(int recivedNeededSite = 0)
    {
        if (HowToSits.Count >0 )
        {
            currentSite = recivedNeededSite;

            for (int i = 0; i < HowToSits.Count; i++)
            {
                if (i == recivedNeededSite)
                {
                    HowToSits[i].SetActive(true);
                }
                else
                {
                    HowToSits[i].SetActive(false);
                }
            }
        }
        if (recivedNeededSite == 0)
        {
            arrowLeft.SetActive(false);
        }
        else
        {
            arrowLeft.SetActive(true);
        }
        if (recivedNeededSite >= HowToSits.Count-1)
        {
            arrowRight.SetActive(false);
        }
        else
        {
            arrowRight.SetActive(true);
        }
    }

    

    public void NextSite(int recivedWichDirection )
    {
        currentSite += recivedWichDirection;
        currentSite = Mathf.Clamp(currentSite, 0, HowToSits.Count - 1);
        SetCurrentSite(currentSite);
    }

    //public void cangeHowToState()
    //{
    //    cangeHowToState(!howToState);
    //}
    public void cangeHowToStateButton()
    {
        Gamemanager.PublicStaricGamemanager.SetHowTo(false);
    }

    public void cangeHowToState(bool recivedState)
    {
        howToState = recivedState;
        if (recivedState)
        {
            //Gamemanager.PublicStaricGamemanager.TimeScaling(false);
            SetCurrentSite(0);
        }
        else
        {
            Debug.Log("bumbs" + "maek normal timeScale");
            //Gamemanager.PublicStaricGamemanager.TimeScaling(true);
            //PlayerManager.mainStaticPlayerManager.SetMousLock(true);
        }
        howToPannel.SetActive(recivedState);
    }

    private void OnEnable()
    {
        SetCurrentSite(0);
    }
}
