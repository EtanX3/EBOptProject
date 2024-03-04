using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    //wrap around to opposite side of screen 
    //private Vector3 viewportPosition;
    //private Vector3 posAdjust;
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3  viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector3  posAdjust = Vector3.zero;
        if (viewportPosition.x < 0)
        { posAdjust.x += 1; }
       else if(viewportPosition.x > 1)
        { posAdjust.x -= 1; }
       else if (viewportPosition.y < 0)
        { posAdjust.y += 1; }
       else if (viewportPosition.y > 1)
        { posAdjust.y -= 1; }

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + posAdjust);
    }
}
