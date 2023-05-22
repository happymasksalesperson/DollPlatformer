using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC.NPC01.Needle
{
    public class NeedleBounce : MonoBehaviour
    {
        public bool facingDir;

        public NPC01Needle needle;

        public float horizontalDist;

        public float hitDir;

        public float verticalDist;

        public Rigidbody rb;

        public float spinTorque;

        public void OnEnable()
        {
            needle = GetComponent<NPC01Needle>();
            rb = GetComponent<Rigidbody>();
        }


        public void Bounce()
        {
            Debug.Log("bounce");
            
            facingDir = needle.facingRight;

            if (facingDir)
            {
                horizontalDist = -horizontalDist;
            }

            if (Random.value < 0.5f)
            {
                hitDir = -1;
            }

            else
                hitDir = 1;

            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ;

            rb.AddForce(new Vector3(horizontalDist, verticalDist, 0), ForceMode.Impulse);

            //unlocks faster spin
            rb.maxAngularVelocity = 100000f;

            //transform.hitDir (see above)
            rb.AddTorque(transform.forward * spinTorque * hitDir);

        }
    }
}
