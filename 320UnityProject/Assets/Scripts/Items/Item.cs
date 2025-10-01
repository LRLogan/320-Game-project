using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ItemType
{

}

[CreateAssetMenu(menuName = "Scriptable object / Item")]
public class Item : ScriptableObject
{
    [SerializeField] protected InputActionType actionType;
    public Sprite image;
    public ItemType itemType;
    public bool isStackable;
}
