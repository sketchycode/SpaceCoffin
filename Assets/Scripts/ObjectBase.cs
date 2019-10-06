using UnityEngine;
using System.Collections;

public abstract class ObjectBase : MonoBehaviour
{

    private Renderer[] renderers;
    private bool isWrappingX = false;
    private bool isWrappingY = false;

    // Use this for initialization
    protected virtual void Start()
    {
        Debug.Log("start called for obj base");
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ScreenWrap();
    }

    private bool CheckRenderers()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
                return true;
        }

        return false;
    }

    private void ScreenWrap()
    {
        var isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;

            isWrappingX = true;
        }

        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;

            isWrappingY = true;
        }

        transform.position = newPosition;
    }
}
