using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxTracker : MonoBehaviour
{
    private DollPlayerModelView modelView;

    //change to multiple box colliders later for NPCs
    private int noBoxes;
    
    private BoxCollider boxCollider;
    private Dictionary<State, SizeAndPosition> stateToBoxInfo = new Dictionary<State, SizeAndPosition>();

    public SizeAndPosition idleBox;
    public SizeAndPosition crouchBox;
    public SizeAndPosition runningBox;

    public SizeAndPosition jumpBox;
    public SizeAndPosition fallBox;

    public SizeAndPosition takeDamageBox;

    public SizeAndPosition attack01Box;
    public SizeAndPosition crouchAttack01Box;

    public SizeAndPosition deathBox;
    
    public State currentState;

    private void Start()
    {
        stateToBoxInfo.Add(State.Idle, idleBox);
        stateToBoxInfo.Add(State.Crouch, crouchBox);
        stateToBoxInfo.Add(State.Running, runningBox);

        stateToBoxInfo.Add(State.Jumping, jumpBox);
        stateToBoxInfo.Add(State.Falling, fallBox);

        stateToBoxInfo.Add(State.TakeDamage, takeDamageBox);

        stateToBoxInfo.Add(State.Attack01, attack01Box);

        stateToBoxInfo.Add(State.CrouchAttack01, crouchAttack01Box);

        stateToBoxInfo.Add(State.Death, deathBox);
    }

    private void OnEnable()
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (modelView == null)
            modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.ChangeState += UpdateBoxCollider;
    }

    public void UpdateBoxCollider(State newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
            if (stateToBoxInfo.TryGetValue(currentState, out SizeAndPosition boxInfo))
            {
                boxCollider.size = boxInfo.size;
                boxCollider.center = boxInfo.position;
            }
            else
            {
                Debug.LogError("Missing SizeAndPosition for State: " + currentState);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (stateToBoxInfo.TryGetValue(currentState, out SizeAndPosition boxInfo))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + boxInfo.position, boxInfo.size);
        }
    }
    
    [System.Serializable]
    public class SizeAndPosition
    {
        public Vector3 size = Vector3.one;
        public Vector3 position = Vector3.zero;
    }

    private void OnDisable()
    {
        modelView.ChangeState -= UpdateBoxCollider;
    }
}