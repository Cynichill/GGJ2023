using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Root parent;
    private LineRenderer lines;
    public GameObject rootPrefab;
    private float mutationRate = 0.8f;
    public List<Root> children = new List<Root>();
    public Vector3 pointA;
    public Vector3 pointB;
    public bool isDead = false;
    private int count = 0;
    public Vector3 goal = new Vector3(0, 0, 0);
    private float focus = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponent<LineRenderer>();
        pointA = transform.position;
        pointB = transform.position + Vector3.down;
        Draw();

        if(Random.Range(0.0f, 1.0f) > 10.0f){
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
            DoesBranch();
        }
    }

    void DoesBranch() {
        if(Vector3.Distance(pointB, goal) < 3.0f){
            mutationRate = 10.0f;
        } else if (Random.Range(-1.0f, 1.0f) > mutationRate) {
            count++;
            createChild();
            DoesBranch();
        }
    }

    float DoStuff(float i) {
        if (i >= 0) {
            return Random.Range(-2.0f * focus, 2.0f);
        } else {
            return Random.Range(2.0f, 2.0f * focus);
        }
    }

    Vector3 GetNewPosition() {
        Vector3 direction = goal - pointB;

        float X = DoStuff(direction.x);
        float Y = DoStuff(direction.y);

        return new Vector3(X, Y, 0);
    }

    void createChild() {
        Vector3 newPoint = pointB + GetNewPosition();
        GameObject newRootRef = Instantiate(rootPrefab);
        Root newRoot = newRootRef.GetComponent<Root>();
        newRoot.SetPoints(pointB, newPoint);
        children.Add(newRoot);
    }
}
