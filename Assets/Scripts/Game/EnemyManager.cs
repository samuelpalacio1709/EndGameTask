using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Handles all the enemy behaviors and controllers
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SteeringBehaviors))]
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(EnemyRadar))]

public class EnemyManager : MonoBehaviour, IGameEntity
{

    [SerializeField] private EnemySettings enemySettings;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private HealthController healthController;

    private CharacterMovement characterMovement;
    private CharacterAnimation characterAnimation;
    private NavMeshAgent agent;
    private SteeringBehaviors steeringBehavior;
    private EnemyRadar enemyRadar;
    private IEnemyInteractable interactable;
    public enum EnemyState { Wandering, Chase, Attack, Evade };
    private EnemyState currentState = EnemyState.Wandering;

    private Coroutine attackCoroutine;
    private float timeToAttack = 0;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        steeringBehavior = GetComponent<SteeringBehaviors>();
        characterAnimation = GetComponent<CharacterAnimation>();
        enemyRadar = GetComponent<EnemyRadar>();
        GetRandomAttackTime();
        agent.speed = enemySettings.movementSpeed;
        characterMovement = GetComponent<CharacterMovement>();
        agent.angularSpeed = enemySettings.turnSpeed;
    }
    private void OnEnable()
    {
        enemyRadar.onInteractableFound += SetChase;
        enemyRadar.onInteractableLost += SetWander;
    }

    private void OnDisable()
    {
        enemyRadar.onInteractableFound -= SetChase;
        enemyRadar.onInteractableFound -= SetWander;

    }
    public void CheckState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Wandering:
                WanderFreely();
                break;
            case EnemyState.Chase:
                ChaseInteractable();
                break;
            case EnemyState.Attack:
                TryAttack();
                break;
        }
    }
    private void WanderFreely()
    {
        steeringBehavior.Wander(agent);
    }
    private void ChaseInteractable()
    {
        SetTargetDestination();
        GetRandomAttackTime();
        currentState = EnemyState.Attack;
    }

    private void TryAttack()
    {
        SetTargetDestination();
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private void SetTargetDestination()
    {
        //Check if target is reachable, sometimes player might be inside a building
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(interactable.GetTransform().position, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            steeringBehavior.Chase(agent, interactable.GetTransform());

        }

    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeToAttack);
        weaponController.Fire(CharacterAttackState.Attack, Vector3.zero);
        characterAnimation.UpdateAttackAnimation(CharacterAttackState.Attack,
                                                        Vector3.zero);


        characterMovement.SetAttackMovement(CharacterAttackState.Attack, agent.destination);
        characterMovement.RotateCharacter();

        yield return new WaitForSeconds(enemySettings.attackTime);
        characterMovement.ResetCharacterVisualRotation();
        characterAnimation.UpdateAttackAnimation(CharacterAttackState.Rest,
                                                        Vector3.zero);


        if (currentState == EnemyState.Attack)
        {
            currentState = EnemyState.Chase;
        }
        attackCoroutine = null;

    }

    private void Update()
    {
        CheckState(currentState);
        characterAnimation.UpdateMovementAnimation(agent.velocity);
    }

    private void SetChase(IEnemyInteractable enemyInteractable)
    {

        interactable = enemyInteractable;
        currentState = EnemyState.Chase;
    }

    private void SetWander(IEnemyInteractable enemyInteractable)
    {
        currentState = EnemyState.Wandering;

    }

    private void GetRandomAttackTime()
    {
        timeToAttack = Random.Range(enemySettings.minAttackTime, enemySettings.maxAttackTime);
    }

    public IEntitySettings GetSettings()
    {
        return enemySettings;
    }

    public void RecieveDamage(float damage)
    {
        if (healthController != null)
        {
            healthController.HandleDamage(damage);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
    }
}
