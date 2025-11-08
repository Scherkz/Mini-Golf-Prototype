using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool hasPlacedBuilding;
    public bool hasFinishedRound;

    public Action OnPlacedBuilding;
    public Action OnFinishedRound;
}
