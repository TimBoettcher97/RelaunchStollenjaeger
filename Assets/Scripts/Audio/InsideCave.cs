using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideCave : MonoBehaviour
{
    [SerializeField] AudioSource[] caveAtmo;
    [SerializeField] AudioSource[] outsideAtmo;

    private void Start()
    {
        EnableOutsideAtmo();
    }

    private void EnableOutsideAtmo()
    {
        foreach (AudioSource source in caveAtmo)
        {
            source.enabled = false;
        }
        foreach (AudioSource source in outsideAtmo)
        {
            source.enabled = true;
        }
    }
    private void EnableCaveAtmo()
    {
        foreach (AudioSource source in caveAtmo)
        {
            source.enabled = true;
        }
        foreach (AudioSource source in outsideAtmo)
        {
            source.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FirstPersonControler>().inCave = true;
            EnableCaveAtmo();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FirstPersonControler>().inCave = false;
            EnableOutsideAtmo();
        }
    }
}
