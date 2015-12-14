using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

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
            if(hasLife())
            {
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 40, ForceMode2D.Impulse);
                PlayerPrefs.SetInt("HasLife", 0);
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }


    private bool hasLife()
    {
        return PlayerPrefs.GetInt("HasLife", 0) == 1;
    }
}
