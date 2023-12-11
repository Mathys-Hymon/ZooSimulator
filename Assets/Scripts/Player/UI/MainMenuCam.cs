using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MainMenuCam : MonoBehaviour
{

    [SerializeField] Button loadSaveButton;
    [SerializeField] Button newSaveButton;
    [SerializeField] Button deleteButton;

    private Vector3 TargetPosition = new Vector3(0,0,-10);

    private void Start()
    {
        ChangeSaveButton();
    }

    private void Update()
    {
       
    }

    public void StartButton()
    {
        transform.position = new Vector3(-21.6399994f, -26.6599998f, -10);
    }

    public void GoBackMenu()
    {
        transform.position = new Vector3(0, 0, -10);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SaveButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Delete(Application.persistentDataPath + "/data.save");
            ChangeSaveButton();
        }
    }


    private void ChangeSaveButton()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            loadSaveButton.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(true);
            newSaveButton.gameObject.SetActive(false);
        }
        else
        {
            loadSaveButton.gameObject.SetActive(false);
            newSaveButton.gameObject.SetActive(true);
            deleteButton.gameObject.SetActive(false);
        }
    }
}
