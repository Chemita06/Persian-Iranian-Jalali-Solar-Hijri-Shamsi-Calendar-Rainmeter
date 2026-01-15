using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManagerPuntos : MonoBehaviour
{
    public int[,] shopItems = new int[3, 6]; // Matriz de tienda
    public GameObject[] prefabsComprables; // Prefabs a instanciar tras la compra
    public Transform spawnPoint; // Punto donde aparecerán los objetos comprados
    public Sprite[] spritesComprados; // Nuevos sprites a mostrar
    public Image[] botonesTienda;     // Referencia a los botones cuyas imágenes cambiarán

    void Start()
    {
        // ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;

        // Precio de los objetos
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 100;
        shopItems[2, 4] = 500;
        shopItems[2, 5] = 1000;
    }

    public void BuyItem()
    {
        GameObject ButtonRef = EventSystem.current.currentSelectedGameObject;
        if (ButtonRef == null)
        {
            Debug.LogError("Error: No se encontró el botón seleccionado.");
            return;
        }

        ButtonInfo buttonInfo = ButtonRef.GetComponent<ButtonInfo>();
        if (buttonInfo == null)
        {
            Debug.LogError("Error: El botón no tiene un componente ButtonInfo.");
            return;
        }

        int itemID = buttonInfo.ItemID;
        if (SumaPuntos.puntuacion >= shopItems[2, itemID])
        {
            SumaPuntos.puntuacion -= shopItems[2, itemID];

            // Instanciar un nuevo objeto si existe en la lista de prefabs
            if (itemID - 1 < prefabsComprables.Length && prefabsComprables[itemID - 1] != null)
            {
                Vector3 posicionSpawn = spawnPoint != null ? spawnPoint.position : Vector3.zero;
                GameObject nuevoObjeto = Instantiate(prefabsComprables[itemID - 1], posicionSpawn, Quaternion.identity);
                Debug.Log("Objeto " + itemID + " comprado e instanciado.");
                if (spritesComprados[itemID - 1] != null && botonesTienda[itemID - 1] != null)
                {
                    botonesTienda[itemID - 1].sprite = spritesComprados[itemID - 1];
                    Debug.Log("Sprite cambiado en el botón del objeto " + itemID);
                }
            }
            else
            {
                Debug.LogWarning("No hay un prefab asignado para este ID.");
            }
        }
        else
        {
            Debug.LogWarning("No tienes suficientes puntos para comprar este objeto.");
        }
        
    }
}
