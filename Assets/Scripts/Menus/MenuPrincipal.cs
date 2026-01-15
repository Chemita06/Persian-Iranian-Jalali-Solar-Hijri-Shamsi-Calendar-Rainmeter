using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject basura;
    
    public void Start()
    {
        if (SumaPuntos.puntuacion != 0)
        {
            basura.SetActive(true);
        }
        else
        {
            basura.SetActive(false);  
        }
    }

    public void EmpezarJuego(string PrincipalScene)
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento
        SceneManager.LoadScene(PrincipalScene);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento
        Jugador.LoadPlayer(); // Ahora sí, cargar el progreso del jugador
    }
    public void SalirJuego()
    {
        Application.Quit();
        print("Saliendo del juego...");
    }


    
}
