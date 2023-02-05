using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Root : Enemy, IChoppable, IBurnable
{
    public Root parent;
    private LineRenderer lines;
    private GameObject rootPrefab;
    public float mutationRate;
    public List<Root> children = new List<Root>();
    public List<Vector3> positions = new List<Vector3>();
    public Vector3 start;
    private bool growing = false;
    private int count = 0;
    private Vector3[] goals = { new Vector3(5, -4, 0), new Vector3(-5, -4, 0), new Vector3(5, -4, 0), new Vector3(-5, -4, 0) };
    public Vector3 goal;
    private float focus = 0.1f;
    public EdgeCollider2D edgeCollider;
    private float step = 0.5f;
    private float time = 0f;
    private float growRate = 0.5f;
    private int branchSpace = 0;
    private DrillHealth daDrill;

    private bool attacking = false;

    // start function which is to be started manually to allow variable set up
    public void Initialize(Vector3 start, float mutate, Vector3 g, GameObject prefab, DrillHealth drill)
    {
        // set the origin point of this root
        positions.Add(start);
        positions.Add(new Vector3(0.01f, 0.01f, -1f) + start);

        // set up the renderer for the lines and the color (brown)
        lines = GetComponent<LineRenderer>();
        lines.material.SetColor("_Color", new Color(225 / 255f, 228 / 255f, 196 / 255f, 1f));

        // set the rate that new branches form and the direction of growth
        mutationRate = mutate;
        goal = g;
        rootPrefab = prefab;

        //Set drill
        daDrill = drill;

        // root is ready
        growing = true;
    }

    void EarlyUpdate()
    {
        positions = new List<Vector3>();
    }

    // Set the origin, called on new root
    void SetPositions(List<Vector3> pos)
    {
        positions = pos;
        Draw();
    }

    // draw path
    public void Draw()
    {
        lines.positionCount = positions.Count;
        //Debug.Log(lines.positionCount);
        lines.SetPositions(positions.ToArray());
        SetEdgeCollider();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= step)
        {
            time = 0f;

            branchSpace++;
            count++;

            if (count < 2 && branchSpace >= 5)
            {
                DoesBranch();   // grow additional path
            }

            if (!attacking)
            {
                Extend();
            }
            else
            {
                daDrill.TakeDamage(1);
            }

            // if(count == 8){
            //     Vector2 B = new Vector2(-3.99f, 1.01f);
            //     Vector2 A = new Vector2(-2.99f, 2.01f);
            //     Chop((A+B)/2);
            //     count++;
            // }

            //  Debug.Log(positions.Count);
        }
    }

    // extends the current root
    void Extend()
    {
        // only grow if the goal is not close
        float distance = Vector3.Distance(positions.Last(), goal);
        //Debug.Log(distance);
        if (distance > 1.5f)
        {
            Vector3 newPoint = new Vector3(0, 0, 0);

            bool create = false;
            int attempt = 0;
            // find a suitable path
            while (!create && attempt < 10)
            {
                newPoint = positions.Last() + GetNewPosition();
                if (IsClearPath(newPoint))
                {
                    create = true;
                }
                attempt++;
            }
            // add new path to collection
            if (create)
            {
                positions.Add(newPoint);
                Draw();
            }
            Draw();
        }
        else
        {
            ChangeToAttackMode();
        }
    }

    private void ChangeToAttackMode()
    {
        Debug.Log("Attack mode!");
        positions.Add(goal);
        Draw();
        attacking = true;
    }

    // create a new branch
    void createChild()
    {
        Vector3 originPoint = positions[Random.Range(0, positions.Count)];
        bool create = false;
        Vector3 newPoint = new Vector3(0, 0, 0);

        int attempt = 0;
        //find a suitable path
        while (!create && attempt < 10)
        {
            newPoint = originPoint + GetNewPosition(originPoint);
            if (IsClearPath(newPoint))
            {
                create = true;
            }
            else
            {
                newPoint = originPoint + GetNewPosition(originPoint * (-0.1f));
            }

            attempt++;
        }

        // path found, intialize new object
        if (create)
        {
            SpawnChild(originPoint, transform.parent);
        }

        branchSpace = 0;
    }

    Root SpawnChild(Vector3 originPoint, Transform parent)
    {
        // create the new object
        GameObject newRef = Instantiate(rootPrefab, transform.position, transform.rotation, parent);
        Root newRoot = newRef.GetComponent<Root>();

        newRoot.Initialize(originPoint, mutationRate, goals[Random.Range(0, 3)], rootPrefab, daDrill);
        children.Add(newRoot);

        return newRoot;
    }

    // set up collision body
    void SetEdgeCollider()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();

        List<Vector2> edges = new List<Vector2>();

        // needs to be recreated for each new point
        foreach (Vector3 point in positions)
        {
            edges.Add(new Vector2(point.x, point.y));
        }

        edgeCollider.SetPoints(edges);
    }

    // decides if a branch is created
    void DoesBranch()
    {
        if (Vector3.Distance(positions.Last(), goal) < 3.0f)
        {
            mutationRate = -1.0f;
        }
        else if (Random.Range(0.0f, 1.0f) < mutationRate || branchSpace >= 8)
        {
            count++;
            createChild();
        }
    }

    // determines the modifier to a new paths X or Y values
    float DoStuff(float i)
    {
        if (i >= 0)
        {
            return Random.Range(-growRate * focus, growRate);
        }
        else
        {
            return Random.Range(-growRate, growRate * focus);
        }
    }

    // get the end point of a new path
    Vector3 GetNewPosition()
    {
        Vector3 direction = goal - positions.Last();

        float X = DoStuff(direction.x);
        float Y = DoStuff(direction.y);

        return new Vector3(X, Y, 0);
    }

    // see above, this allows for a specific origin to be specified
    Vector3 GetNewPosition(Vector3 origin)
    {
        Vector3 direction = goal - origin;

        float X = DoStuff(direction.x);
        float Y = DoStuff(direction.y);

        return new Vector3(X, Y, 0);
    }

    // is there a clear path to the new point
    bool IsClearPath(Vector3 newPoint)
    {
        Vector3 pointA = positions.Last() + 0.2f * (newPoint - positions.Last());// + newPoint * 0.1f;
        Debug.DrawRay(newPoint, pointA - newPoint, Color.green, 10000.0f, false);
        return !Physics2D.Linecast(newPoint, pointA);
    }

    public void Chop(Vector2 hitPoint)
    {

        FindObjectOfType<AudioManager>().Play("RootHurt");
        Debug.Log("chop!");
        Vector3 hit = hitPoint;
        hit = hit + new Vector3(0, 0, -2);
        Debug.Log(hit);

        Vector3 pointA = new Vector3(0, 0, 0);
        Vector3 pointB = new Vector3(0, 0, 0);
        bool found = false;

        for (int i = 1; i < positions.Count; i++)
        {
            Debug.Log(i);
            pointA = positions.ElementAt(i - 1);
            pointB = positions.ElementAt(i);
            //Debug.Log(pointA + " : " + pointB);
            float d1 = Vector3.Distance(pointA, hit);
            float d2 = Vector3.Distance(hit, pointB);
            float sum = d1 + d2;
            float length = Vector3.Distance(pointA, pointB);
            Debug.Log("d1: " + d1);
            Debug.Log("d2: " + d2);
            Debug.Log("Sum: " + sum);
            Debug.Log("Length: " + length);
            if (sum == length)
            {
                Debug.Log("found");
                split(pointA, pointB);
                break;
            }
        }
        Debug.Log("done");
    }

    void split(Vector3 A, Vector3 B)
    {
        Debug.Log("Splitting");
        growing = false;

        int i = positions.IndexOf(A);
        int end = positions.Count - i;
        //Debug.Log(i + " : " + end);

        SpawnChild(B, transform.parent).SetPositions(positions.GetRange(i, end));
        positions.RemoveRange(i, end);
        Draw();
    }
}
