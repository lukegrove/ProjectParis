using System.Collections.Generic;

namespace ProjectParis.Combat.Ships
{
    public class ShipCorvette : Ship
    {
        public ShipCorvette()
        {
            SetShipType(ShipType.Corvette);
            SetMaxSpeed(5f);
            SetAcceleration(2f);
            SetTurnSpeed(4f);
            SetHull(5f);
            SetWeaponLimit(1f);
            List<WeaponType> loadout = new()
            {
                new WeaponShort()
            };
            SetWeaponLoadout(loadout);
        }
    }
}