using System.Collections.Generic;
using UnityEngine;

public class Map
{
	private Grid<GridObject> _grid;
	private List<GridObject> _objects;

	public List<GridObject> objects
	{
		get
		{
			return _objects;
		}
	}

	public Grid<GridObject> grid
	{
		get
		{
			return _grid;
		}
	}

	public Map()
	{
		_grid = new Grid<GridObject>(14, 14, 8f, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
		_objects = new List<GridObject>();
	}

	public void SetCell (int i, int j, string name, GridObject.Type type)
	{
		GridObject gridObject = grid.GetGridObjectByIndex (i, j);
		if (gridObject != null)
		{
			gridObject.Name = name;
			gridObject.SetGridTypes (type);
		}
		_objects.Add (gridObject);
	}

	public void SetCell (int i, int j, string name, GridObject.Type[] type)
	{
		GridObject gridObject = grid.GetGridObjectByIndex (i, j);
		if (gridObject != null)
		{
			gridObject.Name = name;
			gridObject.SetGridTypes (type);
			
		}
		_objects.Add(gridObject);
	}

	public void SetConnections (int i, int j, List<string> connections)
	{
		GridObject gridObject = grid.GetGridObjectByIndex (i, j);

		if (gridObject != null)
		{
			foreach (string s in connections)
			{
				gridObject.AddChild (_objects.Find (x => x.Name == s));
			}
		}
	}
}
