using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoShop : MonoBehaviour
{
    public bool Shop=false;
    public GameObject CanvasTienda;
    public GameObject MenuPrincipal;
    
    void Start()
    {
        CanvasTienda.SetActive(false);
    }
    
    public void Resumir()
    {
        CanvasTienda.SetActive(false);
        Shop = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && MenuPrincipal.activeInHierarchy == false)
        {
            if (Shop == false)
            {
                CanvasTienda.SetActive(true);
                Shop = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } 
            else if (Shop == true)
            {
                Resumir();
            }
        }
        else if (Shop == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Resumir();
        } 
    }
}
