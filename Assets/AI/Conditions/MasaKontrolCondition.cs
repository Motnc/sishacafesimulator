using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "MasaKontrol", story: "Kafede boş [masa] var mı?", category: "Conditions", id: "8ea4edea47163e21047cd6c5a6a995b3")]
public partial class MasaKontrolCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Masa;

    public override bool IsTrue()
    {
        return Masa?.Value != null && Masa.Value.TryGetComponent(out Masa masaComponent) && masaComponent.IsEmpty_;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
