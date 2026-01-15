using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    public Slider sliderSensibilidad;
    void Start()
    {
        sliderSensibilidad.value = Jugador.Sensibilidad; // Iniciar el slider con la sensibilidad actual
        sliderSensibilidad.onValueChanged.AddListener(CambiarSensibilidad);
    }
    public void CambiarSensibilidad(float nuevaSensibilidad)
    {
        Jugador.SetSensibilidad(nuevaSensibilidad); // Actualizar la sensibilidad
    }
    [SerializeField] public AudioMixer audiomixer;
    public Jugador jugador;
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        audiomixer.SetFloat("Volumen", volumen);
    }

    public void Sensibilidad(float sensibilidad)
    {
        Jugador.SetSensibilidad(sensibilidad); // Usa el método estático para cambiar la sensibilidad
    }
}
