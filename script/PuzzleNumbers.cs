using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PuzzleNumbers : MonoBehaviour
{
    public List<GameObject> opciones;
    public List<GameObject> cajas;
    private Vector3[] initialPos= new Vector3[10];
    public List<diccionario> dicci;
    public AudioClip incorrecto;
  public AudioSource aSource;
  public List<AudioClip> audios;
    public GameObject panelTerminado;
    private int contador=0;

  // Start is called before the first frame update
  void Start()
    {

    }
  public void Drag(int num)
  {
        opciones[num].transform.position = Input.mousePosition;
  }


  public void Drop(int num)
  {
        string objectText = opciones[num].GetComponentInChildren<Text>().text;
        int caja = obtenerCaja(objectText);
        float distance = Vector3.Distance(opciones[num].transform.position, cajas[caja].transform.position);
    if (distance < 100)
    {
      Debug.Log("cerca");

      opciones[num].transform.position = cajas[caja].transform.position;
      aSource.PlayOneShot(audios[0]);
      opciones[num].transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            contador += 1;

    }
    else
    {
      Debug.Log("lejos");
      opciones[num].transform.position = initialPos[num];
      aSource.PlayOneShot(incorrecto);
    }
  }
    public void posiciones() {
        Invoke("obtenerPosiciones",0.5f);
    }
    public void obtenerPosiciones() {
       for (int i = 0; i < opciones.Count; i++)
        {
            initialPos[i] = opciones[i].transform.position;
            Debug.Log("posicion inicial del " + i + " es:" + initialPos[i]);
        }

    }

  public void reiniciar()
  {
        panelTerminado.SetActive(false);
        for (int i = 0; i < opciones.Count; i++)
        {
            opciones[i].transform.position= initialPos[i];
            opciones[i].transform.DOScale(new Vector3(1, 1, 1), 0.3f);

        }
  }

    public int obtenerCaja(string texto) {
        int numero=3;
        for (int i=0;i<dicci.Count;i++) {
            if (dicci[i].texto.Equals(texto)) {
                 numero=dicci[i].numCaja;
            }
        }
        return numero;
    }
  // Update is called once per frame
  void FixedUpdate()
    {
        if (contador==opciones.Count) {
            panelTerminado.SetActive(true);
            contador = 0;
        }
    }
}
[System.Serializable]
public class diccionario{
    public string texto;
    public int numCaja;
    }
