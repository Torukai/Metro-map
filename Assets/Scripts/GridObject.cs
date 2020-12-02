using System.Collections.Generic;

public class GridObject {

    public enum Type {
        Empty,
        Red,
        Green,
        Blue,
        Black
    }

    private Grid<GridObject> grid;
    public string Name;
    private int x, y;
    private Type type;
    public List<Type> types = new List<Type>();
    public List<GridObject> m_Connections;

    public virtual List<GridObject> connections
    {
        get
        {
            return m_Connections;
        }
    }

    public GridObject this[int index]
    {
        get
        {
            return m_Connections[index];
        }
    }

    public GridObject (Grid<GridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        type = Type.Empty;
        m_Connections = new List<GridObject>();
    }

    public void AddChild (GridObject aWPoints)
    {
        m_Connections.Add (aWPoints);
    }

    public int GetX() => x;
    public int GetY() => y;

    public Type GetGridType() {
        return type;
    }

    public List<Type> GetGridTypes()
    {
        return types;
    }

    public void SetGridType (Type type) {
        this.type = type;
    }

    public void SetGridTypes (params Type[] types)
    {
        foreach (var v in types)
		{
            this.types.Add(v);
		}
    }

    public override string ToString() {
        return Name.ToString();
    }
}
