using Assets.Scripts.Models;
using Assets.Scripts.View;
using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class PopupPair
    {
        public Popups Type;
        public BasePopup Prefab;
    }
}