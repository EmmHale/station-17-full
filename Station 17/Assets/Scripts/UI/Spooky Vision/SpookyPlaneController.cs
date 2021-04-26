using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyPlaneController : MonoBehaviour
{
    public static SpookyPlaneController instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two spooky planes !");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void DisablePlane()
    {
        GetComponent<MeshRenderer>().enabled = false;

        foreach(MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
        {
            child.enabled = false;
        }
    }

    public void EnablePlane()
    {
        GetComponent<MeshRenderer>().enabled = true;

        foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
        {
            child.enabled = true;
        }
    }
}
