using UnityEngine;
using UnityEngine.AI;

public class WanderingBehavior : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimeToChange = 5f;
    private float timer;



    public void Wander(NavMeshAgent agent)
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimeToChange)
        {
            Vector3 newPos = GetRandomPosition(transform.position, wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    /// <summary>
    /// Returns a random position within a radius
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPosition(Vector3 origin, float radius)
    {
        Vector3 randDirection = Random.insideUnitSphere * radius;

        randDirection += origin;

        NavMeshHit navHit;
        //Ensure the random position is inside the nav mesh.
        NavMesh.SamplePosition(randDirection, out navHit, radius, -1);

        return navHit.position;
    }

}
