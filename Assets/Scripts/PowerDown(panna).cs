using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDown : MonoBehaviour
{
    public float multiplier = 0f;
    public float duration = 5f;
    public GameObject pickupEffect;

    // Start is called before the first frame update
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
          StartCoroutine (  Pickup(other));
        }
    }
    IEnumerator Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
           Debug.Log("PowerUp");

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.jumpForce *= multiplier;

        yield return new WaitForSeconds (duration);
        playerMovement.jumpForce /= multiplier;
     
        Destroy(gameObject);
    }
}
