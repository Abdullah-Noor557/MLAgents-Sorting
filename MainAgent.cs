using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MainAgent : Agent
{

    public DropPosition.DropType dropType;

    public TextMesh text; // environment reward text;

    public Transform environment;

    [Header("Object Positions")]
    public GameObject[] objects;
    public GameObject[] dropPositions;

    public GameObject dropPosition;

    public GameObject spawnedObject;

    public int dropTypeInt;

    public int trainType = 0;

    public bool pickedUpObject = false;
    public bool dropped = false;

    public float totalReward = 0;

    void setRandomPositions()
    {
  
        //set agent position to random
        transform.localPosition = new Vector3(Random.Range(-5, 5), 0.5f, Random.Range(-5, 5));



    }


    public override void OnEpisodeBegin()
    {

        if(!pickedUpObject || !dropped)
        {
            text.text = "Agent Reward: " + GetCumulativeReward();
            if (GetCumulativeReward() > 1)
            {
                text.color = Color.green;

            }
            else if (GetCumulativeReward() >= 0.5f)
            {
                text.color = Color.yellow;
            }
            else
            {
                text.color = Color.red;
            }
        }

        pickedUpObject = false;
        dropped = false;

        

        setRandomPositions();
    }


    public override void CollectObservations(VectorSensor sensor)
    {

        if(spawnedObject == null || dropPosition == null)
        {
            return;
        }

        if(dropType == DropPosition.DropType.SPHERE)
        {
            dropTypeInt = 0;
        }
        else if(dropType == DropPosition.DropType.CUBE)
        {
            dropTypeInt = 1;
        }
        else
        {
            dropTypeInt = 2;
        }

        // Agent position
        sensor.AddObservation(transform.localPosition);

        // Object position
        sensor.AddObservation(spawnedObject.transform.localPosition);

        // Drop points positions
        sensor.AddObservation(dropPosition.transform.localPosition);

        // Agent has object
        sensor.AddObservation(pickedUpObject);
        sensor.AddObservation(dropped);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //move agent
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) *4* Time.deltaTime;


        // smooth look at towards move direction
        Vector3 lookDir = new Vector3(moveX, 0, moveZ);
        if (lookDir != Vector3.zero)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5);
        }

        if (Mathf.Abs(transform.localPosition.z) >= 7 || Mathf.Abs(transform.localPosition.x) >= 7)
        {
            totalReward += -1;
            AddReward(-1f);
            text.text = "Agent Reward: " + GetCumulativeReward();
            if(GetCumulativeReward()>1)
            {
                text.color = Color.green;

            }
            else if (GetCumulativeReward() >= 0.5f)
            {
                text.color  = Color.yellow; 
            }
            else
            {
                text.color = Color.red;
            }
            EndEpisode();

        }
    }


}
