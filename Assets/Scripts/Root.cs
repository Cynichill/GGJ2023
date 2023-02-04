using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Root parent;
    private LineRenderer lines;
    public GameObject rootPrefab;
    private float mutationRate = 0.5f;
    public List<Root> children = new List<Root>();
    public Vector3 pointA = new Vector3(0,0,0);
    public Vector3 pointB = new Vector3(0,-1,0);
    public bool isDead = false;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponent<LineRenderer>();
        Draw();

        if(Random.Range(0.0f, 1.0f) > 0.75f){
            mutationRate = 1.1f;
        }
    }

    public void Draw() {
        lines.positionCount = 2;
        lines.SetPositions(new Vector3[2]{pointA, pointB});
    }

    public void SetPoints(Vector3 A, Vector3 B){
        pointA = A;
        pointB = B;
    }

    // Update is called once per frame
    void Update()
    {
        if(count < 3){
            doesBranch();
        }
    }

    void doesBranch() {
        if (Random.Range(0.0f, 1.0f) > mutationRate) {
            count++;
            createChild();
            doesBranch();
        }
    }

    void createChild() {
        Vector3 newPoint = new Vector3(Random.Range(-2.0f,2.0f), Random.Range(-0.5f,-2.0f)) + pointB;
        GameObject newRootRef = Instantiate(rootPrefab);
        Root newRoot = newRootRef.GetComponent<Root>();
        newRoot.SetPoints(pointB, newPoint);
        newRoot.Draw();
        children.Add(newRoot);
    }
}
