using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPos : MonoBehaviour
{
    public GameObject landern;
    public float spawnRange;

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(landern, (Vector2)transform.position + Vector2.right * Random.Range(0, spawnRange), Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * spawnRange);
    }
}
