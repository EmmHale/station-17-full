/****************************************
 * Author: Emmett Hale
 * 
 * Date Created: A month in 2020
 * 
 * Purpose: Main enemy script
 ****************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Tooltip("Whether the enemy will case the player")]
    public bool active = false;

    [Tooltip("Radius enemy checks for player to move")]
    public float lookRadius = 10f;

    [Tooltip("Seconds between movements")]
    public float timeBetweenMoves = 1f;

    [Tooltip("Time spent moving")]
    public float timeMoving = .5f;

    NavMeshAgent agent;
    float timeSinceMovement = 0f;
    float timeSinceStartMoving = 0f;
    bool isMoving = false;

    [Tooltip("Audio Source of movements")]
    public AudioSource source;

    public List<AudioClip> walkClips = new List<AudioClip>();

    public Vector3 defaultPosition;
    public Quaternion defaultRotation;

    [Tooltip("Whether the enemy can jump to hiding spots")]
    public bool isSmart = false;

    [Tooltip("Range to check for jump")]
    public float jumpRange = 1f;

    [Tooltip("ID to check for valid jumppoints with")]
    public int enemyTypeID = -1;

    public LayerMask layerMaskSphere = new LayerMask();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (PlayerMovement.instance != null && other.tag == "Player")
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("Enemy Kill 2");
            }
            PlayerMovement.instance.Kill();
            Debug.Log("Killing player");
        }
        else if(other.tag == "Door")
        {
            if(other.gameObject.GetComponent<Animator>().GetBool("IsClosed"))
            {
                other.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
                other.gameObject.GetComponent<Animator>().SetBool("IsClosed", false);
            }
        }
    }

    public void ActivateEnemy()
    {
        if(!active)
        {
            active = true;
        }
    }

    public void DeactivateEnemy()
    {
        if (active)
        {
            active = false;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, jumpRange);   
    }

    float distance = 0f;

    bool was_chasing = false;

    public void Update()
    {
        if(active)
            distance = Vector3.Distance(PlayerInteract.instance.transform.position, transform.position);

        if (active && distance <= lookRadius)
        {
            //Check for player proximity

            if (PlayerInteract.instance.IsDoingLongAction())
            {
                //If the enemy is not moving
                if (!isMoving)
                {
                    if (timeSinceMovement >= timeBetweenMoves)
                    {
                        StartMoving();
                    }
                    else
                    {
                        timeSinceMovement += Time.deltaTime;
                    }
                }
                else
                {
                    if(timeSinceStartMoving >= timeMoving)
                    {
                        StopMoving();
                    }
                    else
                    {
                        timeSinceStartMoving += Time.deltaTime;
                    }
                }

                was_chasing = true;
            }
            else
            {
                if (was_chasing)
                {
                    StopMoving();

                    if (isSmart)
                    {
                        Collider[] possible_colliders = Physics.OverlapSphere(transform.position, jumpRange, layerMaskSphere, QueryTriggerInteraction.Collide);

                        EnemyJumpPoint closest_point = null;

                        Debug.Log("Colliders found: " + possible_colliders.Length);

                        foreach (Collider collider in possible_colliders)
                        {
                            if (collider.GetComponent<EnemyJumpPoint>() != null)
                            {
                                if (closest_point != null)
                                {
                                    if (Vector3.Distance(transform.position, closest_point.transform.position)
                                        > Vector3.Distance(transform.position, collider.transform.position)
                                        && collider.GetComponent<EnemyJumpPoint>().ValidIDList.BinarySearch(enemyTypeID) > -1)
                                    {
                                        closest_point = collider.GetComponent<EnemyJumpPoint>();
                                    }
                                }
                                else
                                {
                                    if (collider.GetComponent<EnemyJumpPoint>().ValidIDList.BinarySearch(enemyTypeID) > -1)
                                    {
                                        closest_point = collider.GetComponent<EnemyJumpPoint>();
                                    }
                                }

                            }
                        }

                        if (closest_point != null)
                        {
                            GetComponent<NavMeshAgent>().isStopped = true;
                            gameObject.GetComponent<NavMeshAgent>().ResetPath();
                            gameObject.GetComponent<NavMeshAgent>().Warp(closest_point.transform.position);
                            transform.rotation = closest_point.transform.rotation;
                            GetComponent<NavMeshAgent>().isStopped = false;
                        }
                    }

                    was_chasing = false;
                }
            }
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        agent.isStopped = true;
        timeSinceMovement = 0;

        if (source)
        {
            source.Stop();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        agent.isStopped = false;
        timeSinceMovement = 0;
        timeSinceStartMoving = 0;

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(PlayerInteract.instance.transform.position, path))
        {
            agent.SetDestination(PlayerInteract.instance.transform.position);
            if (source)
            {
                source.clip = walkClips[Random.Range(0, walkClips.Count - 1)];
                source.Play();
            }
        }
        else
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<NavMeshAgent>().ResetPath();
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        
    }
}
