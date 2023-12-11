using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected UnityEngine.UI.Image image;
    [SerializeField] public Transform realParent;
    public CanvasGroup canvasGroup;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        this.realParent = transform.parent;
        transform.SetParent(UIHotBarCtrl.Instance.transform);
        image = GetComponent<UnityEngine.UI.Image>();
        // image.raycastTarget = false;
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 mousePosW = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mousePosW;
        // rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        // Debug.Log(canvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("enddrag");
        transform.SetParent(this.realParent);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }


}
