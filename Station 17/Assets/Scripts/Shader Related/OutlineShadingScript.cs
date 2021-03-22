using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShadingScript : MonoBehaviour
{
    public Shader outlineShader;

    public Shader[] base_shaders;

    // Start is called before the first frame update
    void Start()
    {
        if(outlineShader == null)
        {
            outlineShader = Shader.Find("Unlit/Color");
        }

        base_shaders = new Shader[gameObject.GetComponent<MeshRenderer>().materials.Length];

        int i = 0;
        foreach(Material material in gameObject.GetComponent<MeshRenderer>().materials)
        {
            base_shaders[i] = material.shader;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadShader()
    {
        foreach (Material material in gameObject.GetComponent<MeshRenderer>().materials)
        {
            material.shader = outlineShader;
        }
    }

    public void RemoveShader()
    {
        int i = 0;
        foreach (Material material in gameObject.GetComponent<MeshRenderer>().materials)
        {
            material.shader = base_shaders[i];
            i++;
        }
    }
}
