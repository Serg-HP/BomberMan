using Assets.Scripts;
using Assets.Scripts.ObjectGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

    public enum GenerationMethod
    {
        Random,
        Default
    }
    public int width;
    public int length;
    public int breakWallNumber;
    public GenerationMethod method;
    public int enemyNumber;
    public int smartEnemyNumber;
    void Start ()
    {
        MapGenerator newMap = new MapGenerator(width,length,breakWallNumber);
        newMap.GenerateConcreteWalls();
        newMap.GenerateGround();
        if (method == GenerationMethod.Random)
            newMap.RandomGenerateBreakWalls();
        else newMap.GenerateBreakWalls();
        DynamicObjectGenerator dynamicOG = new DynamicObjectGenerator(newMap.GetMatrix(), width, length);
        dynamicOG.CreatePlayer();
        dynamicOG.CreateEnemy(enemyNumber);
        dynamicOG.CreateSmartEnemy(smartEnemyNumber);
        PowerUpGenerator powerUpGen = new PowerUpGenerator(newMap.GetMatrix(), width, length);
        powerUpGen.SetPowerUps();
        powerUpGen.SetExit();

    }

}
