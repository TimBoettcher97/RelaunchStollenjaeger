using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OreField : MonoBehaviour
{
    [SerializeField]
    private float oreValue = 1;
    [SerializeField]
    private MaterialType materialType = MaterialType.Smaragd;

    [SerializeField]
    private ParticleSystem breakingParticles;

    [SerializeField]
    private Mesh[] particleMesh = new Mesh[4];

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OreHit()
    {        
        
        Gamemanager.PublicStaricGamemanager.OreChange(oreValue, materialType);
        if (breakingParticles)
        {
            ChangeParticle();
            Instantiate(breakingParticles, transform.position, transform.rotation, transform.parent);
        }
        else
        {
            Debug.LogFormat("OreField: OreHit: no breakingParticles", this.gameObject);
        }
        this.gameObject.SetActive(false);       
    }

    void ChangeParticle()
    {
        breakingParticles.GetComponent<ParticleSystemRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;

        ParticleSystem ps = breakingParticles;
        var sh = ps.shape;
        sh.enabled = true;
        sh.shapeType = ParticleSystemShapeType.Mesh;
        sh.mesh = gameObject.GetComponent<MeshFilter>().mesh;

        int chosenMesh = 0;
        if (oreValue > 0)
        {
            switch (materialType)
            {
                case MaterialType.Smaragd:
                    chosenMesh = 1;
                    break;
                case MaterialType.Amethyst:
                    chosenMesh = 2;
                    break;
                case MaterialType.Rubin:
                    chosenMesh = 3;
                    break;
                default:
                    break;
            }
        }

        breakingParticles.GetComponent<ParticleSystemRenderer>().mesh = particleMesh[chosenMesh];

    }
}
