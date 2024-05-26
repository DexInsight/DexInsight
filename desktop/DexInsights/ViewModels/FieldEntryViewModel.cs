using CommunityToolkit.Mvvm.ComponentModel;
using DexInsights.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexInsights.ViewModels {
    public partial class FieldEntryViewModel : ObservableObject {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private DateTime date_added;

        [ObservableProperty]
        private DateTime date_sold;

        [ObservableProperty]
        private List<string> boughtBy;

        [ObservableProperty]
        private int price;

        [ObservableProperty]
        private int size;

        [ObservableProperty]
        private string county;

        [ObservableProperty]
        private string city;

        [ObservableProperty]
        private List<string> address;

        [ObservableProperty]
        private string addedBy;

        public FieldEntryViewModel(DbHouse house) {
            this.id = house.GetId();
            this.date_added = house.GetDateAdded();
            this.date_sold = house.GetDateSold();
            this.boughtBy = house.GetBoughtBy();
            this.price = house.GetPrice();
            this.size = house.GetSize();
            this.county = house.GetCounty();
            this.city = house.GetCity();
            this.address = house.GetAddress();
            this.addedBy = house.GetAddedBy();
        }

        public string DateAddedFormatted => Date_added.ToString("yyyy-MM-dd");

        public void OnDateAddedChanged() {
            OnPropertyChanged(nameof(DateAddedFormatted));
        }

        public string DateSoldFormatted => Date_sold.ToString("yyyy-MM-dd");

        public void OnDateSoldChanged() {
            OnPropertyChanged(nameof(DateSoldFormatted));
        }

        public string BoughtByFormatted {
            get {
                if (string.Join(", ", BoughtBy).Length < 27) {
                    return string.Join(", ", BoughtBy);
                }
                return string.Join(", ", BoughtBy).Substring(0, 27) + "...";
            }
        }

        public void OnBoughtByChanged() {
            OnPropertyChanged(nameof(BoughtByFormatted));
        }

        public string AddressFormatted {
            get {
                if (string.Join(", ", Address).Length < 20) {
                    return string.Join(", ", Address);
                }
                return string.Join(", ", Address).Substring(0, 20) + "...";
            }
        }

        public void OnAddressChanged() {
            OnPropertyChanged(nameof(AddressFormatted));
        }
    }
}
