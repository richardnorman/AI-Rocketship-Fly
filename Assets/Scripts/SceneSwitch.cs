using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSimulation()
    {
        int enteredFuelAmount = int.Parse(GameObject.Find("InputField").GetComponent<InputField>().textComponent.text);
        //Save fuel amount and load next scene
        if (enteredFuelAmount != 0)
        {
            PlayerPrefs.SetInt("FuelAmount", enteredFuelAmount);
            SceneManager.LoadScene("SimulationScene", LoadSceneMode.Single);
        }
    }
}
