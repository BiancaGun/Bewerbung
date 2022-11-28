using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampfsimulator
{
    public enum ERace
    {
        Orc = 1, Troll, Goblin
    }

    class Mob
    {
        public ERace Race;
        public float MonHP, MonDP, MonAP, MonS;
        public static int Rounds;

        public Mob(ERace race, float monHP, float monDP, float monAP, float monS)
        {
            this.Race = race;
            this.MonHP = monHP;
            this.MonDP = monDP;
            this.MonAP = monAP;
            this.MonS = monS;
        }
        public static void SpeedCheck() //Überprüfung ob die Monster den gleichen Speed haben + "Münzwurf"
        {
            Random rnd = new Random();
            if (Program.Monster1.MonS == Program.Monster2.MonS)
            {
                Console.WriteLine($"{Program.Monster1.Race} and {Program.Monster2.Race} got the same speed. A coin toss will pick, who starts.");
                int num = rnd.Next(1, 51);
                if (num % 2 == 0)
                {
                    Program.Monster1.MonS = 1;
                    Program.Monster2.MonS = 0;
                }
                else if (num % 2 != 0)
                {
                    Program.Monster1.MonS = 0;
                    Program.Monster2.MonS = 1;
                }
            }
        }
        public static void Attack() //Attack-Methode + Dmg-Überprüfung und Rundenzähler
        {
            while (Program.Monster1.MonHP > 0 || Program.Monster2.MonHP > 0)
            {
                if (Program.Monster1.MonS > Program.Monster2.MonS)
                {
                    float dmg = Program.Monster1.MonAP - Program.Monster2.MonDP;
                    if (dmg <= 0)
                    {
                        dmg = 0;
                    }
                    Program.Monster2.MonHP -= dmg;
                    Rounds++;

                    if (Program.Monster2.MonHP <= 0)
                    {
                        break;
                    }

                    dmg = Program.Monster2.MonAP - Program.Monster1.MonDP;
                    if (dmg <= 0)
                    {
                        dmg = 0;
                    }
                    Program.Monster1.MonHP -= dmg;
                    Rounds++;
                }

                if (Program.Monster2.MonS > Program.Monster1.MonS)
                {
                    float dmg = Program.Monster2.MonAP - Program.Monster1.MonDP;
                    if (dmg <= 0)
                    {
                        dmg = 0;
                    }
                    Program.Monster1.MonHP -= dmg;
                    Rounds++;

                    if (Program.Monster1.MonHP <= 0)
                    {
                        break;
                    }

                    dmg = Program.Monster1.MonAP - Program.Monster2.MonDP;
                    if (dmg <= 0)
                    {
                        dmg = 0;
                    }
                    Program.Monster2.MonHP -= dmg;
                    Rounds++;
                }
            }
        }
    }
}

