using UnityEngine;
using System.Collections;

[System.Serializable]

public class Constraint {
	public float xMin, xMax, zMin, zMax, speed, tilt;
}

public class PlayerController : MonoBehaviour {

	public Constraint boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.3f;
    private float nextFire = 0.0f;

    // Called on every frame.
    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            nextFire = Time.time + fireRate;
            GetComponent<AudioSource>().Play();
        }
    }

	// Called before each physics step.
	void FixedUpdate() {
		float mvHori = Input.GetAxis ("Horizontal");
		float mvVert = Input.GetAxis ("Vertical");
		Rigidbody rgbody = GetComponent<Rigidbody> ();
		Vector3 clamp = new Vector3 (
			Mathf.Clamp (rgbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rgbody.position.z, boundary.zMin, boundary.zMax)
		);
		Vector3 mvmt = new Vector3 (mvHori, 0.0f, mvVert);
		mvmt *= boundary.speed;
		Vector3 tiltV = new Vector3 (0.0f, 0.0f, mvHori * -boundary.tilt);

		rgbody.velocity = mvmt;
		rgbody.position = clamp;
		rgbody.rotation = Quaternion.Euler(tiltV);
	}
}
