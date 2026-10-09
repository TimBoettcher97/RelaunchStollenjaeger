using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemyMeshAgent;
    private Transform Target;
    [SerializeField] private Animator larvaAnimator;
    public NavMeshPathStatus navMeshPathStatus;
    public bool hasPath;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMeshAgent.hasPath)
        {
            AnimatorParameters();
        }
        //else
        //{
        //    AnimatorStop();
        //}
        Debug.Log("EnemyMovement: pathStatus: " + enemyMeshAgent.pathStatus);
        
        navMeshPathStatus = enemyMeshAgent.pathStatus;
        Debug.Log("EnemyMovement: pathStatus has path: " + enemyMeshAgent.hasPath);
        hasPath = enemyMeshAgent.hasPath;

        if (moveSound != null)
        {
            PlaySqueak();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Recalculate();
        }
    }

    private void Setup()
    {
        Debug.Log("EnemyMovement: Setup");
        Target = Gamemanager.PublicStaricGamemanager.EnemyTarget.transform;
        if (!enemyMeshAgent)
        {
            enemyMeshAgent = this.GetComponent<NavMeshAgent>();
        }
                
    }

    

    public void MoveToTarget()
    {
        Debug.Log("EnemyMovement: MoveToTarget");
        if (!Target)
        {
            Target = Gamemanager.PublicStaricGamemanager.EnemyTarget.transform;
        }
        if (!enemyMeshAgent)
        {
            enemyMeshAgent = this.GetComponent<NavMeshAgent>();
        }
        Debug.Assert(Target,"NoTarget");
        enemyMeshAgent.destination = Target.position;
        enemyMeshAgent.isStopped = false;
    }
    public void StopMovingToTarget()
    {
        enemyMeshAgent.isStopped = true;
    }

    private void AnimatorParameters()
    {
        Vector3 heading = enemyMeshAgent.steeringTarget - transform.position;
        Vector3 perp = Vector3.Cross(transform.forward, heading);
        float aim = Vector3.Dot(perp, transform.up);    

        if (aim > -.5f && aim < 0.5f)
        {
            larvaAnimator.SetBool("TurnRight", false);
            larvaAnimator.SetBool("TurnLeft", false);
            larvaAnimator.SetBool("Moving", true);
        }       
        else if (aim > 0.5f)
        {
            larvaAnimator.SetBool("Moving", false);
            larvaAnimator.SetBool("TurnRight", true);
        }
        else if (aim < -0.5f)
        {
            larvaAnimator.SetBool("Moving", false);
            larvaAnimator.SetBool("TurnLeft", true);
        }
        else
        {
            AnimatorStop();
            //larvaAnimator.SetBool("TurnRight", false);
            //larvaAnimator.SetBool("TurnLeft", false);
            //larvaAnimator.SetBool("Moving", false);
        }
    }

    private void AnimatorStop()
    {
        larvaAnimator.SetBool("TurnRight", false);
        larvaAnimator.SetBool("TurnLeft", false);
        larvaAnimator.SetBool("Moving", false);
    }

    [Header("Audio")]
    [SerializeField]
    AudioSource moveSound;
    [SerializeField]
    AudioClip moveSoundClip;
    [SerializeField]
    AudioSource squeak1, squeak2;
    [SerializeField]
    Vector2 squeakTimerRange;

    public void SetSpeedForAnimation(float setSpeed)
    {
        enemyMeshAgent.speed = setSpeed;
        if (setSpeed > 0.5f && moveSound != null)
        {
            moveSound.PlayOneShot(moveSoundClip);
        }
    }
    float squeakTimer;
    void PlaySqueak()
    {
        if (squeakTimer <= 0)
        {
            squeakTimer = Random.Range(squeakTimerRange.x, squeakTimerRange.y);
            int whichSqueak = Random.Range(0, 2);
            if (whichSqueak == 1)
            {
                squeak1.Play();
            }
            else
            {
                squeak2.Play();
            }
        }
        else
        {
            squeakTimer -= Time.deltaTime;
        }
    }

    private void Recalculate()
    {
        //enemyMeshAgent.ResetPath();
        NavMeshPath newPath = new NavMeshPath();
        enemyMeshAgent.CalculatePath(Target.position, newPath);
        enemyMeshAgent.SetPath(newPath);
    }

    private void OnEnable()
    {
        Setup();
    }
    private void OnDisable()
    {

    }
}
