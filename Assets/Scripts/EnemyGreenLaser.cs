using UnityEngine;

public class EnemyGreenLaser : Deletable
{
    [SerializeField] float laserSpeed = 1.0f;
    void Start()
    {
        this.delete_zone = DeleteZone.Bottom;
    }

    public override void Update()
    {
        transform.position -= new Vector3(0, laserSpeed, 0) * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 0, 180) * Time.deltaTime;
        base.Update();
    }
}
