using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
	[SerializeField] private List<GridObject> m_Nodes = new List<GridObject>();
	private GridObject start;
	private GridObject end;

	public List<GridObject> nodes
	{
		get
		{
			return m_Nodes;
		}
	}

	public void GetShortestPath()
	{

		// We don't accept null arguments
		if (start == null || end == null)
		{
			throw new ArgumentNullException();
		}

		// The final path
		Path path = new Path();

		// If the start and end are same node, we can return the start node
		if (start == end)
		{
			path.nodes.Add(start);
			Debug.Log("Path: " + path); ;
		}

		// The list of unvisited nodes
		List<GridObject> unvisited = new List<GridObject>();

		// Previous nodes in optimal path from source
		Dictionary<GridObject, GridObject> previous = new Dictionary<GridObject, GridObject>();

		// The calculated distances, set all to Infinity at start, except the start Node
		Dictionary<GridObject, float> distances = new Dictionary<GridObject, float>();

		for (int i = 0; i < m_Nodes.Count; i++)
		{
			GridObject node = m_Nodes[i];
			unvisited.Add(node);

			// Setting the node distance to Infinity
			distances.Add(node, float.MaxValue);
		}

		// Set the starting Node distance to zero
		distances[start] = 0f;
		while (unvisited.Count != 0)
		{

			// Ordering the unvisited list by distance, smallest distance at start and largest at end
			unvisited = unvisited.OrderBy(node => distances[node]).ToList();

			// Getting the Node with smallest distance
			GridObject current = unvisited[0];

			// Remove the current node from unvisisted list
			unvisited.Remove(current);

			// When the current node is equal to the end node, then we can break and return the path
			if (current == end)
			{
				// Construct the shortest path
				while (previous.ContainsKey(current))
				{
					// Insert the node onto the final result
					path.nodes.Insert(0, current);

					// Traverse from start to end
					current = previous[current];
				}

				// Insert the source onto the final result
				path.nodes.Insert(0, current);
				break;
			}

			// Looping through the Node connections (neighbors) and where the connection (neighbor) is available at unvisited list
			for (int i = 0; i < current.connections.Count; i++)
			{
				GridObject neighbor = current.connections[i];

				// Getting the distance between the current node and the connection (neighbor)
				float length = 1;// Vector3.Distance(current.transform.position, neighbor.transform.position);

				// The distance from start node to this connection (neighbor) of current node
				float alt = distances[current] + length;

				// A shorter path to the connection (neighbor) has been found
				if (alt < distances[neighbor])
				{
					distances[neighbor] = alt;
					previous[neighbor] = current;
				}
			}
		}
		path.Bake();

		Debug.Log(path.ToString());
		path.GetTransfersCount();
	}

	public void SetStartPosition(Vector3 worldPosition, Grid<GridObject> grid)
	{
		GridObject gridObject = grid.GetGridObject(worldPosition);
		if (gridObject != null)
		{
			start = gridObject;
		}
	}

	public void SetEndPosition(Vector3 worldPosition, Grid<GridObject> grid)
	{
		GridObject gridObject = grid.GetGridObject(worldPosition);
		if (gridObject != null)
		{
			end = gridObject;
		}
	}

	public void SetNodes(List<GridObject> nodes)
	{
		this.m_Nodes = nodes;
	}
}
