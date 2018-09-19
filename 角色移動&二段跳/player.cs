using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player : MonoBehaviour {

    public int jumpnt=0;
    [Header("二段跳")]
    public bool item = false;

    //GameObject TwoJump;
    Rigidbody2D playerRigidbody2D;

    [Header("目前水平速度")]
    public float speedX;

    [Header("目前的水平方向")]
    public float horizontalDirection;

    const string HORIZONTAL = "Horizontal";

    [Header("水平推力")]
    [Range(0,300)]
    public float xForce;

    float speedY;

    [Header("最大水平速度")]
    public float maxspeedX;

    /// <summary>
    /// 水平最大速度控制
    /// </summary>
    public void ControlSpeed()
    {
        speedX = playerRigidbody2D.velocity.x;
        speedY = playerRigidbody2D.velocity.y;
        float newspeedX = Mathf.Clamp(speedX, -maxspeedX, maxspeedX);
        playerRigidbody2D.velocity = new Vector2(newspeedX, speedY);
    }

    [Header("垂直向上推力")]
    public float yForce;

    public bool jumpitem
    {
        get
        {
            if (item)return true;
            else return false;
                    
        }
    }
    public bool jumpsky
    {
        get
        {
            jumpnt += 1;
            if (jumpnt < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool JumpKey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    void TryJump()
    {
        if (!jumpitem)
        {
            if (IsGround && JumpKey)
            {
                playerRigidbody2D.velocity = new Vector2(speedX, 0);
                playerRigidbody2D.AddForce(Vector2.up * yForce);
            }
        }
        else if (JumpKey)
        {
            if (jumpsky)
            {
                playerRigidbody2D.velocity = new Vector2(speedX, 0);
                playerRigidbody2D.AddForce(Vector2.up * yForce);
                if (IsGround) jumpnt = 0;
            }
            else if (IsGround) jumpnt = 0;
        }
    }

    [Header("感應地板的距離")]
    [Range(0,0.5f)]
    public float distance;

    [Header("偵測地板的射線起點")]
    public Transform groundCheck;

    [Header("地面圖層")]
    public LayerMask groundLayer;

    public bool grounded;

    bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end,groundLayer);
            return grounded;
        }
    }

    private void OnTriggerEnter2D(Collider2D itemm)
    {
        if(itemm.CompareTag("2jump"))
        {
            item = true;
            itemm.gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start ()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        //TwoJump = GameObject.FindGameObjectWithTag("2jump");
	}
    /// <summary>
    /// 水平移動
    /// </summary>
    void Movementx()
    {
        horizontalDirection = Input.GetAxis(HORIZONTAL);
        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
    }

	
	// Update is called once per frame
	void Update ()
    {
        Movementx();
        ControlSpeed();
        TryJump();
	}
}
