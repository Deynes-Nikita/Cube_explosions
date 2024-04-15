using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minQuantityPartsInExplosion = 2;
    [SerializeField] private int _maxQuantityPartsInExplosion = 7;
    [SerializeField] private float _reductionRatio = 2f;
    [SerializeField] private float _probability = 1f;
    [SerializeField] private float _explosionForce = 1000f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (_probability >= Random.value)
        {
            SetProbability();

            int quantityParts = Random.Range(_minQuantityPartsInExplosion, _maxQuantityPartsInExplosion);

            for (int i = 0; i < quantityParts; i++)
            {
                Instantiate(_cubePrefab, transform.position, Quaternion.identity).SetScale(_reductionRatio);
            }

            Explode();
        }

        Destroy(gameObject);
    }

    private void SetProbability()
    {
        _probability /= _reductionRatio;
    }

    public void SetScale()
    {
        transform.localScale /= _reductionRatio;
    }

    private void Explode()
    {
        _rigidbody.AddExplosionForce(_explosionForce, transform.position, transform.localScale.x);
    }
}
