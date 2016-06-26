using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IClickable
    {
        void OnClick(RaycastHit hit);
    }
}