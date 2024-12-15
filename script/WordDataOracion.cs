using UnityEngine;
using UnityEngine.UI;

public class WordDataOracion : MonoBehaviour
{
    [SerializeField] private Text wordText;

    [HideInInspector]
    public string wordValue;

    private Button buttonComponent;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        if (buttonComponent)
        {
            buttonComponent.onClick.AddListener(() => WordSelected());
        }
    }

    public void SetWord(string value)
    {
        wordText.text = value;
        wordValue = value;
        Debug.Log(wordValue);
    }

    private void WordSelected()
    {
        Debug.Log("entra a wordselected");
        QuizManagerOraciones.instance.SelectedOption(this);
    }

}

