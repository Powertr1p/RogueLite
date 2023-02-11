namespace Loot
{
    public class ExpCrystal : LootBase
    {
        public override void Consume()
        {
            Destroy(gameObject);
        }
    }
}