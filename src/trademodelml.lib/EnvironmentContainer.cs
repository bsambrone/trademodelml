using System;
using Microsoft.ML.Runtime.Data;

namespace trademodelml.lib
{
    public sealed class EnvironmentContainer
    {
        static readonly LocalEnvironment _instance = new LocalEnvironment();
        public static LocalEnvironment Instance
        {
            get
            {
                return _instance;
            }
        }
        EnvironmentContainer()
        {
            
        }
    }
}