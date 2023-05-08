using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed => GameManager.instance.playerId == 0 ? 1.1f : 1.0f;
    public static float WeaponSpeed => GameManager.instance.playerId == 1 ? 1.1f : 1.0f;
    public static float WeaponRate => GameManager.instance.playerId == 1 ? 0.9f : 1.0f;
    public static float Damage => GameManager.instance.playerId == 2 ? 1.2f : 1.0f;
    public static int Count => GameManager.instance.playerId == 3 ? 1 : 0;
}
