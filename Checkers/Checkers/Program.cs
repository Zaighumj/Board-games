using System;

namespace Checkers
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Checkers game = new Checkers())
            {
                game.Run();
            }
        }
    }
#endif
}

