using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestinChunkTrigger : MonoBehaviour, IBorderBreche
{
    public AudioSource TestAudio;
    public GameObject TestObjekt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BorderBreche()
    {
        TestAudio.Play();
        TestObjekt.SetActive(!(TestObjekt.activeSelf));
    }
}
