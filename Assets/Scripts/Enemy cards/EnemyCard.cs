using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "Valera/New enemy card")]
public class EnemyCard : ScriptableObject
{
    public string EnemyName;
    public Sprite Image;
    [TextArea]
    public string Description;
}
