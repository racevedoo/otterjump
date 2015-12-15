using UnityEngine;
using System.Collections;

public class AppleScript : MonoBehaviour {

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
            GameObject bouncer = GameObject.Find("Bouncer");
            bouncer.transform.localScale = bouncer.transform.localScale + new Vector3(0.05f,0f);
            setPlatformSize();
        }
    }

    private void setPlatformSize()
    {
        PlayerPrefs.SetInt("PlatformBig", 1);
    }
}
