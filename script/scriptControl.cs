using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class scriptControl : MonoBehaviour
{
  public RectTransform panelMain;
    // Start is called before the first frame update
    void Start()
    {
      panelMain.DOAnchorPos(Vector2.zero,0.01f);
      //GameObject.Find("contenidoPrincipal").GetComponent<RectTransform>().LeanSetPosY(-284);
        if (variables.marcador=="atras") {
            if (variables.escena == "vowels" || variables.escena == "alphabet" || variables.escena == "letterSounds")
            {
                mostrarPanel(GameObject.Find("panelUnidad1").GetComponent<RectTransform>());
            }
            else if (variables.escena == "numbers" || variables.escena == "ordinalNumbers")
            {
                mostrarPanel(GameObject.Find("panelUnidad2").GetComponent<RectTransform>());
            }
            else if (variables.escena == "pronouns" || variables.escena == "verbtobe" || variables.escena == "presentePasadoFuturo")
            {
                mostrarPanel(GameObject.Find("panelUnidad3").GetComponent<RectTransform>());
            }
            else if (variables.escena == "simplePresent" || variables.escena == "simplePast" || variables.escena == "dailyroutine" || variables.escena == "simpleFuture" || variables.escena == "InmediateFuture" || variables.escena == "goandhow")
            {
                mostrarPanel(GameObject.Find("panelUnidad4").GetComponent<RectTransform>());
            }
            else if (variables.escena == "presentContinous" || variables.escena == "pastContinuous" || variables.escena == "futureContinuous" )
            {
                mostrarPanel(GameObject.Find("panelUnidad5").GetComponent<RectTransform>());
            }
            else if (variables.escena == "presentPerfect" || variables.escena == "pastPerfect" || variables.escena == "futurePerfect")
            {
                mostrarPanel(GameObject.Find("panelUnidad6").GetComponent<RectTransform>());
            }
            else if (variables.escena == "presentperfectcontinuous" || variables.escena == "pastperfectcontinuous" || variables.escena == "futureperfectcontinuous")
            {
                mostrarPanel(GameObject.Find("panelUnidad7").GetComponent<RectTransform>());
            }
            else if (variables.escena == "whquestions" || variables.escena == "some" || variables.escena == "dailyphrases" || variables.escena == "mostusedwords")
            {
                mostrarPanel(GameObject.Find("panelUnidad8").GetComponent<RectTransform>());
            }

        }
    }

    // Update is called once per frame
    public void mostrarPanel(RectTransform panel)
    {
      panelMain.DOAnchorPos(new Vector2(-900,0), 0.25f).SetDelay(0.2f);
      panel.DOAnchorPos(Vector2.zero, 0.25f).SetDelay(0.2f); 
    }
  public void cerrarPanel(RectTransform panel)
  {
    panelMain.DOAnchorPos(Vector2.zero,0.25f).SetDelay(0.2f); ;
    panel.DOAnchorPos(new Vector2(900, 0), 0.25f).SetDelay(0.2f); 
  }

  public void CerrarApp() {
    Application.Quit();
  }

  public void vistPrevia(RectTransform vistprevia) {

    vistprevia.DOScale(new Vector2(1,1),0.25f);
    vistprevia.DOScale(Vector2.zero, 0.25f).SetDelay(1.5f);
  }

}
