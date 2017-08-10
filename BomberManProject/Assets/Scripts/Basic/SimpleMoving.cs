using Assets.Scripts.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract public class SimpleMoving: BasicMoving
    {
        protected float speed;

        public override void Move(Vector3 direction, int rotation)
        {
            Vector3 position = this.transform.position + direction;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            transform.position = Vector3.MoveTowards(transform.position, position, speed);
        }
        
    }
}
