using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantiatorComponent : MonoBehaviour
{
    public float DestroyDelay = 0;
    public GameObject ObjectToSpawn;
    public UnityEvent OnSpawned;
    public UnityEvent OnDeSpawned;

    GameObject spawnedObject;
    float timer;

    public void Spawn()
    {
        timer = DestroyDelay;
        if(!spawnedObject && ObjectToSpawn)
        {
            spawnedObject = Instantiate(ObjectToSpawn, transform.position, transform.rotation, transform);
            OnSpawned.Invoke();
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
            OnDeSpawned.Invoke();
        }
    }
}
