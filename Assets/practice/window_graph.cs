using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class window_graph : MonoBehaviour
{
    public Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer;
    public List<int> points = new List<int> { 230, 540, 180, 670, 310, 490, 720 };


    private void Start()
    {
        ShowGraph(points);
    }

    GameObject CreateCircle(Vector2 anchoredPos)
    {
        GameObject circle = new GameObject("circle", typeof(Image));
        circle.transform.SetParent(graphContainer, false);
        circle.GetComponent<Image>().sprite = circleSprite;
        circle.GetComponent<Image>().color = Color.yellow;

        RectTransform circleRTf = circle.GetComponent<RectTransform>();
        circleRTf.anchoredPosition = anchoredPos;
        circleRTf.sizeDelta = new Vector2(25, 25);
        circleRTf.anchorMin = new Vector2(0, 0);
        circleRTf.anchorMax = new Vector2(0, 0);
        return circle;

    }


    void ShowGraph(List<int> pointsList )
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float xSize = graphWidth / pointsList.Count - 1;
        float yMax = 1000f;
        GameObject lastCircle = null;
        for (int i = 0; i < pointsList.Count; i++)
        {
            float xPos = i * xSize;
            float yPos = (pointsList[i] / yMax) * graphHeight;
            GameObject currentCircle = CreateCircle(new Vector2(xPos, yPos));
            if (lastCircle != null) 
            {
                DrawDotConnection(lastCircle.GetComponent<RectTransform>().anchoredPosition, currentCircle.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircle = currentCircle;
        }

    }


    void DrawDotConnection(Vector2 dotAPos, Vector2 dotBPos)
    {
        GameObject line = new GameObject("dotConnection", typeof(Image));
        line.transform.SetParent(graphContainer, false);
        Vector2 dir = (dotBPos - dotAPos).normalized;
        float distance = Vector2.Distance(dotAPos, dotBPos);

        RectTransform lineRTf = line.GetComponent<RectTransform>();
        lineRTf.sizeDelta = new Vector2(distance, 4f);
        lineRTf.anchorMin = new Vector2(0, 0);
        lineRTf.anchorMax = new Vector2(0, 0);
        lineRTf.anchoredPosition = dotAPos + dir * distance * 0.5f;
        line.GetComponent<Image>().color = new Color(255, 165, 0);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lineRTf.localEulerAngles = new Vector3(0, 0, angle);

        

    }

}
