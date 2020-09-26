using System;

[Serializable]
public class PetParameter
{
    public IAttackable Attackable { get; set; }
    public IHealable Healable { get; set; }
    public IResetable Resetable { get; set; }
}
