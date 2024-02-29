using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    public delegate void EnterEventHandler(object sender, EventArgs e);
    public delegate void ExitEventHandler(object sender, EventArgs e);
    public delegate void UpEventHandler(object sender, EventArgs e);
    public delegate void DownEventHandler(object sender, EventArgs e);

    public event EnterEventHandler  PointerEnterEvent;
    public event ExitEventHandler   PointerExitEvent;
    public event UpEventHandler     PointerUpEvent;
    public event DownEventHandler   PointerDownEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnterEvent?.Invoke(this, new EventArgs());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExitEvent?.Invoke(this, new EventArgs());
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUpEvent?.Invoke(this, new EventArgs());
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDownEvent?.Invoke(this, new EventArgs());
    }

}
