using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Follower : MonoBehaviour
    {
        public Transform target;

        public float scanFrequency = 0.5f;
        public float stopFollowDistance = 2;

        private float _lastScanTime = 0;

        private Navigator navigator;

        private void Start()
        {
            navigator = GetComponent<Navigator>();
        }

        private void Update()
        {
            if (IsReadyToScan() && !IsInRange())
            {
                Debug.Log("Scanning nav path");
                navigator.NavigateTo(target.position);
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(target.position, transform.position) < stopFollowDistance;
        }

        private bool IsReadyToScan()
        {
            return Time.time - _lastScanTime > scanFrequency && target;
        }
    }
}
