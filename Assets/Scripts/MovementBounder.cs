using UnityEngine;
using System.Collections;

public class MovementBounder : MonoBehaviour {

    public float maxX = 10f;
    public float minX = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= minX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }
        else if (transform.position.x >= maxX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
    }
}
