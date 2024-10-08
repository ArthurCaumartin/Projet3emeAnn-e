using System;
using UnityEngine;

[Serializable]
public enum PartType //TODO ALED enum pour verifier le type de part ? On n'a plusieur type mais on sais pas comment les comparer
{
    None,
    Core,
    Body,
    Cannon,
}

public class ScriptableTurretPart : ScriptableObject
{
    public PartType partType;
    public string partName;
    [TextArea] public string description;
    [Space]
    public Sprite inventorySprite;
    public Mesh mesh;
    //! Rarity
}
