using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    public Transform target;
    public float attackDistance;
    public float stoppingDistance = 1.5f;
    public float rotationSpeed = 5f;
    public int enemyHP = 40;

    private PauseManager pauseManager;

    public NavMeshAgent enemyAgent;
    public Animator enemyAnimator;
    private float m_distance;
    private float timeInAttackRange = 0f;
    private bool hasDealtInitialDamage = false;
    private float timeSinceLastDamage = 0f;
    private bool playerInRange = false;
    public int damageAmount = 10;
    public float damageDelay = 0.5f;
    public float damageInterval = 1f;
    [SerializeField] AudioManager audioManager;

    // Death state flag
    private bool isDead = false;

    void Start()
    {
        enemyAgent.stoppingDistance = stoppingDistance;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseManager>();
        Debug.Log(pauseManager);
    }

    void Update()
    {
        // Exit early if enemy is dead
        if (isDead) return;

        m_distance = Vector3.Distance(enemyAgent.transform.position, target.position);
        if (enemyAgent.velocity.magnitude != 0f)
        {
            enemyAnimator.SetBool("Run", true);
        }

        if (m_distance < attackDistance)
        {
            enemyAgent.isStopped = true;
            FaceTarget();

            enemyAnimator.SetBool("Attack", true);
            if (!playerInRange)
            {
                playerInRange = true;
                timeInAttackRange = 0f;
                hasDealtInitialDamage = false;
                timeSinceLastDamage = 0f;
            }

            timeInAttackRange += Time.deltaTime;
            timeSinceLastDamage += Time.deltaTime;

            HandleDamage();
        }
        else
        {
            enemyAgent.isStopped = false;
            enemyAnimator.SetBool("Attack", false);
            enemyAgent.destination = target.position;
            playerInRange = false;
            timeInAttackRange = 0f;
            hasDealtInitialDamage = false;
            timeSinceLastDamage = 0f;
        }
    }



    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage()
    {
        // Don't take damage if already dead
        if (isDead) return;

        enemyHP -= 10;
        if (enemyHP <= 0)
        {
            StartDeathSequence();
        }
    }

    void StartDeathSequence()
    {
        isDead = true;
        // Stop all movement and navigation
        enemyAgent.isStopped = true;
        enemyAgent.enabled = false; // Disable NavMeshAgent completely

        // Stop all animations except death
        enemyAnimator.SetBool("Run", false);
        enemyAnimator.SetBool("Attack", false);
        enemyAnimator.SetBool("Dead", true);

        // Reset attack state variables
        playerInRange = false;
        timeInAttackRange = 0f;
        hasDealtInitialDamage = false;
        timeSinceLastDamage = 0f;

        // Optional: Disable collider to prevent further interactions
        Collider enemyCollider = GetComponent<Collider>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }
    }

    void HandleDamage()
    {
        // Don't deal damage if dead
        if (isDead) return;

        if (!hasDealtInitialDamage && timeInAttackRange >= damageDelay)
        {
            DealDamageToPlayer();
            hasDealtInitialDamage = true;
            timeSinceLastDamage = 0f;
        }
        else if (hasDealtInitialDamage && timeSinceLastDamage >= damageInterval)
        {
            DealDamageToPlayer();
            timeSinceLastDamage = 0f;
        }
    }

    void DealDamageToPlayer()
    {
        SFPSC_PlayerMovement playerScript = target.GetComponent<SFPSC_PlayerMovement>();
        if (playerScript!= null)
        {
            playerScript.TakeDamage(damageAmount);
            Debug.Log($"Enemy dealt {damageAmount} damage to player!");
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found on target!");
        }
    }
    public void ChangeScoreThroughEnemy()
    {
        pauseManager.EnemyKilled();
        pauseManager.ChangeScore();
    }
    public void OnDeathAnimationComplete()
    {

        Debug.Log("enemy destroyed");
        Destroy(gameObject);
    }

    public void EnemySound()
    {
        audioManager.EnemySwingSound();
    }

    public void EnemyDeathAudio()
    {
        audioManager.EnemyDeathSound();
    }
}