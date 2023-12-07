using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameInfos
{
    public List<AnimalInfos> AnimalInfos = new List<AnimalInfos>();
    public int minutes;
    public int hours;
    public int days;
    public int months;
    public int years;
}

[Serializable]
public class AnimalInfos
{
    public string animalRef;
    public float animalTransformX,animalTransformY,animalTransformZ;
    public string name;
    public int age;
    public int deathAge;
    public float hunger;
    public float thirst;
    public float tiredness;
    public bool canBreed;
}

