using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Erwine.Leonard.T.Toolkit.Text
{
    public class EmailAddress
    {
        public string Login { get; private set; }
        public string Address { get; private set; }
        public string DisplayName { get; private set; }

        private const string LoginPattern = @"\G((?<login>([^=;\\]+|\\.)*)(?<!\\)=)?";
        private const string DisplayNamePattern = @"(?<displayName>([^<>;\\]+|(?<!\\)""([^""]+|\\.)*(?<!\\)""|\\.)*?)";
        private const string AddressPattern1 = @"(?<!\\)<(?<address>([^<>\\]+|\\.)*)(?<!\\)>";
        private const string AddressPattern2 = @"(?<address>([^<>;\\\s]+|\\.)*)";
        private const string TerminusPattern1 = @"((?<!\\);|$)";
        private static readonly Regex Pattern = new Regex(String.Format(@"{0}{1}({2}|{3}){4}", LoginPattern, DisplayNamePattern, AddressPattern1, AddressPattern2,
            TerminusPattern1));
        private static readonly Regex ParseLoginPattern = new Regex(@"^(?<login>([^<>;\\@]+|\\.)*)(@(?<domain>([^<>;\\]+|\\.)*))?$");
        private static readonly Regex EscapePattern = new Regex(@"\G\\(.)");
        /// <summary>
        /// login="display name" <account@domain.com>; 
        /// </summary>
        /// <param name="emailSetting"></param>
        /// <returns></returns>
        public static EmailAddress[] ParseEmailAddresses(string emailSetting)
        {
            if (emailSetting == null)
                return new EmailAddress[0];
            string s = Pattern.ToString();
            MatchCollection matches = Pattern.Matches(emailSetting);
            return matches.OfType<Match>()
                .Select(m => new EmailAddress((m.Groups["login"].Success) ? m.Groups["login"].Value : null, m.Groups["displayName"].Value, m.Groups["address"].Value))
                .Where(e => e.Login.Length > 0).ToArray();
        }

        public EmailAddress(string login, string displayName, string address)
        {
            this.Login = (login == null) ? "" : this.Unescape(login).Trim();
            this.DisplayName = (displayName == null) ? "" : this.Unescape(displayName).Trim();
            this.Address = (address == null) ? "" : this.Unescape(address).Trim();

            if (this.Login.Length > 0)
            {
                if (this.Address.Length == 0)
                    this.Address = this.Login;

                if (this.DisplayName.Length == 0)
                    this.DisplayName = this.Login;

                return;
            }

            if (this.Address.Length == 0)
                return;

            Match m = ParseLoginPattern.Match(address);
            this.Login = (m.Success) ? this.Unescape(m.Groups["login"].Value.Trim()) : this.Address;

            if (this.DisplayName.Length == 0)
                this.DisplayName = this.Address;
        }

        private string Unescape(string displayName)
        {
            return EscapePattern.Replace(displayName, (Match m) => m.Groups[1].Value);
        }
    }
}
