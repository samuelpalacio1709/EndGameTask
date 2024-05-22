using UnityEngine;
using UnityEngine.AI;

public class SteeringBehaviors : MonoBehaviour
{

    [Header("Wandering")]
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTimeToChange = 5f;
    [SerializeField] private float minDistanceToMove = 2f;
    private float timer;

    [Header("Chase")]
    [SerializeField] private float chaseReactionSpeed = 1.0f;


    public void Wander(NavMeshAgent agent)
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimeToChange)
        {
            Vector3 newPos = GetRandomPosition(transform.position, wanderRadius);
            if (Vector3.Distance(newPos, agent.transform.position) >= minDistanceToMove)
            {
                agent.SetDestination(newPos);

            }
            timer = 0;
        }
    }

    public void Chase(NavMeshAgent agent, Transform target)
    {

        agent.SetDestination(target.position);
    }

    /// <summary>
    /// Returns a random position within a radius
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPosition(Vector3 origin, float radius)
    {
        Vector3 randomPosition = Random.insideUnitSphere * radius;

        randomPosition += origin;

        //Make sure the random position is inside the nav mesh.
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPosition, out navHit, radius, -1);

        return navHit.position;
    }

}
