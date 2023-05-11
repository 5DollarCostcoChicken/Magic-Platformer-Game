using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    GameObject mainCamera;
    CameraPans cam;
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = mainCamera.GetComponent<CameraPans>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            cam.target = this.gameObject;
            cam.PanningType = "Target";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            cam.PanningType = "Players";
            cam.zoomSize = 5;
        }
    }
}
