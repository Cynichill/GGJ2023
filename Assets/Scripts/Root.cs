using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

class Root : MonoBehaviour
{
    public Root parent;
    private LineRenderer lines;
    public GameObject rootPrefab;
    private float mutationRate = 0.4f;
    public List<Root> children = new List<Root>();
    public List<Vector3> positions = new List<Vector3>();
    public bool isDead = false;
    private int count = 0;
    private Vector3 goal = new Vector3(5, -4, 0);
    private float focus = 0.4f;

    void Start()
    {
        lines = GetComponent<LineRenderer>();
        lines.material.SetColor("_Color", new Color(225/255f,228/255f,196/255f, 1));
        positions.Add(transform.position);
        positions.Add(transform.position);
        Draw();
        if(Random.Range(0.0f, 1.0f) > 10.0f){
            mutationRate = 1.1f;
        }
    }

    public void Draw() {
        Debug.Log(lines);
        lines.positionCount = positions.Count;
        lines.SetPositions(positions.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        Extend();
        if(count < 1){
            DoesBranch();
        }
        Debug.Log(goal);
    }

    void SetRoot(Vector3 start, Vector3 end) {
    }

    void Extend() {
        if(Vector3.Distance(positions.Last(), goal) > 0.5f){
            Vector3 newPoint = positions.Last() + GetNewPosition();
            positions.Add(newPoint);
            Debug.Log(positions.ToArray());
            Draw();
        } else {
            Debug.Log("found");
        }
    }

    void DoesBranch() {
        if(Vector3.Distance(positions.Last(), goal) < 3.0f){
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
            return Random.Range(-2.0f, 2.0f * focus);
        }
    }

    Vector3 GetNewPosition() {
        Vector3 direction = goal - positions.Last();

        float X = DoStuff(direction.x);
        float Y = DoStuff(direction.y);

        return new Vector3(X, Y, 0);
    }

    void createChild() {
        GameObject newRootRef = Instantiate(rootPrefab, positions.Last(), rootPrefab.transform.rotation);
        Root newRoot = newRootRef.GetComponent<Root>();
        Vector3 newPoint = positions.Last() + GetNewPosition();

        newRoot.SetRoot(positions.Last(), newPoint);

        children.Add(newRoot);
    }
}
