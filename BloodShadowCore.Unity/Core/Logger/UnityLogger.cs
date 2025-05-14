namespace BloodShadow.Unity.Core.Logger
{
    using System;
    using UnityEngine;

    public class UnityLogger : BloodShadow.Core.Logger.Logger
    {
        public override string ColorReadLine(object? mess, ConsoleColor BC, ConsoleColor FC) { ColorWriteLine(mess, BC, FC); return ""; }
        public override string ColorReadLine(ConsoleColor BC, ConsoleColor FC) { ColorWriteLine(BC, FC); return ""; }
        public override string ColorRead(object? mess, ConsoleColor BC, ConsoleColor FC) { ColorWrite(mess, BC, FC); return ""; }
        public override void ColorWriteLine(object? mess, ConsoleColor BC, ConsoleColor FC) { Debug.Log(mess); }
        public override void ColorWriteLine(ConsoleColor BC, ConsoleColor FC) { Debug.Log(""); }
        public override void ColorWrite(object? mess, ConsoleColor BC, ConsoleColor FC) { Debug.Log(mess); }
    }
}
