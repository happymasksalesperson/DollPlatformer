using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPC01Tester : MonoBehaviour
{
    public NPC01Brain brain;

    [Button]
    public void ChangeState(NPC01Brain.allNPC01States newState)
    {
        brain.SetNewCurrentState(newState);
    }
}
