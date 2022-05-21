using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class DodgeRain : Agent
{
    [SerializeField]
    private float time = 8f;
    private Vector3 inicialPosition;
    public List<GameObject> rain;
    private BufferSensorComponent mySensor;
    private float timer;

    private void Start()
    {
        timer = time;
        inicialPosition = gameObject.transform.localPosition;
        mySensor = GetComponent<BufferSensorComponent>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Debug.Log("Reward");
            SetReward(+1f);
            timer = time;
            EndEpisode();
        }
        
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = inicialPosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        foreach(GameObject rainDrop in rain)
        {
            Rigidbody rainRB = rainDrop.GetComponent<Rigidbody>();
            float[] observations = {rainDrop.transform.position.x - transform.position.x, rainDrop.transform.position.y - transform.position.y, rainRB.velocity.y};
            mySensor.AppendObservation(observations);
        }

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];

        float moveSpeed = 4f;

        transform.localPosition += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Rain" || collider.tag == "Wall")
        {
            Debug.Log(collider.tag);
            SetReward(-1f);
            timer = time;
            EndEpisode();
        }
    }
}
