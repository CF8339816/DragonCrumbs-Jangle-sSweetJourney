using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Cinemachine;
#region
/// <summary>
///NSCC GAME2065/4087/Game Development III(B)/Cameron,Jordan
///Jam 3: Dragon Crumbs: Jangle's Sweet Journey
/// team: Chris French, Kevin Card, Percy Hebb, Myranda Roy.
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations: Kevin Card
/// code review: 
/// </summary>
#endregion
public class ObjectButtonsInteractables : MonoBehaviour
{    

    [Header("UI Elements")]//---- Defines Game Elements for active et calls----CF
    [SerializeField] public GameObject ReferenePannel;
    [SerializeField] public GameObject OKButton;
    [SerializeField] public GameObject Accept;
    [SerializeField] public GameObject RePick;
    [SerializeField] public GameObject RecipeRefButton;
    [SerializeField] public GameObject CheckMark1;
    [SerializeField] public GameObject CrossOut1;
    [SerializeField] public GameObject CheckMark2;
    [SerializeField] public GameObject CrossOut2;
    [SerializeField] public GameObject CheckMark3;
    [SerializeField] public GameObject CrossOut3;








    [Header ("Apple Pie recipie")]//---- defines quick ref visual objects for apple pie recipe----CF
    [SerializeField] public GameObject IngredientAP1;
    [SerializeField] public GameObject IngredientAP1A;
    [SerializeField] public GameObject IngredientAP1B;
    [SerializeField] public GameObject IngredientAP1C;
    [SerializeField] public GameObject IngredientAP1D;
    [SerializeField] public GameObject IngredientAP1E;
    [SerializeField] public GameObject IngredientAP2;
    [SerializeField] public GameObject IngredientAP2A;
    [SerializeField] public GameObject IngredientAP3;
    [SerializeField] public GameObject IngredientAP3A;
    [SerializeField] public GameObject IngredientMill1;
    [SerializeField] public GameObject IngredientWheat1A;
    [SerializeField] public GameObject IngredientWheat1B;
    [SerializeField] public GameObject IngredientWheat1C;
    [SerializeField] public GameObject IngredientWheat1D;
    [SerializeField] public GameObject IngredientWheat1E;
    [SerializeField] public GameObject RefCheckMark1;
    [SerializeField] public GameObject RefCrossOut1;
    [SerializeField] public GameObject RefCheckMark2;
    [SerializeField] public GameObject RefCrossOut2;
    [SerializeField] public GameObject RefCheckMark3;
    [SerializeField] public GameObject RefCrossOut3;




    //[Header ("Additional recipie i there is time ")]//----Defines ingredient visuals for second recipie if there is time to incude one----CF
    //[SerializeField] public GameObject IngredientNA1;
    //[SerializeField] public GameObject IngredientNA2;
    //[SerializeField] public GameObject IngredientNA3;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ReferenePannel.SetActive(false);// ---- turns off the reference card when hud begins----CF
    }

    // Update is called once per frame
    void Update()
    {
        













    }

    public void OnRecipeCheck()
    {
    ReferenePannel.SetActive(true);    
    OKButton.SetActive(true);
    Accept.SetActive(false);
    RePick.SetActive(false);

    }
    public void OnRecipeOK()
    {
        ReferenePannel.SetActive(false);

    }

    public void OnRecipeSelect()
    {
        ReferenePannel.SetActive(true);
        OKButton.SetActive(false);
        Accept.SetActive(true);
        RePick.SetActive(true);

    }


}
