using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public GameInfos Infos;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource MusicSource;

    private float volume = 0.5f;

    void Start()
    {
        instance = this;
        Load();
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        volumeSlider.value = volume;
        MusicSource.volume = volume;


    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            Infos = JsonUtility.FromJson<GameInfos>(json);

            foreach (AnimalInfos i in Infos.AnimalInfos)
            {

                GameObject Animal = Instantiate(Resources.Load<GameObject>("AnimalPrefab/"+ i.animalRef.Replace("(Clone)", "").Trim()));
                Animal.transform.position = new Vector3(i.animalTransformX, i.animalTransformY, i.animalTransformZ);
                AnimalMasterScript AnimalScript = Animal.GetComponent<AnimalMasterScript>();
                AnimalScript.SetName(i.name);
                AnimalScript.SetAge(i.age);
                AnimalScript.SetHunger(i.hunger);
                AnimalScript.SetThirst(i.thirst);
                AnimalScript.SetTiredness(i.tiredness);
                AnimalScript.SetDeathAge(i.deathAge);
                AnimalScript.SetCanBreed(i.canBreed);
            }

            foreach (ObjectInfos i in Infos.ObjectInfos)
            {
                GameObject Food = Instantiate(Resources.Load<GameObject>("FoodPrefab/" + i.FoodRef.Replace("(Clone)", "").Trim()));
                Food.transform.position = new Vector3(i.foodPosX, i.foodPosY, i.foodPosZ);
            }

            UIScript.instance.SetMin(Infos.minutes);
            UIScript.instance.SetHour(Infos.hours);
            UIScript.instance.SetDay(Infos.days);
            UIScript.instance.SetMonth(Infos.months);
            UIScript.instance.SetYear(Infos.years);

        }
    }

    public void Save()
    {
        Infos.AnimalInfos.Clear();
        Infos.ObjectInfos.Clear();

        var FoundedAnimals = Object.FindObjectsOfType<AnimalMasterScript>();
        foreach (AnimalMasterScript i in FoundedAnimals)
        {
            Infos.AnimalInfos.Add(new AnimalInfos() {animalRef = i.GetGameobject(), animalTransformX = i.GetPositionX(), animalTransformY = i.GetPositionY(), animalTransformZ = i.GetPositionZ(), name = i.GetName(), age = i.GetAge(), deathAge = i.GetDeathAge(), hunger = i.GetHunger(), thirst = i.GetThirst(), tiredness = i.GetTiredness(), canBreed = i.GetCanBreed() });
        }
        var Grass = GameObject.FindGameObjectsWithTag("Grass");
        for (int i = 0; i < Grass.Length; i++)
        {
            Infos.ObjectInfos.Add(new ObjectInfos() { FoodRef = Grass[i].name, foodPosX = Grass[i].transform.position.x, foodPosY = Grass[i].transform.position.y, foodPosZ = Grass[i].transform.position.z });
        }
        var Meat = GameObject.FindGameObjectsWithTag("Meat");
        for (int i = 0; i < Meat.Length; i++)
        {
            Infos.ObjectInfos.Add(new ObjectInfos() { FoodRef = Meat[i].name, foodPosX = Meat[i].transform.position.x, foodPosY = Meat[i].transform.position.y, foodPosZ = Meat[i].transform.position.z });
        }

        Infos.hours = UIScript.instance.GetHours();
        Infos.days = UIScript.instance.GetDays();
        Infos.months = UIScript.instance.GetMonth();
        Infos.years = UIScript.instance.GetYear();

        string json = JsonUtility.ToJson(Infos);

        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Create(Application.persistentDataPath + "/data.save").Dispose();
        }
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);

        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeVolume()
    {
        
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        MusicSource.volume = volumeSlider.value;
    }
}
