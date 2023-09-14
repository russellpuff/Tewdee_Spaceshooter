using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private bool isGameOver;
    public static GameManager instance;
    private static float scoreBasedSpawnMult = 1;
    private Vector3 lockedMousePos;

    [SerializeField] TextMeshProUGUI scoreboard;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject background_1;
    [SerializeField] GameObject background_2;

    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && isGameOver)
        { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }


        // Move background. Scrolls downwards indefinitely, moves opposite to mouse current position to simulate real movement. 
        Vector3 mousePos = isGameOver ? lockedMousePos : Camera.main.ScreenToWorldPoint(Input.mousePosition);
        background_1.transform.position = new Vector3(mousePos.x * -1.5f, background_1.transform.position.y - (8f * Time.deltaTime), 0);
        background_2.transform.position = new Vector3(mousePos.x * -1.5f, background_2.transform.position.y - (8f * Time.deltaTime), 0);
        if (background_1.transform.position.y < -17.8f)
        { background_1.transform.position = new Vector3(mousePos.x * -1.5f, 12.5f, 0); }
        if (background_2.transform.position.y < -17.8f)
        { background_2.transform.position = new Vector3(mousePos.x * -1.5f, 12.5f, 0); }
    }

    public void IncreaseScore(int _score)
    {
        if (!isGameOver) // On game over, stop updating the score (more specifically, stop deducting points)
        {
            this.score += _score;
            if (this.score < 0) { this.score = 0; }
            scoreboard.text = $"Score: {this.score}";
            // Update score-based spawn multiplier. 
            int tier = score / 100;
            scoreBasedSpawnMult = (float)Math.Pow(0.9f, tier);
        }
    }

    public void InitiateGameOver()
    {
        isGameOver = true;
        lockedMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lock mouse to prevent horizontal background movement.
        scoreboard.text = "";
        gameOverText.SetActive(true);
        TextMeshProUGUI tmp = gameOverText.GetComponent<TextMeshProUGUI>();
        tmp.text = $"GAME OVER\n\nSCORE: {score}\n\nENTER TO RESTART";
    }

    public float GetSpawnBasedScoreMult()
    {
        return scoreBasedSpawnMult;
    }
}