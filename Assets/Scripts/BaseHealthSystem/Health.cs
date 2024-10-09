using System;

public class Health : BaseHeroPoints
{
    private void FixedUpdate()
    {
        if (Value == 0)
            Destroy(gameObject);
    }
}
