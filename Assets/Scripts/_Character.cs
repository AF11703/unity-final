using System.Diagnostics.Contracts;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class _Character : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _strength = 5f;
    [SerializeField] private float _defense = 10f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private bool _isAttacking = false;

    public HealthBar targetHealthBar;


    private void Awake()
    {
        targetHealthBar.SetMaxHealthValue(_maxHealth);
    }


    public float getSpeed()
    {
        return _speed;
    }

    public void setSpeed(float speed)
    {
        _speed = speed;
    }

    public float getStrength()
    {
        return _strength;
    }

    public void setStrength(float strength)
    {
        _strength = strength;
    }

    public float getDefense()
    {
        return _defense;
    }

    public void setDefense(float defense)
    {
        _defense = defense;
    }

    public float getHealth()
    {
        return _health;
    }

    public void setHealth(float health)
    {
        _health = health;
        if (_health > _maxHealth)
        {
            _health = _maxHealth; // Ensure health doesn't exceed max health
        }
        else if (_health <= 0f)
        {
            Die();
        }
    }

    public bool isDead()
    {
        return _isDead;
    }

    public bool isAttacking()
    {
        return _isAttacking;
    }

    
    public void setAttacking(bool val)
    {
        _isAttacking = val;
    }

    public void Damage(_Character ch, bool blocked = false)
    {
        if (blocked)
        {
            return; // If the attack is blocked, no damage is dealt
        }

        float damage;

        if (this._strength <= ch._defense)
        {
            damage = 5f;
        }
        else
        {
            damage = Mathf.Abs(this._strength - ch._defense);
        }
        
        ch._health -= damage;
        targetHealthBar.SetHealthValue(ch._health);
    }

        
    

    public void Die()
    {
        if (_health <= 0f)
        {
            _isDead = true;
            _health = 0f; // Ensure health doesn't go below 0
        }

    }
}
