using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    private int count;
    public GameObject winTextObject;
    public GameObject PickUpParent;
    public GameObject collisionSound;
    public GameObject explosionFX;
    public GameObject pickupFX;
    //public GameObject winFX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= PickUpParent.transform.childCount)
        {
            winTextObject.SetActive(true);
            winTextObject.GetComponent<AudioSource>().Play();
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));

            //Instantiate(winFX, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            var currentPickupFX = Instantiate(pickupFX, other.transform.position, Quaternion.identity);
            Destroy(currentPickupFX, 3);
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Play game over sound
            collision.gameObject.GetComponent<AudioSource>().Play();
            // VFX
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        if (collision.gameObject.CompareTag("Wall")) {
            Debug.Log("wall collision");
            collisionSound.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
