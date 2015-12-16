using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController main;

    void Start() {
        GameObject controller = GameObject.FindWithTag("GameController");
        if (controller != null) {
            main = controller.GetComponent<GameController>();
        }
        else {
            Debug.Log("Can't find GameController object.");
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("Shot")) {
            Destroy(c.gameObject);
            Destroy(gameObject);
            Instantiate(
                    explosion,
                    gameObject.transform.position,
                    gameObject.transform.rotation
            );
            main.AddScore(scoreValue);
        }

        if (c.tag.Equals("Player")) {
            Destroy(c.gameObject);
            Destroy(gameObject);
            Instantiate(
                    playerExplosion,
                    gameObject.transform.position,
                    gameObject.transform.rotation
            );
            main.GameOver();
        }
    }
}
