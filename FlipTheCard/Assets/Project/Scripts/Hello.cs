using Engine;
using Engine.Attributes;

public class Hello : Script
{
    [Editable]
    public string message = "Hello, World!";

    public override void Start()
    {
        Log.Info(message);
    }
}