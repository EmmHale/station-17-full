using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PowerCoreEnemyTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnter;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Important Enemy")
        {
            Debug.Log("Enemy Found and Triggered");
            other.gameObject.GetComponent<Enemy>().DeactivateEnemy();

            other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            other.gameObject.GetComponent<NavMeshAgent>().ResetPath();
            other.gameObject.GetComponent<NavMeshAgent>().Warp(transform.position);

            OnEnter.Invoke();
        }
    }
}
