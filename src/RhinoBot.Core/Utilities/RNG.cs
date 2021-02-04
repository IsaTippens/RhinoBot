//The R in RNG stands for Rhino
using System;
using System.Security.Cryptography;

namespace RhinoBot.Core.Utilities 
{
    public static class Randomiser
    {
        public static Random RNG = new Random();

        public static int NextInt(int max) {
            return RandomNumberGenerator.GetInt32(max);
        }
    }
}