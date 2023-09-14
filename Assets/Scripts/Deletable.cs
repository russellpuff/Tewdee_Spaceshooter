using System;
using System.Collections.Generic;
using UnityEngine;

// GameObjects that are automatically deleted when they exit the screen. 
// DeleteZone determines where they must be to be deleted, as some objects spawn off screen. 
// markForDelete allows some children of this class to determine whether they are about to be deleted. This is mostly used to deduct points for failing to kill an enemy.
public abstract class Deletable : MonoBehaviour
{
    protected DeleteZone delete_zone;
    protected bool markForDelete = false;
    protected enum DeleteZone
    {
        Top,
        Bottom,
        Left,
        Right
    }

    // Map a delete zone to a function that returns a bool determining whether the object's current position is past the threshold for delete. 
    private static readonly Dictionary<DeleteZone, Func<Transform, bool>> deleteZoneThresholds = new()
    {
        { DeleteZone.Top, transform => transform.position.y > Camera.main.ViewportToWorldPoint(new Vector3(0, 1.1f, 0)).y},
        { DeleteZone.Bottom, transform => transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, -0.1f, 0)).y },
        { DeleteZone.Left, transform => transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0, 0)).x },
        { DeleteZone.Right, transform => transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x }
    };

    public virtual void Update()
    {
        deleteZoneThresholds.TryGetValue(delete_zone, out var conditonal); // Dynamically get threshold checking conditional from dictionary.
        markForDelete = conditonal.Invoke(transform);
        if (markForDelete) { Destroy(gameObject); }
    }

    // All deletable objects have collission.
    // The scene is set up so certain things can't collide with each other, and this handles the remaining cases.
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("Player")) { GameManager.instance.InitiateGameOver(); }
        else { GameManager.instance.IncreaseScore(10); } // Enemy destroyed
    }
}
