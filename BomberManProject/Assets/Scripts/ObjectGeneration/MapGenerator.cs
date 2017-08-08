using Assets.New_Folder.Basis;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Generator
{
    private int breakWallNumber;
    private const int minBorder = 5;
    private const int maxBorder = 51;

    public MapGenerator(int rows, int columns, int walls = 0)
    {
        width = CorrectSize(rows);
        length = CorrectSize(columns);
        breakWallNumber = CorrectBreakWallNumber(walls);
        Matrix = new int[width, length];
    }
    public void GenerateGround()
    {
        for(int x=0; x<width; x++)
        {
            for(int z=0; z < length; z++)
            {
                Instantiate(EnvironmentTools.GetGround(), new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    public void GenerateConcreteWalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                if (IsBorder(x,z) || IsEvenCell(x, z))
                {
                    Instantiate(EnvironmentTools.GetConcreteWall(), new Vector3(x, 0, z), Quaternion.identity);
                    Matrix[x, z] = 1;
                }
            }
        }
    }

    public void GenerateBreakWalls()
    {
        for (int x = 2; x < width-2; x++)
        {
            for (int z = 2; z < length-2; z++)
            {
                if (!IsBorder(x, z) && IsOddCell(x, z))
                {
                    Instantiate(EnvironmentTools.GetBreakWall(), new Vector3(x, 0, z), Quaternion.identity);
                    Matrix[x, z] = 2;
                }
            }
        }
    }

    public void RandomGenerateBreakWalls()
    {
        for (int k =0; k<breakWallNumber; k++)
        {
            int x;
            int z;
            GetFreeCoordinates(out x, out z);
            Instantiate(EnvironmentTools.GetBreakWall(), new Vector3(x, 0, z), Quaternion.identity);
            Matrix[x, z] = 2;
        }    
    }

    private int CorrectSize(int value)
    {
        if (value < minBorder)
            return minBorder;
        if (value > maxBorder)
            return maxBorder;
        return value;
    }

    private int CorrectBreakWallNumber(int number)
    {
        if (number > (length * width/2) || number<0)
            return length * width / 2;
        return number;
    }  

    public int[,] GetMatrix()
    {
        return Matrix;
    }

}


