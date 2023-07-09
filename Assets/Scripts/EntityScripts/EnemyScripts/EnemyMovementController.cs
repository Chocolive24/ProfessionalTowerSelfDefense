using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private Nexus _nexus;
    
    private NavMeshAgent _navMeshAgent;

    public NavMeshAgent NavMeshAgent
    {
        get => _navMeshAgent;
        set => _navMeshAgent = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        _nexus = FindObjectOfType<Nexus>();
        
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.SetDestination(_nexus.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
