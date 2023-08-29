using UnityEngine;

public class SpinTransform : MonoBehaviour
{
    public float speed;
    public bool direction = true;

    public Transform target;

    public void ChangeDirection(bool newDirection)
    {
        direction = newDirection;
    }

    void Update()
    {
        int rotationDirection = direction ? 1 : -1;

        float rotationZ = rotationDirection * speed * Time.deltaTime;

        target.transform.Rotate(0, 0, rotationZ);
    }
}