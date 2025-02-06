

namespace AdventureGame
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("Hi");

            Player player = new Player();
            player.Health = 100;

            while (player.Health > 0)
            {
                // Player Heal
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.A)
                {
                    Console.WriteLine(player.Heal(100));
                }
                // Player TakeDamage
                if (keyInfo.Key == ConsoleKey.S)
                {
                    Console.WriteLine(player.TakeDamage(10));
                }
                // Player HealPotion
                if (keyInfo.Key == ConsoleKey.D)
                {
                    HealthPotion healPotion = new HealthPotion();
                    healPotion.PotionIntensity = 45;
                    Console.WriteLine(healPotion.AffectPlayer(player));
                }
                // Player DamagePotion
                if (keyInfo.Key == ConsoleKey.F)
                {
                    DamagePotion damagePotion = new DamagePotion();
                    damagePotion.PotionIntensity = 45;
                    Console.WriteLine(damagePotion.AffectPlayer(player));
                }


                while (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(intercept: true);
                }
            }
            Console.WriteLine("You lose.");
        }
    }
}

