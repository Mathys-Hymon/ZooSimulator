using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseInfoScript : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer sr;
    private bool WindowOn;
    private AnimalMasterScript AnimalInfos;
    [SerializeField] private GameObject canvasRef;

    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Age;
    [SerializeField] private TMP_Text Species;
    [SerializeField] private TMP_Text FavFood;
    [SerializeField] private Slider HungerSlider;
    [SerializeField] private Slider ThirstSlider;
    [SerializeField] private Slider TirednessSlider;
    [SerializeField] private LayerMask ZoneMask;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        canvasRef.SetActive(false);
        mainCamera = Camera.main;
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 300f, ~ZoneMask);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<AnimalMasterScript>() != null)
        {
            AnimalInfos =  hit.collider.gameObject.GetComponent<AnimalMasterScript>();
            if(!WindowOn)
            {
                ShowWindow();
            }
            transform.position = hit.transform.position;
            HungerSlider.value = AnimalInfos.GetHunger();
            ThirstSlider.value = AnimalInfos.GetThirst();
            TirednessSlider.value = AnimalInfos.GetTiredness();
            Age.SetText(AnimalInfos.GetAge() + " ans");
        }

        else
        {
            AnimalInfos = null;
            if(WindowOn)
            {
                CloseWindow();
            }
        }

        

    }

    private void ShowWindow()
    {
        WindowOn = true;
        sr.enabled = true;
        canvasRef.SetActive(true);

        Name.SetText(AnimalInfos.GetName());
        Species.SetText("espèce : " + AnimalInfos.species);
    }

    private void CloseWindow()
    {
        WindowOn = false;
        sr.enabled = false;
        canvasRef.SetActive(false);
    }
}
