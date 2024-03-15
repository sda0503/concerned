using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector2 currentPos;
    bool coll = false;
    public void OnDrag(PointerEventData eventData)
    {
        if(!coll)
        {
            currentPos = Input.mousePosition;
            this.transform.position = currentPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        coll = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "collider")
        {
            Debug.Log("ºÎµúÈû");
            coll = true;
        }
        if (collision.transform.tag == "same")
        {
            gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "collider")
        {
            gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
