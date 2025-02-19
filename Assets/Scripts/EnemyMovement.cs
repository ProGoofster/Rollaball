using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    void Start(){
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Update(){
        if(player != null) navMeshAgent.SetDestination(player.position);
    }

}
