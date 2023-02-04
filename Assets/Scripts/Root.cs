using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    //public Root[] parents = [];
    private LineRenderer lines;
    public GameObject rootPrefab;
    private float mutationRate = 0.5f;
    public List<Root> children = new List<Root>();
    private Vector3 pointA = new Vector3(0,0,0);
    private Vector3 pointB = new Vector3(0,-1,0);
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponent<LineRenderer>();
        lines.positionCount = 2;
        Vector3[] positions = new Vector3[2] { pointA , pointB };
        Draw(positions);
    }

    void Draw(Vector3[] positions) {
        lines.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        if(count < 10){
            doesBranch();
        } else {
            //Debug.Log(branches);
            Time.timeScale = 0;
        }
    }

    void doesBranch() {
        if (Random.Range( 0.0f, 1.0f) > mutationRate) {
            count++;
            createChild();
        }
    }

    void createChild() {
        //Vector3 newPoint

        //branches.Add(newRoot);
    }
}
