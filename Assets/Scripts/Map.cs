using System.Collections.Generic;
using UnityEngine;

public class Map
{
	private Grid<GridObject> grid;
	public List<GridObject> addedObjects;

	public Map()
	{
		grid = new Grid<GridObject>(14, 14, 8f, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
		addedObjects = new List<GridObject>();
	}

	public Grid<GridObject> GetGrid()
	{
		return grid;
	}

	public void SetCell(int i, int j, string name, GridObject.Type type)
	{
		GridObject gridObject = grid.GetGridObjectByIndex(i, j);
		if (gridObject != null)
		{
			gridObject.Name = name;
			gridObject.SetGridTypes(type);
			gridObject.SetGridType(type);
		}

		addedObjects.Add(gridObject);
	}

	public List<GridObject> GetAddedObjects()
	{
		return addedObjects;
	}

	public void SetCell (int i, int j, string name, GridObject.Type[] type)
	{
		GridObject gridObject = grid.GetGridObjectByIndex (i, j);
		if (gridObject != null)
		{
			gridObject.Name = name;
			gridObject.SetGridTypes (type);
			
		}
		addedObjects.Add(gridObject);
	}

	public void SetConnections (int i, int j, List<string> connections)
	{
		GridObject gridObject = grid.GetGridObjectByIndex (i, j);

		if (gridObject != null)
		{
			foreach (string s in connections)
			{
				gridObject.AddChild (addedObjects.Find (x => x.Name == s));
			}
		}
	}
}
