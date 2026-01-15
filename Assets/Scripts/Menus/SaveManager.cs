using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    public GameObject TextoConfirmacion;
    public GameObject CanvasMenu;
    public GameObject Fondo;
    
    //Este codigo sirve para aplicarlo a un GameObject y así poder borrar el progreso en una escena donde no está el jugador
    
    public void ReiniciarProgreso()
    {
        SaveSystem.DeleteSaveGame();
    }
    
    public void EliminandoDatos()
    {
        Invoke("EliminandoLosDatos", 2f);
        
    }

    public void EliminandoLosDatos()
    {
        TextoConfirmacion.SetActive(false);
        CanvasMenu.SetActive(true);
        Fondo.SetActive(false);
        
        
    }
}

