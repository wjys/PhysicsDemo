using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //get mouse position in pixel coordinates on screen
        Vector2 mousePositionOnScreen = Input.mousePosition;

        //clamp this number to screenspace
        mousePositionOnScreen.x = Mathf.Clamp(mousePositionOnScreen.x, 0f, Camera.main.pixelWidth);
        mousePositionOnScreen.y = Mathf.Clamp(mousePositionOnScreen.y, 0f, Camera.main.pixelHeight);

        //translate this new position to in-world coordinates
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        mousePositionInWorld.z = 0;

        //move this to that space
        transform.position = mousePositionInWorld;
    }
}
