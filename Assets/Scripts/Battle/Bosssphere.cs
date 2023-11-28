using UnityEngine;

public class BossSphere : MonoBehaviour
{
    public float rotationSpeed = 100f;

    public void Update()
    {
        RotateSphere();
    }

    private void RotateSphere()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;


    }
}
