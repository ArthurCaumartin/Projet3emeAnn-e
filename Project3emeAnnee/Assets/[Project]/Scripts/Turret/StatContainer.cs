
using System;

[Serializable]
public struct StatContainer
{
    public float damage;
    public float attackPerSecond;
    public float range;
    public float rotateSpeed;
    public float projectileSpeed;
    public int perforationCount;

    public StatContainer(float damage, float attackPerSecond, float range, float rotateSpeed, float projectileSpeed, int perforationCount)
    {
        this.damage = damage;
        this.attackPerSecond = attackPerSecond;
        this.range = range;
        this.rotateSpeed = rotateSpeed;
        this.projectileSpeed = projectileSpeed;
        this.perforationCount = perforationCount;
    }

    public StatContainer GetDivideValue(float divideFactor)
    {
        return new StatContainer(
            damage * divideFactor,
            attackPerSecond * divideFactor,
            range,
            rotateSpeed * divideFactor,
            projectileSpeed * divideFactor,
            perforationCount
        );
    }
}