using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRegistry", menuName = "Scriptable Objects/PlayerRegistry")]
public class PlayerRegistry : ScriptableObject
{
    public List<Player> players;
}
