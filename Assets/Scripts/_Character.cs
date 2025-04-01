using UnityEngine;
using UnityEngine.Rendering;

public class _Character : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _strength = 5f;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private bool _isDead = false;

}
