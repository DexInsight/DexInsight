using CommunityToolkit.Mvvm.Messaging;
using DexInsights.Messages;
using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class FieldEntryView : ContentView, IRecipient<RevertEditFieldMessage>
{
	private FieldEntryViewModel fevm;
    public EventHandler<int> SelectForEdit;
    public event EventHandler<int> DeleteTapEvent;
    private bool _inEditMode = false;
    public FieldEntryView(FieldEntryViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
		this.fevm = model;

        WeakReferenceMessenger.Default.Register<RevertEditFieldMessage>(this);
    }

    private void DeleteTapped(object sender, TappedEventArgs e) {
        DeleteTapEvent?.Invoke(this, fevm.Id);
    }

    private void FieldTapped(object sender, TappedEventArgs e) {
        if (_inEditMode) return;
        _inEditMode = true;
        HandleConvertToEdit();
        WeakReferenceMessenger.Default.Send(new RevertEditFieldMessage(fevm.Id));
        SelectForEdit?.Invoke(this, fevm.Id);
    }

	private void HandleConvertToEdit() {
        DateAddedStack.Children.Clear();
        Entry addedEntry = new Entry();
        ChangeEntryAttributes(addedEntry);
        addedEntry.Text = fevm.DateAddedFormatted;

        DateAddedStack.Children.Add(addedEntry);

        DateSoldStack.Children.Clear();
        Entry soldEntry = new Entry();
        ChangeEntryAttributes(soldEntry);
        soldEntry.Text = fevm.DateSoldFormatted;

        DateSoldStack.Children.Add(soldEntry);

        BoughtByStack.Children.Clear();
        Entry boughtEntry = new Entry();
        ChangeEntryAttributes(boughtEntry);
        boughtEntry.Text = fevm.BoughtByFormatted;

        BoughtByStack.Children.Add(boughtEntry);

        PriceStack.Children.Clear();
        Entry priceEntry = new Entry();
        ChangeEntryAttributes(priceEntry);
        priceEntry.Text = fevm.Price.ToString();

        PriceStack.Children.Add(priceEntry);

        SizeStack.Children.Clear();
        Entry sizeEntry = new Entry();
        ChangeEntryAttributes(sizeEntry);
        sizeEntry.Text = fevm.Size.ToString();

        SizeStack.Children.Add(sizeEntry);

        CountyStack.Children.Clear();
        Entry countyEntry = new Entry();
        ChangeEntryAttributes(countyEntry);
        countyEntry.Text = fevm.County;

        CountyStack.Children.Add(countyEntry);

        CityStack.Children.Clear();
        Entry cityEntry = new Entry();
        ChangeEntryAttributes(cityEntry);
        cityEntry.Text = fevm.City;

        CityStack.Children.Add(cityEntry);

        AddressStack.Children.Clear();
        Entry addressEntry = new Entry();
        ChangeEntryAttributes(addressEntry);
        addressEntry.Text = fevm.AddressFormatted;

        AddressStack.Children.Add(addressEntry);
    }

    private void ChangeEntryAttributes(Entry entry) {
        entry.FontFamily = "Arial";
        entry.FontSize = 23;
        entry.TextColor = Colors.White;
        entry.VerticalTextAlignment = TextAlignment.Center;
        entry.HorizontalTextAlignment = TextAlignment.Center;
    }

    public void Receive(RevertEditFieldMessage message) {
        if (message.Value != fevm.Id && _inEditMode) {
            _inEditMode = false;
            HandleRevertFromEdit();
        }
    }

    private void HandleRevertFromEdit() {
        DateAddedStack.Children.Clear();
        Label addedLabel = new Label();
        ChangeLabelAttributes(addedLabel);
        addedLabel.Text = fevm.DateAddedFormatted;

        DateAddedStack.Children.Add(addedLabel);

        DateSoldStack.Children.Clear();
        Label soldLabel = new Label();
        ChangeLabelAttributes(soldLabel);
        soldLabel.Text = fevm.DateSoldFormatted;

        DateSoldStack.Children.Add(soldLabel);

        BoughtByStack.Children.Clear();
        Label boughtLabel = new Label();
        ChangeLabelAttributes(boughtLabel);
        boughtLabel.Text = fevm.BoughtByFormatted;

        BoughtByStack.Children.Add(boughtLabel);

        PriceStack.Children.Clear();
        Label priceLabel = new Label();
        ChangeLabelAttributes(priceLabel);
        priceLabel.Text = fevm.Price.ToString();

        PriceStack.Children.Add(priceLabel);

        SizeStack.Children.Clear();
        Label sizeLabel = new Label();
        ChangeLabelAttributes(sizeLabel);
        sizeLabel.Text = fevm.Size.ToString();

        SizeStack.Children.Add(sizeLabel);

        CountyStack.Children.Clear();
        Label countyLabel = new Label();
        ChangeLabelAttributes(countyLabel);
        countyLabel.Text = fevm.County;

        CountyStack.Children.Add(countyLabel);

        CityStack.Children.Clear();
        Label cityLabel = new Label();
        ChangeLabelAttributes(cityLabel);
        cityLabel.Text = fevm.City;

        CityStack.Children.Add(cityLabel);

        AddressStack.Children.Clear();
        Label addressLabel = new Label();
        ChangeLabelAttributes(addressLabel);
        addressLabel.Text = fevm.AddressFormatted;

        AddressStack.Children.Add(addressLabel);
    }

    private void ChangeLabelAttributes(Label label) {
        label.FontFamily = "Arial";
        label.FontSize = 20;
        label.TextColor = Colors.White;
        label.VerticalTextAlignment = TextAlignment.Center;
        label.HorizontalTextAlignment = TextAlignment.Center;
    }
}