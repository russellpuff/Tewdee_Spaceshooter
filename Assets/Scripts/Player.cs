using UnityEngine;

public class Player : MonoBehaviour
{
    private float yPos;
    [SerializeField] GameObject laser;
    void Start()
    {
        yPos = transform.position.y;
    }

    void Update()
    {
        Vector3 convertedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(convertedPos.x, yPos, 0);

        if (Input.GetButtonDown("FireLaser"))
            { Instantiate(laser, transform.position, Quaternion.identity); }
    }
}
