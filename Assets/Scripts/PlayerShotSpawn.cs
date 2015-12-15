﻿using UnityEngine;
using System.Collections;

public class PlayerShotSpawn : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;

	void Start () {
        offset = this.transform.position - player.transform.position;
	}
	
	void FixedUpdate () {
        this.transform.position = player.transform.position + offset;
	}
}
