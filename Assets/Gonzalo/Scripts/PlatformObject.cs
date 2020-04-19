using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlatform", menuName = "Game/Platform")]
public class PlatformObject : ScriptableObject
{
    public GameObject visuals;
    public Vector2 dimensions;
}
