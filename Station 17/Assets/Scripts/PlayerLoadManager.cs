/*********************************************
 * Author: Emmett Hale
 * 
 * Purpose: Manages Player loading, saving,
 * and updating player progress
 * 
 ********************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoadManager : MonoBehaviour
{
    //animation thing for loading
    public Animator transition;

    //currently used save game
    public FloorData[] CurrentFloorData = new FloorData[PlayerSaveData.NUM_FLOORS];

    //currently used save data
    public PlayerSaveData currentSaveData;

    //singleton
    public static PlayerLoadManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two player loaders!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        CurrentFloorData = new FloorData[PlayerSaveData.NUM_FLOORS];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Usually called by checkpoints
    public void UpdateCurrentFloorData()
    {
        foreach (RoomManager room in RoomManager.rooms)
        {
            room.SaveRoom();
        }

        CurrentFloorData[SceneManager.GetActiveScene().buildIndex] = new FloorData(SceneManager.GetActiveScene().buildIndex);
    }

    //Creates list of all saved files
    //Used by save points and title screen
    public List<PlayerSaveData> LoadAllFiles()
    {
        //Create List for data from files to be stored
        List<PlayerSaveData> data = new List<PlayerSaveData>();

        //Create binary formatter
        BinaryFormatter bf = new BinaryFormatter();

        //Find directory
        Directory.CreateDirectory(Application.persistentDataPath + "/saves");

        //Load file names from directory
        string[] names = Directory.GetFiles(Application.persistentDataPath + "/saves");

        //
        foreach(string name in names)
        {
            if (name.Contains(".onion"))
            {
                PlayerSaveData temp;

                FileStream file = new FileStream(name, FileMode.Open, FileAccess.Read);

                temp = (PlayerSaveData)bf.Deserialize(file);

                data.Add(temp);

                file.Close();
            }
        }

        return data;
    }

    public void LoadFromPlayerData(PlayerSaveData data)
    {
        //Set currently used data to chosen data from file
        currentSaveData = data;

        if(data.gameStateData.Length < PlayerSaveData.NUM_FLOORS)
        {
            FloorData[] temp = new FloorData[PlayerSaveData.NUM_FLOORS];

            for(int i = 0; i < data.gameStateData.Length; i++)
            {
                temp[i] = data.gameStateData[i];
            }

            data.gameStateData = temp;
        }

        CurrentFloorData = data.gameStateData;

        //Create inventory
        PlayerInventory.instance.SetItems(data.inventory);

        //Load proper floor
        StartCoroutine(LoadFloor(data.floorID, data.saveLocationID));
    }

    public void LoadFloorToFloor(int level_index, int entry_postion, bool fast = false)
    {
        //Save current floor data
        CurrentFloorData[SceneManager.GetActiveScene().buildIndex] = new FloorData(SceneManager.GetActiveScene().buildIndex);

        //if there isnt any data for where we are goin make some new stuff
        if(CurrentFloorData[level_index] == null)
        {
            CurrentFloorData[level_index] = new FloorData();
        }

        //Start loading the floor
        if(!fast)
            StartCoroutine(LoadFloor(level_index, entry_postion));
        else
            StartCoroutine(LoadFloor(level_index, entry_postion, false, true));
    }

    public void SavePlayerData(string name, int ID)
    {
        //Save Current Floors Data
        CurrentFloorData[SceneManager.GetActiveScene().buildIndex] = new FloorData(SceneManager.GetActiveScene().buildIndex);

        //Create new data
        PlayerSaveData data = new PlayerSaveData(PlayerInventory.instance.GetItemNames(), name, ID);

        //Save floor data
        for(int i = 0; i < CurrentFloorData.Length; i++)
        {
            data.SetFloor(i, CurrentFloorData[i]);
        }

        CurrentFloorData = data.gameStateData;

        //Write data to proper file
    
        BinaryFormatter bf = new BinaryFormatter();

        Directory.CreateDirectory(Application.persistentDataPath + "/saves");

        FileStream file = File.Create(Application.persistentDataPath + "/saves/" + data.saveName + "_" + data.saveTime.ToString("dddd, dd MMMM yyyy") + ".onion");

        bf.Serialize(file, data);

        file.Close();
    }

    public void DeleteFile(PlayerSaveData data)
    {
        File.Delete(Application.persistentDataPath + "/saves/" + data.saveName + "_" + data.saveTime.ToString("dddd, dd MMMM yyyy") + ".onion");
    }


    //Does not set current data. Use when loading save or returning to main menu
    public IEnumerator LoadFloor(int level_index, int entry_postion, bool going_to_main = false, bool fast = false)
    {
        if(!fast)
            transition.SetTrigger("Start");

        PlayerInteract.instance.Controllable = false;
        PlayerMovement.instance.ToggleMovement();

        if(!fast)
            yield return new WaitForSeconds(2);

        //If we are not returning to main menu, save data
        if (!going_to_main)
        {
            //Make new floor data from empty data
            //CurrentFloorData[SceneManager.GetActiveScene().buildIndex] = new FloorData(SceneManager.GetActiveScene().buildIndex);
        }

        //Turn off sound
        float tempVolume = SettingsHandler.instance.GetVolume();
        SettingsHandler.instance.SetVolume(0);

        
        //Load new Scene
        SceneManager.LoadScene(level_index);

        //Wait one second for transition 
        yield return new WaitForSeconds(1);

        //Turn on player controls
        
        PlayerInteract.instance.ClearPlayerInteractionStuff();

        //If transition was to return to main -> clear data
        //else load proper save data
        if (going_to_main)
        {
            Debug.Log("Going to main menu.");
            //Clear Currently Held Save Data
            CurrentFloorData = new FloorData[PlayerSaveData.NUM_FLOORS];
            TitleScreenManager.instance.EnableTitleScreen();
        }
        else
        {
            Debug.Log("Loading floor: " + SceneManager.GetActiveScene().buildIndex);

            //For each interactable, if its progress was saved, preform its action
            //then save its action.
            if (CurrentFloorData[SceneManager.GetActiveScene().buildIndex].isValid)
            {
                for (int i = 0; i < CurrentFloorData[SceneManager.GetActiveScene().buildIndex].RoomCount(); i++)
                {
                    //Get current room data
                    RoomData temp = CurrentFloorData[SceneManager.GetActiveScene().buildIndex].GetRoomData(i);

                    Debug.Log("Loading room: " + RoomManager.rooms[i].identifier);

                    //Apply saved room state and preform necessary actions
                    for (int u = 0; u < temp.interactableSaveState.Length; u++)
                    {
                        if (temp.GetSaveState(u))
                        {
                            RoomManager.rooms[i].managedInteractables[u].OnCompleteAction.Invoke();
                            RoomManager.rooms[i].managedInteractables[u].completed = true;
                            RoomManager.rooms[i].managedInteractables[u].saved = true;

                            Debug.Log("Interactable completed!");
                        }
                    }
                }
            }
        }

        SpookyPlaneController.instance.DisablePlane();
        PlayerInteract.instance.ResetAttributes();

        //Search for the proper spawn and sets players location
        foreach (SpawnLocation spawn in SpawnLocation.spawnLocations)
        {
            Debug.Log("Checking spawn...");
            if(spawn.spawnID == entry_postion)
            {
                Debug.Log("Found spawn!");
                PlayerMovement.instance.GetComponent<CharacterController>().enabled = false;
                PlayerMovement.instance.transform.position = spawn.transform.position;
                PlayerMovement.instance.transform.rotation = spawn.transform.rotation;

                PlayerInteract.instance.transform.rotation = spawn.transform.rotation;

                PlayerMovement.instance.GetComponent<CharacterController>().enabled = true;

                spawn.OnSpawn.Invoke();
            }
        }

        PlayerMovement.instance.ToggleMovement();
        PlayerInteract.instance.Controllable = true;
        //turn on sound
        SettingsHandler.instance.SetVolume(tempVolume);
        
    }

    public void ClearData()
    {

    }
}
