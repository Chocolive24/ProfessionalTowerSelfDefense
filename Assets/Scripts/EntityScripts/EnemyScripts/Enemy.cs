using System;
using UnityEngine;
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
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Nexus nexus = collision.gameObject.GetComponent<Nexus>();

        if (nexus)
        {
            nexus.TakeDamage(20);
            this.TakeDamage(_maxHp);
        }
    }
}
