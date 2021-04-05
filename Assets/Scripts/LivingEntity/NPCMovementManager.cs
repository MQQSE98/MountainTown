using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCMovementManager : MonoBehaviour
{

    public class Graph
    {
        public class Node
        {
            public short id;
            public string scene;
            public Vector2 loc;
            public Node(short id, string scene, Vector2 loc)
            {
                this.id = id;
                this.scene = scene;
                this.loc = loc;
            }
        }

        public class Edge
        {
            public Node src;
            public Node dest;
            public Edge(Node src, Node dest)
            {
                this.src = src;
                this.dest = dest;
            }
        }

        Node[][] Adj_List = new Node[20][];
        public int vertices = 0;
        public Stack<Node> fullPath = new Stack<Node>();
        bool pathFound = false;

        public Graph(Edge[] edges)
        {
            int vertices = edges.Length;
            Adj_List[0] = new Node[2];
            Adj_List[1] = new Node[2];
            Adj_List[2] = new Node[1];
            Adj_List[3] = new Node[1];
            foreach (Edge e in edges)
            {
                //make a connection directional connection both ways betweens source and destination
                for (int i = 0; i < Adj_List[e.src.id].Length; i++)
                {
                    try
                    {
                        if (Adj_List[e.src.id][i].scene != null)
                        {

                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        Adj_List[e.src.id][i] = e.dest;
                        break;
                    }

                }
                for (int i = 0; i < Adj_List[e.dest.id].Length; i++)
                {
                    try
                    {
                        if (Adj_List[e.dest.id][i].scene != null)
                        {

                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        Adj_List[e.dest.id][i] = e.src;
                        break;
                    }

                }
            }
        }

        public Graph()
        {
            Adj_List[0] = new Node[1];
            Adj_List[1] = new Node[1];
            Adj_List[2] = new Node[1];
            Adj_List[3] = new Node[1];
            Adj_List[4] = new Node[1];
            Adj_List[5] = new Node[1];
            Adj_List[6] = new Node[1];
            Adj_List[7] = new Node[1];
            Adj_List[8] = new Node[1];
            Adj_List[9] = new Node[1];

            Adj_List[10] = new Node[10];
            Adj_List[11] = new Node[10];
            Adj_List[12] = new Node[10];
            Adj_List[13] = new Node[10];
            Adj_List[14] = new Node[10];
            Adj_List[15] = new Node[10];
            Adj_List[16] = new Node[10];
            Adj_List[17] = new Node[10];
            Adj_List[18] = new Node[10];
            Adj_List[19] = new Node[10];

            //Will start with 20 to start Town->interiors and interiors->Town
            Node[] door = new Node[20];
            //Town transitions
            door[0] = new Node(0, "House1", new Vector2(100, 100));
            door[1] = new Node(1, "House2", new Vector2(100, 100));
            door[2] = new Node(2, "House3", new Vector2(100, 100));
            door[3] = new Node(3, "House4", new Vector2(120, -30));
            door[4] = new Node(4, "Town Hall", new Vector2(100, 100));
            door[5] = new Node(5, "Shop", new Vector2(100, 100));
            door[6] = new Node(6, "Blacksmith", new Vector2(100, 100));
            door[7] = new Node(7, "Morgue", new Vector2(100, 100));
            door[8] = new Node(8, "Inn", new Vector2(100, 100));
            door[9] = new Node(9, "Library", new Vector2(100, 100));


            door[10] = new Node(10, "Town", new Vector2(31.45f, -15.28f));
            door[11] = new Node(11, "Town", new Vector2(39f, -14.95f));
            door[12] = new Node(12, "Town", new Vector2(31.5f, -24.18f));
            door[13] = new Node(13, "Town", new Vector2(39f, -24f));
            door[14] = new Node(14, "Town", new Vector2(17.52f, -16.2f));
            door[15] = new Node(15, "Town", new Vector2(19.09f, 3.17f));
            door[16] = new Node(16, "Town", new Vector2(31, -.61f));
            door[17] = new Node(17, "Town", new Vector2(-4.02f, -.41f));
            door[18] = new Node(18, "Town", new Vector2(7.03f, -.51f));
            door[19] = new Node(19, "Town", new Vector2(1.5f, -16.2f));

            //Creating edges of graph
            Edge[] edges = new Edge[55];
            edges[0] = new Edge(door[0], door[10]);
            edges[1] = new Edge(door[1], door[11]);
            edges[2] = new Edge(door[2], door[12]);
            edges[3] = new Edge(door[3], door[13]);
            edges[4] = new Edge(door[4], door[14]);
            edges[5] = new Edge(door[5], door[15]);
            edges[6] = new Edge(door[6], door[16]);
            edges[7] = new Edge(door[7], door[17]);
            edges[8] = new Edge(door[8], door[18]);
            edges[9] = new Edge(door[9], door[19]);

            AddEdge(edges[0]);
            AddEdge(edges[1]);
            AddEdge(edges[2]);
            AddEdge(edges[3]);
            AddEdge(edges[4]);
            AddEdge(edges[5]);
            AddEdge(edges[6]);
            AddEdge(edges[7]);
            AddEdge(edges[8]);
            AddEdge(edges[9]);

            //need edges between each town location
            edges[10] = new Edge(door[10], door[11]);
            edges[11] = new Edge(door[10], door[12]);
            edges[12] = new Edge(door[10], door[13]);
            edges[13] = new Edge(door[10], door[14]);
            edges[14] = new Edge(door[10], door[15]);
            edges[15] = new Edge(door[10], door[16]);
            edges[16] = new Edge(door[10], door[17]);
            edges[17] = new Edge(door[10], door[18]);
            edges[18] = new Edge(door[10], door[19]);

            edges[19] = new Edge(door[11], door[12]);
            edges[20] = new Edge(door[11], door[13]);
            edges[21] = new Edge(door[11], door[14]);
            edges[22] = new Edge(door[11], door[15]);
            edges[23] = new Edge(door[11], door[16]);
            edges[24] = new Edge(door[11], door[17]);
            edges[25] = new Edge(door[11], door[18]);
            edges[26] = new Edge(door[11], door[19]);

            edges[27] = new Edge(door[12], door[13]);
            edges[28] = new Edge(door[12], door[14]);
            edges[29] = new Edge(door[12], door[15]);
            edges[30] = new Edge(door[12], door[16]);
            edges[31] = new Edge(door[12], door[17]);
            edges[32] = new Edge(door[12], door[18]);
            edges[33] = new Edge(door[12], door[19]);

            edges[34] = new Edge(door[13], door[14]);
            edges[35] = new Edge(door[13], door[15]);
            edges[36] = new Edge(door[13], door[16]);
            edges[37] = new Edge(door[13], door[17]);
            edges[38] = new Edge(door[13], door[18]);
            edges[39] = new Edge(door[13], door[19]);

            edges[40] = new Edge(door[14], door[15]);
            edges[41] = new Edge(door[14], door[16]);
            edges[42] = new Edge(door[14], door[17]);
            edges[43] = new Edge(door[14], door[18]);
            edges[44] = new Edge(door[14], door[19]);

            edges[45] = new Edge(door[15], door[16]);
            edges[46] = new Edge(door[15], door[17]);
            edges[47] = new Edge(door[15], door[18]);
            edges[48] = new Edge(door[15], door[19]);

            edges[49] = new Edge(door[16], door[17]);
            edges[50] = new Edge(door[16], door[18]);
            edges[51] = new Edge(door[16], door[19]);

            edges[52] = new Edge(door[17], door[18]);
            edges[53] = new Edge(door[17], door[19]);

            edges[54] = new Edge(door[18], door[19]);

            AddEdge(edges[10]);
            AddEdge(edges[11]);
            AddEdge(edges[12]);
            AddEdge(edges[13]);
            AddEdge(edges[14]);
            AddEdge(edges[15]);
            AddEdge(edges[16]);
            AddEdge(edges[17]);
            AddEdge(edges[18]);
            AddEdge(edges[19]);
            AddEdge(edges[20]);
            AddEdge(edges[21]);
            AddEdge(edges[22]);
            AddEdge(edges[23]);
            AddEdge(edges[24]);
            AddEdge(edges[25]);
            AddEdge(edges[26]);
            AddEdge(edges[27]);
            AddEdge(edges[28]);
            AddEdge(edges[29]);
            AddEdge(edges[30]);
            AddEdge(edges[31]);
            AddEdge(edges[32]);
            AddEdge(edges[33]);
            AddEdge(edges[34]);
            AddEdge(edges[35]);
            AddEdge(edges[36]);
            AddEdge(edges[37]);
            AddEdge(edges[38]);
            AddEdge(edges[39]);
            AddEdge(edges[40]);
            AddEdge(edges[41]);
            AddEdge(edges[42]);
            AddEdge(edges[43]);
            AddEdge(edges[44]);
            AddEdge(edges[45]);
            AddEdge(edges[46]);
            AddEdge(edges[47]);
            AddEdge(edges[48]);
            AddEdge(edges[49]);
            AddEdge(edges[50]);
            AddEdge(edges[51]);
            AddEdge(edges[52]);
            AddEdge(edges[53]);
            AddEdge(edges[54]);

        }

        public void AddEdge(Edge edge)
        {
            if (vertices == 0)
            {
                vertices = 2;
            }
            else
            {
                vertices++;
            }

            for (int i = 0; i < Adj_List[edge.src.id].Length; i++)
            {
                try
                {
                    if (Adj_List[edge.src.id][i].scene != null)
                    {

                    }
                }
                catch (NullReferenceException ex)
                {
                    Adj_List[edge.src.id][i] = edge.dest;
                    break;
                }

            }
            for (int i = 0; i < Adj_List[edge.dest.id].Length; i++)
            {
                try
                {
                    if (Adj_List[edge.dest.id][i].scene != null)
                    {

                    }
                }
                catch (NullReferenceException ex)
                {
                    Adj_List[edge.dest.id][i] = edge.src;
                    break;
                }

            }
        }
        public void PrintGraph()
        {

            for (int i = 0; i < Adj_List.Length; i++)
            {
                for (int k = 0; k < Adj_List[i].Length; k++)
                {
                    try
                    {
                        if (Adj_List[i][k].scene != null)
                            Debug.Log("AdjList[" + i + "][" + k + "] = " + Adj_List[i][k].scene + "\n");
                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.Log("AdjList[" + i + "][" + k + "] = null\n");
                    }
                }
            }

        }


        void DFSHelper(short startID, short currentID, bool[] visited)
        {
            //find if the Destination with destID id == endID id
            fullPath.Push(FindNode(currentID));
            if (currentID == startID)
            {
                //we're done?
                //push last node
                pathFound = true;
                return;
            }
            // current node is visited 
            visited[currentID] = true;

            // process all adjacent vertices 
            for (int i = 0; i < Adj_List[currentID].Length; i++)
            {
                //Debug.Log("currentID: " + currentID + ", Length: " + Adj_List[currentID].Length+" vertice: "+vertices);
                //issue in line below
                if (!visited[Adj_List[currentID][i].id] && !pathFound)
                    DFSHelper(startID, Adj_List[currentID][i].id, visited);
                if (i == Adj_List[currentID].Length - 1 && !pathFound)
                    fullPath.Pop();

            }
        }

        public void PrintStack()
        {
            while (fullPath.Count > 0)
            {
                Debug.Log("Pop: " + fullPath.Pop().scene);
            }
        }

        public void DFS(string startScene, string endScene)
        {
            //initially none of the vertices are visited 
            Debug.Log("DFS Called");
            bool[] visited = new bool[vertices];

            // call recursive DFS_helper function for DFS technique
            short endID = FindID(endScene);
            short startID = FindID(startScene);
            DFSHelper(startID, endID, visited);
        }

        public short FindID(string scene)
        {
            //switch to compare Vector2 instead of scene
            for (int i = 0; i < Adj_List.Length; i++)
            {
                for (int k = 0; k < Adj_List[i].Length; k++)
                {
                    try
                    {
                        if (Adj_List[i][k].scene == scene)
                            return Adj_List[i][k].id;
                    }
                    catch (NullReferenceException ex)
                    {

                    }

                }
            }
            return -1;
        }

        public Node FindNode(short id)
        {
            Node temp = new Node(-1, "", new Vector2(0, 0));
            for (int i = 0; i < Adj_List.Length; i++)
            {
                for (int k = 0; k < Adj_List[i].Length; k++)
                {
                    try
                    {
                        if (Adj_List[i][k].id == id)
                        {
                            temp = Adj_List[i][k];
                            return Adj_List[i][k];
                        }
                    }
                    catch (NullReferenceException ex)
                    {

                    }

                }
            }
            return temp;
        }

    }

    protected Graph graph;

    [SerializeField]
    protected GameObject worldClock;

    public Rigidbody2D rb;
    public Animator animator;
    protected Seeker seeker;
    protected Path currentPath;
    bool onPath = false;
    private int currentWaypoint;


    //NPC moving information
    protected Destination currentLocation;
    protected Destination desiredLocation;


    protected float nextWaypointDistance = .5f;

    protected float minMoveTime = 1;
    protected float maxMoveTime = 3;
    private float moveTime;
    private float moveDelta = 0;
    protected float minRandDist = 6;
    protected float maxRandDist = 22;
    private Vector2 currentRandDest;

    protected float minIdleTime = 4;
    protected float maxIdleTime = 10;
    protected float idleTime;
    protected float idleDelta = 0;
    private Vector2 randomDirection;

    private bool randomMovement = false;

    bool testComplete = false;
    bool pathCalculated = false;

    protected bool pathCompleted = false;


    /// <summary>
    /// Saved locations for which location the NPC should travel to
    /// </summary>
    public struct Destination
    {

        public string scene;
        public Vector2 loc;

        public Destination(string scene, Vector2 loc)
        {
            this.scene = scene;
            this.loc = loc;
        }
        public Destination(string scene)
        {
            this.scene = scene;
            this.loc = new Vector2(0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        GameObject ChildGameObject1 = transform.GetChild(0).gameObject;
        animator = ChildGameObject1.GetComponent<Animator>();
        currentLocation = new Destination("Inn");
        desiredLocation = new Destination("Blacksmith", new Vector2(110, 110));
        seeker = this.GetComponent<Seeker>();

        /*
        Destination[] door = new Destination[20];
        door[0] = new Destination("Town", new Vector2(0, 0));
        door[1] = new Destination("Town", new Vector2(10, 0));
        door[2] = new Destination("House1", new Vector2(-20, 20));
        door[3] = new Destination("House2", new Vector2(15, 20));
        */
        //graph = new Graph(edges);
        graph = new Graph();
        //graph.PrintGraph(); 

    }

    // Update is called once per frame

    void FixedUpdate()
    {

        RandomBehavior();


    }

    void Update()
    {
        //HandleMovement(desiredLocation);
        //LoopedAnimation();
    }

    protected void HandleMovement(Destination desiredLocation)
    {
        pathCompleted = false;
        if ((rb.position - desiredLocation.loc).magnitude < .5f)
        {
            testComplete = true;
            return;
        }
        if (currentLocation.scene == desiredLocation.scene)
        {
            //move to destination.loc
            TargetBehavior(desiredLocation.loc);
        }
        else
        {
            if (!pathCalculated)
            {
                graph.DFS(currentLocation.scene, desiredLocation.scene);
                pathCalculated = true;
            }
            if (graph.fullPath.Peek().scene != currentLocation.scene)
            {
                //teleport
                onPath = false;
                transform.position = graph.fullPath.Peek().loc;
                currentLocation.scene = graph.fullPath.Peek().scene;
                graph.fullPath.Pop();
            }
            else
            {
                //walk
                if ((rb.position - graph.fullPath.Peek().loc).magnitude < .5f)
                {
                    // reached dest
                    graph.fullPath.Pop();
                }
                else
                {
                    TargetBehavior(graph.fullPath.Peek().loc);
                }

            }
        }
    }

    protected void TargetBehavior(Vector2 loc)
    {
        if (!onPath)
        {
            MoveToLoc(loc);
        }
        else
        {
            //check distance between here and next waypoint
            float distance = Vector2.Distance(rb.position,
            currentPath.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                //check if need to increase currentwaypoint
                if (currentWaypoint >= currentPath.vectorPath.Count - 1 || (rb.position - loc).magnitude < .5f)
                {
                    //you've reached last waypoint
                    onPath = false;
                    return;
                }
                else
                {
                    //you aint done yet, move along to next waypoint
                    currentWaypoint++;
                }
            }
            else
            {
                //you have not reached a waypoint on the path, so just keep walking
                rb.position = Vector2.MoveTowards(rb.position,
                (Vector2)currentPath.vectorPath[currentWaypoint], 3 * Time.fixedDeltaTime);
            }
        }
    }

    void MoveToLoc(Vector2 loc)
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, loc, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            currentPath = p;
            currentWaypoint = 1;
            onPath = true;
        }
    }

    /// <summary>
    /// Create a random location to travel to and assign it to the currentRandLoc variable
    /// </summary>
    /// <returns>random location for the enemy to move to</returns>
    void GetRandomDestination()
    {
        int dir = UnityEngine.Random.Range(0, 4);
        float dist = UnityEngine.Random.Range(minRandDist, maxRandDist + 1);
        switch (dir)
        {
            case 0:
                currentRandDest = new Vector2(rb.position.x + dist, rb.position.y);
                break;
            case 1:
                currentRandDest = new Vector2(rb.position.x - dist, rb.position.y);
                break;
            case 2:
                currentRandDest = new Vector2(rb.position.x, rb.position.y + dist);
                break;
            case 3:
                currentRandDest = new Vector2(rb.position.x, rb.position.y - dist);
                break;
        }
        randomMovement = true;
    }

    protected void RandomBehavior()
    {
        if (!randomMovement)
        {
            if (idleDelta > idleTime)
            {
                idleDelta = 0;
                GetRandomDestination();
                SetRandomIdleTime();
                MoveToLoc(currentRandDest);
            }
            else
            {
                idleDelta += Time.fixedDeltaTime;
            }
        }
        else
        {
            float distance = 0;
            try
            {
                distance = Vector2.Distance(rb.position,
                currentPath.vectorPath[currentWaypoint]);
            }
            catch (NullReferenceException ex)
            {

            }

            //Debug.Log("Distance: "+distance);
            if (distance < nextWaypointDistance)
            {
                //need to update waypoint
                currentWaypoint++;
            }
            if (currentWaypoint >= currentPath.vectorPath.Count)
            {
                //reached destination
                randomMovement = false;
                return;
            }
            //currently on random path, so continue moving
            rb.position = Vector2.MoveTowards(rb.position,
                (Vector2)currentPath.vectorPath[currentWaypoint], 3 * Time.fixedDeltaTime);
            return;
        }
    }

    void SetRandomIdleTime()
    {
        idleTime = UnityEngine.Random.Range(minIdleTime, maxIdleTime + 1);
    }

    protected void LoopedAnimation()
    {
        float vertical = 0;
        float horizontal = 0;
        if (currentPath != null)
        {
            if (currentWaypoint < currentPath.vectorPath.Count - 1)
            {
                //still on path
                horizontal = currentPath.vectorPath[currentWaypoint].x - rb.position.x;
                vertical = currentPath.vectorPath[currentWaypoint].y - rb.position.y;
            }
        }

        //checking if next movement-if so removes one direction for animation 
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0;
        }
        else
        {
            horizontal = 0;
        }

        if (!onPath)
        {
            animator.SetBool("Move_U", false);
            animator.SetBool("Move_R", false);
            animator.SetBool("Move_L", false);
            animator.SetBool("Move_D", false);
            return;
        }
        //Here need to set back to idle if coming off of a movement period into an idle one 
        if (horizontal != 0)
        {
            //moving horizontal
            if (horizontal > 0)
            {
                //moving right
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_R", true);
            }
            else
            {
                //moving left
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", true);
            }
        }
        else
        {
            //moving vertical
            if (vertical > 0)
            {
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_U", true);
            }
            else
            {
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_D", true);
            }
        }
    }

}