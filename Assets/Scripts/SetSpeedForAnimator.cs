using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpeedForAnimator : MonoBehaviour
{
    [SerializeField] EnemyMovement parent;
    private void Awake()
    {
        parent = transform.parent.GetComponent<EnemyMovement>();
    }
    public void SetSpeedForAnimation(float setSpeed)
    {
        parent.SetSpeedForAnimation(setSpeed);
    }
}
