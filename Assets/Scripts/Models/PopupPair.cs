using System;
using Assets.View;

namespace Assets.Models
{
    [Serializable]
    public class PopupPair
    {
        public Popups Type;
        public BasePopup Prefab;
    }
}