using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public GameObject rocketShip;
    public GameObject highScoreLine;
    private int amountOfRocketShips = 8;
    private float spawnLeftStartLocation = -3.55f;
    private float spawnHeight;

    List<GameObject> rocketShipsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("FuelAmount").GetComponent<Text>().text = "Testing with " + PlayerPrefs.GetInt("FuelAmount") + "L of fuel";
        GameObject.Find("EndofGenerationText").GetComponent<Text>().text = "";

        for (int i = 0; i < amountOfRocketShips; i++)
        {
            GameObject instantiatedRocketShip = Instantiate(rocketShip, new Vector3(spawnLeftStartLocation + i, spawnHeight, 0), rocketShip.transform.rotation);
            rocketShipsList.Add(instantiatedRocketShip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] rocketShipsArray = rocketShipsList.ToArray();

        bool sortMade = true;
        while (sortMade)
        {
            sortMade = false;
            for (int i = 0; i < rocketShipsArray.Length - 1; i++)
            {
                if (rocketShipsArray[i].GetComponent<RocketShipController>().GetHeight() > rocketShipsArray[i + 1].GetComponent<RocketShipController>().GetHeight())
                {
                    //sort from least to greatest
                    GameObject tempRocketShip = rocketShipsArray[i + 1];
                    rocketShipsArray[i + 1] = rocketShipsArray[i];
                    rocketShipsArray[i] = tempRocketShip;
                    //sort has been made therefore continue bubble sorting, if no sort has been made -> everything is sorted
                    sortMade = true;
                }
            }
        }

        //start at highest rocket and go backwards, so camera always following highest rocket
        for(int i = rocketShipsArray.Length - 1; i >= 0; i--)
        {
            if(!rocketShipsArray[i].GetComponent<RocketShipController>().GetIsOutOfFuel())
            {
                transform.position = new Vector3(0, rocketShipsArray[i].transform.position.y, -10);
                break;
            }
            //if all rockets are out of fuel, return camera to launch platform
            if(i == 0)
            {
                transform.position = new Vector3(0, 0, -10);
                //highlight top four rockets and say press return to kill off bottom four and reproduce top 4
                GameObject.Find("EndofGenerationText").GetComponent<Text>().text = "Congrats to the winning rocket of this generation, press return/enter to reproduce the traits of the TOP FOUR rockets and weed out the BOTTOM FOUR rockets.";
            }
        }
        //update crown position to be on top of whichever rocket has the highest score
        GameObject.Find("Crown").transform.position = new Vector3(rocketShipsArray[rocketShipsArray.Length - 1].transform.position.x, rocketShipsArray[rocketShipsArray.Length - 1].transform.position.y + 0.8f, 0);
        //update camera to focus on current highest ship
        GameObject.Find("CurrentGenerationHighscore").GetComponent<Text>().text = "Current Generation's Highest Altitude: " + rocketShipsArray[rocketShipsArray.Length - 1].GetComponent<RocketShipController>().GetScore() + "m with a mass/scale of " + rocketShipsArray[rocketShipsArray.Length - 1].GetComponent<RocketShipController>().GetMass() + "kg";
        GameObject.Find("HighestScoreLine").transform.position = new Vector3(0, rocketShipsArray[rocketShipsArray.Length - 1].GetComponent<RocketShipController>().GetHeight(), 0);

        if (Input.GetKeyDown("return"))
        {
            foreach (GameObject i in rocketShipsArray)
            {
                Debug.Log(i.GetComponent<RocketShipController>().GetScore());
                GameObject.Find("PreviousGenerationHighscore").GetComponent<Text>().text = "Previous Generation's Highest Altitude: " + rocketShipsArray[rocketShipsArray.Length - 1].GetComponent<RocketShipController>().GetScore() + "m with a mass/scale of " + rocketShipsArray[rocketShipsArray.Length - 1].GetComponent<RocketShipController>().GetMass() + "kg";
            }
            //kill off last four rockets
            for (int i = 0; i < 4; i++)
            {
                Destroy(rocketShipsArray[i]);
                GameObject.Find("EndofGenerationText").GetComponent<Text>().text = "";
            }
        }
        
    }
}
