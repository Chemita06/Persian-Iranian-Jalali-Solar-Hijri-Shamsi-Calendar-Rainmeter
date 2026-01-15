using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SumaPuntos : MonoBehaviour
{
    public TMP_Text puntuacionText; // Referencia al texto de puntuación
    public static int puntuacion = 0; // Puntuación global
    private bool permiso = true; // Control de colisiones
    public int valor; // Valor que suma este objeto

    void Start()
    {
        ActualizarTextoPuntuacion(); // Asegurar que la puntuación se muestra correctamente
    }

    void OnCollisionEnter(Collision collision)
    {
        if (permiso && collision.gameObject.CompareTag("mundo"))
        {
            puntuacion += valor;
            ActualizarTextoPuntuacion(); // Actualiza el texto de la puntuación
            permiso = false;
            StartCoroutine(ResetPermiso()); 
        }
    }

    void ActualizarTextoPuntuacion()
    {
        if (puntuacionText != null)
        {
            puntuacionText.text = puntuacion.ToString();
        }
        else
        {
            Debug.LogError("puntuacionText no está asignado en el Inspector.");
        }
    }

    IEnumerator ResetPermiso()
    {
        yield return new WaitForSeconds(0.2f);
        permiso = true;
    }
}