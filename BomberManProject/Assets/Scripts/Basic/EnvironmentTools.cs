using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.New_Folder.Basis
{
    class EnvironmentTools
    {
        public static GameObject GetBreakWall()
        {
            return Resources.Load("Prefabs/BreakWall") as GameObject;
        }
        public static GameObject GetGround()
        {
            return Resources.Load("Prefabs/PlaneGround") as GameObject;
        }
        public static GameObject GetConcreteWall()
        {
            return Resources.Load("Prefabs/ConcreteWall") as GameObject;
        }
        public static GameObject GetPlayer()
        {
            return Resources.Load("Prefabs/Player") as GameObject;
        }
        public static GameObject GetEnemy()
        {
            return Resources.Load("Prefabs/Enemy") as GameObject;
        }
        public static GameObject GetBomb()
        {
            return Resources.Load("Prefabs/Bomb") as GameObject;
        }
        public static GameObject GetExplosion()
        {
            return Resources.Load("Prefabs/Explosion") as GameObject;
        }
        public static GameObject GetSpeedUp()
        {
            return Resources.Load("Prefabs/SpeedUp") as GameObject;
        }
        public static GameObject GetSpeedUpEffect()
        {
            return Resources.Load("Prefabs/SpeedUpEffect") as GameObject;
        }
        public static GameObject GetFlameUp()
        {
            return Resources.Load("Prefabs/FlameUp") as GameObject;
        }
        public static GameObject GetFlameUpEffect()
        {
            return Resources.Load("Prefabs/FlameUpEffect") as GameObject;
        }
        public static GameObject GetBombUp()
        {
            return Resources.Load("Prefabs/BombUp") as GameObject;
        }
        public static GameObject GetBombUpEffect()
        {
            return Resources.Load("Prefabs/BombUpEffect") as GameObject;
        }
        public static GameObject GetWallPass()
        {
            return Resources.Load("Prefabs/WallPass") as GameObject;
        }
        public static GameObject GetWallPassEffect()
        {
            return Resources.Load("Prefabs/WallPassEffect") as GameObject;
        }
    }
}
