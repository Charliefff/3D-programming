using UnityEngine;

public class Bosssphere : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private bool clockwiseRotation = true;

    public void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (clockwiseRotation)
        {
            transform.Rotate(Vector3.up, rotationAmount);
        }
        else
        {
            transform.Rotate(Vector3.up, -rotationAmount);
        }

   
        if (Mathf.Abs(transform.rotation.eulerAngles.y) >= 359f)
        {
            clockwiseRotation = !clockwiseRotation;
        }
    }
}
