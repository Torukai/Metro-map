using System;
using UnityEngine;

public class Grid<TGridObject>
{
	private int width, height;
	private TGridObject[,] gridArray;
	private float cellSize;
	private Vector3 originPosition;

	public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
	public class OnGridObjectChangedEventArgs : EventArgs
	{
		public int x, y;
	}

	public Grid (int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int,int, TGridObject> createGridObject)
	{
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPosition = originPosition;

		gridArray = new TGridObject[width, height];

		for (int x = 0; x < gridArray.GetLength (0); x++)
		{
			for (int y = 0; y < gridArray.GetLength (1); y++)
			{
				gridArray[x, y] = createGridObject (this, x, y);
			}
		}
	}

	public int GetHeight()
	{
		return height;
	}

	public int GetWidth()
	{
		return width;
	}

	public float GetCellSize()
	{
		return cellSize;
	}

	public Vector3 GetWorldPosition (int x, int y)
	{
		return new Vector3 (x, y) * cellSize + originPosition;
	}

	public void GetXY (Vector3 worldPosition, out int x, out int y)
	{
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
	}

	public void SetGridObject (int x, int y, TGridObject value)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
			gridArray[x, y] = value;
			if (OnGridObjectChanged != null)
				OnGridObjectChanged (this, new OnGridObjectChangedEventArgs { x = x, y = y });
		}
	}

	public void TriggerGridObjectChanged (int x, int y)
	{
		OnGridObjectChanged (this, new OnGridObjectChangedEventArgs { x = x, y = y });
	}

	public void SetGridObject (Vector3 worldPosition, TGridObject value)
	{
		int x, y;
		GetXY (worldPosition, out x, out y);
		SetGridObject (x, y, value);
	}

	public TGridObject GetGridObject (int x, int y)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
			return gridArray[x, y];
		}
		else
		{
			return default (TGridObject);
		}
	}

	public TGridObject GetGridObject (Vector3 worldPosition)
	{
		int x, y;
		GetXY (worldPosition, out x, out y);
		return GetGridObject (x, y);
	}

	public TGridObject[,] GetGridObjectByName (string name)
	{
		return gridArray;
	}

	public TGridObject GetGridObjectByIndex (int i, int j)
	{
		return gridArray[i, j];
	}
}
