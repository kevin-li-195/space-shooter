using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    void OnTriggerEnter(Collider c) {
        if (c.tag.Equals("Shot")) {
            Destroy(c.gameObject);
            Destroy(this.gameObject);
            Instantiate(
                    explosion,
                    gameObject.transform.position,
                    gameObject.transform.rotation
            );
        }

        if (c.tag.Equals("Player")) {
            Destroy(c.gameObject);
            Destroy(this.gameObject);
            Instantiate(
                    playerExplosion,
                    gameObject.transform.position,
                    gameObject.transform.rotation
            );
        }
    }
}
