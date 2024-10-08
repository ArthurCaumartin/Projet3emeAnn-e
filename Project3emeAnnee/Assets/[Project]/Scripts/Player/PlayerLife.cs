using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _maxLife = 10; 
    [SerializeField] private int _currentLife;

    private void Start()
    {
        _currentLife = _maxLife;
    }

    public void TakeDamage(int damageToDo)
    {
        _currentLife -= damageToDo;
        PartyManager.instance.PlayerTakeDamage(_maxLife, _currentLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        MobHealth otherMob = other.GetComponent<MobHealth>();
        if(otherMob)
        {
            TakeDamage(1);
            otherMob.DestroyMob();
        }
    }
}
