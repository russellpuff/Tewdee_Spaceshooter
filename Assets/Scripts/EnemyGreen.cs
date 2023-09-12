using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Deletable
{
    public bool leftSpawn; // True = spawn on left, move to right. False = spawn on right, move to left.
    private bool laserFired = false;
    private float randomFireLocation;
    [SerializeField] GameObject laser;
    [SerializeField] float speed = 10f;
    void Start()
    {
        delete_zone = leftSpawn ? DeleteZone.Right : DeleteZone.Left;
        randomFireLocation = Random.Range(0.1f, 0.9f); // Fire one laser at a random location during travel. 
    }

    public override void Update()
    {
        if (!laserFired)
        {
            if ((leftSpawn && transform.position.x > randomFireLocation) ||
            (!leftSpawn && transform.position.x < randomFireLocation))
            { Instantiate(laser, transform.position, Quaternion.identity); laserFired = true; }
        }

        transform.position +=  new Vector3(speed * (leftSpawn ? 1 : -1), 0, 0) * Time.deltaTime;
        base.Update();
    }
}
