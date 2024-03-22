using UnityEngine;
using UnityEngine.UI;

public static class GridLayoutGroupExtensions
{
    public static Vector2Int GetColumnAndRow(this GridLayoutGroup glg)
    {
        if (glg.transform.childCount == 0)
            return Vector2Int.zero;

        var column = 1;
        var row = 1;

        var firstChildObj = glg.transform.GetChild(0).GetComponent<RectTransform>();

        var firstChildPos = firstChildObj.anchoredPosition;
        var stopCountingRow = false;

        for (int i = 1; i < glg.transform.childCount; i++)
        {
            var currentChildObj = glg.transform.GetChild(i).GetComponent<RectTransform>();

            var currentChildPos = currentChildObj.anchoredPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                stopCountingRow = true;
            }
            else if (!stopCountingRow)
                row++;
        }

        return new Vector2Int(column, row);
    }
}