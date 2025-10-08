using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLoader : MonoBehaviour
{
    GameObject player;
    [SerializeField] private GameObject playerObject;
    void Awake()
    {
        player = Instantiate(
          playerObject,
          new Vector3(-19, 0, 2),
          Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
