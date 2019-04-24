using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //wahaha
    private Rigidbody2D rb2d;

    //speed variable
    public float speed;
    public float jumpPower;

    //count stuff
    static private int count;
    public Text winText;
    //don't need count text anymore so that's ok

    //to display health
    public Image heartDisplay;

    //for the sound
    public AudioSource jumpSound;


    //life
    public int currHealth;
    public int maxHealth;

    //for animation
    public Animator moveCat;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        winText.text = " ";

        //health
        maxHealth = 3;
        //at start of the game
        currHealth = maxHealth;

        Debug.Log(count);

    }

    // Update is called once per frame
    void Update()
    {
        //making sure the current health doesn't exceed max
     if(currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        //for death
        if (currHealth == 0)
        {
            SceneManager.LoadScene("Game Over!", LoadSceneMode.Single);
        }
    }

    //the FLIP
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * - 1;
        transform.localScale = Scaler;
    }

    //here for physics babies
    private void FixedUpdate()
    {
        moveCat.SetBool("Is Jumping", false);
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

        //for moving with animations
        moveCat.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        //the flip code
        if(facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }
        //for jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveCat.SetBool("Is Jumping", true);
            rb2d.AddForce(Vector2.up * jumpPower);
            
            //sound for jumping
            jumpSound.Play();
        }

    }

    //for picking up collectibles
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count =  count + 1;
            SetWinText();
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            currHealth = currHealth - 1;
        }

        if (other.gameObject.CompareTag("ExtraTime"))
        {
            other.gameObject.SetActive(false);
            TimerController.timer += 5;
            TimerController.counter += 5;
        }
    }

    void SetWinText()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        //need to change this so that it references the 

        if (count == 4 && currHealth >= 1 && currentScene.name != "Level2")
        {
            SceneManager.LoadScene("Level2");
            count = 0;
        }

        if (count == 4 && currHealth >= 1 && currentScene.name == "Level2")
        {
            SceneManager.LoadScene("Win Scene");
            count = 0;
        }
    }

}
