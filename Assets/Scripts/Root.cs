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
    public MeshCollider collider;
    public bool isDead = false;
    private int count = 0;
    private Vector3 goal = new Vector3(5, -4, 0);
    private float focus = 0.4f;
    private Mesh mesh;
    public EdgeCollider2D edgeCollider;

    void Start()
    {
        lines = GetComponent<LineRenderer>();
        lines.material.SetColor("_Color", new Color(225/255f,228/255f,196/255f, 0.1f));
        positions.Add(transform.position);
        positions.Add(transform.position + new Vector3(0.1f, 0 , 0));
        Draw();
        if(Random.Range(0.0f, 1.0f) > 10.0f){
            mutationRate = 1.1f;
        }
    }

    public void Draw() {
        lines.positionCount = positions.Count;
        lines.SetPositions(positions.ToArray());
        SetEdgeCollider();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && Random.Range(0.0f, 1.0f) > 0.5f){
            Extend();
        }
        if(count < 1){
            //DoesBranch();
        }
    }

    void SetRoot(Vector3 start, Vector3 end) {
    }

    void Extend() {
        if(Vector3.Distance(positions.Last(), goal) > 0.5f){
            Vector3 newPoint = new Vector3(0,0,0);

            int attempts = 0;
            bool create = false;

            while(attempts < 3 && !create){
                newPoint = positions.Last() + GetNewPosition();
                //Debug.Log("Testing Path '" + newPoint + "'...");

                if(IsClearPath(newPoint)){
                    create = true;
                    //Debug.Log("Clear Path: " + newPoint);
                }
                attempts++;
            }

            if(create){
                //Debug.Log("Creating Point: " + newPoint);   
                positions.Add(newPoint);
                Draw();
            } else {
                isDead = true;
            } 
        } else {
            // trigger attack mode
        }
    }

    void GenerateMeshCollider() {
        collider = GetComponent<MeshCollider>();

        if(collider == null) {
            collider.gameObject.AddComponent<MeshCollider>();
        }

        mesh = new Mesh();
        lines.BakeMesh(mesh, Camera.main, false);
        collider.sharedMesh = mesh;

        foreach(var i in mesh.normals) {
            Debug.Log(i);
        }
    }

    void SetEdgeCollider() {
        edgeCollider = GetComponent<EdgeCollider2D>();

        List<Vector2> edges = new List<Vector2>(); 

        foreach(Vector3 point in positions){
            edges.Add(new Vector2(point.x, point.y));
        }

        edgeCollider.SetPoints(edges);
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
    
    bool IsClearPath(Vector3 newPoint) {
        Vector3 pointA = positions.Last() + 0.1f * (newPoint-positions.Last());// + newPoint * 0.1f;
        Debug.Log("TESTING " + newPoint);
        Debug.Log(pointA + " => " + newPoint + " = " + Vector3.Distance(pointA, newPoint));
        Debug.Log(mesh);
        Debug.DrawRay(pointA, newPoint-pointA, Color.green, 10000.0f, false);
        if (Physics2D.Linecast(newPoint, pointA)){
            Debug.Log(":::::Collision Detected::::: ");
            Debug.Log(":::::Collision Detected::::: ");
            Debug.Log(":::::Collision Detected::::: ");
            return false;
        } else {
            return true;
        }
    }

    void createChild() {
        GameObject newRootRef = Instantiate(rootPrefab, positions.Last(), rootPrefab.transform.rotation);
        Root newRoot = newRootRef.GetComponent<Root>();

        int attempts = 0;
        bool create = false;

        Vector3 newPoint = new Vector3(0,0,0);

        while(attempts < 3 && !create){
            newPoint = positions.Last() + GetNewPosition();

            if(IsClearPath(newPoint)){
                create = true;
                Debug.Log("Collision Detected: " + newPoint);
            }
            attempts++;
        }

        if(create) {
            Debug.Log("New Child");
            newRoot.SetRoot(positions.Last(), newPoint);
            children.Add(newRoot);
        }
    }
}
