using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField, Range(0, 50)]
    float detectionRadius = 15;

    [SerializeField, Range(0, 20)]
    float readyDistance = 5;
    public enum State
    {
        Idle, //Stay in an area waiting for the player to approach
        Chase, //Persue enemy, if unable to will go back to idle
        Ready //Idle around player waiting for chance to attack
    }

    public State enemyState;
    // Start is called before the first frame update
    NavMeshAgent agent;

    GameObject playerObject;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = readyDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case State.Idle:
                //Will Actively look around for a player to attack
                //(OPTIONALLY: Give option to stay in a certain area)

                bool playerFound;
                LookForPlayer(out playerObject, out playerFound);
                if(playerFound)
                {
                    enemyState = State.Chase;
                }
                break; 
            case State.Chase:
                //Chase the player until at a conforable distance then will be ready
                //"conforable distance" would most likely be the NavAgent's stopping distance
                agent.destination = playerObject.transform.position;
                
                if(Vector3.Distance(transform.position, agent.destination) <= readyDistance)
                {
                    enemyState = State.Ready;
                    agent.destination = transform.position;
                }

                break; 
            case State.Ready:
                if(Vector3.Distance(transform.position, playerObject.transform.position) >= (readyDistance + 10))
                {
                    enemyState = State.Chase;
                }
                break;
        }
    }

   void LookForPlayer(out GameObject playerObject, out bool playerFound)
    {
        playerFound = false;
        playerObject = null;
        List<Collider> objectsFound = new List<Collider>();
        foreach(Collider thing in Physics.OverlapSphere(transform.position, detectionRadius))
        {
            objectsFound.Add(thing);
        }
        foreach(Collider thing in objectsFound)
        {
            if(thing.gameObject.transform.tag == "Player")
            {
                playerObject = thing.gameObject;
                playerFound = true;
            }
        }
    }

}
