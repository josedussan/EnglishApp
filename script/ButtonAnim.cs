using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnim : MonoBehaviour
{
  Button btn;
  public AudioSource asource;
  public AudioClip sonido;
  private  float x, y, z;

  void Start()
  {
    btn = gameObject.GetComponent<Button>();
    btn.onClick.AddListener(Anim);
        x = btn.transform.localScale.x;
        y = btn.transform.localScale.y;
        z = btn.transform.localScale.z;
  }

  void Anim()
  {
    asource.PlayOneShot(sonido);
    btn.transform.DOScale(new Vector3(x+0.5f,y+0.5f,z+0.5f), 0.1f);
    btn.transform.DOScale(new Vector3(x, y, z), 0.2f);
  }
}
