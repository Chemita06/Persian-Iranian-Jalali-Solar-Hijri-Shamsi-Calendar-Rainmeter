using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;


public class Jugador : MonoBehaviour
{
    public float speed;
    public float velocidad;
    private new Transform camera;
    public static float Sensibilidad { get; private set; } = 1f;

    void Start()
    {
        camera = transform.Find("Camara");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LoadPlayer();
    }

    void Update()
    {
        // Movimiento del jugador
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.back * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * Time.deltaTime * speed);

        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        // Movimiento de la cámara
        if (Cursor.visible == true) return;
        if (hor != 0) transform.Rotate(Vector3.up * hor * Sensibilidad);
        if (ver != 0)
        {
            float angle = (camera.localEulerAngles.x - ver * Sensibilidad + 360) % 360;
            if (angle > 180) angle -= 360;
            angle = Mathf.Clamp(angle, -89, 89);
            camera.localEulerAngles = Vector3.right * angle;
        }
        

        // Sprint
        speed = Input.GetKey(KeyCode.LeftShift) ? velocidad + 2 : velocidad;

    }
    public static void SetSensibilidad(float sensibilidad)
    {
        Sensibilidad = sensibilidad;
    }

    public void SavePlayer()
    {
            SaveSystem.SaveGame(this);
    }

    public static void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadGame();
        if (data == null)
        {
            Debug.LogError("No se encontró ningún archivo de guardado.");
            return;
        }

        // Resto del código para cargar los datos
        Sensibilidad = data.sensibilidad; 
        SumaPuntos.puntuacion = data.puntuacion;

        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            Vector3 posicion;
            posicion.x = data.posicion[0];
            posicion.y = data.posicion[1];
            posicion.z = data.posicion[2];
            jugador.transform.position = posicion;
        }
        else
        {
            Debug.LogError("No se encontró un jugador en la escena.");
        }
        
    }



    
}
