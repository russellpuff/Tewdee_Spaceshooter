using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Deletable
{
    [SerializeField] float laserSpeed = 1.0f;
    void Start()
    {
        this.delete_zone = DeleteZone.Top;
    }

    public override void Update()
    {
        transform.position += new Vector3(0, laserSpeed, 0) * Time.deltaTime;
        base.Update();
    }
}
