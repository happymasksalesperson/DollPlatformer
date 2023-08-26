using UnityEngine;

public class SpinTransform : MonoBehaviour
{
    public float speed;
    public bool direction = true;

    void Update()
    {
        int rotationDirection = direction ? 1 : -1;

        float rotationZ = rotationDirection * speed * Time.deltaTime;

        transform.Rotate(0, 0, rotationZ);
    }
}