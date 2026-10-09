using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;

public class ExtinsionExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject ExtinsionSphere;
    private float ScalingFloat;
    [SerializeField]
    private float scaleSpeed = 1f; 
    [SerializeField]
    private float scalingTimneMax = 100f;
    private float scalingTimneAtMoment;
    [SerializeField]
    private float fadingTime = 1f;
    private float fadingTimeAtMoment;
    [SerializeField]
    private bool scalingAllowed;
    [SerializeField]
    public AnimationCurve analogIntensityCurve;
    [SerializeField]
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        StartSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (scalingAllowed)
        {
            if (scalingTimneAtMoment >= scalingTimneMax)
            {
                StartSetup();
            }
            else
            {
                ScalingFloat += scaleSpeed * Time.deltaTime;
                ChangeScaling(ScalingFloat);
                if (scalingTimneAtMoment >= scalingTimneMax- fadingTime)
                {
                    fadingTimeAtMoment -= Time.deltaTime;
                    ChangeAlphaOverride();
                }
                scalingTimneAtMoment += Time.deltaTime;
            }
        }
    }

    private void StartSetup()
    {
        scalingAllowed = false;
        ScalingFloat = 0f; 
        fadingTimeAtMoment = fadingTime;
        ChangeAlphaOverride();
        scalingTimneAtMoment = 0f;
        ChangeScaling(ScalingFloat);
    }

    private void ChangeScaling(float recivedScaling)
    {
        recivedScaling = analogIntensityCurve.Evaluate(scalingTimneAtMoment /*/ scalingTimneMax*/);
        //Debug.LogFormat("ExtinsionExplosion: ChangeScaling: TimeAtMoment({0}) TimeMax({1}) Procentage({2})", scalingTimneAtMoment, scalingTimneMax, scalingTimneAtMoment/ scalingTimneMax);
        Vector3 scalingVector = new Vector3(1f,1f,1f) * recivedScaling;
        ExtinsionSphere.transform.localScale = scalingVector;
    }
    private void ChangeAlphaOverride()
    {
        float timePercentage = (fadingTimeAtMoment / fadingTime);
        timePercentage = Mathf.Clamp01(timePercentage);
        material.SetFloat("_AlphaOverride", timePercentage);
    }

    public void StartScaling()
    {
        Debug.Log("ExtinsionExplosion: StartScaling");
        scalingAllowed = true;

    }
}
