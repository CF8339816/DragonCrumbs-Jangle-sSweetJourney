using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Kevin's scripts combined into one manager for  streamlining.---CF
/// I have had to rework the entire code block to convert the variable references and 
/// naming conventions to match the larger block of code as treying to bridge the two 
/// blocks did not work ..... A.K.A. "The Code of Theseus"
/// </summary>
/// 

public class DialogManager : MonoBehaviour
{
    public float wrightSpeed = 0.01f;
    public static DialogManager dialogManager;
    private bool wrighting = false;
    public bool talking = false;

    private Queue<string> dialog;
    private AddAudio addAudio;
    private string currentText;

    public GameObject textBox;
    public TMP_Text dialogText;

    private void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }

        if (dialogManager == null)//---- moved to ensure initalization before scripts ----CF
        {
            dialogManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);// ----changed to ensure no empty game objects left ----cf
        }


        if (addAudio == null) addAudio = FindFirstObjectByType<AddAudio>();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindDialogUI();
    }

    //finds "DialogBox" even if it starts inactive in the scene hierarchy
    private void FindDialogUI()
    {
        if (textBox == null)
        {
            foreach (Transform t in FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (t.name == "DialogBox")
                {
                    textBox = t.gameObject;
                    break;
                }
            }
        }
        if (dialogText == null && textBox != null)
        {
            dialogText = textBox.GetComponentInChildren<TMP_Text>(true);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialog = new Queue<string>();
        FindDialogUI();
        ClearDialog();
        if (textBox != null) textBox.SetActive(false);
    }

    public void StartDialog(string[] text)
    {
        FindDialogUI(); // ensures teh UI is linked before opening
        ToggleTextBox();
        Debug.Log("Talking");
        dialog.Clear();
        talking = true;
        foreach (string line in text)
        {
            dialog.Enqueue(line);
            Debug.Log($"added {line} to queue");
        }
        ContinueDialog();
    }

    public void StartDialog(string[] text, string[] secondText)
    {
        FindDialogUI(); // ensures the UI is linked before opening
        ToggleTextBox();
        Debug.Log("Talking");
        dialog.Clear();
        talking = true;
        foreach (string line in text)
        {
            dialog.Enqueue(line);
            Debug.Log($"added {line} to queue");
        }
        foreach (string line in secondText)
        {
            dialog.Enqueue(line);
            Debug.Log($"added {line} to queue");
        }
        ContinueDialog();
    }

    public void ContinueDialog()
    {
        if (!wrighting)
        {

            ClearDialog();
            if (dialog.Count == 0)
            {
                StopTalking();

                return;
            }
            currentText = dialog.Dequeue();
            StartCoroutine(WriteDialog());
        }
        else
        {
            StopAllCoroutines();
            ClearDialog();
            WriteDialog(currentText);
            wrighting = false;
        }
    }

    public void StopTalking()
    {
        Debug.Log("stopped talking");
        ToggleTextBox();
        talking = false;
    }

    IEnumerator WriteDialog()
    {
        wrighting = true;
        if (addAudio == null) addAudio = FindFirstObjectByType<AddAudio>();
        if (addAudio != null) addAudio.OnVoicesNewSprites();
        for (int i = 0; i < currentText.Length; i += 1)
        {
            WriteDialog(currentText[i].ToString());
            yield return new WaitForSeconds(wrightSpeed);
        }
        wrighting = false;
        if (addAudio != null) addAudio.StopVoicesNewSprites();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleTextBox()
    {
        if (textBox == null) return;
        if (textBox.activeSelf)
        {
            textBox.SetActive(false);
        }
        else
        {
            textBox.SetActive(true);
        }
    }

    public void WriteDialog(string Dialog)
    {
        if (dialogText != null) dialogText.text += Dialog;
    }

    public void ClearDialog()
    {
        if (dialogText != null) dialogText.text = "";
    }
}