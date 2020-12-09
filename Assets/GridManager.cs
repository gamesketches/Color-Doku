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
    Color[] startingGridMap;
    // Start is called before the first frame update
    void Start()
    {

        // Modify this to change starting grid
        startingGridMap = new Color[] {Color.red, Color.white, Color.red,
                                       Color.white, Color.red, Color.white,
                                        Color.red, Color.white, Color.red};

        instance = this;
        grid = new SpriteRenderer[gridSize * gridSize];
        currentColor = Color.red;
        for(int i = 0; i < (gridSize * gridSize); i++)
        {
            grid[i] = Instantiate<GameObject>(cellPrefab).GetComponent<SpriteRenderer>();
            grid[i].transform.position = new Vector3((i % gridSize) * cellSize, (i / gridSize) * cellSize, 0);
            // Colors in via grid map if gridmap is accurate
            if(gridSize * gridSize == startingGridMap.Length)
            {
                grid[i].color = startingGridMap[i];
            }
            /*if(i % 2 == 1)
            {
                grid[i].color = Color.LerpUnclamped(Color.white, Color.red, 0.5f);
            }*/
            // Color in corners
            else if ((i % gridSize == 0 || i % gridSize == gridSize - 1) && (i / gridSize == 0 || i / gridSize == gridSize - 1))
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
        // Find the cell
        for(int i = 0; i < instance.grid.Length; i++)
        {
            if(instance.grid[i].GetComponent<CellBehavior>() == clickedCell)
            {
                clickedCellID = i;
                break;
            }
        }
        instance.CalcAndFillSquare(clickedCellID);
        int horizontalOffset = clickedCellID % instance.gridSize;
        // Fill to the left if a cell exists to the left
        if(horizontalOffset > 0)
        {
            instance.CalcAndFillSquare(clickedCellID - 1);
        }
        // Fill to the right if a cell exists to the left
        if (horizontalOffset < instance.gridSize - 1)
        {
            instance.CalcAndFillSquare(clickedCellID + 1);
        }
        int verticalOffset = clickedCellID / instance.gridSize;
        // Fill above if a cell exists above
        if (verticalOffset > 0)
        {
            instance.CalcAndFillSquare(clickedCellID - instance.gridSize);
        }
        // Fill below if a cell below exists
        if (verticalOffset < instance.gridSize - 1)
        {
            instance.CalcAndFillSquare(clickedCellID + instance.gridSize);
        }
    }

    void CalcAndFillSquare(int targetCell)
    {
        Color targetColor = BlendColors(currentColor, instance.grid[targetCell].color);
        StartCoroutine(FillColors(targetCell, targetColor));

    }

    public static IEnumerator FillColors(int cell, Color newColor)
    {
        Color startColor = instance.grid[cell].color;
        for(float t = 0; t < 0.4f; t+= Time.deltaTime)
        {
            instance.grid[cell].color = Color.Lerp(startColor, newColor, t / 0.4f);
            yield return null;
        }
        instance.grid[cell].color = newColor;
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

