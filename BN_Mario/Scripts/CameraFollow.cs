using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 3f;
    public GameObject target; // To follow
    public Vector2 followOffset; // x and y offsets

    private Vector2 threshold;
    private Rigidbody2D rb2D; // Character's rigidbody

    // Start is called before the first frame update
    void Start()
    {
        threshold = calculateThreshold();
        rb2D = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Move character on the same frame that the character is moving
    private void FixedUpdate()
    {
        // Update target's position by frame
        Vector2 follow = target.transform.position;

        // Calculate distance target is from the center of the camera
        // Distance from center
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        // moveSpeed = to character speed if character exceeds cameraSpeed
        float moveSpeed = Mathf.Abs(rb2D.velocity.x) > cameraSpeed ? Mathf.Abs(rb2D.velocity.x) : cameraSpeed;
        // Transform new position of the camera
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        
    }

    private Vector3 calculateThreshold()
    {
        // Aspect ratio of camera
        Rect aspect = Camera.main.pixelRect;

        // Define dimensions of camera
        Vector2 d = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);

        // Calculate threshold
        d.x -= followOffset.x;
        d.y -= followOffset.y;
        return d;
    }

    // Draw a gizmo of camera borders
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 border = calculateThreshold();

        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
