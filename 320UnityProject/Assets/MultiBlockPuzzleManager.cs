using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBlockPuzzleManager : MonoBehaviour
{
    Player player;

    public int numberInteracted = 0;
    [SerializeField] int[] correctNumbers = new int[3];
    public int[] orderSelect = new int[3];
    [SerializeField] public bool dialogue;
    [SerializeField] public string dialogueString;
    [SerializeField] bool item;
    [SerializeField] GameObject objectGiven;
    [SerializeField] bool destroy;
    [SerializeField] GameObject objectDestroy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updatePuzzle(int x)
    {
        orderSelect[numberInteracted] = x;
        numberInteracted++;
        if (numberInteracted == 3)
        {
            if (correctNumbers[0] == orderSelect[0] && correctNumbers[1] == orderSelect[1] && correctNumbers[2] == orderSelect[2])
            {
                Debug.Log("yay");
                if (item)
                {
                    if (player != null)
                    {
                        player.AddToInventory(objectGiven);
                    }
                }
                if (destroy)
                {
                    Destroy(objectDestroy);
                }

            }
            else
            {
                numberInteracted = 0;
                Debug.Log("no");
                player.dialogueDisplay.InfoText("You hear locks reactivating. Did you do something wrong...?");
            }


        }




    }
    public void Interacted()
    {
        Debug.Log(dialogueString);
        numberInteracted = 0;
    }
}
