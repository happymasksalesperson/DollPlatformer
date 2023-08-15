using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Candlewitch
{
    public class CandlewitchFireballView : MonoBehaviour
    {
        public SpriteRenderer spr;

        public bool inView = true;

        public event Action AnnounceVisibilityEvent;

        public void Update()
        {
            if (inView)
                VisibilityCheck();
        }

        private void VisibilityCheck()
        {
            if (!spr.isVisible)
            {
                AnnounceVisibilityEvent?.Invoke();
                inView = false;
            }
        }
    }
}