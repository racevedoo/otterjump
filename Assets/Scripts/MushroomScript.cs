using UnityEngine;
using System.Collections;

public class MushroomScript : MonoBehaviour {

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
            GameObject character = GameObject.Find("Character");
            character.GetComponent<Rigidbody2D>().gravityScale++;
            setGravity();
        }
    }

    private void setGravity()
    {
        PlayerPrefs.SetInt("Gravity", 1);
    }
}
