using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    private float length;

    private Transform cam;
    public float parallaxEffect;
    private Transform player;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float RePos = player.position.x * parallaxEffect; 
        float distancia = cam.position.x * (1 - parallaxEffect); 
        transform.position = new Vector3(startPos + distancia, transform.position.y, transform.position.z); 

       
        if (RePos > startPos + length) 
        {
            startPos += length;
        }
        else if (RePos < startPos - length) 
        {
            startPos -= length;
        }


    }
}