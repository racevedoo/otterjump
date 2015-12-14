using UnityEngine;
using System.Collections;

public class BouncerScript : MonoBehaviour {

    public float bounceFactor = 40f;
    public float repositionDelay = 0.1f;

    private float minXDefault = 1.41f;
    private float maxXDefault = 8.59f;

    private float minXBig = 2.18f;
    private float maxXBig = 7.82f;

    private float minX;
    private float maxX;

    private int _Score;
    private bool countScore = true;
    private GUIText _ScoreGUI;
    /// <summary>
    /// Score needed to show life
    /// </summary>
    private int _LifeScore = 15;
    private int _LifeScoreRange = 2;
    private int _LifeDuration = 3;
    private int _LifeCount;


    private int _AppleScore = 10;
    private int _AppleScoreRange = 2;
    private int _AppleDuration = 3;
    private int _AppleCount;

    private bool wasPlatformBig = false;

    // Use this for initialization
    void Start ()
    {
        GameObject.Find("HighScoreValue").GetComponent<GUIText>().text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        _ScoreGUI =  GameObject.Find("ScoreValue").GetComponent<GUIText>();
        GameObject.Find("Heart").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Heart").GetComponent<Collider2D>().enabled = false;
        GameObject.Find("Apple").GetComponent<Renderer>().enabled = false;
        GameObject.Find("Apple").GetComponent<Collider2D>().enabled = false;
        _LifeCount = 0;
        _AppleCount = 0;
        PlayerPrefs.SetInt("HasLife", 0);
        PlayerPrefs.SetInt("PlatformBig", 0);
        _LifeScore = Random.Range(_LifeScore - _LifeScoreRange, _LifeScore + _LifeScoreRange);
        _AppleScore = Random.Range(_AppleScore - _AppleScoreRange, _AppleScore + _AppleScoreRange);
        minX = minXDefault;
        maxX = maxXDefault;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(PlayerPrefs.GetInt("PlatformBig", 0) == 1)
        {
            minX = minXBig;
            maxX = maxXBig;
        }
        else
        {
            minX = minXDefault;
            maxX = maxXDefault;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (countScore)//enter only once
            {
                _Score++;
                GameObject heart = GameObject.Find("Heart");
                GameObject apple = GameObject.Find("Apple");
                if (heart.GetComponent<Renderer>().enabled)
                {
                    _LifeCount++;
                    if(_LifeCount == _LifeDuration)
                    {
                        heart.GetComponent<Renderer>().enabled = false;
                        heart.GetComponent<Collider2D>().enabled = false;
                        _LifeCount = 0;
                    }
                }
                else if(_Score == _LifeScore)
                {
                    heart.GetComponent<Renderer>().enabled = true;
                    heart.GetComponent<Collider2D>().enabled = true;
                    heart.transform.position = new Vector2(Random.Range(minX + 1, maxX - 1), heart.transform.position.y);
                }

                if (PlayerPrefs.GetInt("PlatformBig", 0) == 1)
                {
                    _AppleCount++;
                    if (_AppleCount == _AppleDuration)
                    {
                        transform.localScale = transform.localScale - new Vector3(0.05f, 0f);
                        PlayerPrefs.SetInt("PlatformBig", 0);
                        _AppleCount = 0;
                    }
                }
                else if (_Score == _AppleScore)
                {
                    apple.GetComponent<Renderer>().enabled = true;
                    apple.GetComponent<Collider2D>().enabled = true;
                    apple.transform.position = new Vector2(Random.Range(minX + 1, maxX - 1), apple.transform.position.y);
                }


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
