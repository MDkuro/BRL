using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{

    public float attackTime = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            CoinUI.CurrentCoinQuantity += 1;
            CoinUI.CurrentPointQuantity += 100;
            Destroy(gameObject);
        }
    }


    
}
