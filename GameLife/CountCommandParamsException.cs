using System;

namespace GameLife
{

    class CountCommandParamsException : SystemException
    {
        public enum Comprasion { MORE, EQUALS }
        public CountCommandParamsException(string command, int expected, int received, Comprasion comprasion = Comprasion.EQUALS)
            : base($"{(command == null ? "" : command + ": ")}expected {(comprasion == Comprasion.MORE ? ">" : "")}{expected} parametrs, " +
                  $"but get {received}")
        {

        }
        public CountCommandParamsException(int expected, int received, Comprasion comprasion = Comprasion.EQUALS)
            : this(null, expected, received, comprasion)
        {

        }
    }
}
