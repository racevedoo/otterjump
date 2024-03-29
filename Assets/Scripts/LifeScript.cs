﻿using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            setLife();
        }
    }

    private void setLife()
    {
        PlayerPrefs.SetInt("HasLife", 1);
        var renderers = GameObject.Find("LifeFloor").GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}
