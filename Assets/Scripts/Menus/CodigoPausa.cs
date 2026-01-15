using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodigoPausa : MonoBehaviour
{
    public bool Pausa=false;
    public GameObject ObjetoMenuPausa;
    public GameObject Menu_Opciones;
    public GameObject IconoGuardado;
    public GameObject ResumirJuego;
    public GameObject MenuTienda;
    
    // Start is called before the first frame update
    void Start()
    {
        ObjetoMenuPausa.SetActive(false);
        ResumirJuego.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MenuTienda.activeSelf == false)
        {
            if (Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Menu_Opciones.SetActive(false);
                Pausa = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } 
            else if (Pausa == true)
            {
                Resumir();
            }
        }
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenu()
    {
        Invoke("BackToMenu2", 1f);
        
    }
    public void BackToMenu2()
    {
        SceneManager.LoadScene("menu");
        IconoGuardado.SetActive(false);
    }
    public void BackToMenu3()
    {
        Invoke("BackToMenu4", 1f);
    }
    public void BackToMenu4()
    {
        IconoGuardado.SetActive(false);
    }
    
}
