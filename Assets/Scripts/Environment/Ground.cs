using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Renderer rend;
    void Start()
    {
        rend = this.GetComponent<Renderer>();
      
    }
    
    void OnEnable()
    {
        DollEventManager.DollSafeEvent += ChangeColour;
    }

    void OnDisable()
    {
        DollEventManager.DollSafeEvent -= ChangeColour;
    }

    private void ChangeColour()
    {
          rend.material.SetColor("_Color", Color.green);
    }

}
