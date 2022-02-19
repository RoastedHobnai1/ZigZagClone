using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public Transform rayStart;
    private Animator animator;
    private Rigidbody rb;
    private bool isWalkingRight;
    private GameManager gameManager;
    public GameObject crystalEffect;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void FixedUpdate()
    {
        if (!gameManager.isGameStarted)
        {
            return;
        }
        else
        {
            animator.SetTrigger("isGameStarted");
        }
        rb.transform.position = transform.position + transform.forward * Time.deltaTime * 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }
        RaycastHit hit;
        if(!(Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity)))
        {
            animator.SetTrigger("isFalling");
        }
        else
        {
            animator.SetTrigger("notFalling");
        }
        if(transform.position.y < -2)
        {
            gameManager.EndGame();
        }
    }
    private void Switch()
    {
        isWalkingRight = !isWalkingRight;
        if (isWalkingRight)
            transform.rotation = Quaternion.Euler(0, 45, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
            Destroy(other.gameObject);
            gameManager.IncreaseScore();
            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);
        }
        
    }
}
