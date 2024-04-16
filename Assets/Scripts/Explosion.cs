using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
        foreach (Rigidbody expodableCube in GetExpodableCube())
        {
            expodableCube.AddExplosionForce(_explosionForce, transform.position, transform.localScale.x);
        }
    }

    private List<Rigidbody> GetExpodableCube()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
