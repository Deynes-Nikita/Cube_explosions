using UnityEngine;

public class Cube : MonoBehaviour
{
    private void Start()
    {
        SetColor();
    }

    private void SetColor()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void SetScale(float reductionRatio)
    {
        transform.localScale /= reductionRatio;
    }
}
