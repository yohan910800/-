using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    public float distance;
    public Vector3 originPos;
    public bool justOnceEscapMenu;

    Rigidbody2D rb;
    GameObject mouth;
    LineRenderer lr;
    bool clicked;
    float clickeTimer;

    float power;
    float timerchargeFace;
    bool startChargeFace;
    float timermaface;
    bool startMaFace;

    GameObject ma;
    GameObject ma2;
    GameObject goal;
    

    Text goalText;

    SpriteRenderer face;

    Sprite normalFace;
    Sprite chargeFace;
    Sprite maFace;


    float i;
    int jumpCharge;

    GameObject endPanel;

    AudioSource audioSource;

    AudioClip maSound1;
    AudioClip maSound2;
    AudioClip maSound3;

    GameObject escapeMenu;
    

    void Start()
    {
        originPos=transform.position;
        rb = GetComponent<Rigidbody2D>();
        mouth = gameObject.transform.GetChild(0).transform.gameObject;

        lr = mouth.GetComponent<LineRenderer>();
        ma = Resources.Load<GameObject>("Prefabs/ma");
        ma2 = Resources.Load<GameObject>("Prefabs/ma2");
        power = 250.0f;
        goal = GameObject.Find("Start");
        goalText = GameObject.Find("PlayerUi").
            transform.GetChild(0).gameObject.GetComponent<Text>();
        face = GetComponent<SpriteRenderer>();

        normalFace= Resources.Load<Sprite>("Sprites/1");
        chargeFace= Resources.Load<Sprite>("Sprites/2");
        maFace= Resources.Load<Sprite>("Sprites/3");
        endPanel= Resources.Load<GameObject>("Prefabs/GameOverPanel");

        face.sprite = normalFace;
        i = 1;
        audioSource = GetComponent<AudioSource>();
        maSound1= Resources.Load<AudioClip>("Audio/ma1");
        maSound2= Resources.Load<AudioClip>("Audio/ma2");
        maSound3= Resources.Load<AudioClip>("Audio/ma3");


        escapeMenu = Resources.Load<GameObject>("Prefabs/EscapeMenuPanel");
    }
    void ControlMainMenu()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (justOnceEscapMenu == false)
            {
                GameObject escapeMenuObj = Instantiate(escapeMenu, gameObject.transform.position, Quaternion.identity);
                Time.timeScale = 0.01f;
                justOnceEscapMenu = true;
            }
        }
            
    }

    void Update()
    {

        ControlMainMenu();
        LimitVelocity();

        Debug.Log(rb.velocity.magnitude);
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ShowDirection(dir);

        if (Input.GetMouseButtonDown(0))
        {
            //if (transform.position.y <=-2.7f) {
            //rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            //if (jumpCharge < 2)
            //{
                rb.AddForce((new Vector2(dir.x, dir.y) - (Vector2)transform.position) * power);
            audioSource.clip = maSound1;
            audioSource.Play();
                clicked = true;
                SayMa();
                //jumpCharge ++;
            //}
            //}
            //else
            //{
            //    //rb.constraints = RigidbodyConstraints2D.None;
            //    rb.AddForce((new Vector2(dir.x, 0) - (Vector2)transform.position) * power);
            //    SayMa();
            //    clicked = true;
            //}
        }
        else if (Input.GetMouseButtonUp(0))
        {
            i = 1;  
            clicked = false;
            startMaFace = true;
        }

        Debug.Log(clicked);
        if (clicked == true)
        {
            face.sprite = chargeFace;
            clickeTimer += Time.deltaTime;
            i -= 0.01f;
            if (rb.velocity.magnitude <= 20)
            {
                ChangeFaceColor(i);
            }
            
            
            if (clickeTimer >= 1.5f)
            {
                i = 1;
                startMaFace = true;
                power += 2500.0f;
                rb.AddForce((new Vector2(dir.x, dir.y) - (Vector2)transform.position) * power);
                audioSource.clip = maSound2;
                audioSource.Play();
                GameObject maObj2 = Instantiate(ma2, mouth.transform.position, Quaternion.identity);
                maObj2.transform.GetChild(0).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                maObj2.transform.GetChild(1).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                maObj2.transform.GetChild(2).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                rb.constraints = RigidbodyConstraints2D.None;
                clickeTimer = 0.0f;
            }
            
        }
        else if(clickeTimer >= 0.5f && clicked == false)
        {
            i = 1;
            startMaFace = true;
            power += 500.0f;
            rb.AddForce((new Vector2(dir.x, dir.y) - (Vector2)transform.position) * power);
            audioSource.clip = maSound3;
            audioSource.Play();
            GameObject maObj2 = Instantiate(ma2, mouth.transform.position, Quaternion.identity);
            maObj2.transform.GetChild(0).transform.localScale = new Vector3(0.02f, 0.02f, 1);
            maObj2.transform.GetChild(1).transform.localScale = new Vector3(0.02f, 0.02f, 1);
            maObj2.transform.GetChild(2).transform.localScale = new Vector3(0.02f, 0.02f, 1);
            rb.constraints = RigidbodyConstraints2D.None;
            clickeTimer = 0.0f;
            //clickeTimer = 0.0f;
            //power = 250.0f;
        }
        else
        {
            clickeTimer = 0.0f;
            power = 250.0f;
        }
        CalculateGoalDistance();
        StartMaFace();
        
    }

    void LimitVelocity()
    {
        if (rb.velocity.magnitude >= 20/*&& transform.position.y <= -1.0f*/)
        {
            rb.velocity = rb.velocity * 0.9f;

            //face.color = Color.blue;


        }
    }
    
    void SayMa()
    {
        GameObject maObj = Instantiate(ma, mouth.transform.position, Quaternion.identity);
        maObj.transform.GetChild(0).transform.localScale = new Vector3(0.02f, 0.02f, 1);
    }

    void ShowDirection(Vector3 dir)
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, dir);
    }
   

    void ChangeFaceColor(float i)
    {

        face.color = new Color(1,i,i);
    }
    void StartMaFace()
    {
        if (startMaFace == true)
        {
            timermaface += Time.deltaTime;
            face.sprite = maFace;
            if (timermaface >= 0.5f)
            {
                face.sprite = normalFace;
                timermaface = 0.0f;
                startMaFace = false;
            }
        }
        
    }
   

    void CalculateGoalDistance()
    {
        distance = Vector3.Distance(transform.position, goal.transform.position);
        goalText.text = distance.ToString()+" km";
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            jumpCharge = 0;
            rb.drag = 0;
        }
        if (collision.gameObject.name == "End")
        {
            Instantiate(endPanel, transform.position, Quaternion.identity);
        }
    }
}

