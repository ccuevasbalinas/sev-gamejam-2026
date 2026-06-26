using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.State
{
    // State example
    public class AttackState : IState<Enemy>
    {
        public void Enter(Enemy owner)
        {
            Debug.Log($"{owner.Name} entered ATTACK state.");
            owner.SetAnimation("Attack");
        }

        public void Execute(Enemy owner)
        {
            Debug.Log($"{owner.Name} attacks the player!");
        }

        public void Exit(Enemy owner)
        {
            Debug.Log($"{owner.Name} exited ATTACK state.");
        }
    }
}


