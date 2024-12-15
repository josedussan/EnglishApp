using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsDataOracion", menuName = "QuestionsDataOracion", order = 1)]
public class OracionDataScriptable : ScriptableObject
{
    public List<QuestionDataOracion> questions;
}
