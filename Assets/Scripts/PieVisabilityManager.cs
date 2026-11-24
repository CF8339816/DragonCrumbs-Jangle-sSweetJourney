using UnityEngine;
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
public class PieVisabilityManager : MonoBehaviour
{

    public GameObject ProxyOvenRawPie;
    public GameObject ProxyBakedPie;
    public GameObject RawApplePieProxy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        //bootStrappin.FindManagers();

        if (ProxyOvenRawPie != null) ProxyOvenRawPie.SetActive(false);
        if (ProxyBakedPie != null) ProxyBakedPie.SetActive(false);
        if (RawApplePieProxy != null) RawApplePieProxy.SetActive(false);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}