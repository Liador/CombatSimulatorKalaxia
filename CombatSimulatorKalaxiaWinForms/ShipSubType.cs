using System.Collections.Generic;
namespace Simulator
{
    class ShipSubType : ShipType
    {
        private List<ShipType> favoriteTargets;

        public List<ShipType> FavoriteTargets { get => favoriteTargets; set => favoriteTargets = value; }

        public ShipSubType(string name, int number, List<ShipType> favoriteTargets) : base(name, number)
        {
            FavoriteTargets = favoriteTargets;
        }   
    }
}