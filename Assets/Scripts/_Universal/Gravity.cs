using UnityEngine;
 
[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{
 
    public float gravityScale;
 
    public float globalGravity;
 
    Rigidbody m_rb;
 
    void OnEnable ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }
 
    void FixedUpdate ()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }

    public void ChangeGravity(float amount)
    {
        gravityScale = amount;
    }

    public float CurrentGravity()
    {
        return gravityScale;
    }
}