using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int damage, hideHealth, hideMaxHealth;
    public bool isAlive;
    public Animator anim;
    public CircleCollider2D hitBox;
    public GameObject PlayerUI;
    public int playerId = 1;
    public int coinNumber = 65;
    public int hasCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        
    }

    public void DamagePlayer(int damageGet)     //伤害量判定
    {
        
        hideHealth -= damageGet;
        

        


        if (hideHealth < 0)
            hideHealth = 0;

        if (hideHealth <= 0)
        {
            hitBox.enabled = false;
            isAlive = false;
            anim.SetTrigger("Die");
            // rb.velocity = new Vector2(0, -10);
            PlayerUI.GetComponent<GameOver>().Setup();
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            var getCheckPoint = other.gameObject.GetComponent<CheckPoint>();
            playerId = getCheckPoint.Id;
        }

        if(other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Watch"))
        {
            hasCoin += 1;
            if(hasCoin == coinNumber)
            {
                SceneManager.LoadScene(29);
            }
        }
    }
}
