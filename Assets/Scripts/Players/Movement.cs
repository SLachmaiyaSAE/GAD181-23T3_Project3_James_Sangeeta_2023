using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Movement : MonoBehaviour
{
    //input stuff.
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    //movement stuff.
    public float speed = 16;
    public float speedMultiplier = 1.0f;
    public float inputCooldown = 0.1f;

    //timestamp of last input
    private float lastInputTime;

    public GameObject wallPrefab;
    //current wall.
    Collider2D wall;
    //last wall's end.
    Vector2 lastWallEnd;

    //ref to menu functionality.
    public MenuFunctionality menuFunctionality;

    //dash stuff.
    private bool isDashActive = false;

    public Vector2 currentDirection;

    //phase stuff.
    private bool isPhasing = false;
    private bool isFlashing = false;
    private float playAreaWidth = 140.3f;
    private float playAreaHeight = 140.8f;
    public bool IsPhasing => isPhasing;

    //slow stuff
    private bool isSlowing = false;
    public bool IsSlowing => isSlowing;

    private void Start()
    {
        //initial velocty.
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        SpawnWall();
        currentDirection = Vector2.up;
    }
    void Update()
    {
        if (!menuFunctionality.IsGamePaused())
        {
            if (Time.time - lastInputTime >= inputCooldown)
            {
                //check for input cooldown before processing new input.
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


            if (isPhasing)
            {
                //check for player movement during phasing and restrict it within the play area.
                float newX = Mathf.Clamp(transform.position.x, -playAreaWidth / 2, playAreaWidth / 2);
                float newY = Mathf.Clamp(transform.position.y, -playAreaHeight / 2, playAreaHeight / 2);
                transform.position = new Vector3(newX, newY, transform.position.z);
            }
        }
    }
    void HandleInput(Vector2 direction)
    {
        currentDirection = direction;

        if (!isDashActive)
        {
            //apply speed boost.
            GetComponent<Rigidbody2D>().velocity = currentDirection * speed * speedMultiplier;
        }
        else
        {
            //regular movement without speed boost.
            GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
        }

        SpawnWall();
        lastInputTime = Time.time;  //timestamp of the last input.
    }

    void SpawnWall()
    {
        //save last wall's position.
        lastWallEnd = transform.position;

        //spawn a new lightwall.
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        //calculate the centre position.
        co.transform.position = a + (b - a) * 0.5f;

        //scale it horizontally or vertically.
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 1, 1);
        }
        else
        {
            co.transform.localScale = new Vector2(1, dist + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Ability"))
        {
            DashAbilityPickup dashPickup = otherCollider.GetComponent<DashAbilityPickup>();
            if (dashPickup != null)
            {
                Debug.Log("Dash ability picked up");
                //activate dash ability.
                StartCoroutine(ActivateDash(dashPickup.duration, dashPickup.speedMultiplier));
            }
        }
        else
        {
            if (otherCollider != wall)
            {
                menuFunctionality.PlayerDestroyed(name);
                Debug.Log("Player has been destroyed");
                Destroy(gameObject);
            }
        }
    }
    //DASH
    public void ActivateDashAbility(float duration, float speedMultiplier)
    {
        if (!isDashActive)
        {
            StartCoroutine(ActivateDash(duration, speedMultiplier));
        }
    }

    private IEnumerator ActivateDash(float duration, float speedMultiplier)
    {
        Debug.Log("Dash ability activated");

        isDashActive = true;

        //increase player speed.
        speed *= speedMultiplier;

        //wait for the specified duration.
        yield return new WaitForSeconds(duration);

        //reset player speed to the original value.
        speed /= speedMultiplier;

        isDashActive = false;
    }
    ///PHASE 
    public void SetPhaseState(bool isPhasing, float flashInterval)
    {
        this.isPhasing = isPhasing;

        if (isPhasing)
        {
            //disable the collider.
            GetComponent<Collider2D>().enabled = false;
            //start flashing the sprite.
            if (!isFlashing)
            {
                InvokeRepeating("FlashSprite", 0f, flashInterval);
                isFlashing = true;
            }
        }
        else
        {
            //re-enable collider.
            GetComponent<Collider2D>().enabled = true;
            //stop flashing.
            if (isFlashing)
            {
                CancelInvoke("FlashSprite");
                isFlashing = false;
            }
            //visible sprite
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void FlashSprite()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }
    public void StartPhase()
    {
        //set the player's movement to be unaffected by obstacles during phasing.
        isPhasing = true;
        SetPhaseState(isPhasing, 0.5f);
    }
    public void EndPhase()
    {
        //reset the player's movement to normal.
        isPhasing = false;
        SetPhaseState(isPhasing, 0f);
    }
    //SLOW
    public void SlowOpponent(float slowSpeedDecrease, float slowDuration)
    {
        isSlowing = true;
        speed -= slowSpeedDecrease;

        StartCoroutine(EndSlow(slowSpeedDecrease, slowDuration));
    }

    private IEnumerator EndSlow(float slowSpeedDecrease, float slowDuration)
    {
        yield return new WaitForSeconds(slowDuration);

        //restore the original speed.
        speed += slowSpeedDecrease;
        isSlowing = false;
    }
}