using UnityEngine;
using BeykozEdu.FSM;
using System.Collections;

public class EatFoodStateEXP : BaseState<CustomerStateDataEXP>
{
    float timer = 10f;

    public override void OnEnter()
    {
        StateData.UIController?.DeleteOrderSprite();
        Debug.Log("Hi");
        StateData.Animator.SetTrigger("eat");
        Debug.Log("Müþteri yemeði yiyor");
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StateMachineHandler.AddState(new PayStateEXP(), StateData);
        }
    }

    public override void OnExit() { }
}