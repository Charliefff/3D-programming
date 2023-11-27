using UnityEngine;

public class BossSphere : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private bool clockwiseRotation = true;
    //private bool changeDirection = false;
    private const float rotationThreshold = 359f;

    public void Update()
    {
        RotateSphere();

        
        //if (Input.GetKeyDown(KeyCode.F8))
        //{
        //    changeDirection = !changeDirection;
        //    if (changeDirection)
        //        Debug.Log("Animation Start");
        //    else
        //        Debug.Log("Animation Stop");
        //}
    }

    private void RotateSphere()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        

        //if (changeDirection)
        //{
        //    if (clockwiseRotation)
        //    {
        //        transform.Rotate(Vector3.up, rotationAmount);
        //    }
        //    else
        //    {
        //        transform.Rotate(Vector3.up, -rotationAmount);
        //    }

            
        //    if (Mathf.Abs(transform.rotation.eulerAngles.y) >= rotationThreshold)
        //    {
        //        clockwiseRotation = !clockwiseRotation;
        //    }
        //}
        //else
        //    transform.Rotate(Vector3.up, rotationAmount);

    }
}
