using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameScreen : MonoBehaviour
{
    [SerializeField] private UIDocument UIDocument;
    [SerializeField] private UIDocument card;

    public void ShowCard()
    {
        UIDocument.rootVisualElement.Q<VisualElement>("Cards").SetEnabled(false);
        UIDocument.rootVisualElement.Q<VisualElement>("Card").SetEnabled(true);
    }
}
