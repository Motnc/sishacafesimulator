using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set New State", story: "State changed as [State]", category: "Action", id: "8297bb6baf6f4fa1c96c339085a0aebd")]
public partial class SetNewStateAction : Action
{
    [SerializeReference] public BlackboardVariable<States> State;
    [SerializeReference] public BlackboardVariable<States> CurrentState;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        CurrentState.Value = State.Value;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

