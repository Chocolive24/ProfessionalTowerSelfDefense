using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    private NavMeshAgent _navMeshAgent;
    private Nexus _nexus;
    
    private bool _hasDealtDamag = false;

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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    
        if (_currentHp <= 0)
        {
            _navMeshAgent.enabled = false;
           BoxCollider bd =  GetComponent<BoxCollider>();
           Rigidbody rb = GetComponent<Rigidbody>();

           if (rb) rb.useGravity = false;
           
           if (bd)
           {
               bd.enabled = false;
           }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Nexus nexus = collision.gameObject.GetComponent<Nexus>();

        Debug.Log("coll " + collision.gameObject.name);

        if (nexus && !_hasDealtDamag)
        {
            _hasDealtDamag = true;
            nexus.TakeDamage(20);
            this.TakeDamage(_maxHp);
        }
    }
}
