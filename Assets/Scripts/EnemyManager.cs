using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls all the enemy behaviors
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SteeringBehavior))]
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(EnemyRadar))]
public class EnemyManager : MonoBehaviour
{
    public Transform target;
    private CharacterAnimation characterAnimation;
    private NavMeshAgent agent;
    private SteeringBehavior steeringBehavior;
    private EnemyRadar enemyRadar;
    private IEnemyInteractable interactable;
    public enum EnemyState { Wandering, Search, Chase, Attack };
    private EnemyState currentState = EnemyState.Wandering;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        steeringBehavior = GetComponent<SteeringBehavior>();
        characterAnimation = GetComponent<CharacterAnimation>();
        enemyRadar = GetComponent<EnemyRadar>();
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
                steeringBehavior.Wander(agent);
                break;
            case EnemyState.Chase:
                steeringBehavior.Chase(agent, interactable.GetTransform());
                break;
        }
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

}
