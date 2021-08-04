using System;
using System.Collections.Generic;
using System.Text;

namespace FoccoEmFrente.Kanban.Api.Configurations
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
