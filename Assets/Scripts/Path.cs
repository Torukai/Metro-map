using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Path.
/// </summary>
public class Path
{

	/// <summary>
	/// The nodes.
	/// </summary>
	protected List<GridObject> m_Nodes = new List<GridObject>();

	private int transfersCount;

	/// <summary>
	/// The length of the path.
	/// </summary>
	protected float m_Length = 0f;

	/// <summary>
	/// Gets the nodes.
	/// </summary>
	/// <value>The nodes.</value>
	public virtual List<GridObject> nodes
	{
		get
		{
			return m_Nodes;
		}
	}

	/// <summary>
	/// Gets the length of the path.
	/// </summary>
	/// <value>The length.</value>
	public virtual float length
	{
		get
		{
			return m_Length;
		}
	}

	/// <summary>
	/// Gets the amount of transfers that was made along the path
	/// </summary>
	public int transfers
	{
		get
		{
			return transfersCount;
		}
	}

	/// <summary>
	/// Bake the path.
	/// Making the path ready for usage, Such as caculating the length.
	/// </summary>
	public virtual void Bake()
	{
		List<GridObject> calculated = new List<GridObject>();
		m_Length = 0f;
		for (int i = 0; i < m_Nodes.Count; i++)
		{
			GridObject node = m_Nodes[i];
			for (int j = 0; j < node.connections.Count; j++)
			{
				GridObject connection = node.connections[j];

				// Don't calcualte calculated nodes
				if (m_Nodes.Contains(connection) && !calculated.Contains(connection))
				{
					// Calculating the distance between a node and connection when they are both available in path nodes list
					m_Length += 1;
				}
			}
			calculated.Add(node);
		}
	}

	public void GetTransfersCount()
	{
		transfersCount = 0;
		var current = m_Nodes[0].types;
		foreach (var node in m_Nodes)
		{
			if (!node.GetGridTypes().Intersect(current).Any())
			{
				current = node.types;
				transfersCount++;
			}
		}
		Debug.Log("Amount of transfers: " + transfersCount);
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return string.Format(
			"Path: {0}\nLength: {1}",
			string.Join(
				", ",
				nodes.Select(node => node.Name).ToArray()),
			length);
	}
}
