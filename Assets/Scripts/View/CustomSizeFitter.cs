using UnityEngine;

namespace Assets.View
{
    public class CustomSizeFitter : MonoBehaviour
    {
        [SerializeField] private float _unitsPerChildren;
        [SerializeField] private RectTransform _root;

        private int _lastChild;
		
        private void Update()
        {
            if (_root.childCount != _lastChild)
            {
                _lastChild = _root.childCount;
                _root.sizeDelta = new Vector2(_root.sizeDelta.x, _root.childCount * _unitsPerChildren);
            }
        }
    }
}