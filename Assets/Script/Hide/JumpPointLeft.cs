using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointLeft : MonoBehaviour
{
    
    private Rabbit rabbit;

    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        
        rabbit = GetComponent<Rabbit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
