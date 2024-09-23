using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private GameObject canvas;

    public void OnDetectedGoku()
    {
        Debug.Log("Goku Found");
    }

    public void OnLostGoku()
    {
        Debug.Log("Goku Lost");
    }

    public void Update()
    {
        canvas.transform.LookAt(camera.transform);
    }
}
