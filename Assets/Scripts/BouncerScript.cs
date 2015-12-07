using UnityEngine;
using System.Collections;

public class BouncerScript : MonoBehaviour {

    public float bounceFactor = 4f;
    public float repositionDelay = 0.1f;

    private float minX = 0;
    private float maxX = 8.9f;

    private int _Score;
    private bool countScore = true;
    private GUIText _ScoreGUI;

    // Use this for initialization
    void Start ()
    {
        GameObject.Find("HighScoreValue").GetComponent<GUIText>().text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        _ScoreGUI =  GameObject.Find("ScoreValue").GetComponent<GUIText>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // toggle visibility:
            Renderer renderer = GetComponent<Renderer>();
            Collider2D collider = GetComponent<Collider2D>();
            renderer.enabled = !renderer.enabled;
            collider.enabled = !collider.enabled;
            transform.position = transform.position - new Vector3(transform.position.x + 0.1f, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (countScore)
            {
                _Score++;
                _ScoreGUI.text = _Score.ToString();
                countScore = false;
            }
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceFactor, ForceMode2D.Impulse);            
            Invoke("SpawnPlatform", repositionDelay);
        }        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        countScore = true;
    }

    void SpawnPlatform()
    {
        transform.position = new Vector2(Random.Range(minX, maxX), transform.position.y);

        if (_Score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", System.Convert.ToInt32(_ScoreGUI.text));
    }

}
