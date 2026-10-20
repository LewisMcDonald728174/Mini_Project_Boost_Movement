using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float currentEnergy;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int minEnergy;
    [SerializeField] private int regenRate;

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void Update()
    {
        RegenEnergy();

        currentEnergy = Mathf.Clamp(currentEnergy, minEnergy, maxEnergy);

        Debug.Log(currentEnergy);
    }

    private void RegenEnergy()
    {
        if (currentEnergy != maxEnergy)
        {
            currentEnergy = currentEnergy + regenRate * Time.deltaTime;
        }
    }

    public void ConsumeEnergy(float amount)
    {
        currentEnergy -= amount;
    }

    public bool HasEnergy(float energyCost)
    { return currentEnergy > energyCost; }
}