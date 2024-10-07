using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    private bool _isVampire;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxisRaw(Horizontal);

        if (Input.GetKeyDown(KeyCode.W))
            _isJump = true;

        if (Input.GetKeyDown(KeyCode.F))
            _isVampire = true;
    }

    public bool GetIsJump() =>
        GetBoolAsTrigger(ref _isJump);

    public bool GetIsVampire() =>
        GetBoolAsTrigger(ref _isVampire);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;

        return localValue;
    }
}
