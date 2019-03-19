using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorComponent : MonoBehaviour
{
    public float Reach = 1;
    public Vector3 offset;
    public void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + offset, Reach);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<InteractableComponent>()?.Interact(this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + offset, Reach);
    }
}
