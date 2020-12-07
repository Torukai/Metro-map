using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
	[SerializeField] private List<GridObject> _nodes = new List<GridObject>();
	public GridObject start;
	public GridObject end;
	public int count = 0;
	public Path path;

	public List<GridObject> nodes
	{
		get
		{
			return _nodes;
		}
	}

	public void GetShortestPath()
	{
		if (start == null || end == null)
		{
			throw new ArgumentNullException();
		}

		// The final path
		path = new Path();

		// If the start and end are same node, we can return the start node
		if (start == end)
		{
			path.nodes.Add (start);
			Debug.Log("Path: " + path); ;
		}

		// The list of unvisited nodes
		List<GridObject> unvisited = new List<GridObject>();

		// Previous nodes in optimal path from source
		Dictionary<GridObject, GridObject> previous = new Dictionary<GridObject, GridObject>();

		// The calculated distances, set all to Infinity at start, except the start Node
		Dictionary<GridObject, int> distances = new Dictionary<GridObject, int>();

		for (int i = 0; i < _nodes.Count; i++)
		{
			GridObject node = _nodes[i];
			unvisited.Add (node);

			// Setting the node distance to Infinity
			distances.Add (node, int.MaxValue);
		}

		// Set the starting Node distance to zero
		distances[start] = 0;
		while (unvisited.Count != 0)
		{

			// Ordering the unvisited list by distance, smallest distance at start and largest at end
			unvisited = unvisited.OrderBy (node => distances[node]).ToList();

			// Getting the Node with smallest distance
			GridObject current = unvisited[0];

			// Remove the current node from unvisisted list
			unvisited.Remove (current);

			// When the current node is equal to the end node, then we can break and return the path
			if (current == end)
			{
				// Construct the shortest path
				while (previous.ContainsKey (current))
				{
					// Insert the node onto the final result
					path.nodes.Insert (0, current);

					// Traverse from start to end
					current = previous[current];
				}

				// Insert the source onto the final result
				path.nodes.Insert (0, current);
				break;
			}

			// Looping through the Node connections (neighbors) and where the connection (neighbor) is available at unvisited list
			for (int i = 0; i < current.connections.Count; i++)
			{
				GridObject neighbor = current.connections[i];

				// Getting the distance between the current node and the connection (neighbor)
				int length = 1;// Vector3.Distance(current.transform.position, neighbor.transform.position);

				// The distance from start node to this connection (neighbor) of current node
				int alt = distances[current] + length;

				// A shorter path to the connection (neighbor) has been found
				if (alt < distances[neighbor])
				{
					distances[neighbor] = alt;
					previous[neighbor] = current;
				}
			}
		}
		path.Build();
		Debug.Log (path.ToString());
	}

	public void SetStartPosition (Vector3 worldPosition, Grid<GridObject> grid)
	{
		GridObject gridObject = grid.GetGridObject (worldPosition);
		if (gridObject != null)
		{
			if (!gridObject.types.Contains (GridObject.Type.Empty))
			{
				start = gridObject;
			}
			else
			{
				Debug.Log ("It's empty");
			}
		}
	}

	public void SetEndPosition (Vector3 worldPosition, Grid<GridObject> grid)
	{
		GridObject gridObject = grid.GetGridObject (worldPosition);
		if (gridObject != null)
		{
			if (!gridObject.types.Contains (GridObject.Type.Empty))
			{
				end = gridObject;
			}
			else
			{
				Debug.Log ("It's empty");
			}
		}
	}

	public void SetNodes (List<GridObject> nodes)
	{
		_nodes = nodes;
	}
}
