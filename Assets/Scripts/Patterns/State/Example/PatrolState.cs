using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.State
{
    // State example
    public class PatrolState : IState<Enemy>
    {
        public void Enter(Enemy owner)
        {
            Debug.Log($"{owner.Name} entered PATROL state.");
            owner.SetAnimation("Patrol");
        }

        public void Execute(Enemy owner)
        {
            owner.MoveForward();
        }

        public void Exit(Enemy owner)
        {
            Debug.Log($"{owner.Name} exited PATROL state.");
        }
    }
}


