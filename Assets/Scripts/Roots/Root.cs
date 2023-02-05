using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Root parent;
    private LineRenderer lines;
    public GameObject rootPrefab;
    private float mutationRate;
    public List<Root> children = new List<Root>();
    public List<Vector3> positions = new List<Vector3>();
    public Vector3 start;
    private bool growing = false;
    private int count = 0;
    private Vector3[] goals = {new Vector3(5, -4, 0), new Vector3(-5, -4, 0), new Vector3(5, -4, 0), new Vector3(-5, -4, 0)};
    public Vector3 goal;
    private float focus = 0.4f;
    public EdgeCollider2D edgeCollider;
    private float step = 0.25f;
    private float time = 0f;
    private float growRate = 0.5f;
    private int branchSpace = 0;

    // start function which is to be started manually to allow variable set up
    public void Initialize(Vector3 start, float mutate, Vector3 g){

        // set up the renderer for the lines and the color (brown)
        lines = GetComponent<LineRenderer>();
        lines.material.SetColor("_Color", new Color(225/255f,228/255f,196/255f, 1f));

        // set the origin point of this root
        positions = new List<Vector3> {start, new Vector3(0.01f,0.01f,-1f) + start};

        // set the rate that new branches form and the direction of growth
        mutationRate = mutate;
        goal = g;
    
        // root is ready
        growing = true;
    }

    // Set the origin, called on new root
    void SetRoot(Vector3 start, Vector3 end) {
        positions.Add(start);
        positions.Add(end);
    }

    // draw path
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
                time = 0f;

                if(growing){
                    branchSpace++;
                    Extend();   // grow this path
                    if(count < 1 && branchSpace >= 5){
                        DoesBranch();   // grow additional path
                    }
                } 

            }
    }

    // extends the current root
    void Extend() {
        // only grow if the goal is not close
        float distance = Vector3.Distance(positions.Last(), goal);
        Debug.Log(distance);
        if(distance > 1.5f){
            Vector3 newPoint = new Vector3(0,0,0);

            bool create = false;
            int attempt = 0;
            // find a suitable path
            while(!create && attempt < 3){
                newPoint = positions.Last() + GetNewPosition();
                if(IsClearPath(newPoint)){
                    create = true;
                }
                attempt++;
            }

            // add new path to collection
            if(create){ 
                positions.Add(newPoint);
                Draw(); 
            } 
            
            
        } else {
            growing = false;  
            positions.Add(goal);
            lines.material.SetColor("_Color", new Color(0/255f,0/255f,255/255f, 1f));
            lines.Simplify(0.1f);
            Draw();
            Debug.Log("GOALLLL");
        }
    }

    // create a new branch
    void createChild() {
        
        // create the new object
        GameObject newRootRef = Instantiate(rootPrefab, transform.position, transform.rotation);
        Root newRoot = newRootRef.GetComponent<Root>();
        Vector3 originPoint = positions[Random.Range(0, positions.Count)];
        bool create = false;

        Vector3 newPoint = new Vector3(0,0,0);

        int attempt = 0;
        //find a suitable path
        while(!create && attempt < 10){
            newPoint = originPoint + GetNewPosition(originPoint);
            if(IsClearPath(newPoint)){
                create = true;
            } else {
                newPoint = originPoint + GetNewPosition(originPoint * (-0.1f));
            }

            attempt++;
        }

        // path found, intialize new object
        if(create){
            newRoot.Initialize(originPoint, mutationRate, goals[Random.Range(0,3)]);
            children.Add(newRoot);
        }

        branchSpace = 0;
    }

    // set up collision body
    void SetEdgeCollider() {
        edgeCollider = GetComponent<EdgeCollider2D>();

        List<Vector2> edges = new List<Vector2>(); 

        // needs to be recreated for each new point
        foreach(Vector3 point in positions){
            edges.Add(new Vector2(point.x, point.y));
        }

        edgeCollider.SetPoints(edges);
    }

    // decides if a branch is created
    void DoesBranch() {
        if(Vector3.Distance(positions.Last(), goal) < 3.0f){
            mutationRate = -1.0f;
        } else if (Random.Range(0.0f, 1.0f) < mutationRate || branchSpace >= 8) {
            count++;
            createChild();
        }
    }

    // determines the modifier to a new paths X or Y values
    float DoStuff(float i) {
        if (i >= 0) {
            return Random.Range(-growRate * focus, growRate);
        } else {
            return Random.Range(-growRate, growRate * focus);
        }
    }

    // get the end point of a new path
    Vector3 GetNewPosition() {
        Vector3 direction = goal - positions.Last();

        float X = DoStuff(direction.x);
        float Y = DoStuff(direction.y);

        return new Vector3(X, Y, 0);
    }

    // see above, this allows for a specific origin to be specified
    Vector3 GetNewPosition(Vector3 origin) {
        Vector3 direction = goal - origin;

        float X = DoStuff(direction.x);
        float Y = DoStuff(direction.y);

        return new Vector3(X, Y, 0);
    }
    
    // is there a clear path to the new point
    bool IsClearPath(Vector3 newPoint) {
        Vector3 pointA = positions.Last() + 0.01f * (newPoint-positions.Last());// + newPoint * 0.1f;
        Debug.DrawRay(pointA, newPoint-pointA, Color.green, 10000.0f, false);
        return !Physics2D.Linecast(newPoint, pointA);
    } 
}
