using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findAllPuzzle : MonoBehaviour
{
    [SerializeField] findAllPuzzleManager manager;
    private bool interacted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interacted()
    {
        if (manager != null && !interacted)
        {
            
            interacted = true;
            manager.UpdateCounter();
        }
    }
}
