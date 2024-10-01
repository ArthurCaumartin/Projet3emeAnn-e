using UnityEngine;

public class ScriptableTurretPart : ScriptableObject
{
    public string partName;
    [Space]
    public Sprite inventorySprite;
    //! Rarity
    public Mesh mesh;
}
