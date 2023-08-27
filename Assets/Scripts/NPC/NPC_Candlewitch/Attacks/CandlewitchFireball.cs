using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchFireball : PooledObject
    {
        public CandlewitchFireballView view;

        public void Awake()
        {
            view.AnnounceVisibilityEvent += DestroySelf;
        }

        public void DestroySelf()
        {
            view.AnnounceVisibilityEvent -= DestroySelf;
            Destroy(this);
        }
    }
}