using UnityEngine;

namespace Knight.Command
{
    public interface IKnightCommand
    {
        void Execute(GameObject gameObject);
    }
}