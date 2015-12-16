using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float hazardDelay;
    public float startDelay;
    public float levelDelay;

    private Text scoreText;
    private Text gameOverText;
    private Text restartText;
    private int score;

    private bool gameOver;
    private bool restart;

    private GUIStyle leftStyle, centerStyle, rightStyle;

//     void OnGUI() {
//         rightStyle = GUI.skin.GetStyle("Label");
//         leftStyle = GUI.skin.GetStyle("Label");
//         centerStyle = GUI.skin.GetStyle("Label");
//         leftStyle.alignment = TextAnchor.UpperLeft;
//         GUI.Label(new Rect(10, 10, 100, 50), scoreText.text, leftStyle);
//         rightStyle.alignment = TextAnchor.UpperRight;
//         GUI.Label(new Rect(Screen.height - 10, Screen.width - 10, 100, 50), restartText.text, leftStyle);
//         centerStyle.alignment = TextAnchor.MiddleCenter;
//         GUI.Label(new Rect(Screen.height / 2, Screen.width / 2, 300, 50), gameOverText.text, centerStyle);
//     }

    void Start () {
        gameOver = false;
        restart = false;

        gameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        if (gameOverText == null) {
            Debug.Log("Couldn't find gameOverText");
        }
        restartText = GameObject.Find("Restart").GetComponent<Text>();
        if (restartText == null) {
            Debug.Log("Couldn't find restartText");
        }
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        if (scoreText == null) {
            Debug.Log("Couldn't find ScoreText");
        }
        gameOverText.text = "";
        restartText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update() {
        if (restart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startDelay);
        while (!gameOver) {
            for (int i = 0; i < hazardCount; i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x,spawnValues.x),
                                                    spawnValues.y,
                                                    spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                if (gameOver) {
                    restartText.text = "Press 'R' to restart.";
                    restart = true;
                    break;
                }
                yield return new WaitForSeconds(hazardDelay);
            }
            yield return new WaitForSeconds(levelDelay);
        }
    }

    public void GameOver() {
        gameOver = true;
        gameOverText.text = "Game over.";
    }

    public void AddScore(int s) {
        score += s;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }
}
