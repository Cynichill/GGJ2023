using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootOrigin : MonoBehaviour
{
    public GameObject rootPrefab;
    private Vector3[] goals = {new Vector3(5, -4, 0), new Vector3(-5, -4, 0), new Vector3(5, 4, 0), new Vector3(-5, 4, 0)};
    public List<Root> roots = new List<Root>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject newRootRef = Instantiate(rootPrefab, new Vector3(0,0,0), transform.rotation);
        Root newRoot = newRootRef.GetComponent<Root>();
        newRoot.Initialize(transform.position, 0.1f, goals[Random.Range(0,3)]);
        roots.Add(newRoot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}