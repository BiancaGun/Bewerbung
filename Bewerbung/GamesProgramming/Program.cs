using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampfsimulator
{
    class Program
    {
        static int i;
        static bool mobCheck = true;
        public static int Race;
        public static float MonHP, MonDP, MonAP, MonS;
        public static Mob Monster1;
        public static Mob Monster2;
        public static bool WinMon1;
        public static bool WinMon2;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Monsterfightsimulator");
            Console.WriteLine("You need to pick your Monsters and their stats!");
            for (i = 1; i <= 2; i++) // for-Schleife für die Zuweisung beider Monster
            {
                MobPick();
                MonHP = FloatCheck($"Type in monster {i} healpoints (hp).");
                MonDP = FloatCheck($"Type in monster {i} defencepoints (dp).");
                MonAP = FloatCheck($"Type in monster {i} attackpoints (ap).");
                MonS = FloatCheck($"Type in monster {i} attackspeed (s).");
                if (i == 1)
                {
                    Monster1 = new Mob((ERace)Race, MonHP, MonDP, MonAP, MonS);
                }
                else if (i == 2)
                {
                    Monster2 = new Mob((ERace)Race, MonHP, MonDP, MonAP, MonS);
                }
                Console.Clear();
            }
            Mob.SpeedCheck();
            Mob.Attack();
            WinCheck();
            StatsOut();

            Console.ReadKey();
        }
        public static float FloatCheck(string _message) //Input-Überprüfung der float-Werte
        {
            do
            {
                Console.WriteLine(_message);
                if (float.TryParse(Console.ReadLine(), out float result))
                    return result;
                else
                {
                    Console.Clear();
                    Console.WriteLine("A float number is needed, try it again!");
                }
            } while (true);
        }
        public static void MobPick() //Input-Überprüfung und Verhinderung das sich gleiche Monster bekämpfen
        {
            mobCheck = true;
            while (mobCheck)
            {
                Console.Clear();
                Console.WriteLine($"Pick the number for your monster {i} \n 1: Orc \n 2: Troll \n 3: Goblin");
                if (Int32.TryParse(Console.ReadLine(), out Race) && Race >= 1 && Race <= 3)
                {
                    switch (i)
                    {
                        case 1:
                            mobCheck = false;
                            break;
                        case 2:
                            if (Monster1.Race != (ERace)Race)
                            {
                                mobCheck = false;
                            }
                            else
                            {
                                Console.WriteLine("Wrong number, try again!");
                                Console.Clear();
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Wrong number, try again!");
                    Console.Clear();
                }
            }
        }

        static void WinCheck() //Überprüft ob ein Monster gestorben ist und wer der Gewinner ist
        {
            if (Monster2.MonHP <= 0)
            {
                WinMon1 = true;
            }
            if (Monster1.MonHP <= 0)
            {
                WinMon2 = true;
            }
        }
   

        public static void StatsOut() //Gibt die Werte aus, wie sie am Ende des Kampfes sind.
        {
            Console.WriteLine("Monster 1 Rasse: " + Monster1.Race);
            Console.WriteLine("Monster 1 HP: " + Monster1.MonHP);
            Console.WriteLine("Monster 1 DP: " + Monster1.MonDP);
            Console.WriteLine("Monster 1 AP: " + Monster1.MonAP);
            Console.WriteLine("Monster 1 S: " + Monster1.MonS);
            Console.WriteLine("Monster 2 Rasse: " + Monster2.Race);
            Console.WriteLine("Monster 2 HP: " + Monster2.MonHP);
            Console.WriteLine("Monster 2 DP: " + Monster2.MonDP);
            Console.WriteLine("Monster 2 AP: " + Monster2.MonAP);
            Console.WriteLine("Monster 2 S: " + Monster2.MonS);
            Console.WriteLine("Rundenanzahl: " + Mob.Rounds);
            if (WinMon1 == true)
            {
                Console.WriteLine(Monster1.Race + " Wins!");
            }
            if (WinMon2 == true)
            {
                Console.WriteLine(Monster2.Race + " Wins!");
            }
        }
    }
}
