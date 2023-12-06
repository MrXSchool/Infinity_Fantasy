using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected Transform realParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        this.realParent = transform.parent;
        transform.parent = UIHotBarCtrl.Instance.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 mousePosW = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mousePosW;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end");
        transform.parent = this.realParent;
    }
}
