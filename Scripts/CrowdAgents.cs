using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdAgents : NPCAbilities, IReactHDTruck
{
    // Array to hold random game objects
    private GameObject[] _destinations;

    // create navmeshagent
    private NavMeshAgent _agent;

    // gameobject variable to hold the random destination
    private GameObject _loc;

    // gameobject for the hot dog truck
    [SerializeField] private GameObject _hotDogTruck;

    // distance from hot dog struck
    private float _hotDogDistance = 2f;

    // access the animator
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        // Add game objects into the array
        _destinations = GameObject.FindGameObjectsWithTag("Destination");

        // get the agent
        _agent = GetComponent<NavMeshAgent>();

        // Pick a random destination
        PickRandomDestination();

        // Walk to Destination
        WalktoRandomDestination();

        // define the hot dog truck
        _hotDogTruck = GameObject.FindGameObjectWithTag("HotDog");

        // Initialize the animator variable
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DidReachDestination())
        {
            PickRandomDestination();
            WalktoRandomDestination();
        }

        float distance = Vector3.Distance(_agent.transform.position, _hotDogTruck.transform.position);
        if (distance <= _hotDogDistance)
        {
            // makes the agents look at the hot dog truck
            _agent.transform.LookAt(_hotDogTruck.transform);
            ReactToHDTruck();
        }

    }
    public override bool DidReachDestination()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (_agent.velocity.sqrMagnitude == 0f)
            {
                return true; // The agent has reached the destination
            }
        }

        return false;
    }

    public override void PickRandomDestination()
    {
        _loc = _destinations[Random.Range(0, 5)];
    }

    public override void WalktoRandomDestination()
    {
        _agent.SetDestination(_loc.transform.position);
    }

    public void ReactToHDTruck()
    {
        // stop the agent from moving
        _animator.SetBool("hotdog", true);
        _agent.isStopped = true;
        StartCoroutine(WaitAtHDTruck());
    }

    public void ResetReaction()
    {
        // allow agent to move again
        _agent.isStopped = false;

        // change animator bool
        _animator.SetBool("hotdog", false);
    }

    IEnumerator WaitAtHDTruck()
    {
        // suspend execution for 6 seconds
        yield return new WaitForSeconds(6);

        // call the reset reaction function
        ResetReaction();
    }
}
