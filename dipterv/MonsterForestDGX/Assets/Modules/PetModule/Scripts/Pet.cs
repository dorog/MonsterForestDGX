using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Pet : MonoBehaviour
{
    public string petName = "Unknown";
    public PetAbility[] normalAbilities;
    public UpdatePetAbility[] updateAbilities;

    public void Init(PetParameter petParameter)
    {
        foreach(var petAbility in updateAbilities)
        {
            petAbility.Init(petParameter);
        }
        foreach (var petAbility in normalAbilities)
        {
            petAbility.Init(petParameter);
        }
    }

    public void Update()
    {
        foreach (var petAbility in updateAbilities)
        {
            petAbility.UpdateEffect();
        }
    }

    public List<PetAbilityDesciption> GetAbilityDesciptions()
    {
        List<PetAbilityDesciption> petAbilityDesciptions = new List<PetAbilityDesciption>();

        foreach(var normal in normalAbilities)
        {
            petAbilityDesciptions.Add(new PetAbilityDesciption()
            {
                Name = normal.GetAbilityName(),
                Description = normal.GetAbilityDescription(),
                Color = normal.GetAbilityNameColor()
            });
        }

        foreach (var update in updateAbilities)
        {
            petAbilityDesciptions.Add(new PetAbilityDesciption()
            {
                Name = update.GetAbilityName(),
                Description = update.GetAbilityDescription(),
                Color = update.GetAbilityNameColor()
            });
        }

        return petAbilityDesciptions;
    }

    public void OnDestroy()
    {
        foreach (var petAbility in updateAbilities)
        {
            petAbility.Destroy();
        }
        foreach (var petAbility in normalAbilities)
        {
            petAbility.Destroy();
        }
    }
}
