using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player's save data that will be assigned from JSON
/// </summary>
public class PlayerData : MonoBehaviour
{
    public Vector3 playerPos;
    public string curPuzzleName;
    public string[] inventoryItems;
}
