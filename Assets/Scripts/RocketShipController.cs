using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShipController : MonoBehaviour
{
    //10*mass = speed
    private float speedMultiplier = 5f;
    private bool startLaunch;
    private float fuel = 4000;
    private float timer = 0;
    private float enginePower = 1;
    private float height = 0;
    private bool isOutOfFuel;
    private float rocketShipScale;
    private bool reachedMaxHeight;

    // Start is called before the first frame update
    void Start()
    {
        //pick random scale
        rocketShipScale = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(rocketShipScale, rocketShipScale, 0);
        GetComponentInChildren<ParticleSystem>().Stop();
        enginePower = transform.localScale.x * transform.localScale.x;

        //set scale in range from 0 to 1 for color
        float normalizedScale = rocketShipScale - 0.5f;

        //set color of ship relative to size
        //smaller ships are more red
        //larger ships are more white
        GetComponent<SpriteRenderer>().color = new Color(1, normalizedScale, normalizedScale);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            startLaunch = true;
            //start launch
            GetComponentInChildren<ParticleSystem>().Play();
        }
        if (startLaunch && fuel > 0)
        {
            float speed = enginePower / transform.localScale.x;
            GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0);
            //subtract from fuel
            timer += 0.005f;
            fuel -= transform.localScale.x * speed * speed + timer;
        }
        if (fuel <= 0 && !reachedMaxHeight)
        {
            //reached max height because no fuel is left
            reachedMaxHeight = true;
            startLaunch = false;
            isOutOfFuel = true;
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        if (!reachedMaxHeight)
            height = transform.position.y;
    }

    public float GetScore()
    {
        //to get a readable score
        return Mathf.Abs(height) * 100f;
    }

    public float GetMass()
    {
        //to get a readable mass
        return rocketShipScale * 1000f;
    }

    public float GetHeight()
    {
        return height;
    }

    public bool GetIsOutOfFuel()
    {
        return isOutOfFuel;
    }

    //make array of all rocketships and store all scores to get highest
}
