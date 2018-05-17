using System.Collections.Generic;
namespace Simulator
{
    class ShipSubType : ShipType
    {
        private List<ShipType> favoriteTargets;
        private List<ShipType> worstEnnemies;

        public List<ShipType> FavoriteTargets { get => favoriteTargets; set => favoriteTargets = value; }
        public List<ShipType> WorstEnnemies { get => worstEnnemies; set => worstEnnemies = value; }

        public ShipSubType(string name, int number, List<ShipType> favoriteTargets, List<ShipType> worstEnnemies) : base(name, number,0)
        {
            FavoriteTargets = favoriteTargets;
            WorstEnnemies = worstEnnemies;
        }   
    }
}