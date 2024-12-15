using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class quizManager : MonoBehaviour
{
  private int val = 0;
  [SerializeField] private quizUI quizUI;
  [SerializeField]
  private List<Question> questions;
  public AudioSource asource;
  private Question selectedQuestion;
  [SerializeField]
  private List<AudioClip> sonidosCorrectos;
  [SerializeField]
  private List<AudioClip> sonidosError;
  public Image termiando;
    // Start is called before the first frame update
    void Start()
    {
    SelectQuestion();
    }

    // Update is called once per frame
  void SelectQuestion()
  {
    //int val = Random.Range(0, questions.Count);
    if (val != questions.Count)
    {
      selectedQuestion = questions[val];
      quizUI.SetQuestion(selectedQuestion);
      val++;
    }
    else {
      termiando.transform.DOScale(new Vector2(1f,1f),0.4f).SetDelay(0.4f).SetEase(Ease.InElastic);
      Debug.Log("JuegoTerminado");
    }
    

  }

  public bool Answer(string answered)
  {
    bool correctAns = false;
    if (answered == selectedQuestion.correctAns)
    {
      asource.PlayOneShot(sonidosCorrectos[Random.Range(0,sonidosCorrectos.Count)]);
      correctAns = true;
      Invoke("SelectQuestion", 0.4f); 
    }
    else
    {
      asource.PlayOneShot(sonidosError[Random.Range(0, sonidosError.Count)]);
      correctAns = false;
    }
    return correctAns;
  }

  public void reiniciarJuego() {
    val = 0;
    SelectQuestion();
    termiando.transform.DOScale(new Vector2(0, 0), 0.1f).SetEase(Ease.OutElastic);
  }
}

[System.Serializable]
public class Question
{
  public string questionInfo;
  public QuestionType questionType;
  public Sprite questionImg;
  public string questionText;
  public AudioClip questionClip;
  public List<string> options;
  public string correctAns;
  

}

[System.Serializable]
public enum QuestionType
{
  TEXT,IMAGE,AUDIO
}
