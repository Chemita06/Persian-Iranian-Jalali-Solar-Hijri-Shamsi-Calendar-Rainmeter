using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public Transform holdPos; // Posición donde se mantendrá el objeto
    public float throwForce = 2500f; // Fuerza de lanzamiento
    public float pickUpRange = 5f; // Rango de recogida
    public float rotationSensitivity = 1.6f; // Sensibilidad de rotación del objeto
    private GameObject heldObj; // Objeto que se está sosteniendo
    private Rigidbody heldObjRb; // Rigidbody del objeto que se está sosteniendo
    private bool canDrop; // Control para evitar soltar mientras se rota
    private int layerNumber; // Índice de la capa
    private bool isThrowing = false; // Bandera para controlar si se está lanzando un objeto
    

    // Referencia al script que controla el movimiento del jugador
    // Se usa para deshabilitar la capacidad de mirar alrededor mientras se rota el objeto
    Jugador mouseLookScript;

    void Start()
    {
        // Asigna el índice de la capa para los objetos sostenidos
        layerNumber = LayerMask.NameToLayer("holdLayer");

        // Obtén el script de movimiento del jugador
        mouseLookScript = player.GetComponent<Jugador>();
        

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // Cambia "E" si deseas otro botón para recoger
        {
            
            if (heldObj == null && !isThrowing) // Si no estás sosteniendo nada
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
                {
                    // Verifica si el objeto tiene la etiqueta "recogible"
                    if (hit.transform.CompareTag("recogible"))
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else if (heldObj != null) // Si hay un objeto sostenido
            {
                MoveObject(); // Mantén el objeto en la posición del "holdPos"
            }
        }
        else
        {
            // Reinicia la bandera cuando se suelta el botón izquierdo
            isThrowing = false;

            if (heldObj != null) // Si hay un objeto sostenido
            {
                DropObject(); // Suelta el objeto
            }
        }

        if (heldObj != null && Input.GetKey(KeyCode.Mouse1))
        {
            StopClipping();
            ThrowObject();
            isThrowing = true; // Activa la bandera para evitar recoger inmediatamente
        }
        if (heldObj != null)
        {
            RotateObject();
        }
        else
        {
            // Si no hay objeto sostenido, asegúrate de que el script de la cámara esté habilitado
            mouseLookScript.enabled = true;
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) // Asegúrate de que tenga un Rigidbody
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos; // Haz que el objeto sea hijo de "holdPos"
            heldObj.layer = layerNumber; // Cambia el objeto a la capa de objetos sostenidos
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        // Reactiva la colisión con el jugador
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // Cambia el objeto a la capa predeterminada
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; // Desvincula el objeto
        heldObj = null;
    }

    void MoveObject()
    {
        // Mantén la posición del objeto en "holdPos"
        heldObj.transform.position = holdPos.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R)) // Mantén presionado "R" para rotar
        {
            canDrop = false; // Evita que se suelte mientras se rota

            // Deshabilita la capacidad de mirar alrededor
            mouseLookScript.enabled = false;

            float xAxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            float yAxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;

            heldObj.transform.Rotate(Vector3.down, xAxisRotation);
            heldObj.transform.Rotate(Vector3.right, yAxisRotation);
        }
        else
        {
            // Reactiva la capacidad de mirar alrededor
            mouseLookScript.enabled = true;
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; // Vuelve a la capa predeterminada
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;

        // Ajusta la fuerza de lanzamiento según el peso
        float adjustedThrowForce = (throwForce * Mathf.Clamp(heldObjRb.mass, 1f, 100f)) / Mathf.Sqrt(Mathf.Clamp(heldObjRb.mass, 1f, 100f));
        heldObjRb.AddForce(transform.forward * adjustedThrowForce);

        heldObj = null;
    }


    void StopClipping()
    {
        float clipRange = Vector3.Distance(heldObj.transform.position, transform.position);

        // Realiza un RaycastAll para detectar colisiones
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, clipRange);

        if (hits.Length > 1) // Si hay más de un objeto detectado
        {
            // Ajusta la posición del objeto ligeramente hacia abajo
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
