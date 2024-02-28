using UnityEngine;

namespace Inspector
{
    public class TextInspectable : Inspectable
    {
        public override string Text => _text;

        [SerializeField, TextArea]
        private string _text;
    }
}