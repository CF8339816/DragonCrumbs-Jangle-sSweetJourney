using UnityEngine;

public class PieVisabilityManager : MonoBehaviour
{

    public GameObject ProxyOvenRawPie;
    public GameObject ProxyBakedPie;
    public GameObject RawApplePieProxy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ProxyOvenRawPie.SetActive(false);
        ProxyBakedPie.SetActive(false);
        RawApplePieProxy.SetActive(false);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
