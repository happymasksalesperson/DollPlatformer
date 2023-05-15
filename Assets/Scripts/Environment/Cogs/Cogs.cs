using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cogs : MonoBehaviour
{
    public float moveSpeed;
    public bool rotatingLeft;
    private Rigidbody rb;

    public bool isMoving;

    public CogsView cogsView;

    public Color myColor;
    public Color prevColor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cogsView = GetComponent<CogsView>();
    }

    public void ReverseRotation(bool newRotation)
    {
        //rotatingLeft = newRotation;
        rotatingLeft = !rotatingLeft;
    }

    public void IsMoving(bool newMoving)
    {
        isMoving = newMoving;
        if (isMoving)
        {
            myColor = Color.gray;
            ChangeColor(myColor);
        }
        else
        {
            myColor = Color.black;
            ChangeColor(myColor);
        }
    }

    public void ChangeColor(Color newColor)
    {
        if (newColor != prevColor)
        {
            cogsView.ChangeColor(newColor);
            prevColor = newColor;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            float rotationAmount = moveSpeed * Time.deltaTime;

            if (rotatingLeft)
            {
                rb.transform.Rotate(Vector3.forward, rotationAmount);
            }
            else
            {
                rb.transform.Rotate(Vector3.forward, -rotationAmount);
            }
        }
    }
}
