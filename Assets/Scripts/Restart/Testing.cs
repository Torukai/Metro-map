using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid<Waypoint> grid;
    // Start is called before the first frame update
    void Start()
    {
		grid = new Grid<Waypoint>(4, 2, 10f, new Vector3(20, 20), (Grid<Waypoint> g, int x,int y) => new Waypoint(g,x,y));
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
		{
            //grid.SetValue( UtilsClass.GetMouseWorldPosition(), 56);
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            Waypoint waypoint = grid.GetGridObject(position);
            if (waypoint != null)
			{
                waypoint.AddValue(5);
			}
		}

        if (Input.GetMouseButton(1))
        {
            Debug.Log(grid.GetGridObject(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
