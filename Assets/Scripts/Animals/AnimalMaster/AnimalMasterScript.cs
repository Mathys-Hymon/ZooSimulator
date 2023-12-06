using UnityEngine;
using UnityEngine.UIElements;

public class AnimalMasterScript : MonoBehaviour
{
    protected float MaxFood = 100;
    protected float Hunger;
    protected float Thirst = 20;
    protected float Tiredness;
    protected bool dead;
    protected bool moveTo;
    protected GameObject CollisionRef;
    protected Vector2 MVDistanceSave;
    [SerializeField]  protected int Age = 1;
    [SerializeField] protected string Name;
    [SerializeField] protected FoodList FoodState;
    [SerializeField] protected int OlderAge;
    [SerializeField] private GameObject HitActor;
    [SerializeField] private GameObject deadBodyRef;
    [SerializeField] private ParticleSystem ParticleSystem;
    public speciesList species;

    private string[] nameList = new string[] { "Rigolo", "Chantilly", "Zouzou", "Pompon", "Choco", "Biscotte", "Zébulon", "Frimousse", "Muffin", "Sardine", "Tournesol", "Guimauve", "Caramel", "Zigzag", "Plume", "Banjo", "Roudoudou", "Macaron", "Chaussette", "Pistache", "Sushi", "Quenotte", "Bidule", "Zibeline", "Truffe", "Champignon", "Paillette", "Tornade", "Canaille", "Nougat", "Citronnelle", "Ketchup", "Poisson-chat", "Frisouille", "Brioche", "Ciboulette", "Marmelade", "Cactus", "Boulette", "Sauterelle", "Froufrou", "Doudou", "Caprice", "Popcorn", "Chausson", "Zinzin", "Pétale", "Quinoa", "Macadam", "Bidon", "Mirabelle", "Éclair", "Frisson", "Spaghetti", "Citron", "Coton", "Calinou", "Grignote", "Cassis", "Zouzou", "Tagada", "Meringue", "Poêlée", "Grisou", "Sirocco", "Biscuit", "Sésame", "Chiffon", "Barbouille", "Pamplelune", "Kawaï", "Potiron", "Vanille", "Clémentine", "Papillon", "Rigolo", "Chantilly", "Zouzou", "Roudoudou", "Gribouille", "Chipie", "Paillasson", "Bouboule", "Vinaigrette", "Canapé", "Zeste", "Touffu", "Bambino", "Gadget", "Barbapapa", "Gribouillis", "Trompette", "Sirocco", "Quiche", "Cassoulet", "Pamplemousse", "Frisquette", "Trottinette", "Radis", "Nuage", "Zorro", "Toupie", "Fringale", "Frisotté", "Chicorée", "Flibustier", "Turbulence", "Ziboulette", "Marmouset", "Chalumeau", "Gribouillis", "Tourniquet", "Papouille", "Flonflon", "Chamallow", "Froufrou", "Capriccio", "Zinzolin", "Chicorée", "Flibustier", "Praline", "Zigzag", "Froufrou", "Brioche", "Ricochet", "Fugace", "Papillon", "Froufrou", "Faribole", "Mouffette", "Quenotte", "Bouillotte", "Cacahuète", "Zeste", "Zigzag", "Galipette", "Pamplemousse", "Sauté", "Rigolo", "Grignote", "Chahut", "Mirliton", "Papouille", "Gadget", "Chaloupe", "Fugue", "Zébulon", "Savonnette", "Nougat", "Pistache", "Frisson", "Ratiboisé", "Ziboulette", "Gadget", "Flibustier", "Chantilly", "Froufrou", "Bouclette", "Flonflon", "Chicorée", "Gribouillis" };
    private bool InvincibilityFrame;
    private int DeathAge;
    public enum speciesList
    {
        giraffe,
        Panda,
        Lion,
        Tigre,
        TheThing

    }
    protected enum FoodList
    {
        Grass,
        Fish,
        Meat,
        Humans
    }

    private Vector2 MovingDistance;
    private float WalkTime = 3f;
    private bool CanMove = true;
    private bool ISChilling = false;
    private bool ChillMidTime = false;
    private bool fuckable;
    private bool touchBorder;
    private bool sleep;
    private bool tired;


    public int GetAge()
    {
        return Age;
    }

    public float GetHunger()
    {
        return Hunger / MaxFood;
    }

    public float GetTiredness()
    {
        return Tiredness / 50;
    }

    public float GetThirst()
    {
        return Thirst / 100;
    }
    public string GetName()
    {
        return Name;
    }

    private void Start()
    {
        DeathAge = Random.Range(OlderAge - 5, OlderAge);
        fuckable = false;
        moveTo = false;
        Age = 1;
        Name = nameList[Random.Range(0, nameList.Length)];
        WalkTime = Random.Range(3f, 5f);
        MovingDistance = new Vector2(-1.5f,1.5f);
        CanMove = true;
        Hunger = MaxFood;
        Starving();
        Chill();
    }


    private void Update()
    {
        if (!dead)
        {
            
            gameObject.transform.localScale = new Vector3(0.5f + (float)Age / 50, 0.5f + (float)Age / 50,1);

            if (CanMove && !sleep) { Move(MVDistanceSave); }

            if (!ISChilling && !sleep) { ISChilling = true; Invoke("Chill", WalkTime); Invoke("MidChill", WalkTime / 3); }

            if(sleep)
            {
                Sleep();
            }
        }
    }

    private void Starving()
    {
        if (Hunger <= 0 || Thirst <= 0)
        {
            Die();
        }

        else if (!dead)
        {
            Thirst--;
            Hunger--;
            Invoke("Starving", 1f);
        }
    }

    public void GetOld()
    {
        Age++;
        if(Age >= DeathAge)
        {
            Die();
        }
    }

    protected void Feed(GameObject Food)
    {
        if (Food.tag == FoodState.ToString())
        {
            Hunger = MaxFood;
            Destroy(Food);
            moveTo = false;
            CollisionRef = null;

            if(Age > OlderAge/10 )
            {
              fuckable = true;
            }
        }
        else if (Food.tag == "Water")
        {
         Thirst = 100;
         moveTo = false;
         CollisionRef = null;
        }
    }


    protected void Move(Vector2 MVDistanceSave)
    {
        
        if (moveTo && CollisionRef != null)
        {
            if(tired)
            {
                MVDistanceSave = new Vector2(CollisionRef.transform.position.x, CollisionRef.transform.position.y) - new Vector2(transform.position.x, transform.position.y)/3;
            }

            else
            {
                MVDistanceSave = new Vector2(CollisionRef.transform.position.x, CollisionRef.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            }
            MovingDistance += MVDistanceSave/500;
        }

        if (Mathf.Abs(MovingDistance.x) <= Mathf.Abs(MVDistanceSave.x) && Mathf.Abs(MovingDistance.y) <= Mathf.Abs(MVDistanceSave.y) && !ChillMidTime && !moveTo)
        {
            Vector2 MovingLerp = Vector2.Lerp(new Vector2(0, 0), MVDistanceSave, (WalkTime / 2)*Time.deltaTime);
            MovingDistance += MovingLerp;
        }

        else if (ChillMidTime && !moveTo) 
        {
            MovingDistance -= (MovingDistance/1.1f) * Time.deltaTime;
        }


        if(MVDistanceSave.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Tiredness < 50)
        {
            Tiredness += (Mathf.Abs(MovingDistance.x + MovingDistance.y))*Time.deltaTime;
            transform.position = transform.position + new Vector3((MovingDistance.x * Time.deltaTime), (MovingDistance.y * Time.deltaTime), 0);
            tired = false;
        }

        else
        {
            transform.position = transform.position + new Vector3((MovingDistance.x * Time.deltaTime), (MovingDistance.y * Time.deltaTime), 0)/3;
            tired = true;
        }
    }

    public virtual void MoveTo(GameObject collision)
    {
            if (collision != null && collision.tag == FoodState.ToString() && Hunger <= MaxFood / 1.5f)
            {
                CollisionRef = collision;
                moveTo = true;
                MVDistanceSave = new Vector2(collision.transform.position.x, collision.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            }
       
            else if (collision.tag == "Water" && Thirst <= 20)
        {
            CollisionRef = collision;
            moveTo = true;
            MVDistanceSave = new Vector2(collision.transform.position.x, collision.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
        }
        else if(collision.GetComponent<AnimalMasterScript>() != null && collision.GetComponent<AnimalMasterScript>().species == species && collision.GetComponent<AnimalMasterScript>().fuckable)
        {
            if(fuckable)
            {
                CollisionRef = collision;
                moveTo = true;
                MVDistanceSave = new Vector2(collision.transform.position.x, collision.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            }
        }

    }


    public void CancelMoveTo(GameObject food)
    {
        if(food == CollisionRef)
        {
            CollisionRef = null;
            moveTo = false;
            Chill();
        }
    }

    protected void MidChill()
    {
        ChillMidTime = true;
    }

    public void GoToSleep()
    {
        ParticleSystem.Play();
        Sleep();
    }
    public void Sleep()
    {
        sleep = true;
        CanMove = false;
        ISChilling = false;

        if(Tiredness >= 0) 
        { 
            Tiredness -= Time.deltaTime*2;
        }
    }

    public void WakeUp()
    {
        sleep = false;
        MoveAgain();
    }

    protected void Chill()
    {
        if(!sleep)
        {
        if(moveTo)
        {
            WalkTime = 100f;
            MovingDistance = new Vector2(0, 0);
            MoveAgain();
        }

        else 
        {
            Tiredness -= Time.deltaTime * 2;
            ChillMidTime = false;
            CanMove = false;
            WalkTime = Random.Range(3f, 5f);
            float ChillTime = Random.Range(0.1f, 4f);
            MovingDistance = new Vector2(0, 0);
            MVDistanceSave = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            touchBorder = false;
            Invoke("MoveAgain", ChillTime);
        }
        }


    }

    protected void MoveAgain()
    {
        ISChilling = false;
        CanMove = true;
        ParticleSystem.Stop();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Fence" && !touchBorder)
        {
            touchBorder = true;
            MovingDistance *= -0.1f;
            MVDistanceSave *= -0.1f;
        }

        else if (collision.gameObject.tag == FoodState.ToString() || collision.gameObject.tag == "Water")
        {
            Feed(collision.gameObject);
        }

        else if (collision.gameObject == CollisionRef && collision.gameObject.GetComponent<AnimalMasterScript>() != null && collision.gameObject.GetComponent<AnimalMasterScript>().species == species && collision.gameObject.GetComponent<AnimalMasterScript>().fuckable)
        {
            MakeKids(collision.gameObject);
        }
    }

    private void MakeKids(GameObject collisionObject)
    {
        fuckable = false;
        if(collisionObject.GetComponent<AnimalMasterScript>().fuckable) 
        {
            fuckable = false;
            Instantiate(gameObject, transform.position, Quaternion.identity);

        }
        fuckable = false;

    }


    public void GetHit(GameObject ennemi)
    {
        if (!InvincibilityFrame)
        {
            InvincibilityFrame = true;
            Invoke("HitReset", 1f);
            int HungerDegats = Random.Range(0, 10);
            if (tired)
            {
               HungerDegats = Random.Range(7, 15);
            }
            Hunger -= HungerDegats;
            gameObject.GetComponent<Rigidbody2D>().velocity += (new Vector2(transform.position.x - ennemi.transform.position.x, transform.position.y - ennemi.transform.position.y) * 100) * Time.fixedDeltaTime;
            GameObject HitRef = Instantiate(HitActor, transform.position, Quaternion.identity);
            HitRef.GetComponent<HitScript>().Hit(HungerDegats);
        }
    }

    private void HitReset()
    {
        InvincibilityFrame = false;
    }

    private void Die()
    {
        dead = true;
        Instantiate(deadBodyRef, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
