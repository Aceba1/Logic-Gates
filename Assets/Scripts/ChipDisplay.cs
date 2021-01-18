using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChipDisplay : MonoBehaviour
{
    [SerializeField]
    private Vector2Int coords;

    private Material material;

    public Vector2Int IconCoords
    {
        get => coords;
        set
        {
            if (coords != value)
            {
                coords = value;
                UpdateCoords();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        UpdateCoords();
    }

    void UpdateCoords() =>
        material.SetVector("_Coords", new Vector4(coords.x, -coords.y));
}
