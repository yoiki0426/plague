using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "New Sick")]//¦Û­q¯e¯f

public class Sick : ScriptableObject
{
    public int Human, ID;
    public string Name;
    public int[] Diagnosis;
    public int Rare;
    public Item [] potion;
}
