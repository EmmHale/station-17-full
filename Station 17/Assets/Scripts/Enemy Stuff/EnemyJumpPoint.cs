using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpPoint : MonoBehaviour
{
    [Tooltip("List of all enemy IDs that can jump to this point")]
    public List<int> ValidIDList = new List<int>();
}
