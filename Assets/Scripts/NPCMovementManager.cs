using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCMovementManager : MonoBehaviour {

    public struct Destination {

        public string scene;
        public Vector2 loc;

        public Destination (string scene, Vector2 loc) {
            this.scene = scene;
            this.loc = loc;
        }
        public Destination(string scene) {
            this.scene = scene;
            this.loc = new Vector2(0, 0);
        }
    }

    public class Graph {
        public class Node {
            public short id;
            public string scene;
            public Vector2 loc;
            public Node(short id, string scene, Vector2 loc) {
                this.id = id;
                this.scene = scene;
                this.loc = loc;
            }
        }
        public class Edge {
            public Node src;
            public Node dest;
            public Edge(Node src, Node dest) {
                this.src = src;
                this.dest = dest;
            }
        }
        Node[][] Adj_List = new Node[4][];
        public int vertices = 0;
        public Stack<Node> fullPath = new Stack<Node>();
        bool pathFound = false;

        public Graph(Edge[] edges) {
            int vertices = edges.Length;
            Adj_List[0] = new Node[2];
            Adj_List[1] = new Node[2];
            Adj_List[2] = new Node[1];
            Adj_List[3] = new Node[1];
            foreach(Edge e in edges) {
                //make a connection directional connection both ways betweens source and destination
                for(int i = 0; i < Adj_List[e.src.id].Length; i++) {
                    try {
                        if(Adj_List[e.src.id][i].scene != null) {
                            
                        }
                    } catch(NullReferenceException ex) {
                        Adj_List[e.src.id][i] = e.dest;
                        break;
                    }
                    
                }
                for (int i = 0; i < Adj_List[e.dest.id].Length; i++) {
                    try {
                        if (Adj_List[e.dest.id][i].scene != null) {

                        }
                    } catch (NullReferenceException ex) {
                        Adj_List[e.dest.id][i] = e.src;
                        break;
                    }

                }
            }
        }

        public Graph() {
            Adj_List[0] = new Node[2];
            Adj_List[1] = new Node[2];
            Adj_List[2] = new Node[1];
            Adj_List[3] = new Node[1];
            Node[] door = new Node[20];
            door[0] = new Node(0, "Town", new Vector2(-5, 4));
            door[1] = new Node(1, "Town", new Vector2(12, 4));
            door[2] = new Node(2, "House1", new Vector2(-7, 12));
            door[3] = new Node(3, "House2", new Vector2(16, 12));
            //edges will

            Edge[] edges = new Edge[3];
            edges[0] = new Edge(door[2], door[0]);
            edges[1] = new Edge(door[0], door[1]);
            edges[2] = new Edge(door[1], door[3]);
            AddEdge(edges[0]);
            AddEdge(edges[1]);
            AddEdge(edges[2]);
        }

        public void AddEdge(Edge edge) {
            if(vertices == 0) {
                vertices = 2;
            } else {
                vertices++;
            }
            
            for (int i = 0; i < Adj_List[edge.src.id].Length; i++) {
                try {
                    if (Adj_List[edge.src.id][i].scene != null) {

                    }
                } catch (NullReferenceException ex) {
                    Adj_List[edge.src.id][i] = edge.dest;
                    break;
                }

            }
            for (int i = 0; i < Adj_List[edge.dest.id].Length; i++) {
                try {
                    if (Adj_List[edge.dest.id][i].scene != null) {

                    }
                } catch (NullReferenceException ex) {
                    Adj_List[edge.dest.id][i] = edge.src;
                    break;
                }

            }
        }
        public void PrintGraph() {

            Debug.Log(Adj_List[2][0].scene);
            Debug.Log(Adj_List[0][0].scene);

            Debug.Log("");

            Debug.Log(Adj_List[0][1].scene);
            Debug.Log(Adj_List[1][0].scene);

            Debug.Log("");

            Debug.Log(Adj_List[1][1].scene);
            Debug.Log(Adj_List[3][0].scene);

        }

        
        void DFSHelper(short startID, short currentID, bool[] visited) {
            //find if the Destination with destID id == endID id
            fullPath.Push(FindNode(currentID));
            if (currentID == startID) {
                //we're done?
                //push last node
                pathFound = true;
                return;
            }
            // current node is visited 
            visited[currentID] = true;
            
            // process all adjacent vertices 
            for (int i = 0; i < Adj_List[currentID].Length; i++) {
                //Debug.Log("currentID: " + currentID + ", Length: " + Adj_List[currentID].Length+" vertice: "+vertices);
                //issue in line below
                if (!visited[Adj_List[currentID][i].id] && !pathFound) 
                    DFSHelper(startID, Adj_List[currentID][i].id, visited);
                if (i == Adj_List[currentID].Length - 1 && !pathFound)
                  fullPath.Pop();

            }
        }

        public void PrintStack() {
            while(fullPath.Count > 0) {
                Debug.Log("Pop: " + fullPath.Pop().scene);
            }
        }

        public void DFS(string startScene, string endScene) {
            //initially none of the vertices are visited 
            bool[] visited = new bool[vertices];

            // call recursive DFS_helper function for DFS technique
            short endID = FindID(endScene);
            short startID = FindID(startScene);
            DFSHelper(startID, endID, visited);
        }

        public short FindID(string scene) {
            for(int i = 0; i < Adj_List.Length; i++) {
                for(int k = 0; k < Adj_List[i].Length; k++) {
                    if(Adj_List[i][k].scene == scene) {
                        return Adj_List[i][k].id;
                    }
                }
            }
            return -1;
        }

        public Node FindNode(short id) {
            Node temp = new Node(-1, "", new Vector2(0,0));
            for (int i = 0; i < Adj_List.Length; i++) {
                for (int k = 0; k < Adj_List[i].Length; k++) {
                    if (Adj_List[i][k].id == id) {
                        temp = Adj_List[i][k];
                        return Adj_List[i][k];
                    }
                }
            }
            return temp;
        }
        
    }

    Graph graph;

    //currentScene contains a reference to the current scene NPC is in
    private Destination currentScene;

    public Rigidbody2D rb;
    public Animator animator;
    Seeker seeker;
    Path currentPath;
    bool onPath = false;
    private int currentWaypoint;
    Destination currentLocation;
    Destination desiredLocation;
    protected float nextWaypointDistance = .5f;

    protected float minMoveTime = 1;
    protected float maxMoveTime = 2;
    private float moveTime;
    private float moveDelta = 0;
    protected float minRandDist = 1;
    protected float maxRandDist = 3;
    private Vector2 currentRandDest;

    protected float minIdleTime = 1;
    protected float maxIdleTime = 1;
    protected float idleTime;
    protected float idleDelta = 0;
    private Vector2 randomDirection;

    private bool randomMovement = false;

    bool testComplete = false;
    bool pathCalculated = false;
    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        GameObject ChildGameObject1 = transform.GetChild(0).gameObject;
        animator = ChildGameObject1.GetComponent<Animator>();
        currentLocation = new Destination("House1");
        desiredLocation = new Destination("House2", new Vector2(18, 15));
        seeker = this.GetComponent<Seeker>();
        
        /**
        Destination[] door = new Destination[20];
        door[0] = new Destination(0, "Town", new Vector2(0, 0));
        door[1] = new Destination(1, "Town", new Vector2(10, 0));
        door[2] = new Destination(2, "House1", new Vector2(-20, 20));
        door[3] = new Destination(3, "House2", new Vector2(15, 20));
        */
        //graph = new Graph(edges);
        graph = new Graph();
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        if(!testComplete) {
            HandleMovement(desiredLocation);
        } else {
            RandomBehavior();
        }
        
    }

    void Update() {
        LoopedAnimation();
    }

    void HandleMovement(Destination desiredLocation) {
        if ((rb.position - desiredLocation.loc).magnitude < .5f) {
            testComplete = true;
            return;
        }
        if (currentLocation.scene == desiredLocation.scene) {
            //move to destination.loc
            TargetBehavior(desiredLocation.loc);
        } else {
            if(!pathCalculated) {
                graph.DFS(currentLocation.scene, desiredLocation.scene);
                pathCalculated = true;
            }
            if(graph.fullPath.Peek().scene != currentLocation.scene){
                //teleport
                onPath = false;
                transform.position = graph.fullPath.Peek().loc;
                currentLocation.scene = graph.fullPath.Peek().scene;
                graph.fullPath.Pop();
            } else {
                //walk
                if((rb.position- graph.fullPath.Peek().loc).magnitude < .5f) {
                    // reached dest
                    graph.fullPath.Pop();
                } else {
                    TargetBehavior(graph.fullPath.Peek().loc);
                }
                
            }
        }
    }

    protected void TargetBehavior(Vector2 loc) {
        if(!onPath) {
            MoveToLoc(loc);
        } else {
            //check distance between here and next waypoint
            float distance = Vector2.Distance(rb.position,
            currentPath.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                //check if need to increase currentwaypoint
                if (currentWaypoint >= currentPath.vectorPath.Count - 1 || (rb.position - loc).magnitude < .5f) {
                    onPath = false;
                    return;
                } else {
                    currentWaypoint++;
                }
            } else {
                    rb.position = Vector2.MoveTowards(rb.position,
                    (Vector2)currentPath.vectorPath[currentWaypoint], 3 * Time.fixedDeltaTime);
            }
        }    
    }

    void MoveToLoc(Vector2 loc) {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, loc, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            currentPath = p;
            currentWaypoint = 1;
            onPath = true;
        }
    }

    /// <summary>
    /// Create a random location to travel to and assign it to the currentRandLoc variable
    /// </summary>
    /// <returns>random location for the enemy to move to</returns>
    void GetRandomDestination() {
        int dir = UnityEngine.Random.Range(0, 4);
        float dist = UnityEngine.Random.Range(minRandDist, maxRandDist + 1);
        switch (dir) {
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

    protected void RandomBehavior() {
        if (!randomMovement) {
            if (idleDelta > idleTime) {
                idleDelta = 0;
                GetRandomDestination();
                SetRandomIdleTime();
                MoveToLoc(currentRandDest);
            } else {
                idleDelta += Time.fixedDeltaTime;
            }
        } else {
            float distance = Vector2.Distance(rb.position,
                currentPath.vectorPath[currentWaypoint]);
            //Debug.Log("Distance: "+distance);
            if (distance < nextWaypointDistance) {
                //need to update waypoint
                currentWaypoint++;
            }
            if (currentWaypoint >= currentPath.vectorPath.Count) {
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

    void SetRandomIdleTime() {
        idleTime = UnityEngine.Random.Range(minIdleTime, maxIdleTime + 1);
    }

    protected void LoopedAnimation() {
        float vertical = 0;
        float horizontal = 0;
        if (currentPath != null) {
            if (currentWaypoint < currentPath.vectorPath.Count - 1) {
                //still on path
                horizontal = currentPath.vectorPath[currentWaypoint].x - rb.position.x;
                vertical = currentPath.vectorPath[currentWaypoint].y - rb.position.y;
            }
        }
        
        //checking if next movement-if so removes one direction for animation 
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
            vertical = 0;
        } else {
            horizontal = 0;
        }

        if (!onPath) {
            animator.SetBool("Move_U", false);
            animator.SetBool("Move_R", false);
            animator.SetBool("Move_L", false);
            animator.SetBool("Move_D", false);
            return;
        }
        //Here need to set back to idle if coming off of a movement period into an idle one 
        if (horizontal != 0) {
            //moving horizontal
            if (horizontal > 0) {
                //moving right
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_R", true);
            } else {
                //moving left
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", true);
            }
        } else {
            //moving vertical
            if (vertical > 0) {
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_D", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_U", true);
            } else {
                animator.SetBool("Move_U", false);
                animator.SetBool("Move_R", false);
                animator.SetBool("Move_L", false);
                animator.SetBool("Move_D", true);
            }
        }
    }

}




