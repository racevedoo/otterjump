using UnityEngine;
using System.Collections;

public class BouncerScript : MonoBehaviour {

    public float bounceFactor = 4f;

    private float minX;
    private float minY;
    private float maxY;
    private float maxX;


    // Use this for initialization
    void Start () {
        float vert = Camera.main.orthographicSize;
        float hor = vert * Screen.width / Screen.height;
    }
	
	// Update is called once per frame
	void Update () {
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
        other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceFactor, ForceMode2D.Impulse);
    }

}
