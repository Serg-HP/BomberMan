using UnityEngine;

namespace Assets.Scripts
{
   abstract public class Generator: MonoBehaviour
    {
        public int[,] Matrix;
        public int length;
        public int width;
        protected virtual void GetFreeCoordinates(out int x, out int z)
        {
            do
            {
                x = Random.Range(1, width - 1);
                z = Random.Range(1, length - 1);
            } while (IsObjectExist(x, z));
        }

        protected bool IsObjectExist(int x, int z)
        {
            return Matrix[x, z] != 0;
        }

        protected bool IsBorder(int x, int z)
        {
            return (x == 0 || z == 0 || x == width - 1 || z == length - 1);
        }

        protected bool IsEvenCell(int x, int z)
        {
            return (x % 2 == 0 && z % 2 == 0);
        }

        protected bool IsOddCell(int x, int z)
        {
            return (x % 2 != 0 && z % 2 != 0);
        }
    }


}
