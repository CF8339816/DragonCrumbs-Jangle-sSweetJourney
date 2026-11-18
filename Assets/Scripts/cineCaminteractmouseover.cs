using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;
#region coder & project
/// <summary>
/// NSCC GAME2065/4087/Game Development III(B)/Cameron,Jordan
/// Jam 3 :DragonCrumbs: Jangle's Sweet Journey
/// team: Chris French, Percy Hebb, Kevin Card, Myranda Roy
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// code review: 
/// </summary>
#endregion

public class cineCaminteractmouseover : MonoBehaviour
{
    [Header("Highlight Settings")]
    public Color highlightColor = Color.purple;
    private Renderer objRenderer;
    private Color originalColor;
    private bool isURP = false;
    [Header("Button Action")]
    public UnityEvent onObjectClicked;

    void Start()
    {
        objRenderer = GetComponentInChildren<Renderer>();

        if (objRenderer != null)
        {
           
            if (objRenderer.material.HasProperty("_BaseColor"))
            {
                isURP = true;
                originalColor = objRenderer.material.GetColor("_BaseColor");
            }
            else
            {
                originalColor = objRenderer.material.color;
            }
        }



        //objRenderer = GetComponent<Renderer>();
        //if (objRenderer != null)
        //    originalColor = objRenderer.material.color;
    }

    public void SetHighlight(bool isHighlighted)
    {
        if (objRenderer == null) return;

        Color targetColor = isHighlighted ? highlightColor : originalColor;

        if (isURP)
            objRenderer.material.SetColor("_BaseColor", targetColor);
        else
            objRenderer.material.color = targetColor;




        //if (objRenderer != null)
        //    objRenderer.material.color = isHighlighted ? highlightColor : originalColor;
    }

    public void TriggerClick()
    {
        Debug.Log(gameObject.name + " clicked!");
        onObjectClicked?.Invoke();
    }
}










