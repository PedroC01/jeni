using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject parent;
    [SerializeField]
    private GameObject rainDrop;
    [SerializeField]
    private float rate = 0.8f;

    private int limit = 5;

    private float x;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = rate;
        parent = gameObject;
    }

    private void Update()
    {
        if(parent.transform.childCount < limit)
        {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0)
            {
                GetXPosition();
                Vector3 position = new Vector3(x, 0, 0);
                GameObject obj = Instantiate(rainDrop, position, Quaternion.identity, parent.transform);
                obj.transform.localPosition = position;
                spawnTimer = rate;
            }
        }
    }

    private void GetXPosition()
    {
        x = Random.Range(-2.5f, 2.5f);
    }
}
