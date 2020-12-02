using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPrefabVisual : MonoBehaviour
{
    public static GridPrefabVisual Instance { get; private set; }

    [SerializeField] private Transform pfGridPrefabVisualNode;

    private List<Transform> visualNodeList;
    private Transform[,] visualNodeArray;
    private Grid<GridObject> grid;
    private bool updateVisual;

    private void Awake()
    {
        Instance = this;
        visualNodeList = new List<Transform>();
    }

    public void Setup (Grid<GridObject> grid)
    {
        this.grid = grid;
        visualNodeArray = new Transform[grid.GetWidth(), grid.GetHeight()];

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector3 gridPosition = new Vector3 (x, y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f;
                Transform visualNode = CreateVisualNode (gridPosition);
                visualNodeArray[x, y] = visualNode;
                visualNodeList.Add (visualNode);
            }
        }

        UpdateVisual (grid);

        grid.OnGridObjectChanged += Grid_OnGridObjectChanged;
    }

    private void Update()
    {
        if (updateVisual)
        {
            updateVisual = false;
            UpdateVisual (grid);
        }
    }

    private void Grid_OnGridObjectChanged (object sender, Grid<GridObject>.OnGridObjectChangedEventArgs e)
    {
        updateVisual = true;
    }

    public void UpdateVisual (Grid<GridObject> grid)
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                GridObject gridObject = grid.GetGridObject (x, y);

                Transform visualNode = visualNodeArray[x, y];
                visualNode.gameObject.SetActive (true);
                SetupVisualNode (visualNode, gridObject);
            }
        }
    }

    private Transform CreateVisualNode (Vector3 position)
    {
        Transform visualNodeTransform = Instantiate(pfGridPrefabVisualNode, position, Quaternion.identity);
        return visualNodeTransform;
    }

    private void SetupVisualNode (Transform visualNodeTransform, GridObject mapGridObject)
    {
        SpriteRenderer RedSprite = visualNodeTransform.Find ("Red").GetComponent<SpriteRenderer>();
        SpriteRenderer GreenSprite = visualNodeTransform.Find ("Green").GetComponent<SpriteRenderer>();
        SpriteRenderer BlueSprite = visualNodeTransform.Find ("Blue").GetComponent<SpriteRenderer>();
        SpriteRenderer BlackSprite = visualNodeTransform.Find ("Black").GetComponent<SpriteRenderer>();
        TextMeshPro NameText = visualNodeTransform.Find ("Name").GetComponent<TextMeshPro>();

        foreach (var v in mapGridObject.types)
		{
            switch (v)
            {
                default:
                case GridObject.Type.Empty:
                    break;
                case GridObject.Type.Red:
                    RedSprite.gameObject.SetActive (true);
                    NameText.gameObject.SetActive (true);
                    NameText.SetText (mapGridObject.Name);
                    break;
                case GridObject.Type.Green:
                    GreenSprite.gameObject.SetActive (true);
                    NameText.gameObject.SetActive (true);
                    NameText.SetText (mapGridObject.Name);
                    break;
                case GridObject.Type.Blue:
                    BlueSprite.gameObject.SetActive (true);
                    NameText.gameObject.SetActive (true);
                    NameText.SetText(mapGridObject.Name);
                    break;
                case GridObject.Type.Black:
                    BlackSprite.gameObject.SetActive (true);
                    NameText.gameObject.SetActive (true);
                    NameText.SetText (mapGridObject.Name);
                    break;
            }
		}
    }
}

