using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private GridPrefabVisual gridPrefabVisual;
    private int lineCount = 0;
    private LineRenderer line;

    public Map map;
    public Pathfinding pathfinding = new Pathfinding();
    public Material connectionsMaterial;

    // Start is called before the first frame update
    void Start()
    {
        map = new Map();

        //metroMap = new MetroMap(14, 14, 8f, Vector3.zero);
        line = GetComponent<LineRenderer>();

        SetupStations();
        SetupConnections();
        DrawConnections();

        gridPrefabVisual.Setup(map.GetGrid());
        pathfinding.SetNodes(map.addedObjects);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
		{
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pathfinding.SetStartPosition(position, map.GetGrid());
		}

        if (Input.GetMouseButtonUp(1))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pathfinding.SetEndPosition(position, map.GetGrid());
            pathfinding.GetShortestPath();
        }
    }

    public void SetupStations()
    {
        map.SetCell(0, 13, "A", GridObject.Type.Red);
        map.SetCell(2, 11, "B", new GridObject.Type[] { GridObject.Type.Red, GridObject.Type.Black });
        map.SetCell(4, 9, "C", new GridObject.Type[] { GridObject.Type.Red, GridObject.Type.Green });
        map.SetCell(6, 7, "D", new GridObject.Type[] { GridObject.Type.Red, GridObject.Type.Blue });
        map.SetCell(10, 7, "E", new GridObject.Type[] { GridObject.Type.Red, GridObject.Type.Green });
        map.SetCell(13, 7, "F", new GridObject.Type[] { GridObject.Type.Red, GridObject.Type.Black });
        map.SetCell(13, 3, "G", GridObject.Type.Black);
        map.SetCell(6, 13, "H", GridObject.Type.Black);
        map.SetCell(8, 11, "J", new GridObject.Type[] { GridObject.Type.Green, GridObject.Type.Black, GridObject.Type.Blue });
        map.SetCell(2, 6, "K", GridObject.Type.Green);
        map.SetCell(4, 3, "L", new GridObject.Type[] { GridObject.Type.Green, GridObject.Type.Blue });
        map.SetCell(8, 3, "M", GridObject.Type.Green);
        map.SetCell(2, 0, "N", GridObject.Type.Blue);
        map.SetCell(11, 13, "O", GridObject.Type.Blue);
    }

    public void SetupConnections()
    {
        map.SetConnections(0, 13, new List<string> { "B" }); // A
        map.SetConnections(2, 11, new List<string> { "A", "H", "C" }); // B 
        map.SetConnections(4, 9, new List<string> { "B", "J", "K", "D" }); // C
        map.SetConnections(6, 7, new List<string> { "C", "J", "E", "L" }); // D
        map.SetConnections(10, 7, new List<string> { "D", "J", "F", "M" }); // E
        map.SetConnections(13, 7, new List<string> { "E", "G", "J" }); // F
        map.SetConnections(13, 3, new List<string> { "F" }); // G
        map.SetConnections(6, 13, new List<string> { "B", "J" }); // H
        map.SetConnections(8, 11, new List<string> { "H", "C", "D", "E", "F", "O" }); // J
        map.SetConnections(2, 6, new List<string> { "C", "L" }); // K
        map.SetConnections(4, 3, new List<string> { "K", "N", "M", "D" }); // L
        map.SetConnections(8, 3, new List<string> { "L", "E" }); // M
        map.SetConnections(2, 0, new List<string> { "L" }); // N
        map.SetConnections(11, 13, new List<string> { "J" }); // O
    }

    public void DrawConnections()
    {
        var objects = map.GetAddedObjects();
        for (int i = 0; i < objects.Count; i++)
        {
            GridObject source = objects[i];
            for (int j = 0; j < source.connections.Count; j++)
            {
                GridObject target = source.connections[j];
                line = new GameObject("Line" + lineCount).AddComponent<LineRenderer>();
                line.SetPosition(0, new Vector3((source.GetX() * 8f) + 4f, (source.GetY() * 8f) + 4f, 2f));
                line.SetPosition(1, new Vector3((target.GetX() * 8f) + 4f, (target.GetY() * 8f) + 4f, 2f));
                line.material = connectionsMaterial;
                line.useWorldSpace = true;
                line.positionCount = 2;
                lineCount++;
            }
        }
    }
}
