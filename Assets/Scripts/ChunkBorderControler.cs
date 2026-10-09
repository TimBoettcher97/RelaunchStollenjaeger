using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBorderControler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Borderpices;
    [SerializeField]
    private float[] borderPiceHealth = new float[3] { 1f,1f,1f};

    private List<ChunkControler> chunkControlers = new List<ChunkControler>();

    // Start is called before the first frame update
    void Start()
    {
        GetBorderPieces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetBorderPieces()
    {
        int children = transform.childCount;
        for(int i = 0 ; i < children; i++)
        {
            Borderpices[i] = transform.GetChild(i).gameObject;
        }
    }
    public void AddChunkController(ChunkControler recivedChunkControler)
    {
        chunkControlers.Add(recivedChunkControler);
    }


    [SerializeField] ParticleSystem breakingParticles;
    [SerializeField] Mesh particleMesh;
    [SerializeField] Vector3 offset;
    void ChangeParticle(Transform child)
    {
        breakingParticles.GetComponent<ParticleSystemRenderer>().material = GetComponentInChildren<MeshRenderer>().material;

        ParticleSystem.MainModule main = breakingParticles.main;
        
        ParticleSystem ps = breakingParticles;
        ParticleSystem.ShapeModule sh = ps.shape;
        sh.enabled = true;
        sh.shapeType = ParticleSystemShapeType.Mesh;
        sh.mesh = child.GetComponent<MeshFilter>().mesh;
        sh.scale = transform.localScale;

        breakingParticles.GetComponent<ParticleSystemRenderer>().mesh = particleMesh;

    }


    public void BorderPiceHit(GameObject recivedBorderPice,float recivedShootingDamage)
    {
        int index = -1;
        for (int i = 0; i < Borderpices.Length; i++)
        {
            if (Borderpices[i] == recivedBorderPice)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            Debug.Log("ChunkBorderControler: BorderPiceHit: Border not found");
        }

        borderPiceHealth[index] -= recivedShootingDamage;
        if (borderPiceHealth[index] <= 0f)
        {
            Transform child = transform.GetChild(index);
            ChangeParticle(child);
            Instantiate(breakingParticles, child.position + offset, child.rotation, transform);
            Borderpices[index].gameObject.SetActive(false); 
        }

        for (int i = 0; i < Borderpices.Length; i++)
        {
            if (Borderpices[i].gameObject.activeSelf == true)
            {
                return;
            }
        }

        SetBorderToOpen();
    }

    private void SetBorderToOpen()
    {
        Debug.Log("ChunkBorderControler: SetBorderToOpen:");
        for (int i = 0; i < chunkControlers.Count; i++)
        {
            chunkControlers[i].ReciveBorderBreach();
        }
    }
}
