using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inspector
{
    [RequireComponent(typeof(CanvasRenderer))]
    public abstract class Inspectable : Graphic, IPointerEnterHandler, IPointerExitHandler
    {
        public abstract string Text { get; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            FindObjectOfType<Inspector>().SetText(Text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            FindObjectOfType<Inspector>().Clear();
        }
    }
}