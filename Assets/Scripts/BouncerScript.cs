using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class PowerUp
{
    public int Duration;

    public int DisappearCount;

    public int Count;

    public string Name;

    public PowerUp(string name, int duration)
    {
        Count = 0;
        Duration = duration;
        DisappearCount = 0;
        Name = name;
    }
}

public class BouncerScript : MonoBehaviour
{

    public float bounceFactor = 40f;
    public float repositionDelay = 0.1f;

    private float minXDefault = 1.41f;
    private float maxXDefault = 8.59f;

    private float minXBig = 2.18f;
    private float maxXBig = 7.82f;

    private float minXSmall = 0.96f;
    private float maxXSmall = 9.04f;

    private float minX;
    private float maxX;

    private int _Score;
    private bool countScore = true;
    private GUIText _ScoreGUI;

    private IList<PowerUp> powerups;

    private bool wasPlatformBig = false;

    private int powerupIndex;

    // Use this for initialization
    void Start()
    {
        powerups = new List<PowerUp>();
        powerups.Add(new PowerUp("Life", 3));
        powerups.Add(new PowerUp("Apple", 3));
        powerups.Add(new PowerUp("Fish", 3));
        powerups.Add(new PowerUp("Mushroom", 3));
        GameObject.Find("HighScoreValue").GetComponent<GUIText>().text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        _ScoreGUI = GameObject.Find("ScoreValue").GetComponent<GUIText>();
        var renderers = GameObject.Find("LifeFloor").GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
        PlayerPrefs.SetInt("HasLife", 0);
        PlayerPrefs.SetInt("PlatformBig", 0);
        PlayerPrefs.SetInt("PlatformSmall", 0);
        PlayerPrefs.SetInt("Gravity", 0);
        minX = minXDefault;
        maxX = maxXDefault;
        powerupIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PlatformBig", 0) == 1)
        {
            minX = minXBig;
            maxX = maxXBig;
        }
        else if(PlayerPrefs.GetInt("PlatformSmall", 0) == 1)
        {
            minX = minXSmall;
            maxX = maxXSmall;
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
                GetComponent<AudioSource>().Play();
                _Score++;
                if(_Score % 10 == 0 || powerupIndex == -1)
                {
                    if(PlayerPrefs.GetInt("HasLife", 0) == 1)
                    {
                        powerupIndex = Random.Range(1, powerups.Count);
                    }
                    else
                    {
                        powerupIndex = Random.Range(0, powerups.Count);
                    }
                }
                PowerUp powerup = powerups[powerupIndex];
                GameObject obj = GameObject.Find(powerup.Name);

                switch (powerup.Name)
                {
                    case "Life":


                        if (obj.GetComponent<Renderer>().enabled)
                        {
                            powerup.Count++;
                            if (powerup.Count == powerup.Duration)
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                var renderers = GameObject.Find("LifeFloor").GetComponentsInChildren<Renderer>();
                                foreach (var renderer in renderers)
                                {
                                    renderer.enabled = false;
                                }
                                powerup.Count = 0;
                            }
                        }
                        else if(_Score % 10 == 0)
                        {
                            obj.GetComponent<Renderer>().enabled = true;
                            obj.GetComponent<Collider2D>().enabled = true;
                            obj.transform.position = new Vector2(Random.Range(minX + 1, maxX - 1), obj.transform.position.y);
                        }

                        break;
                    case "Apple":
                        if (PlayerPrefs.GetInt("PlatformBig", 0) == 1)
                        {
                            powerup.Count++;
                            if (powerup.Count == powerup.Duration)//
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                transform.localScale = transform.localScale - new Vector3(0.05f, 0f);
                                PlayerPrefs.SetInt("PlatformBig", 0);
                                powerup.Count = 0;
                            }
                        }
                        else if (!obj.GetComponent<Renderer>().enabled && _Score % 10 == 0)//show fruit
                        {
                            obj.GetComponent<Renderer>().enabled = true;
                            obj.GetComponent<Collider2D>().enabled = true;
                            obj.transform.position = new Vector2(Random.Range(minX + 1, maxX - 1), obj.transform.position.y);
                        }
                        else if(PlayerPrefs.GetInt("PlatformBig", 0) == 0)
                        {
                            powerup.DisappearCount++;
                            if (powerup.DisappearCount == powerup.Duration)//
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                powerup.DisappearCount = 0;
                            }
                        }

                        break;
                    case "Fish":
                        if (PlayerPrefs.GetInt("PlatformSmall", 0) == 1)
                        {
                            powerup.Count++;
                            if (powerup.Count == powerup.Duration)//
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                transform.localScale = transform.localScale + new Vector3(0.05f, 0f);
                                PlayerPrefs.SetInt("PlatformSmall", 0);
                                powerup.Count = 0;
                            }
                        }
                        else if (!obj.GetComponent<Renderer>().enabled && _Score % 10 == 0)//show fruit
                        {
                            obj.GetComponent<Renderer>().enabled = true;
                            obj.GetComponent<Collider2D>().enabled = true;
                            obj.transform.position = new Vector2(Random.Range(minX + 1, maxX - 1), obj.transform.position.y);
                        }
                        else if (PlayerPrefs.GetInt("PlatformSmall", 0) == 0)
                        {
                            powerup.DisappearCount++;
                            if (powerup.DisappearCount == powerup.Duration)//
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                powerup.DisappearCount = 0;
                            }
                        }
                        break;
                    case "Mushroom":
                        if (PlayerPrefs.GetInt("Gravity", 0) == 1)
                        {
                            powerup.Count++;
                            if (powerup.Count == powerup.Duration)//
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                GameObject character = GameObject.Find("Character");
                                character.GetComponent<Rigidbody2D>().gravityScale--;
                                PlayerPrefs.SetInt("Gravity", 0);
                                powerup.Count = 0;
                            }
                        }
                        else if (!obj.GetComponent<Renderer>().enabled && _Score % 10 == 0)//show fruit
                        {
                            obj.GetComponent<Renderer>().enabled = true;
                            obj.GetComponent<Collider2D>().enabled = true;
                            obj.transform.position = new Vector2(Random.Range(minX + 1, maxX - 1), obj.transform.position.y);
                        }
                        else if (PlayerPrefs.GetInt("Gravity", 0) == 0)
                        {
                            powerup.DisappearCount++;
                            if (powerup.DisappearCount == powerup.Duration)//
                            {
                                obj.GetComponent<Renderer>().enabled = false;
                                obj.GetComponent<Collider2D>().enabled = false;
                                powerup.DisappearCount = 0;
                            }
                        }
                        break;
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
