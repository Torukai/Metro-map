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
	private List<GridObject> _nodes = new List<GridObject>();

	/// <summary>
	/// The length of the path.
	/// </summary>
	private int _transfers = 0;

	/// <summary>
	/// The length of the path.
	/// </summary>
	private int _length = 0;

	/// <summary>
	/// Gets the nodes.
	/// </summary>
	/// <value>The nodes.</value>
	public virtual List<GridObject> nodes
	{
		get
		{
			return _nodes;
		}
	}

	/// <summary>
	/// Gets the length of the path.
	/// </summary>
	/// <value>The length.</value>
	public virtual int length
	{
		get
		{
			return _length;
		}
	}

	/// <summary>
	/// Gets the amount of transfers that was made along the path
	/// </summary>
	public int transfers
	{
		get
		{
			return _transfers;
		}
	}

	/// <summary>
	/// Build the path.
	/// </summary>
	public virtual void Build()
	{
		List<GridObject> calculated = new List<GridObject>();
		_length = 0;
		for (int i = 0; i < _nodes.Count; i++)
		{
			GridObject node = _nodes[i];
			for (int j = 0; j < node.connections.Count; j++)
			{
				GridObject connection = node.connections[j];

				// Don't calcualte calculated nodes
				if (_nodes.Contains (connection) && !calculated.Contains (connection))
				{
					_length += 1;
				}
			}
			calculated.Add (node);
		}
		CalculateTransfers();
	}

	public void CalculateTransfers()
	{
		_transfers = 0;
		var current = _nodes[0].types;
		foreach (var node in _nodes)
		{
			if (!node.GetGridTypes().Intersect (current).Any())
			{
				current = node.types;
				_transfers++;
			}
		}
		Debug.Log("Amount of transfers: " + _transfers);
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return string.Format (string.Join ( " - ", nodes.Select (node => node.Name).ToArray()));
	}
}
