using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributeSetter : MonoBehaviour
{
    public void SetPlayerInstanceViewDistance(float distance)
    {
        PlayerInteract.instance.SetViewDistance(distance);
    }
}
