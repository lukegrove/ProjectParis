using System.Collections.Generic;

namespace ProjectParis.Combat.Ships
{
    public class ShipCommand : Ship
    {
        public ShipCommand()
        {
            SetShipType(ShipType.Command);
            SetMaxSpeed(10f);
            SetAcceleration(10f);
            SetTurnSpeed(1f);
            SetHull(100f);
            SetWeaponLimit(3f);
            List<WeaponType> loadout = new()
            {
                new WeaponLong(),
                new WeaponLong(),
                new WeaponLong()
            };
            SetWeaponLoadout(loadout);
        }
    }
}