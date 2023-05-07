using CO2Bakalauras.Models;
using CO2Bakalauras.Services;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CO2Bakalauras.ViewModels
{
    public class CheckUsageViewModel : BindableObject
    {
        readonly Vartotojas vartotojas = ((App)App.Current).CurrentUser;
        private List<Sanaudos> usageList;
        private List<Sanaudos> usageListCopy;
        List<ChartEntry> entries = new List<ChartEntry>();
        private string selected;
        WebService web = new WebService();
        public string Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                FilterList();
                OnPropertyChanged();
            }
        }
        public List<Sanaudos> UsageList
        {
            get
            {
                return usageList;
            }
            set
            {
                usageList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Items { get; set; } = new ObservableCollection<string>
        {
            "Elektrą (kv/h)",
            "Šildymą (m3)",
            "Transportą (km)",
            "Vandenį (m3)",
            "Co2 sąnaudas (kg)"
        };

        private Chart chart;
        public Chart ChartView
        {
            get
            {
                return chart;
            }
            set
            {
                chart = value;
                OnPropertyChanged();
            }
        }


        public CheckUsageViewModel()
        {
            UsageList = new List<Sanaudos>();
            Load();
        }
        
        void FilterList()
        {
            if (selected == null)
                return;

            switch (Selected)
            {
                case "Elektrą (kv/h)":
                    ResetList();
                    foreach(Sanaudos u in UsageList)
                    {
                        entries.Add(new ChartEntry((float)u.ELEKTROS_SANAUDOS)
                        {
                            Label = u.DATA.Month.ToString(),
                            ValueLabel = u.ELEKTROS_SANAUDOS.ToString(),

                        });
                    }
                    ChartView = new LineChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };
                    break;

                case "Šildymą (m3)":
                    ResetList();
                    foreach (Sanaudos u in UsageList)
                    {
                        entries.Add(new ChartEntry((float)u.DUJU_SANAUDOS)
                        {
                            Label = u.DATA.Month.ToString(),
                            ValueLabel = u.DUJU_SANAUDOS.ToString(),

                        });
                    }
                    ChartView = new LineChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };
                    break;

                case "Transportą (km)":
                    ResetList();
                    foreach (Sanaudos u in UsageList)
                    {
                        entries.Add(new ChartEntry((float)u.AUTOMOBILIO_RIDA)
                        {
                            Label = u.DATA.Month.ToString(),
                            ValueLabel = u.AUTOMOBILIO_RIDA.ToString(),

                        });
                    }
                    ChartView = new LineChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };
                    break;

                case "Vandenį (m3)":
                    ResetList();
                    foreach (Sanaudos u in UsageList)
                    {
                        entries.Add(new ChartEntry((float)u.VANDENS_SANAUDOS)
                        {
                            Label = u.DATA.Month.ToString(),
                            ValueLabel = u.VANDENS_SANAUDOS.ToString(),

                        });
                    }
                    ChartView = new LineChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };
                    break;

                case "Co2 sąnaudas (kg)":
                    ResetList();
                    Load();
                    break;

                default:
                    return;

            }
        }
        
        private void ResetList()
        {
            entries.Clear();
        }

        async void Load()
        {
            if(UsageList.Count == 0)
                UsageList = await web.GetUserUsage(vartotojas.VARTOTOJO_ID);


            List<decimal> co2 = new List<decimal>();
            foreach (Sanaudos u in UsageList)
            {
                co2.Add(await SumProperties(u));
            }

            for(int i=0; i<co2.Count(); i++)
            {
                entries.Add(new ChartEntry((float)co2[i])
                {
                    Label = usageList[i].DATA.Month.ToString(),
                    ValueLabel = co2[i].ToString(),
                    
                });
            }

            ChartView = new LineChart { Entries = entries, LabelTextSize = 50, BackgroundColor = SKColors.Transparent };
        }


        async Task<decimal> SumProperties(Sanaudos sanaudos)
        {

            List<CO2> co = await web.GetCo2();
            CO2 co2 = co.OrderByDescending(o => o.PASKUTINIS_ATNAUJINIMAS).Take(1).FirstOrDefault();
            decimal auto = ((decimal)co2.AUTOMOBILIO_CO2 * (decimal)sanaudos.AUTOMOBILIO_RIDA) / 1000;
            decimal electr = ((decimal)co2.ELEKTROS_CO2 * (decimal)sanaudos.ELEKTROS_SANAUDOS) / 1000;
            decimal water = ((decimal)co2.VANDENS_CO2 * (decimal)sanaudos.VANDENS_SANAUDOS) / 1000;
            decimal gas = ((decimal)co2.DUJU_CO2 * (decimal)sanaudos.DUJU_SANAUDOS) / 1000;
            decimal sum = (auto + electr + water + gas);

            return sum;
        }

    }
}
