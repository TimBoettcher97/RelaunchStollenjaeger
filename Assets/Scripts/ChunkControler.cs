using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkControler : MonoBehaviour
{
    [SerializeField]
    private ChunkBorderControler[] borderController;
    
    public bool ChunktIsOpen;

    [SerializeField]
    private GameObject[] BrecheReciverObjekts;
    //private IBorderBreche[] BrecheReciver;
    private List<IBorderBreche> brecheReciverList = new List<IBorderBreche>();



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < borderController.Length; i++)
        {
            if (borderController[i] != null)
            {
                borderController[i].AddChunkController(this);
            }

        }

        foreach (GameObject forechObjekt in BrecheReciverObjekts)
        {
            if (forechObjekt.TryGetComponent(out IBorderBreche interfaceObjekt))
            {
                brecheReciverList.Add(interfaceObjekt);
            }
        }
        Debug.LogFormat("ChunkControler: Start: {0} BorderBrechInterfaces were founed", brecheReciverList.Count,this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReciveBorderBreach()
    {
        Debug.Log("ChunkControler: ReciveBorderBreach", this.gameObject);
        if (ChunktIsOpen)
        {
            Debug.Log("ChunkControler: ReciveBorderBreach: Chunk is alredy Open");
            return;
        }

        ChunktIsOpen = true;
        foreach (IBorderBreche forechObjekt in brecheReciverList)
        {
            forechObjekt.BorderBreche();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0.25f, 0.5f, 0.25f);
        Gizmos.DrawSphere(this.gameObject.transform.position, 1f);
        for (int i = 0; i < borderController.Length; i++)
        {
            if (borderController[i] != null)
            {
                Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
                Gizmos.DrawCube(borderController[i].gameObject.transform.position, new Vector3(1f, 1f, 1f));
            }

        }
    }
}
