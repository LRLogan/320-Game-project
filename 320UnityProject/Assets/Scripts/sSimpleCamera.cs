using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sSimpleCamera : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = FindAnyObjectByType<Player>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(-7.55f, 6.35f, -7.89f);
    }
}
