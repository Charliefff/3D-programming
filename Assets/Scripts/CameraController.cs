using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;
    public float cameraDampValue = 0.05f;


    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camera;


    private Vector3 cameraDampVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up , pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX,-30,30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX,0,0);

        model.transform.eulerAngles = tempModelEuler;

        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position , ref cameraDampVelocity , cameraDampValue);
        camera.transform.eulerAngles = transform.eulerAngles;
    }
}
