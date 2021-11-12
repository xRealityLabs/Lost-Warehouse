using System;
using System.Collections.Generic;

namespace LostWarehouse.ProviderManager
{

    public class ProviderLocator : LocatorBase
    {
        private static ProviderLocator s_instance;

        public static ProviderLocator Get
        {
            get
            {
                if (s_instance == null)
                    s_instance = new ProviderLocator();
                return s_instance;
            }
        }

        protected override void FinishConstruction()
        {
            s_instance = this;
        }
    }
}