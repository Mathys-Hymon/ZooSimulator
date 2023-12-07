using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public GameInfos Infos;

    void Start()
    {
        instance = this;
        Load();
    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            Infos = JsonUtility.FromJson<GameInfos>(json);

            var FoundedAnimals = GameObject.FindGameObjectsWithTag("Animal");

            foreach(GameObject i in FoundedAnimals)
            {
                Destroy(i);
            }

            //for(int i  = 0; i < Infos.AnimalInfos.Count; i++)
            //{
            //    var Animal = Instantiate(Infos.AnimalInfos[i].animalRef, Infos.AnimalInfos[i].animalTransform);
            //    var AnimalScript = Animal.GetComponent<AnimalMasterScript>();
            //    AnimalScript.SetName(Infos.AnimalInfos[i].name);
            //    AnimalScript.SetAge(Infos.AnimalInfos[i].age);
            //    AnimalScript.SetHunger(Infos.AnimalInfos[i].hunger);
            //    AnimalScript.SetThirst(Infos.AnimalInfos[i].thirst);
            //    AnimalScript.SetTiredness(Infos.AnimalInfos[i].tiredness);
            //    AnimalScript.SetDeathAge(Infos.AnimalInfos[i].deathAge);
            //    AnimalScript.SetCanBreed(Infos.AnimalInfos[i].canBreed);
            //}

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

            UIScript.instance.SetMin(Infos.minutes);
            UIScript.instance.SetHour(Infos.hours);
            UIScript.instance.SetDay(Infos.days);
            UIScript.instance.SetMonth(Infos.months);
            UIScript.instance.SetYear(Infos.years);

        }
    }

    public void Save()
    {

        var FoundedAnimals = Object.FindObjectsOfType<AnimalMasterScript>();
        foreach (AnimalMasterScript i in FoundedAnimals)
        {
            Infos.AnimalInfos.Add(new AnimalInfos() {animalRef = i.GetGameobject(), animalTransformX = i.GetPositionX(), animalTransformY = i.GetPositionY(), animalTransformZ = i.GetPositionZ(), name = i.GetName(), age = i.GetAge(), deathAge = i.GetDeathAge(), hunger = i.GetHunger(), thirst = i.GetThirst(), tiredness = i.GetTiredness(), canBreed = i.GetCanBreed() });
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
    }
}
