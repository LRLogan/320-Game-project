using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sGreyBoxMove : MonoBehaviour
{
    public Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerPos.z += 1;
            playerPos.x += 1;
        
            transform.position = playerPos;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerPos.x -= 1;
            playerPos.z += 1;
            transform.position = playerPos;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerPos.x -= 1;
            playerPos.z -= 1;
            transform.position = playerPos;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerPos.x += 1;
            playerPos.z -= 1;
            transform.position = playerPos;
        }
    }
}
