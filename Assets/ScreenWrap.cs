using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    //wrap around to opposite side of screen 
    private Camera mainCamera;

    private Vector3 vector3Zero;

    private void Awake()
    {
        mainCamera = Camera.main;
        vector3Zero = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        Vector3 posAdjust = vector3Zero;

        if (viewportPosition.x < 0) 
            posAdjust.x += 1;

        else if(viewportPosition.x > 1) 
            posAdjust.x -= 1;

        else if (viewportPosition.y < 0) 
            posAdjust.y += 1;

        else if (viewportPosition.y > 1) 
            posAdjust.y -= 1;

        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition + posAdjust);
    }
}
