using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModelView : MonoBehaviour
{
    public event Action<int> ChangeHealth;

    public void OnChangeHealth(int x)
    {
        ChangeHealth?.Invoke(x);
    }
    
    public event Action YouDied;

    public void OnYouDied()
    {
        YouDied?.Invoke();
    }
}