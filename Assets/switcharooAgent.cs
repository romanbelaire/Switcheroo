using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class switcharooAgent : Agent
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //ML Agent overrides
    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //get player info
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
        sensor.AddObservation(playerPos);

        int playerX = (int)playerPos[0] + 4;
        int playerY = (int)playerPos[1] + 4;
        for(int i = playerX - 2; i < playerX + 2; i++)//this repeats a total of 25 times, 24 observations (1 guaranteed skip) of size 6
        {
            for (int j = playerY - 2; j < playerY + 2; j++)
            {
                if(i == playerX && j == playerY){ continue; } //this is player location
                if (i < 0 || j < 0 || i >= 9 || j >= 9)//i,j is out of bounds, observe white space
                {
                    Vector3 pointPos = new Vector3(i-4, j-4, 0);//actual coordinate
                    Vector3 colorVec = new Vector3(1, 1, 1);
                    sensor.AddObservation(pointPos);
                    sensor.AddObservation(colorVec);
                }

                else//point is in-bounds
                {
                    Debug.Log(i);
                    Debug.Log(j);
                    if(GameData.Grid[i,j] == null)//no point at a space, observe a white space
                    {
                        Vector3 pointPos = new Vector3(i-4, j-4, 0);
                        Vector3 colorVec = new Vector3(1, 1, 1);
                        sensor.AddObservation(pointPos);
                        sensor.AddObservation(colorVec);
                    }
                    else
                    {
                        //point detected, observe position and color
                        Color color = GameData.Grid[i, j].GetComponent<SpriteRenderer>().color;
                        Vector3 colorVec = new Vector3(color[0], color[1], color[2]);
                        sensor.AddObservation(GameData.Grid[i, j].transform.position);
                        sensor.AddObservation(colorVec);
                    }
                }
            }
        }

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //change direction based on agent action
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<playerController>().direction = (int)vectorAction[0];
    }
}
