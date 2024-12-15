using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections.Generic;
using static SpeechRecognizerPlugin;

public class SpeechRecognizer : MonoBehaviour, ISpeechRecognizerPlugin
{
    [SerializeField] private Button startListeningBtn = null;
    [SerializeField] private Button stopListeningBtn = null;
    [SerializeField] private List<Sprite> Listamensajes;
  [SerializeField] private Image gifAudio;
  //prueba
    [SerializeField] private QuizDataSpeechScriptable questionDataScriptable;
    private GameStatus gameStatus = GameStatus.Playing;
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;
    private bool correctAnswer = true;
    private string answerWord,answerWord2,answerWord3;
    public List<AudioClip> sonidos;
    public AudioSource asource;
    public TMP_Text oracionpregunta, titulopregunta;
    [SerializeField] private List<Color32> coloresTexto;
  //fin prueba


  public Image imagenmensaje,imagenpregunta;
    public GameObject panelmensaje,panelterminado;
  //[SerializeField] private Toggle continuousListeningTgle = null;
    [SerializeField] private Text resultsTxt = null;
    [SerializeField] private Text errorsTxt = null;

    private SpeechRecognizerPlugin plugin = null;




  private void Start()
    {
        plugin = SpeechRecognizerPlugin.GetPlatformPluginVersion(this.gameObject.name);

        startListeningBtn.onClick.AddListener(StartListening);
        stopListeningBtn.onClick.AddListener(StopListening);
        //continuousListeningTgle.onValueChanged.AddListener(SetContinuousListening);
        plugin.SetLanguageForNextRecognition("en-US");
        plugin.SetMaxResultsForNextRecognition(3);
    //prueba
    SetQuestion();
    //finprueba
  }

    private void StartListening()
    {
      
    
    plugin.StartListening();
    gifAudio.transform.DOScale(new Vector2(1,1),0.1f).SetDelay(0.2f);
    gifAudio.transform.DOScale(new Vector2(0, 0), 0.1f).SetDelay(2.2f);
  }

    private void StopListening()
    {
        plugin.StopListening();
    }

    /*private void SetContinuousListening(bool isContinuous)
    {
        plugin.SetContinuousListening(isContinuous);
    }*/


    public void OnResult(string recognizedResult)
    {
    currentAnswerIndex++;   //increase currentAnswerIndex

    char[] delimiterChars = { '~' };
        string[] result = recognizedResult.Split(delimiterChars);

        resultsTxt.text = "";
        for (int i = 0; i < 1; i++)
        {
            resultsTxt.text = result[i];
      //resultsTxt.text += result[i] + '\n';
          string texto = resultsTxt.text;
          Debug.Log("contenido variable texto");
          Debug.Log(texto);
          if (texto != null)
          {
              //if (commands.ContainsKey(texto))

              //{
              //var response = commands[texto];

              //if (response != null)
              bool comparicion = texto.Equals(answerWord, StringComparison.OrdinalIgnoreCase);
              bool comparicion2 = texto.Equals(answerWord2, StringComparison.OrdinalIgnoreCase);
              bool comparicion3 = texto.Equals(answerWord3, StringComparison.OrdinalIgnoreCase);
              if (comparicion || comparicion2 || comparicion3)
                {
                  //response?.Invoke();
                  Debug.Log("respuesta correcta");
                  activarMnesaje(0);
                  asource.PlayOneShot(sonidos[0]);
          Invoke("SetQuestion", 0.5f); //go to next question
                                       /*gameStatus = GameStatus.Next; //set the game status
                                       currentQuestionIndex++;
                                       if (currentQuestionIndex < questionDataScriptable.questions.Count)
                                       {
                                         Invoke("SetQuestion", 0.5f); //go to next question
                                       }
                                       else
                                       {
                                         Debug.Log("Game Complete"); //else game is complete
                                         //Debug.Log("entra para abrir el mensaje: " + id);
                                         panelterminado.transform.DOScale(new Vector2(1, 1), 0.2f).SetDelay(1).SetEase(Ease.InElastic);
                                       }*/
        }
                else
                {
                  activarMnesaje(1);
                  asource.PlayOneShot(sonidos[1]);
        }
             /* }
              else {
                activarMnesaje(1);
              }*/
          
        

        
      
          }
      //Debug.Log(resultsTxt.text);
        }
    /*-----------*/
    
    /*_____________*/


    }

  public void OnError(string recognizedError)
    {
        ERROR error = (ERROR)int.Parse(recognizedError);
        switch (error)
        {
            case ERROR.UNKNOWN:
                Debug.Log("<b>ERROR: </b> Unknown");
                errorsTxt.text += "Unknown";
                break;
            case ERROR.INVALID_LANGUAGE_FORMAT:
                Debug.Log("<b>ERROR: </b> Language format is not valid");
                errorsTxt.text += "Language format is not valid";
                break;
            default:
                break;
        }
    }

  public void activarMnesaje(int id)
  {
    imagenmensaje.sprite = Listamensajes[id];
    Debug.Log("entra para abrir el mensaje: "+id);
    panelmensaje.transform.DOScale(new Vector2(1,1),0.2f).SetEase(Ease.InOutElastic);
    if (id != 2) {
      panelmensaje.transform.DOScale(Vector2.zero, 0.2f).SetDelay(0.6f);
    }
    
    
  }
  //prueba
 

  public void SetQuestion() {
    gameStatus = GameStatus.Next; //set the game status
    
    if (currentQuestionIndex < questionDataScriptable.questions.Count)
    {
      gameStatus = GameStatus.Playing;
            switch (questionDataScriptable.questions[currentQuestionIndex].tipo) {
                case QuestionType.IMAGE:
                    imagenpregunta.gameObject.SetActive(true);
                    oracionpregunta.gameObject.SetActive(false);
                    imagenpregunta.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;
                    break;
                case QuestionType.TEXT:
                    imagenpregunta.gameObject.SetActive(false);
                    oracionpregunta.gameObject.SetActive(true);
                    oracionpregunta.text = questionDataScriptable.questions[currentQuestionIndex].sentence.ToString();
                    oracionpregunta.color = coloresTexto[UnityEngine.Random.Range(0, coloresTexto.Count)];
                    break;

            }
      titulopregunta.text = questionDataScriptable.questions[currentQuestionIndex].title;
      answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
      answerWord2 = questionDataScriptable.questions[currentQuestionIndex].answeralternative1;
      answerWord3 = questionDataScriptable.questions[currentQuestionIndex].answeralternative2;
      
      currentQuestionIndex++;
    }
    else
    {
      Debug.Log("Game Complete"); //else game is complete
                                  //Debug.Log("entra para abrir el mensaje: " + id);
      panelterminado.transform.DOScale(new Vector2(1, 1), 0.2f).SetDelay(1).SetEase(Ease.InElastic);
    }
  }

  public void reiniciarJuego() {
    GameStatus gameStatus = GameStatus.Playing;
    currentAnswerIndex = 0; currentQuestionIndex = 0;
    correctAnswer = true;
    Invoke("SetQuestion", 0.1f);
    panelterminado.transform.DOScale(new Vector2(0, 0), 0.1f).SetEase(Ease.OutElastic);
  }

  
}
[System.Serializable]
public class QuestionDataSpeech
{
    public string title;
    public QuestionType tipo;
    public string sentence;
    public Sprite questionImage;
    public string answer;
    public string answeralternative1;
    public string answeralternative2;
}

