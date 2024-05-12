using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public DropPosition.DropType dropType;


    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("agent"))
        {

            MainAgent agent = other.GetComponent<MainAgent>();

            /*
            if (agent.pickedUpObject) {
                agent.totalReward += -0.5f;

                agent.AddReward(-0.5f);
                agent.EndEpisode();
                return;
            };

            */


            if(agent.pickedUpObject)
            {
                return;
            }

            if(agent.spawnedObject != this.gameObject)
            {
                return;
            }
            agent.pickedUpObject = true;
            agent.dropType = this.dropType;
            transform.parent = agent.transform;


           // agent.AddReward(0.5f);
        }
    }

}
