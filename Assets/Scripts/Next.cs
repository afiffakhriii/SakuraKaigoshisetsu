using UnityEngine;
using UnityEngine.EventSystems;

public class TextClickHandler : MonoBehaviour, IPointerClickHandler
{
    public TextSwitcher_Legacy controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.GoToNextScene();
    }
}
