using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] posicion;
    public float sensibilidad;
    public int puntuacion;
    
    public List<float[]> posicionesRecogibles;

    public PlayerData(Jugador jugador)
    {
        sensibilidad = Jugador.Sensibilidad;
        puntuacion = SumaPuntos.puntuacion; 
        
        posicion = new float[3];
        posicion[0] = jugador.transform.position.x;
        posicion[1] = jugador.transform.position.y;
        posicion[2] = jugador.transform.position.z;
        
        // Guardar las posiciones de los recogibles
        posicionesRecogibles = new List<float[]>();
        GameObject[] recogibles = GameObject.FindGameObjectsWithTag("recogible");
        foreach (GameObject recogible in recogibles)
        {
            float[] posicionRecogible = new float[3];
            posicionRecogible[0] = recogible.transform.position.x;
            posicionRecogible[1] = recogible.transform.position.y;
            posicionRecogible[2] = recogible.transform.position.z;
            posicionesRecogibles.Add(posicionRecogible);
        }
    }
}
