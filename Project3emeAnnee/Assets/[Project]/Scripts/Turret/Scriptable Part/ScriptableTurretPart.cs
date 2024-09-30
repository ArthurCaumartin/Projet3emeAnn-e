using UnityEngine;

public class ScriptableTurretPart : ScriptableObject
{
    public string partName;
    [Space]
    public Sprite uiSprite;
    //! Rarity
    public Mesh mesh;
}
