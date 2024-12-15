using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class quizUI : MonoBehaviour
{
  public quizManager quizManager;
  //public Text questionText;
  public TMP_Text questionText,questionTitle;
  public Image questionImage;
  public Image expresions;
  public List<Sprite> expresionsCorrect;
  public List<Sprite> expresionsIncorrect;
  public AudioSource questionAudio;
  public List<Button> options;
  public Sprite correct, incorrect, selected;
  private Question question;
  private bool answered;
  private float audioLenght;
    // Start is called before the first frame update
    void Awake()
    {
      for (int i=0;i<options.Count;i++)
      {
      Button localBtn = options[i];
      localBtn.onClick.AddListener(() => OnClick(localBtn));
      }
    }

  public void SetQuestion(Question question)
  {
    this.question = question;
    switch (question.questionType)
    {
      case QuestionType.TEXT:
        imageHolder();
        questionText.transform.gameObject.SetActive(true);
        questionText.text = question.questionText;
        break;
      case QuestionType.IMAGE:
        imageHolder();
        questionImage.transform.gameObject.SetActive(true);
        questionImage.sprite = question.questionImg;
        break;
      case QuestionType.AUDIO:
        imageHolder();
        questionAudio.transform.gameObject.SetActive(true);
        audioLenght = question.questionClip.length;
        //StartCoroutine(PlayAudio());
        break;
    }

    questionTitle.text = question.questionInfo;

    List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);
    
    for (int i = 0; i < options.Count ;i++) {
      options[i].GetComponentInChildren<Text>().text = answerList[i];
      options[i].name = answerList[i];
      options[i].image.sprite = selected;
    }

    answered = false;
  }

  public void imageHolder() {
    questionImage.transform.parent.gameObject.SetActive(true);
    questionImage.transform.gameObject.SetActive(false);
    questionText.transform.gameObject.SetActive(false);
    questionAudio.transform.gameObject.SetActive(false);
  }
  public void activarAudio()
  {
    if (question.questionType == QuestionType.AUDIO)
    {
      questionAudio.PlayOneShot(question.questionClip);
      
    }
  }
  IEnumerator PlayAudio() {
    if (question.questionType == QuestionType.AUDIO)
    {
      questionAudio.PlayOneShot(question.questionClip);
      yield return new WaitForSeconds(audioLenght + 0.9f);
      StartCoroutine(PlayAudio());
    }
    else {
      StopCoroutine(PlayAudio());
      yield return null;
    }
  }

  private void OnClick(Button button)
  {
    if (!answered)
    {
      answered = true;
      bool val = quizManager.Answer(button.name);
      if (val)
      {
        button.image.sprite = correct;
        expresions.sprite = expresionsCorrect[Random.Range(0, expresionsCorrect.Count)];
        expresions.transform.DOScale(new Vector2(1,1),0.2f).SetEase(Ease.InOutElastic);
        expresions.transform.DOScale(Vector2.zero, 0.2f).SetDelay(0.6f);
      }
      else
      {
        answered = false;
        button.image.sprite = incorrect;
        expresions.sprite = expresionsIncorrect[Random.Range(0, expresionsIncorrect.Count)];
        expresions.transform.DOScale(new Vector2(1, 1), 0.2f).SetEase(Ease.InOutBounce);
        expresions.transform.DOScale(Vector2.zero, 0.2f).SetDelay(0.6f);
        Invoke("CambiarColor", 0.6f);
      }
      
    }
  }

  public void CambiarColor()
  {
    for (int i = 0; i < options.Count; i++)
    {
      options[i].image.sprite = selected;
    }
  }
}
