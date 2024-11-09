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
    private int state = 1;
    private bool isJumping = false;
    private Animator animator;
    private Rigidbody2D rig2D;
    [SerializeField] private int presents = 0;
    private int totalPresents = -1;
    [SerializeField] private int lifes = 3;
    [SerializeField] private TMP_Text battariesText;
    [SerializeField] private TMP_Text stopwatch;
    [SerializeField] private Image[] images;
    [SerializeField] private Image timer;
    private float totalTime = 30f;
    private float currentTime = 0f;

    private float timeToDie;
    private float deathTime = 0.4f;

    [SerializeField] GameObject projectilePrefab;
    public int battery = 0;
    private int maxBattery = 0;
    private GameObject[] cubes;
    private int powerUps = 0;
    private float move;
    
    [SerializeField] TutorialManager tutorialManager;
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
        currentTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        stopwatch.text= time.Minutes.ToString()+":"+time.Seconds.ToString();
        move = Input.GetAxis("Horizontal");
       

        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
            isJumping = true;
            rig2D.velocity = Vector2.zero;
            rig2D.AddForce(new Vector2(0,gravitydirection * Mathf.Sqrt(-2 * Physics2D.gravity.y * gravitydirection * jumpHeight)), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.F) && powerUps>0)
        {
            gravitydirection *= -1;
            Physics2D.gravity*=-1;
            transform.localScale= new Vector2(1, gravitydirection);

        }
        if (Input.GetKeyDown(KeyCode.E) && battery>0 && powerUps>1)
        {
            battery -= 1;
            battariesText.text = "Battaries: " + battery;
            //fireDirection = new Vector2(direction, 0);
            GameObject projectileObject = Instantiate(projectilePrefab,
                rig2D.position, Quaternion.Euler(0, 0, 90*(1-direction)));
            ProjectileController projectile = projectileObject.GetComponent<ProjectileController>();
            projectile.Launch(new Vector2(direction, 0), 300);
        }

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (isJumping)
        {
            isJumping = false;
        }
    }
    public void AddBattery()
    {
        battery++;
        maxBattery++;
        battariesText.text = "Battaries: " + battery;

    }
    public void AddPowerUp()
    {
        powerUps++;
        tutorialManager.popUpIndex++;
    }
   
    public void AddLive()
    {
        lifes++;
        updateLivesUI();
    }
    private void updateLivesUI()
    {
        switch (lifes)
        {
            case 0:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 1:
                images[0].enabled = true;
                images[1].enabled = false;
                images[2].enabled = false;
                break;
            case 2:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = false;
                break;
            case 3:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
        }
    }
    public void RemoveLife()
    {
        lifes--;
        updateLivesUI();


    }
    public void Die()
    {
        animator.SetFloat("MoveY", 1);
        animator.SetFloat("MoveX", 0);
        timeToDie -= Time.deltaTime;
        if (timeToDie < 0)
        {
            Destroy(gameObject);
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

}
