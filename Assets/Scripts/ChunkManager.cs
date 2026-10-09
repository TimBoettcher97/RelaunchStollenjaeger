using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private ChunkControler[] chunkControlers;
    [SerializeField] private float chunksOpen;
    private bool allChunksActive;
    [SerializeField] private Gamemanager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        chunkControlers = GetComponentsInChildren<ChunkControler>();
    }

    // Update is called once per frame
    void Update()
    {
        AllChunksActive();
        if (allChunksActive)
        {
            gamemanager.allChunksOpen = true;
        }
    }

    private void AllChunksActive()
    {
        //if (!allChunksActive)
        //{
          chunksOpen = 0;
        //}
        foreach (ChunkControler chunk in chunkControlers) 
        {
            if (chunk.ChunktIsOpen)
            {
                chunksOpen++;
            }
            if (chunksOpen == chunkControlers.Length)
            {
                allChunksActive = true;
            }
        }
    }
}
