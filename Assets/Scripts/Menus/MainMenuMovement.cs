using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class MainMenuMovement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public float speed = 16;

    public float inputCooldown = 0.1f;

    //timestamp of last input
    private float lastInputTime;

    public GameObject wallPrefab;
    //current wall.
    Collider2D wall;
    //last wall's end.
    Vector2 lastWallEnd;

    //ref to menu functionality
    //public MenuFunctionality menuFunctionality;


    public Vector2 currentDirection;

    private void Start()
    {
        //initial velocty 
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        SpawnWall();
        currentDirection = Vector2.right;
    }
    void Update()
    {
        
            if (Time.time - lastInputTime >= inputCooldown)
            {
                //check for input cooldown before processing new input
                if (Input.GetKeyDown(up) && currentDirection != Vector2.down)
                {
                    HandleInput(Vector2.up);
                }
                else if (Input.GetKeyDown(down) && currentDirection != Vector2.up)
                {
                    HandleInput(-Vector2.up);
                }
                else if (Input.GetKeyDown(left) && currentDirection != Vector2.right)
                {
                    HandleInput(-Vector2.right);
                }
                else if (Input.GetKeyDown(right) && currentDirection != Vector2.left)
                {
                    HandleInput(Vector2.right);
                }
            }

            FitColliderBetween(wall, lastWallEnd, transform.position);
        
    }
    void HandleInput(Vector2 direction)
    {
        currentDirection = direction;
        GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
        SpawnWall();
        lastInputTime = Time.time;  //timestamp of the last input
    }

    void SpawnWall()
    {
        //save last wall's position
        lastWallEnd = transform.position;

        //spawn a new lightwall.
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        //calculate the centre position
        co.transform.position = a + (b - a) * 0.5f;

        //Scale it horizontally or vertically 
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 0.2f, 0.2f);
        }
        else
        {
            co.transform.localScale = new Vector2(0.2f, dist + 0.2f);
        }
    }

   // private void OnTriggerEnter2D(Collider2D co)
   // {
        //not the current wall
      //  if (co != wall)
      //  {
         //   menuFunctionality.PlayerDestroyed(name);
           // Destroy(gameObject);
       // }
  //  }
}