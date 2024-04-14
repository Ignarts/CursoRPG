using UnityEngine;
using Player.Scriptables;
using System;

namespace UI.Buttons
{
    public class AttributeButton : MonoBehaviour
    {
        public static Action<AttributeType> OnAttributeButtonClicked;

        [SerializeField] private AttributeType _attributeType;

        public void AddAttributePoint()
        {
            OnAttributeButtonClicked?.Invoke(_attributeType);
        }
    }

}