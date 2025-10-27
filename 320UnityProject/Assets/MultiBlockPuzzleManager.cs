using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBlockPuzzleManager : MonoBehaviour
{
    public int numberInteracted = 0;
    [SerializeField] int[] correctNumbers = new int[3];
    public int[] orderSelect = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updatePuzzle(int x)
    {
        orderSelect[numberInteracted] = x;
        numberInteracted++;
        if(numberInteracted == 3)
        {
            if (correctNumbers[0] == orderSelect[0] && correctNumbers[1] == orderSelect[1] && correctNumbers[2] == orderSelect[2])
            {
                Debug.Log("yay");
            }
            else
            {
                numberInteracted = 0;
                Debug.Log("no");

            }


        }
        



    }

}
