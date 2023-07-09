using UnityEngine.AI;

public class Enemy : Entity
{
    private NavMeshAgent _navMeshAgent;
    private Nexus _nexus;

    public bool IsTargeted = false;
    
    void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _nexus = FindObjectOfType<Nexus>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.remainingDistance <= 17f)
        {
            _nexus.TakeDamage(20);
            TakeDamage(_maxHp);
        }
    }
}
