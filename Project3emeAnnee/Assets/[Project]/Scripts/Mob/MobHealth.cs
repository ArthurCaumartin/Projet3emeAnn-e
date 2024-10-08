using UnityEngine;
using UnityEngine.Events;

public class MobHealth : MonoBehaviour
{
    [SerializeField] private float _maxLife;
    [SerializeField] private float _currentLife;
    [SerializeField] private UnityEvent<Mob> _onDeathEvent;
    public UnityEvent<Mob> OnDeathEvent { get => _onDeathEvent; }
    private Mob _thisMob;

    private void Start()
    {
        _currentLife = _maxLife;
        _thisMob = GetComponent<Mob>();
    }

    public void DoDamage(float value)
    {
        _currentLife -= value;
        if (_currentLife <= 0)
        {
            OnDeath();
        }
    }

    public void DestroyMob()
    {
        OnDeath();
    }

    private void OnDeath()
    {
        _onDeathEvent.Invoke(_thisMob);
        Destroy(gameObject);
    }
}
