using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraMover : MonoBehaviour
{
    public float moveSpeed = 4f;

    bool dragging = false;
    Vector3 mouseOrigin;
    Vector3 startRotation;
    Vector3 viewRotation;

    private float ScaledMoveSpeed => moveSpeed * Time.deltaTime * 
        (1 + InputAxis(KeyCode.LeftShift, KeyCode.LeftControl, 3f, -0.75f));

    void StartDrag()
    {
        dragging = true;
        mouseOrigin = Input.mousePosition;
        startRotation = transform.rotation.eulerAngles;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void EndDrag()
    {
        dragging = false;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }

    void HandleDragging()
    {
        Vector3 delta = (Input.mousePosition - mouseOrigin) / Screen.width * 180f;

        viewRotation = new Vector2(
            Mathf.Clamp(Mathf.Repeat(-delta.y + startRotation.x + 180f, 360f) - 180f, -80f, 80f),
            Mathf.Repeat(delta.x + startRotation.y, 360f));

        transform.rotation = Quaternion.Euler(viewRotation);
    }

    // Update is called once per frame
    void Update()
    {
        bool drag = Input.GetMouseButton(1);
        if (drag != dragging)
        {
            if (drag) StartDrag();
            else EndDrag();
        }
        else if (drag)
            HandleDragging();

        Vector3 move = new Vector3(
            InputAxis(KeyCode.D, KeyCode.A),
            InputAxis(KeyCode.E, KeyCode.Q),
            InputAxis(KeyCode.W, KeyCode.S)) * ScaledMoveSpeed;

        transform.position += transform.rotation * move;
    }

    private int InputAxis(KeyCode P, KeyCode N) =>
        (Input.GetKey(P) ? 1 : 0) -
        (Input.GetKey(N) ? 1 : 0);

    private float InputAxis(KeyCode A, KeyCode B, float VA, float VB) =>
        (Input.GetKey(A) ? VA : 0f) +
        (Input.GetKey(B) ? VB : 0f);
}
