using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LongActionCheckerScript : MonoBehaviour
{
    [SerializeField] public UnityEvent OnLongAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInteract.instance.IsDoingLongAction())
        {
            OnLongAction.Invoke();
        }
    }
}
