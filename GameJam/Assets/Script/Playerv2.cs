using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerv2 : MonoBehaviour
{
    public float distance;
    public Vector3 originPos;
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

    float timerBfrJump;
    float angleHead;
    GameObject direction;

    bool isHoldingForLong;
    int jumpCharge;


    void Start()
    {
        originPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        mouth = gameObject.transform.GetChild(0).transform.gameObject;

        lr = mouth.GetComponent<LineRenderer>();
        ma = Resources.Load<GameObject>("Prefabs/ma");
        ma2 = Resources.Load<GameObject>("Prefabs/ma2");
        power = 50.0f;
        goal = GameObject.Find("Start");
        goalText = GameObject.Find("PlayerUi").
            transform.GetChild(0).gameObject.GetComponent<Text>();
        face = GetComponent<SpriteRenderer>();

        normalFace = Resources.Load<Sprite>("Sprites/1");
        chargeFace = Resources.Load<Sprite>("Sprites/2");
        maFace = Resources.Load<Sprite>("Sprites/3");

        face.sprite = normalFace;
        i = 1;

        direction = transform.GetChild(1).transform.gameObject;
    }


    void Update()
    {
        //if (transform.rotation.eulerAngles.z>=0.0f){

        //}
        if (rb.velocity.magnitude >= 20/* && transform.position.y <= -1.0f*/)
        {
            rb.velocity = rb.velocity * 0.5f;

            //face.color = Color.blue;
        }
        Debug.Log("power " + power);
        if (Input.GetKeyDown("space"))
        {
            if (jumpCharge < 2)
            {
                Debug.Log("space");
                clicked = true;
                jumpCharge++;
            }
            else
            {
                clicked = false;
            }
        }
        else if (Input.GetKeyUp("space"))
        {
            rb.drag = 0;
            if (jumpCharge < 2)
            {
                if (isHoldingForLong != true)
                {
                    rb.AddForce(direction.transform.position * power);
                    
                    GameObject maObj = Instantiate(ma, mouth.transform.position, Quaternion.identity);
                    maObj.transform.GetChild(0).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                }
            }
                i = 1;
                clicked = false;
                startMaFace = true;
                isHoldingForLong = false;
                power = 50.0f;
                angleHead = 0.0f;
        }

        Debug.Log(clicked);
        if (clicked == true)
        {
            var rotationVector = transform.rotation.eulerAngles;
            if (rotationVector.z>= 80.0f/*&&power<=2500*/)
            {
                angleHead += 0.3f;
                power += 5f;
                rotationVector.z += angleHead;
                transform.rotation = Quaternion.Euler(rotationVector);
                //Debug.Log(rotationVector.z);
                timerBfrJump = 0.0f;
            }

            face.sprite = chargeFace;
            clickeTimer += Time.deltaTime;
            i -= 0.01f;
            ChangeFaceColor(i);
            if (clickeTimer >= 1.5f)
            {
                i = 1;
                startMaFace = true;
                power = 500;
                isHoldingForLong = true;
                
                rb.AddForce(direction.transform.position * power);
                GameObject maObj2 = Instantiate(ma2, mouth.transform.position, Quaternion.identity);
                maObj2.transform.GetChild(0).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                maObj2.transform.GetChild(1).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                maObj2.transform.GetChild(2).transform.localScale = new Vector3(0.02f, 0.02f, 1);
                clickeTimer = 0.0f;
                angleHead = 0.0f;
            }
        }
        else
        {
            clickeTimer = 0.0f;
            power = 50;
        }
        CalculateGoalDistance();
        StartMaFace();

    }

    void ChangeFaceColor(float i)
    {

        face.color = new Color(1, i, i);
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
        goalText.text = distance.ToString() + " km";
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            jumpCharge = 0;
            rb.drag = 0;
        }
    }
}