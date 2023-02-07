using MainApplication.PluginBase.Interfaces;
using MainApplication.PluginBase.Models;
using System;

namespace pgMathSum
{
    public class MathStuff : IPlugin, IMath
    {
        public string PluginName => "IdoMath";
        public string PluginDescription => "I can do Math =)";

        public bool CanDoMath => true; // yes, I can

        public bool Initialize(InitParams initParams)
        {
            // do my stuff
            return true; // I initialized correctly and can continue
        }


        public void ExecuteTaks()
        {
            Console.WriteLine("First integer Number: ");
            string strN1 = Console.ReadLine();
            Console.WriteLine("Second integer Number: ");
            string strN2 = Console.ReadLine();

            // I do not care ..
            int.TryParse(strN1, out int n1);
            int.TryParse(strN2, out int n2);

            Console.WriteLine($"{n1} + {n2} = {n1 + n2}");
            Console.WriteLine($"{n1} * {n2} = {n1 * n2}");


            Console.WriteLine("Thanks, exiting...");
        }


    }

}
