using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphRenderer : MonoBehaviour
{
	[SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    // Start is called before the first frame update
    private void Awake()
    {
        graphContainer = transform.Find("Graph").GetComponent<RectTransform>();
        //CreateCircle(new Vector2(200, 200));
        List<int> valueList = new List<int>() { 5, 12, 44, 15, 34, 88, 4, 22 };
        ShowGraph(valueList);
        //graph.Graph aGraph = new graph.Graph();
        //aGraph.doSomething();
        GraphStructure aGraph2 = new GraphStructure();
     

    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
	{
        GameObject go = new GameObject("circle", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return go;
	}

    private void ShowGraph(List<int> valueList)
	{
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;
        GameObject lastCircle = null;
        for (int i = 0; i < valueList.Count; i++)
		{
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;

            GameObject go = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircle != null)
			{
                CreateDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition, go.GetComponent<RectTransform>().anchoredPosition);

			}
            lastCircle = go;
		}
	}

    private void CreateDotConnection (Vector2 dotPositionA, Vector2 dotPositionB)
	{
        GameObject go = new GameObject("dotConntection", typeof(Image));
        go.transform.SetParent(graphContainer, false);
        go.GetComponent<Image>().color = new Color(1, 1, 1, .5f); 
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
