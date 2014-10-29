using System;
using System.Configuration;

namespace Erwine.Leonard.T.Toolkit.WebApp.CustomConfigurationSection
{
    public class NotificationEmailElementCollection : ConfigurationElementCollection
    {
        public const string Default_ElementName = "notificationEmails";

        public NotificationEmailConfigElement this[int index]
        {
            get { return base.BaseGet(index) as NotificationEmailConfigElement; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new NotificationEmailConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NotificationEmailConfigElement)element).Address;
        }
    }
}
