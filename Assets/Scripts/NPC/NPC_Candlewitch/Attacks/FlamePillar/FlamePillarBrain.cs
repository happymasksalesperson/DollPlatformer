using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FlamePillarBrain : MonoBehaviour
{
    public float growTime;
    public float activeTime;

    public Vector3 boxColliderSizeGrow;
    public Vector3 boxColliderSizeActive;

    public BoxCollider collider;

    private void OnEnable()
    {
        collider.size = boxColliderSizeGrow;

        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float elapsedTime = 0f;
        Vector3 initialSize = collider.size;
        Vector3 targetSize = boxColliderSizeActive;

        while (elapsedTime < growTime)
        {
            collider.size = Vector3.Lerp(initialSize, targetSize, elapsedTime / growTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        collider.size = targetSize;

        StartCoroutine(Active());
    }

    private IEnumerator Active()
    {
        yield return new WaitForSeconds(activeTime);
        StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        float elapsedTime = 0f;
        Vector3 initialSize = collider.size;
        Vector3 targetSize = boxColliderSizeGrow;

        while (elapsedTime < growTime)
        {
            collider.size = Vector3.Lerp(initialSize, targetSize, elapsedTime / growTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        collider.size = targetSize;

        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
