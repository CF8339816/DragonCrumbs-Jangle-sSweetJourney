using UnityEngine;

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

        ProxyOvenRawPie.SetActive(false);
        ProxyBakedPie.SetActive(false);
        RawApplePieProxy.SetActive(false);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
