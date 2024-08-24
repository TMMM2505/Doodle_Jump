using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlatformData", menuName = "Scriptable Objects/Platform Data Config", order = 1)]

public class PlatformData : ScriptableObject
{
    public PlatformDataConfig[] list;
}

[Serializable]
public class PlatformDataConfig
{
    public Platform prefab;
    public PlatformType platformType;
}
public enum PlatformType
{
    none = 0,
    StaticPlatform = 1,
    DissappearPlatform = 2,
    FallingPlatform = 3,
    DynamicPlatform = 4,
}
