namespace ProjectParis.Combat.Ships
{
    public class WeaponType
    {
        public float Range { get; private set; }
        public float Damage { get; private set; }
        public float FireRate { get; private set; }

        protected void SetRange(float range)
        {
            Range = range;
        }

        protected void SetDamage(float damage)
        {
            Damage = damage;
        }

        protected void SetFireRate(float fireRate)
        {
            FireRate = fireRate;
        }
    }
}