using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;
    private int Minutes;
    private int Hours = 7;
    [SerializeField] private int Days = 1;
    [SerializeField] private int Month = 1;
    [SerializeField] private int Year = 1;
    private bool Drawer1;
    private bool Drawer2;
    private bool Drawer3;

    [SerializeField] private GameObject TimeWheel;
    [SerializeField] private TMP_Text TimeText;
    [SerializeField] private GameObject CarnivorDrawer;
    [SerializeField] private GameObject FoodDrawer;
    [SerializeField] private GameObject HerbivorDrawer;
    [SerializeField] private DeliveryScript DeliveryZone;
    [SerializeField] private TMP_Text DateText;
    [SerializeField] private Gradient SunColor;
    [SerializeField] private Image SkyImage;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Clock();
    }

    public void SetMin(int NewMin)
    {
        Minutes = NewMin;
    }
    public void SetHour(int NewHour)
    {
        Hours = NewHour;
    }
    public void SetDay(int NewDay)
    {
        Days = NewDay;
        DateText.text = string.Format("{0:00}", Days) + "/" + string.Format("{0:00}", Month) + "/" + Year;
    }
    public void SetMonth(int NewMonth)
    {
        Month = NewMonth;
        DateText.text = string.Format("{0:00}", Days) + "/" + string.Format("{0:00}", Month) + "/" + Year;
    }
    public void SetYear(int NewYear)
    {
        Year = NewYear;
        DateText.text = string.Format("{0:00}", Days) + "/" + string.Format("{0:00}", Month) + "/" + Year;
    }

    public int GetMin()
    {
        return Minutes;
    }
    public int GetHours()
    {
        return Hours;
    }
    public int GetDays()
    {
        return Days;

    }
    public int GetMonth()
    {
        return Month;
    }
    public int GetYear()
    {
        return Year;
    }

    public void OpenDrawer(int Drawer)
    {
        if(Drawer == 0)
        {
            FlipFlopSwitch1(CarnivorDrawer);

            if(Drawer2)
            {
                FlipFlopSwitch2(FoodDrawer);
            }
            if(Drawer3)
            {
                FlipFlopSwitch3(HerbivorDrawer);
            }
        }

        else if(Drawer == 1)
        {
            FlipFlopSwitch2(FoodDrawer);

            if (Drawer1)
            {
                FlipFlopSwitch1(CarnivorDrawer);
            }
            if (Drawer3)
            {
                FlipFlopSwitch3(HerbivorDrawer);
            }
        }

        else if(Drawer == 2)
        {
            FlipFlopSwitch3(HerbivorDrawer);

            if (Drawer1)
            {
                FlipFlopSwitch1(CarnivorDrawer);
            }
            if (Drawer2)
            {
                FlipFlopSwitch2(FoodDrawer);
            }
        }
    }

    public void SpawnItem(GameObject Item)
    {

        DeliveryZone.SpawnAnimal(Item);
    }


    private void FlipFlopSwitch1(GameObject Drawer)
    {
        if (!Drawer1)
        {
            Drawer1 = true;
            Drawer.transform.localPosition = new Vector3(Drawer.transform.localPosition.x, Drawer.transform.localPosition.y + 280f, Drawer.transform.localPosition.z);
        }
        else
        {
            Drawer1 = false;
            Drawer.transform.localPosition = new Vector3(Drawer.transform.localPosition.x, Drawer.transform.localPosition.y - 280f, Drawer.transform.localPosition.z);
        }
    }


    private void FlipFlopSwitch2(GameObject Drawer)
    {
        if (!Drawer2)
        {
            Drawer2 = true;
            Drawer.transform.localPosition = new Vector3(Drawer.transform.localPosition.x, Drawer.transform.localPosition.y + 280f, Drawer.transform.localPosition.z);
        }
        else
        {
            Drawer2 = false;
            Drawer.transform.localPosition = new Vector3(Drawer.transform.localPosition.x, Drawer.transform.localPosition.y - 280f, Drawer.transform.localPosition.z);
        }
    }


    private void FlipFlopSwitch3(GameObject Drawer)
    {
        if (!Drawer3)
        {
            Drawer3 = true;
            Drawer.transform.localPosition = new Vector3(Drawer.transform.localPosition.x, Drawer.transform.localPosition.y + 280f, Drawer.transform.localPosition.z);
        }
        else
        {
            Drawer3 = false;
            Drawer.transform.localPosition = new Vector3(Drawer.transform.localPosition.x, Drawer.transform.localPosition.y - 280f, Drawer.transform.localPosition.z);
        }
    }


    private void Clock()
    {
        Minutes += 1;
        SkyImage.color = SunColor.Evaluate((((float)Hours * -15f) + ((float)Minutes * 2.5f) / -10) / -360);

        if (Minutes >= 60)
        {
            Minutes = 0;
            Hours += 1;

            if(Hours == 7)
            {
                GameObject[] AnimalsRef = GameObject.FindGameObjectsWithTag("Animal");

                foreach (GameObject Animal in AnimalsRef)
                {
                    Animal.GetComponent<AnimalMasterScript>().GetOld();
                    Animal.GetComponent<AnimalMasterScript>().WakeUp();

                }
            }

            else if(Hours == 19)
            {
                GameObject[] AnimalsRef = GameObject.FindGameObjectsWithTag("Animal");

                foreach (GameObject Animal in AnimalsRef)
                {
                    Animal.GetComponent<AnimalMasterScript>().GoToSleep();
                }
            }
        }

        if (Hours >= 24)
        {
            Hours = 0;
            Days += 1;
            DateText.text = string.Format("{0:00}",Days) + "/" + string.Format("{0:00}", Month) + "/" + Year;
            if(Days >= 30)
            {
                Days = 0;
                Month += 1;

                if(Month >= 12)
                {
                    Month = 0;
                    Year += 1;
                }
            }

            GameObject[] AnimalsRef = GameObject.FindGameObjectsWithTag("Animal");

            foreach(GameObject Animal in AnimalsRef)
            {
                Animal.GetComponent<AnimalMasterScript>().GetOld();
            }
        }

        TimeWheel.transform.rotation = Quaternion.Euler(0, 0, 180+(((float)Hours * -15f) + ((float)Minutes * 2.5f) / -10));

        if(Minutes < 10)
        {
            TimeText.text = Hours + " : 0" + Minutes;
        }

        else
        {
            TimeText.text = Hours + " : " + Minutes;
        }
        Invoke("Clock", 0.05f);
    }
}
