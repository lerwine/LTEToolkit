using System;
using System.Configuration;

namespace Erwine.Leonard.T.Toolkit.WebApp.CustomConfigurationSection
{
    public class RoleGroupMappingElementCollection : ConfigurationElementCollection
    {
        public const string Default_ElementName = "roleGroupMappings";

        public RoleGroupMappingConfigElement this[int index]
        {
            get { return base.BaseGet(index) as RoleGroupMappingConfigElement; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RoleGroupMappingConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RoleGroupMappingConfigElement)element).RoleText;
        }
    }
}
