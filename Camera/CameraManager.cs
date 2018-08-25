using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    //Pans the camera to a given position over a duration
    //Smoothing is the number of steps
    public static IEnumerator PanCamera(Vector3 pos, float duration, float smoothing)
    {
        Vector3 origin = Camera.main.transform.position;

        Vector3 direction = pos - origin;

        for(float i = 0; i < smoothing; i++)
        {
            Camera.main.transform.position = new Vector3(origin.x + (direction.x/smoothing) * i, origin.y + (direction.y/smoothing) * i, -10);
            //print(duration / smoothing);
            yield return new WaitForSeconds(duration / smoothing);
        }
    }
}
