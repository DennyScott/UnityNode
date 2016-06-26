using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClickFollow : MonoBehaviour, IClickable
    {
        public Follower MyPlayerFollower;

        public void OnClick(RaycastHit hit)
        {
            Debug.Log("Following " + hit.collider.name);
            MyPlayerFollower.target = transform;
        }
    }
}