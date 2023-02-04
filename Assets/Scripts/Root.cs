using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

class Root : MonoBehaviour
{
    public Root parent;
    private LineRenderer lines;
    public GameObject rootPrefab;
    private float mutationRate = 0.5f;
    public List<Root> children = new List<Root>();
    public List<Vector3> positions = new List<Vector3>();
    public MeshCollider collider;
    public bool isDead = false;
    private int count = 0;
    private Vector3[] goals = {new Vector3(5, -8, 0), new Vector3(-5, -8, 0), new Vector3(5, -4, 0), new Vector3(-5, -4, 0)};
    public Vector3 goal;
    private float focus = 0.4f;
    private Mesh mesh;
    public EdgeCollider2D edgeCollider;
    private float step = 1.5f;
    private float time = 0f;

    void Start()
    {
        lines = GetComponent<LineRenderer>();
        lines.material.SetColor("_Color", new Color(225/255f,228/255f,196/255f, 0.1f));
        goal = goals[Random.Range(0,3)];

        if(positions.Count == 0) {
            positions.Add(transform.localPosition);
            positions.Add(transform.localPosition + new Vector3(0.1f, 0 , 0));
        }
        
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
        time += Time.deltaTime;

        if(time >= step){
            Debug.Log(time);
            time = 0f;
            if(!isDead){
            Extend();
            }
            if(count < 3){
                DoesBranch();
            }
        }
    }

    void SetRoot(Vector3 start, Vector3 end) {
        positions.Add(start);
        positions.Add(end);
    }

    void Extend() {
        if(Vector3.Distance(positions.Last(), goal) > 0.5f){
            Vector3 newPoint = new Vector3(0,0,0);
            bool create = false;

            while(!create){
                newPoint = positions.Last() + GetNewPosition();
                if(IsClearPath(newPoint)){
                    create = true;
                }
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

    void createChild() {
        Debug.Log("Creating Child at " + positions.Last());
        GameObject newRootRef = Instantiate(rootPrefab, transform.position, transform.rotation);
        Root newRoot = newRootRef.GetComponent<Root>();
        newRoot.SetRoot(positions.Last(), positions.Last()+ new Vector3(0,0.01f,0));

        bool create = false;

        Vector3 newPoint = new Vector3(0,0,0);

        while(!create){
            newPoint = positions.Last() + GetNewPosition();

            if(IsClearPath(newPoint)){
                create = true;
                Debug.Log("Collision Detected: " + newPoint);
            }
        }

        if(create) {
            newRoot.SetRoot(positions.Last(), newPoint);
            children.Add(newRoot);
        }
    }

    // void GenerateMeshCollider() {
    //     collider = GetComponent<MeshCollider>();

    //     if(collider == null) {
    //         collider.gameObject.AddComponent<MeshCollider>();
    //     }

    //     mesh = new Mesh();
    //     lines.BakeMesh(mesh, Camera.main, false);
    //     collider.sharedMesh = mesh;

    //     foreach(var i in mesh.normals) {
    //         Debug.Log(i);
    //     }
    // }

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
            //DoesBranch();
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
        Vector3 pointA = positions.Last() + 0.01f * (newPoint-positions.Last());// + newPoint * 0.1f;
        Debug.Log("TESTING " + newPoint);
        Debug.Log(pointA + " => " + newPoint + " = " + Vector3.Distance(pointA, newPoint));
        Debug.Log(mesh);
        //Debug.DrawRay(pointA, newPoint-pointA, Color.green, 10000.0f, false);
        if (Physics2D.Linecast(newPoint, pointA)){
            Debug.Log(":::::Collision Detected::::: ");
            Debug.Log(":::::Collision Detected::::: ");
            Debug.Log(":::::Collision Detected::::: ");
            return false;
        } else {
            return true;
        }
    }

    
}
