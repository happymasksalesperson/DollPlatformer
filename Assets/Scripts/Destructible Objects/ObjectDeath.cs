using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectDeath : MonoBehaviour
{
   private void OnEnable()
   {
      Destroy(gameObject);
   }
}