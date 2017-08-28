using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Basic
{
    class CameraController:MonoBehaviour
    {
        public GameObject player;
        private Vector3 offset;
        void Start()
        {
            offset = transform.position - player.transform.position;
        }
        private void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }
    }
}
