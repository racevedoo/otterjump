using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

    private bool start = false;
	// Use this for initialization
	void Start () {
        InvokeRepeating("ChangeBackground", .5f, .5f);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))//left button down
        {
            Play();
        }

    }

    public void Play()
    {
        Application.LoadLevel("MainScene");
    }

    void ChangeBackground()
    {

        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(start? "start" : "start2", typeof(Sprite)) as Sprite;
        start = !start;
    }
}
