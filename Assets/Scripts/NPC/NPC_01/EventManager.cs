using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC01
{
    public class EventManager : MonoBehaviour
    {  
        //event for NPC01 attacking with Attack01
        public delegate void NPC01Attack01();
        public static event NPC01Attack01 NPC01Attack01Event;
        public static void NPC01Attack01Function()
        {
            if(NPC01Attack01Event!=null)
            {
                NPC01Attack01Event();
            }
        }
        
        
        //event for NPC01 taking damage
        public delegate void NPC01TakingDamage();
        public static event NPC01TakingDamage NPC01TakingDamageEvent;
        public static void NPC01TakingDamageFunction()
        {
            if(NPC01TakingDamageEvent!=null)
            {
                NPC01TakingDamageEvent();
            }
        }
        
        //event for NPC01 patrolling
        public delegate void NPC01Patrolling();
        public static event NPC01Patrolling NPC01PatrollingEvent;
        public static void NPC01PatrollingFunction()
        {
            if(NPC01PatrollingEvent!=null)
            {
                NPC01PatrollingEvent();
            }
        }

    }
}
