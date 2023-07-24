using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC01Tester : MonoBehaviour
{
    public NPC01Brain brain;
    
    public void ChangeState(NPC01Brain.allNPC01States newState)
    {
        brain.SetNewCurrentState(newState);
    }
}
