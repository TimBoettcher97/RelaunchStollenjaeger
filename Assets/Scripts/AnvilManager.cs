using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnvilManager : MonoBehaviour
{

    [SerializeField]
    private PlayableDirector playableDirectorAnvilEnd;
    [SerializeField]
    private ExtinsionExplosion extinsionExplosion;


    // Start is called before the first frame update
    void Start()
    {
        if (!playableDirectorAnvilEnd)
        {
            playableDirectorAnvilEnd = GetComponent<PlayableDirector>();
        }
        if (playableDirectorAnvilEnd)
        {
            playableDirectorAnvilEnd.time = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerEnd()
    {
        if (playableDirectorAnvilEnd)
        {
            PlayTimeline();
        }
        else
        {
            fallbackTriggerEnd();
        }
    }
    private void fallbackTriggerEnd()
    {
        extinsionExplosion.StartScaling();
    }

    public void PauseTimeline()
    {
        playableDirectorAnvilEnd.Pause();
    }

    public void ResumeTimeline()
    {
        playableDirectorAnvilEnd.Resume();
    }

    public void PlayTimeline()
    {
        playableDirectorAnvilEnd.Play();
    }
}
