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

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // Do nothing lol
        // This was being triggered for both the laser and an enemy when they collide (giving double points). 
    }
}
