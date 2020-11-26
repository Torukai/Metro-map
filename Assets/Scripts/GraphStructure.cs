using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using System.Linq;

class GraphStructure
{
    public class NodeData
    {
        public int index;
        public string name;
        public string[] route; // identifies the route this point belongs to
        public NodeData(string name, string[] route, int index)
        {
            this.index = index;
            this.name = name;
            this.route = route;
        }


        public bool IsSameRoute(NodeData node)
		{
            string a = this.route.Intersect(node.route).ToString();
            if (a != null)
			{
                return true;
            }

            return false;
		}
    }
    /// <summary>
    /// 4 attributes
    /// A list of vertices (to store node information for each index such as name/text)
    /// a 2D array - our adjacency matrix, stores edges between vertices
    /// a graphSize integer
    /// a StreamReader, to read in graph data to create the data structure
    /// </summary>
    private List<NodeData> vertices;
    private int graphSize = 14;
    //private StreamReader sr;
    private int[,] adjMatrix;
    private const int infinity = 9999;
    public GraphStructure()
    {
        vertices = new List<NodeData>();
        //sr = new StreamReader("graph.txt"); ADD AJACENCY MATRIX

        CreateGraph();
    }

    private void InitPoints()
    {
        vertices.Add(new NodeData("A", new string[] { "Red" }, 0));
        vertices.Add(new NodeData("B", new string[] { "Red", "Black" }, 1));
        vertices.Add(new NodeData("C", new string[] { "Red", "Green" }, 2));
        vertices.Add(new NodeData("D", new string[] { "Red" , "Blue"}, 3));
        vertices.Add(new NodeData("E", new string[] { "Red", "Green" }, 4));
        vertices.Add(new NodeData("F", new string[] { "Red", "Black" }, 5));
        vertices.Add(new NodeData("G", new string[] { "Black" }, 6));
        vertices.Add(new NodeData("H", new string[] { "Black" }, 7));
        vertices.Add(new NodeData("J", new string[] { "Blue", "Green", "Black" }, 8));
        vertices.Add(new NodeData("K", new string[] { "Green" }, 9));
        vertices.Add(new NodeData("L", new string[] { "Blue", "Green" }, 10));
        vertices.Add(new NodeData("M", new string[] { "Green" }, 11));
        vertices.Add(new NodeData("N", new string[] { "Blue" }, 12));
        vertices.Add(new NodeData("O", new string[] { "Blue" }, 13));
    }

    private NodeData Vertex(string name)
	{
        NodeData node = vertices.Find(v => v.name.Equals(name));
        return node;
	}

    // Returns distance between 2 nodes
    private int Distance(NodeData n1, NodeData n2)
	{
        int distance = infinity;
        if (Adjacent(n1, n2))
		{
            distance = 2;
            if (n1.IsSameRoute(n2))
            {
                distance = 1;
            }
        }
        return distance;   
	}
    private void InitEdges()
	{
        // Red path
        this.AddEdge(Vertex("A"), Vertex("B"), 1);   // A -> B
        this.AddEdge(Vertex("B"), Vertex("C"), 1);   // B -> C
        this.AddEdge(Vertex("C"), Vertex("D"), 1);   // C -> D
        this.AddEdge(Vertex("D"), Vertex("E"), 1);   // D -> E
        this.AddEdge(Vertex("E"), Vertex("F"), 1);   // E -> F

        //// Blue path
        //this.AddEdge(12, 10, 1); // N -> L
        //this.AddEdge(10, 3, 1);  // L -> D
        //this.AddEdge(3, 8, 1);   // D -> J
        //this.AddEdge(8, 13, 1);  // J -> O

        //// Green path
        //this.AddEdge(10, 9, 1);  // L -> K
        //this.AddEdge(9, 2, 1);   // K -> C
        //this.AddEdge(2, 8, 1);   // C -> J
        //this.AddEdge(8, 4, 1);   // J -> E
        //this.AddEdge(4, 11, 1);  // E -> M
        //this.AddEdge(11, 10, 1); // M -> L

        //// Black path
        //this.AddEdge(1, 7, 1);   // B -> H
        //this.AddEdge(7, 8, 1);   // H -> J
        //this.AddEdge(8, 5, 1);   // J -> F
        //this.AddEdge(5, 6, 1);   // F -> G

    }
    private void CreateGraph()
    {

        //get the graph size first
        graphSize = 14;//Convert.ToInt32(sr.ReadLine()) + 1;//non-zero arrays, add 1
        adjMatrix = new int[graphSize, graphSize];

        InitPoints();
        InitEdges();
        //ASSUME ALL DATA HAS BEEN READ FROM A TEXT FILE & ADJACENCY MATRIX HAS BEEN INITIALIZED
        RunDijkstra();
    }

    public void RunDijkstra()//runs dijkstras algorithm on the adjacency matrix
    {
        Console.WriteLine("***********Dijkstra's Shortest Path***********");
        int[] distance = new int[graphSize];
        int[] previous = new int[graphSize];

        for (int i = 1; i < graphSize; i++)
        {
            distance[i] = infinity;
            previous[i] = 0;
        }
        int source = 1;
        distance[0] = 0;
        PriorityQueue<int> pq = new PriorityQueue<int>();
        //enqueue the source
        pq.Enqueue(source, 1);
        //insert all remaining vertices into the pq
        for (int i = 1; i < graphSize; i++)
        {
            for (int j = 1; j < graphSize; j++)
            {
                if (adjMatrix[i, j] > 0)
                {
                    pq.Enqueue(i, adjMatrix[i, j]);
                }
            }
        }
        while (!pq.empty())
        {
            int u = pq.dequeue_min();

            for (int v = 1; v < graphSize; v++) //scan each row fully
            {
                if (adjMatrix[u, v] > 0) //if there is an adjacent node
                {
                    int alt = distance[u] + adjMatrix[u, v];
                    if (alt < distance[v])
                    {
                        distance[v] = alt;
                        previous[v] = u;
                        pq.Enqueue(u, distance[v]);
                    }
                }
            }
        }
        //distance to 1..2..3..4..5..6 etc lie inside each index

        for (int i = 1; i < graphSize; i++)
        {
            //Console.WriteLine("Distance from {0} to {1}: {2}", source, i, distance[i]);
        }
        //printPath(previous, source, graphSize - 1);
    }

    private static bool BFS(List<List<int>> adj,
                                int src, int dest,
                                int v, int[] pred,
                                int[] dist)
    {
        // a queue to maintain queue of 
        // vertices whose adjacency list 
        // is to be scanned as per normal
        // BFS algorithm using List of int type
        List<int> queue = new List<int>();

        // bool array visited[] which 
        // stores the information whether 
        // ith vertex is reached at least 
        // once in the Breadth first search
        bool[] visited = new bool[v];

        // initially all vertices are 
        // unvisited so v[i] for all i 
        // is false and as no path is 
        // yet constructed dist[i] for 
        // all i set to infinity
        for (int i = 0; i < v; i++)
        {
            visited[i] = false;
            dist[i] = int.MaxValue;
            pred[i] = -1;
        }

        // now source is first to be 
        // visited and distance from 
        // source to itself should be 0
        visited[src] = true;
        dist[src] = 0;
        queue.Add(src);

        // bfs Algorithm
        while (queue.Count != 0)
        {
            int u = queue[0];
            queue.RemoveAt(0);

            for (int i = 0;
                    i < adj[u].Count; i++)
            {
                if (visited[adj[u][i]] == false)
                {
                    visited[adj[u][i]] = true;
                    dist[adj[u][i]] = dist[u] + 1;
                    pred[adj[u][i]] = u;
                    queue.Add(adj[u][i]);

                    // stopping condition (when we 
                    // find our destination)
                    if (adj[u][i] == dest)
                        return true;
                }
            }
        }
        return false;
    }

    private void printPath(int[] path, int start, int end)
    {
        //prints a path, given a start and end, and an array that holds previous 
        //nodes visited
        Console.WriteLine("Shortest path from source to destination:");
        int temp = end;
        Stack<int> s = new Stack<int>();
        while (temp != start)
        {
            s.Push(temp);
            temp = path[temp];
        }
        Debug.Log(temp);
        Console.Write("{0} ", temp);//print source
        while (s.Count != 0)
        {
            Debug.Log(s.Pop());
            //Console.Write("{0} ", s.Pop());//print successive nodes to destination
        }
    }
    public void AddEdge(NodeData vertexA, NodeData vertexB, int distance)
    {
        if (vertexA.index > 0 && vertexB.index > 0 && vertexA.index <= graphSize && vertexB.index <= graphSize)
        {
            adjMatrix[vertexA.index, vertexB.index] = distance;
        }
    }
    public void RemoveEdge(int vertexA, int vertexB)
    {
        if (vertexA > 0 && vertexB > 0 && vertexA <= graphSize && vertexB <= graphSize)
        {
            adjMatrix[vertexA, vertexB] = 0;
        }
    }
    public bool Adjacent(NodeData vertexA, NodeData vertexB)
    {   //checks whether two vertices are adjacent, returns true or false
        return (adjMatrix[vertexA.index, vertexB.index] > 0);
    }
    public int length(int vertex_u, int vertex_v)//returns a distance between 2 nodes
    {
        return adjMatrix[vertex_u, vertex_v];
    }
    public void Display() //displays the adjacency matrix
    {
        //Console.WriteLine("***********Adjacency Matrix Representation***********");
        //Console.WriteLine("Number of nodes: {0}\n", graphSize - 1);
        //foreach (NodeData n in vertices)
        //{
        //    Console.Write("{0}\t", n.name);
        //}
        //Console.WriteLine();//newline for the graph display
        //for (int i = 1; i < graphSize; i++)
        //{
        //    Console.Write("{0}\t", vertices[adjMatrix[i, 0]].name);
        //    for (int j = 1; j < graphSize; j++)
        //    {
        //        Console.Write("{0}\t", adjMatrix[i, j]);
        //    }
        //    Console.WriteLine();
        //    Console.WriteLine();
        //}
        //Console.WriteLine("Read the graph from left to right");
        //Console.WriteLine("Example: Node A has an edge to Node C with distance: {0}",
        //    length(1, 3));
    }
    private void DisplayNodeData(int v)//displays data/description for a node
    {
        //Console.WriteLine(vertices[v].name);
    }
}

//public static class NodeHelper
//{
//    public static void Add(this Dictionary<string, Node> dict, string nodename)
//    {
//        dict.Add(nodename, new Node(nodename));
//    }
//    public static void Connect(this Dictionary<string, Node> dict, string from, string to)
//    {
//        dict[from].Successors.Add(dict[to]);
//        dict[to].Predecessors.Add(dict[from]);
//    }
//};

//// Start is called before the first frame update
//class Example
//{
//    public List<Node> InitGraph()
//    {
//        var nodes = new Dictionary<string, Node>();

//        nodes.Add("Head", new Node("Head"));
//        nodes.Add("T1", new Node("T1"));
//        nodes.Add("T2", new Node("T2"));
//        // While that works, a method is nicer:
//        nodes.Add("C1");

//        // These two lines should really be factored out to a single method call
//        nodes["Head"].Successors.Add(nodes["T1"]);
//        nodes["T1"].Predecessors.Add(nodes["Head"]);
//        nodes["Head"].Successors.Add(nodes["T2"]);
//        nodes["T2"].Predecessors.Add(nodes["Head"]);

//        // Yes. Much nicer
//        nodes.Connect("Head", "C1");
//        nodes.Connect("T1", "C1");
//        nodes.Connect("T2", "C1");

//        var nodelist = new List<Node>(nodes.Values);
//        return nodelist;
//    }
//}


//public class Node
//{
//    public string Name { get; set; }
//    public int Coolness { get; set; }
//    public List<Node> Predecessors { get; set; }
//    public List<Node> Successors { get; set; }
//    public Node()
//    {
//        Coolness = 1;
//    }

//    public Node(string name) : this()
//    {
//        this.Name = name;
//    }
//};