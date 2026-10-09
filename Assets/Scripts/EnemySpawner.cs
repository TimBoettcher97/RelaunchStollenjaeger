using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour, IBorderBreche
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int spawnAmound;
    private int spawnAmoundRemaining;
    [SerializeField]
    [Tooltip("in seconds")]
    private float spawningSpacingTimer = 0.5f;
    private float spawningSpacingTimerAtMoment;
    [SerializeField]
    private float spawningCooldown;
    private float spawningCooldownAtMoment;

    private bool enemySpawnAllowed;
    [SerializeField]
    private bool enemySpawnByGameManager = true;
    [SerializeField]
    private bool enemySpawnByTrigger = false;
    private bool enemySpawnAllowedByTrigger;
    [SerializeField]
    private List<Collider> gassCollider;
    [SerializeField]
    private List<ParticleSystem> gassPartikelesystem;


    private void Awake()
    {
        spawningSpacingTimerAtMoment = spawningSpacingTimer;
        spawningCooldownAtMoment = spawningCooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnAmoundRemaining = spawnAmound;
    }

    public void BorderBreche()
    {
        Debug.LogFormat("EnemySpawner: BorderBreche", gameObject);
        enemySpawnAllowedByTrigger = true;
        SpawnChecking();
    }

    // Update is called once per frame
    void Update()
    {
        if ((enemySpawnByGameManager && enemySpawnAllowed) ||
            enemySpawnByTrigger && enemySpawnAllowedByTrigger)
        {
            SpawnChecking();
        }
    }

    private void SpawnChecking()
    {
        if (spawningCooldownAtMoment <= 0f)
        {
            if (spawningSpacingTimerAtMoment <= 0f)
            {
                SpawnEnemy();
                if (spawnAmoundRemaining <= 0)
                {
                    spawnAmoundRemaining = spawnAmound;
                    if (enemySpawnByTrigger)
                    {
                        enemySpawnAllowedByTrigger = false;
                        DestryGass();
                    }
                    TimmerResets(true, true);
                    return;
                }
                TimmerResets(true, false);
                return;
            }
            else
            {
                spawningSpacingTimerAtMoment -= Time.deltaTime;
            }
        }
        else
        {
            spawningCooldownAtMoment -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, this.transform.position,this.transform.rotation,this.transform);
        spawnAmoundRemaining--;
    }

    private void TimmerResets(bool resetSpacingTimer,bool resetSpawningCooldown)
    {
        Debug.LogFormat("EnemySpawner: TimmerResets(resetSpacingTimer:{0},resetSpawningCooldown:{1}).", resetSpacingTimer, resetSpawningCooldown, gameObject);
        if (resetSpacingTimer)
        {
            spawningSpacingTimerAtMoment = spawningSpacingTimer;
        }
        if (resetSpawningCooldown)
        {
            spawningCooldownAtMoment = spawningCooldown;
        }
    }


    private void OnEnable()
    {
        Gamemanager.enemySpawnDelegate += EnemySpawnAllowedWasChanged;
        EnemySpawnAllowedWasChanged( Gamemanager.PublicStaricGamemanager.enemySpawnAllowed);
    }
    private void OnDisable()
    {
        Gamemanager.enemySpawnDelegate -= EnemySpawnAllowedWasChanged;
    }

    private void EnemySpawnAllowedWasChanged(bool recivedBoolState)
    {
        //Debug.LogFormat("EnemySpawner: EnemySpawnAllowedWasChanged: enemySpawnAllowed was changed to {0}.", recivedBoolState);
        enemySpawnAllowed = recivedBoolState;
    }

    private void OnDrawGizmos()
    {
        // Draw a red sphere at the transform's position
        Gizmos.color = new Color(1f,0f,0f,0.25f);
        Gizmos.DrawSphere(this.transform.position, 1);
    }

    private void DestryGass()
    {
        if (gassCollider.Count >0)
        {
            for (int i = 0; i < gassCollider.Count; i++)
            {
                gassCollider[i].enabled = false;
            }
        }
        if (gassPartikelesystem.Count > 0)
        {
            for (int i = 0; i < gassPartikelesystem.Count; i++)
            {
                gassPartikelesystem[i].Stop();
            }
        }
    }
}
