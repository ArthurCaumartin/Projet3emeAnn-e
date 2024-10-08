using UnityEngine;
using UnityEngine.Events;

public class MobHealth : MonoBehaviour
{
    [SerializeField] private float _maxLife;
    [SerializeField] private float _currentLife;
    [SerializeField] private UnityEvent<MobHealth> _onDeathEvent;
    public UnityEvent<MobHealth> OnDeathEvent { get => _onDeathEvent; }

    private void Start()
    {
        _currentLife = _maxLife;
    }

    public void DoDamage(float value)
    {
        _currentLife -= value;
        if (_currentLife <= 0)
        {
            OnDeath(true);
        }
    }

    public void DestroyMob()
    {
        OnDeath(false);
    }

    private void OnDeath(bool dropStuff = true)
    {
        _onDeathEvent.Invoke(this);
        if (dropStuff) LootGenerator.instance.GenerateItem();
        Destroy(gameObject);
    }
}
