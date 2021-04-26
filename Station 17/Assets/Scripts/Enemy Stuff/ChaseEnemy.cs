using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemy : MonoBehaviour
{
    [Tooltip("Whether the enemy will case the player")]
    public bool active = false;

    public NavMeshAgent agent;

    [Tooltip("Audio Source of movements")]
    public AudioSource source;

    public Vector3 defaultPosition;
    public Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
       // agent = GetComponent<NavMeshAgent>();

        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            agent.SetDestination(PlayerInteract.instance.transform.position);
        }
    }

    public void ActivateEnemy()
    {
        if (!active)
        {
            active = true;
        }

        StartMoving();
    }

    public void DeactivateEnemy()
    {
        if (active)
        {
            active = false;
        }

        StopMoving();

        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().ResetPath();
        GetComponent<NavMeshAgent>().Warp(defaultPosition);
        transform.rotation = defaultRotation;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (PlayerMovement.instance != null && other.tag == "Player" && active)
        {
            PlayerMovement.instance.Kill();

            Debug.Log("Killing player");

            DeactivateEnemy();
        }
        else if (other.tag == "Door")
        {
            if (other.gameObject.GetComponent<Animator>().GetBool("IsClosed"))
            {
                other.gameObject.GetComponent<Animator>().SetBool("IsOpen", true);
                other.gameObject.GetComponent<Animator>().SetBool("IsClosed", false);
            }
        }
    }

    public void StopMoving()
    {
        agent.isStopped = true;

        if (source)
        {
            source.Stop();
        }
    }

    public void StartMoving()
    {
        agent.isStopped = false;

        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(PlayerInteract.instance.transform.position, path))
        {
            agent.SetDestination(PlayerInteract.instance.transform.position);

            if (source)
            {
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
