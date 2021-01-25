using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class WireLine : MonoBehaviour
{
    //TODO: Make private
    [NonSerialized]
    public LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void OnEnable()
    {
        lineRenderer.enabled = true;
    }

    public void OnDisable()
    {
        lineRenderer.enabled = false;
    }

    public void SetColor(Color color)
    {
        lineRenderer.material.SetColor("_Color", color);
    }

    //TODO: Using directions, add points between to smooth connection
    public void Route(Vector3 A, Vector3 B)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, A);
        lineRenderer.SetPosition(1, B);
    }

    public static Vector3 GetWorldMousePos() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 4f);

    private static float ViewWidth(Vector3 view, Vector3 target)
    {
        return Mathf.Lerp(0.08f, 0.2f, Mathf.Abs(Vector3.Dot(view, target)));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
