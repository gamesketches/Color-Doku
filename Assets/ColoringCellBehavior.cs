using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColoringCellColor {Red, Blue, Yellow, Orange, Green, Cyan, White}
public class ColoringCellBehavior : MonoBehaviour
{
    public Color blendingColor;
    ColoringCellColor cellColor = ColoringCellColor.White;
    SpriteRenderer spriteRenderer;
    public delegate void OnColorChange();
    public OnColorChange colorChangeTrigger;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blendingColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        UpdateCellColor();
    }

    void UpdateCellColor()
    {
        switch(cellColor)
        {
            case ColoringCellColor.White:
                cellColor = ColoringCellColor.Blue;
                blendingColor = Color.blue;
                break;
            case ColoringCellColor.Blue:
                cellColor = ColoringCellColor.Red;
                blendingColor = Color.red;
                break;
            case ColoringCellColor.Red:
                cellColor = ColoringCellColor.Yellow;
                blendingColor = Color.yellow;
                break;
            case ColoringCellColor.Yellow:
                cellColor = ColoringCellColor.Cyan;
                blendingColor = Color.cyan;
                break;
            case ColoringCellColor.Cyan:
                cellColor = ColoringCellColor.Orange;
                blendingColor = new Color(0.9622642f, 0.6008f, 0.05900679f);
                break;
            case ColoringCellColor.Orange:
                cellColor = ColoringCellColor.Green;
                blendingColor = Color.green;
                break;
            case ColoringCellColor.Green:
                cellColor = ColoringCellColor.White;
                blendingColor = Color.white;
                break;
        }
        spriteRenderer.color = blendingColor;
        colorChangeTrigger();
    }

}
