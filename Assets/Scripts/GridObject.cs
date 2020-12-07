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
    public List<Type> _types = new List<Type>();
    public List<GridObject> _connections;

    public List<GridObject> connections
    {
        get
        {
            return _connections;
        }
    }

    public List<Type> types
    {
        get
        {
            return _types;
        }
    }

    public GridObject (Grid<GridObject> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        _types.Add(Type.Empty);
        _connections = new List<GridObject>();
    }

    public void AddChild (GridObject aWPoints)
    {
        _connections.Add (aWPoints);
    }

    public int GetX() => x;
    public int GetY() => y;

    public List<Type> GetGridTypes()
    {
        return _types;
    }

    public void SetGridTypes (params Type[] types)
    {
        _types.Clear();
        foreach (var v in types)
		{
            _types.Add(v);
		}
    }

    public override string ToString() {
        return Name.ToString();
    }
}
