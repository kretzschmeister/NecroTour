﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene(AreaBehaviour.sceneToLoad);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
