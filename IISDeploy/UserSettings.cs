using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISDeploy
{
    public class UserSettings
    {
        public List<webSetting> webSettings { get; set; } = new List<webSetting>();
    }

    public class webSetting
    {
        public string webName { get; set; } = string.Empty;
        public string gitUrl { get; set; } = string.Empty;
        public string buildStrategy { get; set; } = string.Empty;
    }
}
