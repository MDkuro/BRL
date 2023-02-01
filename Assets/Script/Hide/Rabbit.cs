using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rabbit : MonoBehaviour
{

    public int health;
    public int damage;

    public int points = 200;
    public float jumpForce = 20f;
    public bool canAirControl = false;
    public LayerMask groundMask;
    public Transform m_GroundCheck;

    const float k_GroundedRadius = .34f;   // 地面にチェックするための丸
    private bool m_Grounded;            // 今地面にいるかどうかの確認
    private bool m_FacingRight = false;  // プレイヤーの顔が左に向いているか
    private Vector3 m_Velocity = Vector3.zero;

    const float m_NextGroundCheckLag = 0.5f;  // ジャンプしたあと、少しの間がジャンプできないため
    float m_NextGroundCheckTime;             // この時間を過ごしたらジャンプできる

    private Rigidbody2D rigidbody;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnAirEvent;


    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnAirEvent == null)
            OnAirEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // 地面とのタッチのチェック
        if (Time.time > m_NextGroundCheckTime)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }

        if (wasGrounded && !m_Grounded)
        {
            OnAirEvent.Invoke();
        }
    }

    public void Move(float move, bool jump)
    {
        // プレイヤーが地面にいる時、もしくは空でコントロールする時だけ動ける
        if (m_Grounded || canAirControl)
        {
            // moveより移動スピードを決める
            rigidbody.velocity = new Vector2(move, rigidbody.velocity.y);

            if (move < 0 && m_FacingRight)
            {
                Flip();
            }
            else if (move > 0 && !m_FacingRight)
            {
                Flip();
            }

        }

        // 地面にいる時ジャンプがtrueになったらジャンプできる
        if (m_Grounded && jump)
        {
            OnAirEvent.Invoke();
            m_Grounded = false;
            rigidbody.AddForce(new Vector2(0.5f, jumpForce), ForceMode2D.Impulse);
            m_NextGroundCheckTime = Time.time + m_NextGroundCheckLag;
        }

        
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        // 移動方向を左右反転する
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }




}
