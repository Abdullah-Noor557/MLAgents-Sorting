using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{

    public static ObjectTracker instance;

    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> dropPositions = new List<GameObject>();

    public List<GameObject> spawnedObjects = new List<GameObject>();
    public List<GameObject> spawnedDropPositions = new List<GameObject>();  

    public MainAgent agent;

    public bool predefined = true; // true if preset environment testing

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (!predefined)
        {
            spawnAllObjects();
            spawnDropPositions();
            getTargetForAgent();
        }
        else
        {
            getTargetForAgent();

        }

    }
    public void spawnAllObjects()
    {
        print(objects.Count);
        int randomX = Random.Range(-3, 3);
        int randomZ = Random.Range(-3, -1);
        int counter = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj =  Instantiate(objects[i]);
            obj.transform.parent = agent.environment;
            obj.transform.localPosition = new Vector3(-1+counter+Random.Range(1.0f,2f), 0.5f, Random.Range(-3, -1));
            spawnedObjects.Add(obj);
            counter += 3;

        }
    }

    public void spawnDropPositions()
    {

        if(spawnedDropPositions.Count>0)
        {

            for (int i = 0; i < spawnedDropPositions.Count; i++)
            {
                Destroy(spawnedDropPositions[i]);
                
            }
            spawnedDropPositions.Clear();

        }

        int randomZ = Random.Range(1, 3);
        int counter = -1;
        for (int i = 0; i < dropPositions.Count; i++)
        {
            GameObject dropPos = Instantiate(dropPositions[i]);
            dropPos.transform.parent = agent.environment;
            dropPos.transform.localPosition = new Vector3(counter, 0.5f, Random.Range(-2,2));

            spawnedDropPositions.Add(dropPos);
            counter += 3;
        }
    }

    public void getTargetForAgent()
    {

        if(spawnedObjects.Count <= 0)
        {
            spawnDropPositions();
            spawnAllObjects();
        }

        int randomObject = Random.Range(0, spawnedObjects.Count);
        agent.spawnedObject = spawnedObjects[randomObject];

        for(int i = 0; i<spawnedDropPositions.Count; i++)
        {
            if (spawnedDropPositions[i].GetComponent<DropPosition>().dropType == agent.spawnedObject.GetComponent<DropObject>().dropType)
            {
                agent.dropPosition = spawnedDropPositions[i];
                break;
            }
        }

    }

    public void destroyASpawnedObject(GameObject obj)
    {
        agent.pickedUpObject = false;
        agent.dropped = false;
        spawnedObjects.Remove(obj);
        Destroy(obj);
        getTargetForAgent();
    }

}
