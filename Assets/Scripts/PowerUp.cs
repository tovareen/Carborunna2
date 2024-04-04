using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.5f;
    public float duration = 5f;
    public GameObject pickupEffect;

   
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
     
        Debug.Log("PowBOOOOOMerUp");
        Destroy(gameObject);
    }
}
