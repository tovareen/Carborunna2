using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.name != "RIGATONE")
        {
            return;
        }

        // Chiamata al metodo IncrementScore passando il numero di punti delle monete
        GameManager.inst.IncrementScore(GameManager.inst.coinPoints);

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
