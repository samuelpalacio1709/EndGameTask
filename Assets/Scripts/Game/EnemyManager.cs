using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls all the enemy behaviors
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SteeringBehaviors))]
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(EnemyRadar))]
public class EnemyManager : MonoBehaviour, IDamagable
{

    [SerializeField] EnemySettings enemySettings;

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
        agent = GetComponent<NavMeshAgent>();
        steeringBehavior = GetComponent<SteeringBehaviors>();
        characterAnimation = GetComponent<CharacterAnimation>();
        enemyRadar = GetComponent<EnemyRadar>();
        GetRandomAttackTime();
        agent.speed = enemySettings.movementSpeed;
        agent.acceleration = Random.Range(9, 13);
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
                steeringBehavior.Chase(agent, interactable.GetTransform());
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(Attack());
                }
                break;
        }
    }
    private void WanderFreely()
    {
        steeringBehavior.Wander(agent);

    }
    private void ChaseInteractable()
    {
        steeringBehavior.Chase(agent, interactable.GetTransform());
        GetRandomAttackTime();
        currentState = EnemyState.Attack;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeToAttack);
        characterAnimation.UpdateAttackAnimation(CharacterAttackState.Attack,
                                                        Vector3.zero);

        yield return new WaitForSeconds(enemySettings.attackTime);
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

    public void SendDamage()
    {
        Debug.Log("Damage");
    }
}
