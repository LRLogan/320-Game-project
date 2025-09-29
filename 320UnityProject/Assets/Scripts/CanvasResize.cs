using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResize : MonoBehaviour
{
    // Default width and height of the Canvas with which this Scene was worked on
    [SerializeField] float width = 1920, height = 1200;

    Rect canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        canvasRect = GetComponent<RectTransform>().rect;
        GetComponent<CanvasScaler>().scaleFactor =
            Mathf.Min(canvasRect.width / width, canvasRect.height / height);
    }

    // Update is called once per frame
    /* void Update()
    {
        
    } */
}
