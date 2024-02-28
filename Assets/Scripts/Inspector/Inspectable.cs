using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inspector
{
    public class Inspectable : Graphic, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField, TextArea]
        public string Text { get; private set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            FindObjectOfType<Inspector>().Text.text = Text;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            FindObjectOfType<Inspector>().Text.text = "";
        }
    }
}