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

    private void OnMouseDown()
    {
        if (_probability >= Random.value)
        {
            SetParametrsToPrefab();

            int quantityParts = Random.Range(_minQuantityPartsInExplosion, _maxQuantityPartsInExplosion);

            for (int i = 0; i < quantityParts; i++)
            {
                Instantiate(_cubePrefab, transform.position, Quaternion.identity);
            }

            Explode();
        }

        Destroy(gameObject);
    }

    private void SetParametrsToPrefab()
    {
        _probability /= _reductionRatio;
        _cubePrefab.SetScale(_reductionRatio);
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObject())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, transform.localScale.x);
        }
    }

    private List<Rigidbody> GetExplodableObject()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale);

        List<Rigidbody> rigidbodies = new();

        foreach (Collider hit in hits)
        {
            if(hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);
        }

        return rigidbodies;
    }
}
