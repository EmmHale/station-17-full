using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisibilityCheck : MonoBehaviour
{
    [SerializeField] public UnityEvent OnSeen;

    private bool has_been_seen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(RendererExtensions.IsVisibleFrom(GetComponent<Renderer>(), Camera.main) && !has_been_seen)
        {
            OnSeen.Invoke();

            //has_been_seen = true;

            Debug.Log("Here");
        }

        /*if (!RendererExtensions.IsVisibleFrom(GetComponent<Renderer>(), Camera.main) && has_been_seen)
        {
            has_been_seen = false;

            Debug.Log("Gone");
        }*/
    }
}
