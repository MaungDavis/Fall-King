using UnityEngine;

namespace Knight.Command
{
    public class StrafeCharacterLeft : ScriptableObject, IKnightCommand
    {
        private float speed = 3.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();

            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(-this.speed, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}