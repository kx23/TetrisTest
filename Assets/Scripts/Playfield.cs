using System;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public event Action<int> EventDeleteRows;
    public static int GridWigth = 10;
    public static int GridHeigth = 20;
    public Transform[,] Grid = new Transform[GridWigth, GridHeigth];

    public Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }
    public bool CheckInsideGrid(Vector2 pos) 
    {
        return (pos.x >= 0 && pos.x < GridWigth && pos.y >= 0);
    }
    public void DeleteRow(int y)
    {
        for (int x = 0; x < GridWigth; ++x)
        {
            Destroy(Grid[x, y].gameObject);
            Grid[x, y] = null;
        }
    }
    public void DecreaseRow(int y)
    {
        for (int x = 0; x < GridWigth; ++x)
        {
            if (Grid[x, y] != null)
            {
                Grid[x, y - 1] = Grid[x, y];
                Grid[x, y] = null;

                Grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < GridHeigth; ++i)
            DecreaseRow(i);
    }
    public bool IsRowFull(int y)
    {
        for (int x = 0; x < GridWigth; ++x)
            if (Grid[x, y] == null)
                return false;
        return true;
    }
    public void DeleteFullRows()
    {
        int rowsDeleted = 0;
        for (int y = 0; y < GridHeigth; ++y)
        {
            if (IsRowFull(y))
            {

                DeleteRow(y);
                rowsDeleted++;
                DecreaseRowsAbove(y + 1);
                --y;
            }

        }
        if (rowsDeleted > 0)
        {
            CallEventDeleteRows(rowsDeleted);
        }
    }

    public void CallEventDeleteRows(int value)
    {
        EventDeleteRows?.Invoke(value);
    }


}
