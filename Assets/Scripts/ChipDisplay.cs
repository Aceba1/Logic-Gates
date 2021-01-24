using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChipDisplay : MonoBehaviour
{
    [SerializeField]
    private Vector2Int coords;
    [SerializeField]
    [ColorUsage(false, true)]
    private Color color = Color.HSVToRGB(.16f, .5f, 1f);

    private Material material;

    public Vector2Int IconCoords
    {
        get => coords;
        set
        {
            if (coords != value)
            {
                coords = value;
                SetCoords(coords);
            }
        }
    }

    public Color IconColor
    {
        get => color;
        set
        {
            if (color != value)
            {
                color = value;
                SetColor(color);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        SetCoords(coords);
        SetColor(color);
    }

    public void SetCoords(Vector2Int coords) =>
        material.SetVector("_Coords", new Vector4(coords.x, -coords.y));

    public void SetColor(Color color) =>
        material.color = color;
}
