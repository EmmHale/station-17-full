using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerTargeter : MonoBehaviour
{
    public MultiAimConstraint multiAimConstraint;
    public RigBuilder rig;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetSource());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SetSource()
    {

        yield return new WaitForSeconds(0.2f);

        WeightedTransformArray sources = multiAimConstraint.data.sourceObjects;
        sources.Clear();
        sources.Add(new WeightedTransform(PlayerInteract.instance.transform, 1));
        multiAimConstraint.data.sourceObjects = sources;
        rig.Build();
    }
}
