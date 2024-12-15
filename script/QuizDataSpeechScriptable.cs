using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsDataSpeech", menuName = "QuestionsDataSpeech", order = 1)]
public class QuizDataSpeechScriptable : ScriptableObject
{
  public List<QuestionDataSpeech> questions;
}
