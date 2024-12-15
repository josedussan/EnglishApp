using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;

public static class Extensions
{
    public static string[] Split(this string str, string separator)
    {
        return Regex.Split(str, separator);
    }
}
public class QuizManagerOraciones : MonoBehaviour
{
    public static QuizManagerOraciones instance; //Instance to make is available in other scripts without reference


    [SerializeField] private GameObject gameComplete, correctMessage;


    //Scriptable data which store our questions data Datos programables que almacenan los datos de nuestras preguntas.
    [SerializeField] private OracionDataScriptable questionDataScriptable;
    [SerializeField] private Image correctImage;           //image element to show the image elemento de imagen para mostrar la imagen
    [SerializeField] private WordDataOracion[] answerWordList;     //list of answers word in the game lista de preguntas
    [SerializeField] private WordDataOracion[] optionsWordList;    //list of options word in the game lista de opciones
    public List<Sprite> correctImages,errorImages;
    [SerializeField] private AudioSource asource;
    [SerializeField] private AudioClip soundcorrect,sounderror;


    private GameStatusOracion gameStatus = GameStatusOracion.Playing;     //to keep track of game status para realizar un seguimiento del estado del juego
    private string[] wordsArray,palabra;               //array which store char of each options

    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
                                                                    //índice para realizar un seguimiento de la respuesta actual y la pregunta actual
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not bool para decidir si la respuesta es correcta o no
    private string answerWord;                              //string to store answer of current question cadena para almacenar la respuesta de la pregunta actual
    private int tamanoOracion;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedWordsIndex = new List<int>();           //create a new list at start
        Debug.Log("entra al strat");
        SetQuestion();                                  //set question
    }


    void SetQuestion()
    {
        Debug.Log("entra al setquestion");
        gameStatus = GameStatusOracion.Playing;                //set GameStatus to playing 

        //set the answerWord string variable establecer la variable de cadena respuestaPalabra
        answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
        wordsArray = new string[questionDataScriptable.questions[currentQuestionIndex].cantidadPalabras];

        ResetQuestion();                               //reset the answers and options value to orignal     

        selectedWordsIndex.Clear();                     //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array


        //add the correct char to the wordsArray
        wordsArray = answerWord.Split(" ");
        palabra = wordsArray;
        for (int i = 0; i < wordsArray.Length; i++)
        {
            Debug.Log( wordsArray[i]);
            tamanoOracion =+ 1;
        }
       
       /*for (int i = 0; i < answerWord.Length; i++)
       {
           wordsArray[i] = string.ToUpper(answerWord[i]);
           wordsArray[i] = answerWord.Split(" ");
       }*/

       //add the dummy char to wordsArray
       /*for (int j = answerWord.Length; j < wordsArray.Length; j++)
       {
           wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
       }*/

       wordsArray = ShuffleList.ShuffleListItems<string>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array
        Debug.Log("pasa el revolver");
        Debug.Log(optionsWordList.Length);
        //set the options words Text value
        for (int k = 0; k < wordsArray.Length; k++)
        {
            
            optionsWordList[k].SetWord(wordsArray[k]);
        }

    }

    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
        
        Debug.Log("entra al reset");
        //activate all the answerWordList gameobject and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++)
        {
            Debug.Log("entra a restablecer las casillas");
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetWord("______");
        }
        Debug.Log("answerwor-" + wordsArray.Length + " answerwordlist-" + answerWordList.Length);

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = wordsArray.Length; i < answerWordList.Length; i++)
        {
            Debug.Log("desactivar casillas");
            answerWordList[i].gameObject.SetActive(false);
        }
        Debug.Log("pasa de desactivar las casillas");
        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
        }
        for (int i = wordsArray.Length; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(false);
        }
        //tamanoOracion = 0;
        currentAnswerIndex = 0;
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(WordDataOracion value)
    {
        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length 
        //si el estado del juego es el siguiente o el índice de respuesta actual es mayor o igual que la longitud de la palabra de respuesta
        Debug.Log("currentAnswer: "+currentAnswerIndex+" answordword: "+wordsArray.Length);
        if (gameStatus == GameStatusOracion.Next || currentAnswerIndex >= wordsArray.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == wordsArray.Length)
        {

            correctAnswer = true;   //default value
            Debug.Log("entra a comparar si los tamaños son iguales"+wordsArray.Length);                       //loop through answerWordList
            for (int i = 0; i < wordsArray.Length; i++)
            {
                Debug.Log("answord: _" + palabra[i] + "_wordvalue: " + answerWordList[i].wordValue+"_");
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (!palabra[i].Equals(answerWordList[i].wordValue))
                {
                    Debug.Log("no es igual");
                    correctAnswer = false; //set it false
                    break; //and break from the loop
                }
            }

            //if correctAnswer is true
            if (correctAnswer)
            {
                Debug.Log("Correct Answer");//aqui
                correctImage.sprite = correctImages[UnityEngine.Random.Range(0, correctImages.Count)];
                correctMessage.transform.DOScale(new Vector2(1, 1), 0.2f);
                correctMessage.transform.DOScale(Vector2.zero, 0.2f).SetDelay(0.6f).SetEase(Ease.InOutElastic);
                asource.PlayOneShot(soundcorrect);
                gameStatus = GameStatusOracion.Next; //set the game status
                currentQuestionIndex++; //increase currentQuestionIndex

                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    Invoke("SetQuestion", 0.5f); //go to next question
                }
                else
                {
                    Debug.Log("Game Complete"); //else game is complete
                    gameComplete.SetActive(true);
                }
            }
            else {
                correctImage.sprite = errorImages[UnityEngine.Random.Range(0, errorImages.Count)];
                correctMessage.transform.DOScale(new Vector2(1, 1), 0.2f);
                correctMessage.transform.DOScale(Vector2.zero, 0.2f).SetDelay(0.6f).SetEase(Ease.InOutElastic);
                asource.PlayOneShot(sounderror);
                ResetQuestion();
            }
        }
    }

    public void ResetLastWord()
    {
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

            currentAnswerIndex--;
            answerWordList[currentAnswerIndex].SetWord("______");
        }
    }

    public void reiniciarJuego()
    {
        currentAnswerIndex = 0; currentQuestionIndex = 0;
        correctAnswer = true;
        gameStatus = GameStatusOracion.Playing;
        gameComplete.SetActive(false);
        SetQuestion();

    }

}

[System.Serializable]
public class QuestionDataOracion
{
    public string answer;
    public int cantidadPalabras;
}

public enum GameStatusOracion
{
    Next,
    Playing
}
