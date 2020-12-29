using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
namespace uWin
{
    public class uWinJson
    {
        public Setting[] Settings { get; set; }
        public class Setting
        {
            public string ProcessName { get; set; }
            public string Place { get; set; }
        }

        public class Place
        {
            public const string Left = "left";
            public const string Right = "right";
        }

    }


    class uJson
    {
        public uWinJson json { get; set; }

        //  設定Jsonの読み込み
        public Boolean Retrieve()
        {
            Boolean result = false;
            String text = File.ReadAllText("setting\\setting.json");
            json = JsonSerializer.Deserialize<uWinJson>(text);

            result = true;
            return result;
        }


    }
}
