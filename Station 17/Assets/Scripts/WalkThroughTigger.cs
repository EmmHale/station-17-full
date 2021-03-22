using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkThroughTigger : MonoBehaviour
{
    [SerializeField] UnityEvent OnWalkThrough;
    public bool isRepeatable = false;
    private bool already_done = false;
    private bool disabled = false;
    public void OnTriggerEnter(Collider other)
    {
        if(!already_done && !disabled)
        {
            OnWalkThrough.Invoke();
        }

        if(!isRepeatable)
        {
            already_done = true;
        }
    }

    public void SetDisabled(bool enable)
    {
        disabled = enable;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
