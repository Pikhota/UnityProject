using UnityEngine;

namespace Assets.View
{
    public class ViewWithModel<T> : MonoBehaviour
    {
        protected T Model;

        public virtual void Init(T _model)
        {
            Model = _model;
        }
    }
}