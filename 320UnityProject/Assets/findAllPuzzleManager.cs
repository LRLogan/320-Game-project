using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findAllPuzzleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> objectList = new List<GameObject>();
    [SerializeField] int size;
    public int numberInteracted = 0;
    [SerializeField] interactableObject clothes;
    // Start is called before the first frame update
    void Start()
    {
        //size = objectList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCounter()
    {
        numberInteracted++;
        if(numberInteracted >= size)
        {
            clothes.canPickup = true;
        }
    }

}
