using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootOrigin : MonoBehaviour
{
    public GameObject rootPrefab;

    // Start is called before the first frame update
    public void Initialize(List<GameObject> goals)
    {
        int index = Random.Range(0, goals.Count);
        Vector3 goalVector = new Vector3(goals[index].transform.position.x, goals[index].transform.position.y, -2);
        
        GameObject newRootRef = Instantiate(rootPrefab, new Vector3(0,0,0), transform.rotation, transform);
        Root newRoot = newRootRef.GetComponent<Root>();
        newRoot.Initialize(transform.position, 0.6f, goalVector, rootPrefab, goals[index].GetComponent<DrillHealth>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
