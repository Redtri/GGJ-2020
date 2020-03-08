using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    public static CursorManager instance; 
    
    
    public Texture2D cursorArrow;
    public Texture2D cursorHover;
    
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void SetHoverCursor(bool isHovering)
    {
        Cursor.SetCursor(isHovering ? cursorHover : cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}
