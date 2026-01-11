using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public interface IStateChanger
    {
        void ChangeState(State state);
    }
}