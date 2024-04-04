using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Metodo pubblico chiamato dal bottone "Play" quando viene premuto
    public void StartGame()
    {
        // Carica la scena del gioco (assicurati di avere la scena del gioco nel tuo progetto Unity)
        SceneManager.LoadSceneAsync(1);
    }
}
