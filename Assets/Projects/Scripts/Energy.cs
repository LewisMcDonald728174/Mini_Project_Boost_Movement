using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float currentEnergy;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float minEnergy = 0f;
    [SerializeField] private float regenRate = 1f;

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        if (currentEnergy != 100f)
        {
            RegenEnergy();
        }

        Mathf.Clamp(currentEnergy, minEnergy, maxEnergy);

        Debug.Log(currentEnergy);
    }

    private void RegenEnergy()
    {
        currentEnergy = currentEnergy + 1 * Time.deltaTime;
    }

    public void ConsumeEnergy()
    {
        
    }

    public bool HasEnergy(float energyCost)
    { return currentEnergy > energyCost; }
}