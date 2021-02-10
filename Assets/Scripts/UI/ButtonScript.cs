using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private ColorBlock currentColorBlock;
    private ColorBlock previousColorBlock;

    public void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // get the current color block assign to the button selected
        currentColorBlock = button.colors;
        // save the current color block into the previous block so we can set the color back when the button isnt highlighted by the the Ray Interactor
        previousColorBlock = currentColorBlock;
        // change the current color block to transparent
        currentColorBlock.selectedColor = button.colors.highlightedColor;
        // assign the current block to the button highlighted
        button.colors = currentColorBlock;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // assign the button color block to his previous color state when exit
        button.colors = previousColorBlock;
    }
}
