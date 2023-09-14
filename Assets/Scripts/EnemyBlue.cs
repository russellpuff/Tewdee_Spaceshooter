using UnityEngine;

public class EnemyBlue : Deletable
{
    [SerializeField] float speed = 10f;
    void Start()
    {
        this.delete_zone = DeleteZone.Bottom;
    }

    public override void Update()
    {
        transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;
        base.Update();
    }

    private void OnDestroy()
    {
        if(markForDelete) { GameManager.instance.IncreaseScore(-10); }
    }
}
