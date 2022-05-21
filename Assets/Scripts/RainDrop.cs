using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
    private DodgeRain rainList;
    private GameObject agent;

    private void Start()
    {
        agent = GameObject.FindGameObjectWithTag("Agent");
        rainList = agent.GetComponent<DodgeRain>();
        rainList.rain.Add(this.gameObject);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Platform"))
        {
            rainList.rain.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
