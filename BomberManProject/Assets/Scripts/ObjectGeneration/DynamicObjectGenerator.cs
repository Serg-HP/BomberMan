using Assets.New_Folder.Basis;
using UnityEngine;

namespace Assets.Scripts
{
    public class DynamicObjectGenerator: Generator
    {
        public DynamicObjectGenerator(int[,] matrix, int rows, int columns)
        {
            Matrix = matrix;
            width = rows;
            length = columns;
        }

        public void CreatePlayer()
        {
            int x = 0;
            int z = 0;
            GetFreeCoordinates(out x, out z);
            Instantiate(EnvironmentTools.GetPlayer(), new Vector3(x, 0.25f, z), Quaternion.identity);
            Matrix[x, z] = 1;
        }

        public void CreateEnemy(int number)
        {
            for (int k = 0; k < number; k++)
            {
                int x;
                int z;
                base.GetFreeCoordinates(out x, out z);
                Instantiate(EnvironmentTools.GetEnemy(), new Vector3(x, 0.5f, z), Quaternion.identity);
                Matrix[x, z] = 1;
            }
        }

        protected override void GetFreeCoordinates(out int x, out int z)
        {
            do
            {
                x = Random.Range(1, width - 1);
                z = Random.Range(1, length - 1);
            } while (!PlayerCanMove(x, z) || IsObjectExist(x, z));
        }

        private bool PlayerCanMove(int x, int z)
        {
            int ways = 0;
            if (Matrix[x + 1, z] == 0)
                ways++;
            if (Matrix[x - 1, z] == 0)
                ways++;
            if (Matrix[x, z + 1] == 0)
                ways++;
            if (Matrix[x, z - 1] == 0)
                ways++;
            return ways >= 2;
        }

    }
}
