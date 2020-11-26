using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public enum Route
	{
        Red,
        Blue,
        Green,
        Black
	}


    /// <summary>
    /// The connections (neighbors).
    /// </summary>
    [SerializeField]
    protected List<Waypoint> m_Connections = new List<Waypoint>();

    /// <summary>
    /// Gets the connections (neighbors).
    /// </summary>
    /// <value>The connections.</value>
    public virtual List<Waypoint> connections
    {
        get
        {
            return m_Connections;
        }
    }

    public Waypoint this[int index]
    {
        get
        {
            return m_Connections[index];
        }
    }



    public List<Route> routes;
    public bool IsTransferPoint = false;
    public int index;
    private int x, y;
    List<Waypoint> children;

    private Grid<Waypoint> grid;
    public int gCost, hCost, fCost;
    public Waypoint previousNode;

    public Waypoint(Grid<Waypoint> grid, int x, int y)
	{
        this.grid = grid;
        this.x = x;
        this.y = y;
	}

    public void IncreaseIndex()
	{
        index += 1;
	}

    public void AddValue(int newValue)
	{
        index += newValue;
        grid.TrigerGridObjectChanged(x, y);
	}

	public override string ToString()
	{
		return index.ToString();
	}

	/**
	 * Creates a new vertex with empty edges.
	 * @param vName Name of the vertex
	 */
	// Adds all the specified waypoints to the child list
	public void AddChild(params Waypoint[] aWPoints)
    {
        foreach (var point in aWPoints)
		{
            children.Add(point);
        } 
    }

    // Returns a true if the points are adjacent
    public bool IsAdj(Waypoint waypoint)
	{
        if (children.Contains(waypoint))
		{
            return true;
		}
        return false;
	}

    public bool IsSameRoute(Waypoint waypoint)
	{
        foreach(Route r in waypoint.routes)
		{
            if (this.routes.Contains(r))
            {
                return true;
            }
        }
        return false;
	}
};
