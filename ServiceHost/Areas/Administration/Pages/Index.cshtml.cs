using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        public List<DataSetItem> BarDataSet { get; set; }
        public List<DataSetItem> DoughnutDataSet { get; set; }

        public void OnGet()
        {
            BarDataSet = new List<DataSetItem>
            {
                new DataSetItem
                {
                    Label = "ورود به انبار",
                    Data = new List<int> {10, 65, 0, 5, 65, 75, 30, 10, 90, 60, -10, 15},
                    BorderColor = new List<string> {"#7209b7"},
                    BackgroundColor = new List<string> {"#7209b7"},
                    BorderWidth =  1,
                    BorderRadius = 7,
                },
                new DataSetItem
                {
                    Label = "فروش",
                    Data = new List<int> {8, 60, 30, 50, 25, 25, 70, 60, 60, 20, 40, 35},
                    BorderColor = new List<string> {"#3f37c9"},
                    BackgroundColor = new List<string> {"#3f37c9"},
                    BorderWidth =  1,
                    BorderRadius = 7,
                },
            };


            DoughnutDataSet = new List<DataSetItem>
            {
                new DataSetItem
                {
                    Label = "فروش",
                    Data = new List<int> {10, 65, 0, 5, 65, 75, 30, 10, 90, 60, -10, 15},
                    BorderColor = new List<string> {"#fbf8cc", "#fde4cf", "#ffcfd2", "#f1c0e8", "#cfbaf0", "#a3c4f3", "#90dbf4", "#8eecf5", "#98f5e1", "#b9fbc0", "#89b0ae", "#bee3db"},
                    BackgroundColor = new List<string> { "#fbf8cc", "#fde4cf", "#ffcfd2", "#f1c0e8", "#cfbaf0", "#a3c4f3", "#90dbf4", "#8eecf5", "#98f5e1", "#b9fbc0", "#89b0ae", "#bee3db"},
                },
            };
        }
    }


    public class DataSetItem
    {
        [JsonProperty(propertyName: "label")]
        public string Label { get; set; }

        [JsonProperty(propertyName: "data")]
        public List<int> Data { get; set; }

        [JsonProperty(propertyName: "backgroundColor")]
        public List<string> BackgroundColor { get; set; }

        [JsonProperty(propertyName: "borderColor")]
        public List<string> BorderColor { get; set; }

        [JsonProperty(propertyName: "borderWidth")]
        public int BorderWidth { get; set; }

        [JsonProperty(propertyName: "borderRadius")]
        public int BorderRadius { get; set; }
    }
}