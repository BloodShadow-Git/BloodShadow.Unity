using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace BloodShadow.Unity
{
    public static class ConsoleToDebug
    {
        [ExecuteAlways]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Redirect()
        {
            Debug.Log("Redirecting console output to unity");
            Console.SetOut(new UnityTextWriter());
        }

        private class UnityTextWriter : TextWriter
        {
            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(string value)
            {
                base.Write(value);
                Debug.Log(value);
            }
        }
    }
}
