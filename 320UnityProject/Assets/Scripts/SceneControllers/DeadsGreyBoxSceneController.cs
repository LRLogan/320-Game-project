using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Liko other scene controllers this one aims to add game objects to the hierarchy / functionality that must be added on scene load
/// </summary>
public class DeadsGreyBoxSceneController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUIPrefab;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindFirstObjectByType<Canvas>();
        Instantiate(dialogueUIPrefab);
        dialogueUIPrefab.transform.parent = canvas.transform;
        dialogueUIPrefab.layer = 5;
        dialogueUIPrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
