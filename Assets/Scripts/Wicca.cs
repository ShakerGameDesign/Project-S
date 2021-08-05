using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wicca : Player
{
    [Header("Wicca Settings")]
    public float energyRecoveryRate = 1;
    public float heatReductionRate = 1;
    public float maxEnergy = 100;

    [HideInInspector] public float energy = 0;
    [HideInInspector] public float heat = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStart();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
        energy += energyRecoveryRate * Time.deltaTime;
        heat -= heatReductionRate * Time.deltaTime;
        //locks energy and heat to bounds
        energy = Mathf.Min(energy, maxEnergy);
        heat = Mathf.Max(heat, 0);
    }

    public bool useEnergy(float amount)
    {
        //if the action will use more energy then available,   do not allow it
        if (energy - (heat + amount) < 0) return false;
        //otherwise add the amount of heat and remove that amount from energy
        heat += amount;
        energy -= heat;
        return true;
    }
}
