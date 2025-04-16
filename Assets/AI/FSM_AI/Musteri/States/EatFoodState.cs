using UnityEngine;
using BeykozEdu.FSM;
using System.Collections;

public class EatFoodState : BaseState<CustomerStateData>
{
    float timer = 10f;

    public override void OnEnter()
    {
        //StateData.Animator.SetTrigger("eat"); Bu opsiyonel
        Debug.Log("Müþteri yemeði yiyor");
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StateMachineHandler.AddState(new PayState(), StateData);
        }
    }

    public override void OnExit() { }
}