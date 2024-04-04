using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;

    [SerializeField] Text scoreText;

    [SerializeField] PlayerMovement playerMovement;

    // Intervallo di tempo tra gli incrementi del punteggio (in secondi)
    [SerializeField] float scoreIncrementInterval = 0.2f;

    // Punti guadagnati dalle monete
    public int coinPoints = 10;

    // Punti guadagnati dal tempo
    [SerializeField] int timePoints = 1;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Avvia la coroutine per incrementare il punteggio ad intervalli regolari
        StartCoroutine(IncreaseScoreOverTime());
    }

    // Coroutine per incrementare il punteggio ad intervalli regolari
    IEnumerator IncreaseScoreOverTime()
    {
        // Loop infinito che incrementa il punteggio ogni "scoreIncrementInterval" secondi
        while (true)
        {
            yield return new WaitForSeconds(scoreIncrementInterval);
            Debug.Log("Aumento");
            IncrementScore(timePoints); // Incrementa il punteggio del tempo
        }
    }

    public void IncrementScoreFromCoin()
    {
        IncrementScore(coinPoints); // Incrementa il punteggio del numero di punti delle monete
    }

    // Metodo per incrementare il punteggio
    public void IncrementScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score;
        // Aumenta velocità giocatore
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }
}
