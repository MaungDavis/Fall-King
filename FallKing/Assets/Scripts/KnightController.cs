using Knight.Command;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    private IKnightCommand left;
    private IKnightCommand right;

    void Start()
    {
        this.left = ScriptableObject.CreateInstance<StrafeCharacterLeft>();
        this.right = ScriptableObject.CreateInstance<StrafeCharacterRight>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            this.right.Execute(this.gameObject);
        }

        if (Input.GetAxis("Horizontal") < -0.01)
        {
            this.left.Execute(this.gameObject);
        }
    }
}