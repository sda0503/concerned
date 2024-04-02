using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class CursorManage : MonoBehaviour
{
    [SerializeField] Texture2D hand;
    [SerializeField] Texture2D original;

    private void Start()
    {
        Cursor.SetCursor(original, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnMouseOver()
    {
        Cursor.SetCursor(hand, new Vector2(hand.width / 4, 0), CursorMode.ForceSoftware);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(original, new Vector2(original.width / 4, 0), CursorMode.ForceSoftware);
    }
}