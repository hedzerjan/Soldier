using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Mover : MonoBehaviour
{
    public float speed = 3.0f;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void ScoutMove()
    {
        if (agent.remainingDistance == 0f)
        {
            var destination = new Vector3();
            destination.x = Random.Range(-10f, 10f);
            destination.z = Random.Range(-10f, 10f);
            destination.y = 0;
            agent.SetDestination(destination);
        }
    }

    public void FireMove(GameObject target)
    {
        // var direction = target.transform.position - transform.position;
        // Debug.Log($"Direction: {direction.x}, {direction.y}, {direction.z}");
        transform.LookAt(target.transform);
        // transform.rotation = Quaternion.Euler(direction);
    }

    public bool IsStopped
    {
        get
        {
            return agent.isStopped;
        }
        set
        {
            agent.isStopped = value;
        }
    }
}
