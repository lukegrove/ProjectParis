namespace ProjectParis.Combat.Ships
{
    public class WeaponLong : WeaponType
    {
        public WeaponLong()
        {
            SetRange(4f);
            SetDamage(2);
            SetFireRate(0.5f);
        }
    }
}