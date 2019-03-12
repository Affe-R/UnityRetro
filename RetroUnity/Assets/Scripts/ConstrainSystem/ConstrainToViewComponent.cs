using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ConstrainToViewComponent : MonoBehaviour
{
    public UnityEvent OnOutsideBounds;

    BoxCollider2D collider;
    Bounds viewBounds;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        viewBounds = CreateViewBounds();
    }

    Bounds CreateViewBounds()
    {
        Camera cam = Camera.main;
        float size = cam.orthographicSize;
        float aspect = cam.aspect;
        size *= 2;
        Bounds viewBounds = new Bounds((Vector2)cam.transform.position, new Vector2(size * aspect, size));
        return viewBounds;
    }

    void Update()
    {
        Vector2 position = transform.position;
        if(!viewBounds.Contains(position))
            OnOutsideBounds.Invoke();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Bounds viewBounds = CreateViewBounds();
        Gizmos.DrawWireCube(viewBounds.center, viewBounds.size);
    }
}
