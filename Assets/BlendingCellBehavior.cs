using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendingCellBehavior : MonoBehaviour
{
    public ColoringCellBehavior[] linkedCells;
    SpriteRenderer spriteRenderer;
    public bool locked;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach(ColoringCellBehavior cell in linkedCells)
        {
            cell.colorChangeTrigger += BlendColors;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //BlendColors(); 
    }

    void BlendColors()
    {
        Color startColor = spriteRenderer.color;
        foreach(ColoringCellBehavior cell in linkedCells)
        {
            startColor = Color.Lerp(startColor, cell.blendingColor, 0.5f);
        }
        if(spriteRenderer.color != startColor && !locked) {
            StartCoroutine(LerpToColor(startColor));
        }
    }

    IEnumerator LerpToColor(Color newColor) {
        Color startColor = spriteRenderer.color;
        for(float t = 0; t < 1; t += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(startColor, newColor, t / 1);
            yield return null;
        }
        spriteRenderer.color = newColor;
    }
}
