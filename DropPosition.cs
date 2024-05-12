using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPosition : MonoBehaviour
{

    public enum DropType
    {
        CUBE,
        SPHERE,
        CAPSULE
    }

    public DropType dropType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("agent"))
        {
            MainAgent agent = other.GetComponent<MainAgent>();

            if(agent.pickedUpObject && agent.spawnedObject.GetComponent<DropObject>().dropType == dropType)
            {

                agent.dropped = true;
                agent.pickedUpObject = false;
                ObjectTracker.instance.destroyASpawnedObject(agent.spawnedObject);
                /*
                agent.AddReward(1);
                agent.text.text = "Agent Reward: " + agent.GetCumulativeReward();
                if (agent.GetCumulativeReward() > 1)
                {
                    agent.text.color = Color.green;

                }
                else if (agent.GetCumulativeReward() >= 0.5f)
                {
                    agent.text.color = Color.yellow;
                }
                else
                {
                    agent.text.color = Color.red;
                }
                agent.EndEpisode();*/



            }
            else
            {
                agent.dropped = false;

                /*agent.AddReward(-1);
                agent.text.text = "Agent Reward: " + agent.GetCumulativeReward();
                if (agent.GetCumulativeReward() > 1)
                {
                    agent.text.color = Color.green;

                }
                else if (agent.GetCumulativeReward() >= 0.5f)
                {
                    agent.text.color = Color.yellow;
                }
                else
                {
                    agent.text.color = Color.red;
                }
                agent.EndEpisode();*/


            }
        }
    }
}
