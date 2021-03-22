using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnLocation : MonoBehaviour
{
    public static List<SpawnLocation> spawnLocations;

    [Tooltip("Unique identifier for a spwan location on a floor.")]
    public int spawnID = -1;

    public string spawnDescription;

    [SerializeField] public UnityEvent OnSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnLocations != null)
        {
            if(spawnLocations[0] == null)
            {
                spawnLocations = new List<SpawnLocation>();
            }

            spawnLocations.Add(this);
        }
        else
        {
            spawnLocations = new List<SpawnLocation>();
            spawnLocations.Add(this);
        }

        spawnLocations = spawnLocations.OrderBy(o => o.spawnID).ToList<SpawnLocation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplaySaveMenu()
    {
        SaveUIManager.instance.OpenSaveMenu(spawnID, spawnDescription);
    }

    public void CloseSaveMenu()
    {

    }
}
