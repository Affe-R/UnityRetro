using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorComponent : MonoBehaviour
{
    public float DestroyDelay = 0;
    public GameObject ObjectToSpawn;

    GameObject spawnedObject;
    float timer;

    public void Spawn()
    {
        timer = DestroyDelay;
        if(!spawnedObject && ObjectToSpawn)
        {
            spawnedObject = Instantiate(ObjectToSpawn, transform.position, transform.rotation, transform);
        }
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(spawnedObject)
        {
            Destroy(spawnedObject);
        }
    }
}
