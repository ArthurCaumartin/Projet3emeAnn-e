using UnityEngine;

public class MobDeathEvent : MonoBehaviour
{
    //! //////////////////////
    //! Script not use for now
    //! //////////////////////

    [Header("Split Parametre : ")]
    [SerializeField] private int commingSoon;
    [Header("Explode Parametre :")]
    [SerializeField] private float _explodeRaduis = 5f;
    [SerializeField] private float _explodeDamage = 5f;

    void Start()
    {
    }

    public void Split(Mob toSplit)
    {

    }

    public void Explode(Mob toExplode)
    {
        // Collider[] overlapColl = Physics.OverlapSphere(transform.position, _explodeRaduis, 12);
        // if(overlapColl.Length == 0) return;

        // foreach (var item in overlapColl)
        // {
        //     if(item.gameObject == gameObject) continue;
        //     MobHealth mobHealth = item.GetComponent<MobHealth>();
        //     mobHealth?.DoDamage(1000000000000000000);
        // }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position, _explodeRaduis);
    // }
}
