using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class PlayerController : MonoBehaviour
{
    private int gravitydirection = 1;
    [SerializeField] private float speed = 10;
    [SerializeField] private int direction=1;
    [SerializeField] private float jumpHeight;
    
    private bool isJumping = false;
    private Animator animator;
    private Rigidbody2D rig2D;
    
   
    [SerializeField] private TMP_Text battariesText;
    [SerializeField] private TMP_Text stopwatch;
    
    
   
    private float currentTime = 0f;

   

    [SerializeField] GameObject projectilePrefab;
    public int battery = 0;
    private int maxBattery = 0;
    private GameObject[] cubes;
    private int powerUps = 0;
    private float move;
    
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] FinishFlag finishFlag;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        animator = GetComponent<Animator>();
        rig2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        //If the game is not finished, the stopwatch is running
        if (!finishFlag.isFinished)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        stopwatch.text= time.Minutes.ToString()+":"+time.Seconds.ToString();

        //Checking player's inputs
        move = Input.GetAxis("Horizontal");
       
        
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
            isJumping = true;
            rig2D.velocity = Vector2.zero;
            rig2D.AddForce(new Vector2(0,gravitydirection * Mathf.Sqrt(-2 * Physics2D.gravity.y * gravitydirection * jumpHeight)), ForceMode2D.Impulse);
        }
        //Reverse gravity
        if (Input.GetKeyDown(KeyCode.F) && powerUps>0)
        {
            gravitydirection *= -1;
            Physics2D.gravity*=-1;
            transform.localScale= new Vector2(1, gravitydirection);

        }
        //Shooting
        if (Input.GetKeyDown(KeyCode.E) && battery>0 && powerUps>1)
        {
            battery -= 1;
            battariesText.text = "Battaries: " + battery;
            
            GameObject projectileObject = Instantiate(projectilePrefab,
                rig2D.position, Quaternion.Euler(0, 0, 90*(1-direction)));
            ProjectileController projectile = projectileObject.GetComponent<ProjectileController>();
            projectile.Launch(new Vector2(direction, 0), 300);
        }
        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && battery < maxBattery)
        {
            battery += 1;
            battariesText.text = "Battaries: " + battery;
            foreach (GameObject cube in cubes)
            {
                cube.GetComponent<CubeController>().Unfreeze();
            }

        }
       
    }
    //Player movement and animation
    public void FixedUpdate()
    {
        Vector2 position = transform.position;

        position.x += move * speed * Time.deltaTime;
        transform.position = position;

        if (move != 0)
        {
            direction = move < 0 ? -1 : 1;
            animator.SetInteger("direction", direction);
            animator.SetInteger("state", 1);

        }
        else
        {

            animator.SetInteger("state", 0);
        }

        move = 0;
    }
    //Stopping from infinite jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (isJumping)
        {
            isJumping = false;
        }
    }
    //Adding one battery after collecting the collectibles 
    public void AddBattery()
    {
        battery++;
        maxBattery++;
        battariesText.text = "Battaries: " + battery;

    }
    //Unlocking new power up
    public void AddPowerUp()
    {
        powerUps++;
        tutorialManager.popUpIndex++;
    }
   
    

}
