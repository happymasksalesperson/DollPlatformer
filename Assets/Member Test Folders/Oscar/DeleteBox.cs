using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBox : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject,10f);
    }
}
