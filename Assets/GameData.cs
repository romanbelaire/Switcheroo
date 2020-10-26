using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    //static class to hold game rules between scenes
    public static int score = 0;
    public static Color goodColor = Color.red;
    public static Color badColor = Color.green;
    public static int level = 1;
    public static bool Invert = false;
    public static GameObject[,] Grid = new GameObject[9, 9];
    public static switcharooAgent agent;


}
