using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class scenaController : MonoBehaviour
{
  // Start is called before the first frame
  private Animator transitionAnimator;
  public AudioSource asource;
  public AudioClip intro;
  [SerializeField] private float transitionTime = 1f;
    void Start()
    {
    transitionAnimator = GetComponentInChildren<Animator>();
    if (!variables.escena.Equals("principal")) { Invoke("activarIntro", 1.3f);}
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  public void CargarEscena(string escenaCarga)
  {
    if (!escenaCarga.Equals("principal"))
    {
      variables.escena = escenaCarga;
      Debug.Log(variables.escena);
    }
    string nombreBtn = EventSystem.current.currentSelectedGameObject.name;
    variables.marcador = nombreBtn;
    StartCoroutine(SceneLoad(escenaCarga));
  }

  public IEnumerator SceneLoad(string escena)
  {
    transitionAnimator.SetTrigger("animTransicion");
    yield return new WaitForSeconds(transitionTime);
    SceneManager.LoadScene(escena);
  }

  public void activarIntro() {
    asource.PlayOneShot(intro);
  }

}
