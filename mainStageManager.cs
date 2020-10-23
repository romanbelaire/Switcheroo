using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainStageManager : MonoBehaviour {
    //this class handles all logic regarding loading rule and spawning points
    private GameObject[,] pointGrid = new GameObject[9,9];
    private int[,] goodPointsCoords = new int[10, 2];
    public GameObject pointPrefab;
    public GameObject scoreline;
    public GameObject levelline;
    private Text scoretxt;
    private Text leveltxt;
    

    void Start ()
    {
        generatePoints();


        //setup UI link
        scoretxt = scoreline.GetComponent<Text>();
        leveltxt = levelline.GetComponent<Text>();
    }
	
	void FixedUpdate () {
        //also updates UI
        scoretxt.text = GameData.score.ToString();
        leveltxt.text = GameData.level.ToString();
    }

    void generatePoints()
    {
        createBadPoints();
        createGoodPoints();
    }

    void createBadPoints()
    {
        //this function handles the creation of bad points.
        for (int i = 0; i < 10; i++)
        {
            int[] newCoords = genCoordinate();
            Vector2 newCoordsV = new Vector2(newCoords[0], newCoords[1]);
            GameObject newPoint = Instantiate(pointPrefab, newCoordsV, Quaternion.identity);//instantiate new instance of the prefab point at new coords
            newPoint.GetComponent<SpriteRenderer>().color = GameData.badColor;//assign bad color
            //input point into grid for tracking
            pointGrid[newCoords[0] + 4, newCoords[1] + 4] = newPoint;//the +4 offset is because the coordinates can be negative
        }
    }

    void createGoodPoints()
    {
        //This function handles the creation of good points.
        for (int i = 0; i < 10; i++)
        {
            int[] newCoords = genCoordinate();
            Vector2 newCoordsV = new Vector2(newCoords[0], newCoords[1]);
            GameObject newPoint = Instantiate(pointPrefab, newCoordsV, Quaternion.identity);//instantiate new instance of the prefab point at new coords
            newPoint.GetComponent<SpriteRenderer>().color = GameData.goodColor;//assign good color
            //input point into grid
            pointGrid[newCoords[0] + 4, newCoords[1] + 4] = newPoint;//the +4 offset is because the coordinates can be negative

            //store coords for flood checking
            goodPointsCoords[i, 0] = newCoords[0] + 4;//store x
            goodPointsCoords[i, 1] = newCoords[1] + 4;//store y
        }
    }

    private int[] genCoordinate()
    {
        //generates a random valid coordinate. Constraints are such that a point cannot spawn on or adjacent to the player (origin)
        int x = 0;
        int y = 0;
        do
        {
            x = 0;
            y = 0;
            while (Mathf.Abs(x) <= 1)
            {
                x = Random.Range(-4, 5);//upper bound is exclusive
            }
            while (Mathf.Abs(y) <= 1)
            {
                y = Random.Range(-4, 5);
            }
        }
        while (pointGrid[x + 4, y + 4] != null);//the +4 offset is because the coordinates can be negative

        int[] coord = { x, y };
        return coord;
    }
            
    void floodCheck()
    {
        //this function will flood check each good point (with some efficiency) to ensure they are all reachable from the origin
        for(int i = 0;i < 10; i++)
        {

        }
    }
}
