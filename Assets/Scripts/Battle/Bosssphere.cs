using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosssphere : MonoBehaviour
{
    public GameObject targetobj;
    public float rotationSpeed = 50f;

    public void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        targetobj.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
