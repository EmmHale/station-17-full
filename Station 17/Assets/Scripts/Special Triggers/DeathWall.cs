using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerMovement.instance != null && other.tag == "Player")
        {
            PlayerMovement.instance.Kill();
            Debug.Log("Killing player");
        }
    }
}
