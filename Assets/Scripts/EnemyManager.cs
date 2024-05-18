using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls all the enemy behaviors
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(WanderingBehavior))]
[RequireComponent(typeof(CharacterAnimation))]
public class EnemyManager : MonoBehaviour
{
    public Transform target;
    private CharacterAnimation characterAnimation;
    private NavMeshAgent agent;
    private WanderingBehavior wanderingBehavior;

    public enum EnemyState { Wandering, Search, Chase, Attack };
    private EnemyState currentState = EnemyState.Wandering;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderingBehavior = GetComponent<WanderingBehavior>();
        characterAnimation = GetComponent<CharacterAnimation>();
    }
    public void CheckState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Wandering:
                wanderingBehavior.Wander(agent);
                break;
        }
    }


    private void Update()
    {
        CheckState(currentState);
        characterAnimation.UpdateMovementAnimation(agent.velocity);
    }

}
