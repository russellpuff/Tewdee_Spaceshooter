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

    // From tutorial.
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);

        if (collision.gameObject.CompareTag("Player")) { GameManager.instance.InitiateGameOver(); }
        else { GameManager.instance.IncreaseScore(10); } // Enemy destroyed
    }
     */
}
