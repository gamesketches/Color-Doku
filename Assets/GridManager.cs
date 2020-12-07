using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSize = 3;
    public GameObject cellPrefab;
    public float cellSize;
    SpriteRenderer[] grid;
    public static Color currentColor;
    static GridManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        grid = new SpriteRenderer[gridSize * gridSize];
        currentColor = Color.red;
        for(int i = 0; i < (gridSize * gridSize); i++)
        {
            grid[i] = Instantiate<GameObject>(cellPrefab).GetComponent<SpriteRenderer>();
            grid[i].transform.position = new Vector3((i % gridSize) * cellSize, (i / gridSize) * cellSize, 0);
            /*if(i % 2 == 1)
            {
                grid[i].color = Color.LerpUnclamped(Color.white, Color.red, 0.5f);
            }*/
            if(i == 0 || i == 2 || i == 6 || i == 8)
            {
                grid[i].color = Color.LerpUnclamped(Color.white, Color.red, 0.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < grid.Length; i++) {
                if (grid[i].bounds.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    Debug.Log("clicked on item " + i);
                    break;

                }
            }
         }
    }

    public static void HandleClick(CellBehavior clickedCell)
    {
        int clickedCellID = -1;
        for(int i = 0; i < instance.grid.Length; i++)
        {
            if(instance.grid[i].GetComponent<CellBehavior>() == clickedCell)
            {
                clickedCellID = i;
                break;
            }
        }
        //instance.grid[clickedCellID].color = currentColor;
        instance.grid[clickedCellID].color = BlendColors(currentColor, instance.grid[clickedCellID].color);
        int horizontalOffset = clickedCellID % instance.gridSize;
        if(horizontalOffset > 0)
        {
            //instance.grid[clickedCellID - 1].color = currentColor;
            instance.grid[clickedCellID - 1].color = BlendColors(currentColor, instance.grid[clickedCellID - 1].color);

        }
        if (horizontalOffset < instance.gridSize - 1)
        {
            //instance.grid[clickedCellID + 1].color = currentColor;
            instance.grid[clickedCellID + 1].color = BlendColors(currentColor, instance.grid[clickedCellID + 1].color);

        }
        int verticalOffset = clickedCellID / instance.gridSize;
        if (verticalOffset > 0)
        {
            //instance.grid[clickedCellID - instance.gridSize].color = currentColor;
            instance.grid[clickedCellID - instance.gridSize].color = BlendColors(currentColor, instance.grid[clickedCellID - instance.gridSize].color);
        }
        if (verticalOffset < instance.gridSize - 1)
        {
            //instance.grid[clickedCellID + instance.gridSize].color = currentColor;
            instance.grid[clickedCellID + instance.gridSize].color = BlendColors(currentColor, instance.grid[clickedCellID + instance.gridSize].color);
        }
    }

    public void ChangeColor(string colorHex)
    {
        Color newColor = Color.white;
        ColorUtility.TryParseHtmlString(colorHex, out newColor);
        currentColor = newColor;
    }

    public static Color BlendColors(Color color1, Color color2)
    {
        return Color.Lerp(color1, color2, 0.5f);

    }
}

