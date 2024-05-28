using CommunityToolkit.Mvvm.Messaging;
using DexInsights.Database;
using DexInsights.DataModels;
using DexInsights.Messages;
using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class FieldsView : ContentView {
    private static List<DbHouse> houses;
    private ThemeViewModel tvm;
    private int editedId;
    private int tableIndex = 0;
    private string orderedById = "";
    private int orderedDir = 1;
    private DbUser _user;
    public FieldsView(ThemeViewModel model, DbUser user)
	{
        InitializeComponent();
        BindingContext = model;
        this.tvm = model;
        this._user = user;

        AnimateAppearing();
        GetHouses();
        ChangeHousesTable(0);
        ChangeIdToOrderedByAtStart();
        PopulateNextEntry();
    }

    private void ChangeIdToOrderedByAtStart() {
        ((Border)((Grid)((HorizontalStackLayout)GetId).Parent).Parent).BackgroundColor = Color.FromArgb("1F1F1F");
    }

    private void AnimateAppearing() {
        var animation = new Animation {
        { 0, 1, new Animation(v => ((Frame)ContentFrame).TranslationY = v, 200, 0) }
    };
        animation.Commit(((Frame)ContentFrame), "AnimateTranslation", length: 200);
    }

    private void PopulateNextEntry() {
        NextEntryId.Text = (houses.Max(house => house.GetId()) + 1).ToString();
        NextEntryId.IsEnabled = false;

        AddedBy.Text = _user.GetName();
        AddedBy.IsEnabled = false;
    }

    private void GetHouses() {
        houses = ManagementHandler.GetHouses();
    }

    //private void PopulateTable() {
    //    for (int i = 0; i < 9; i++) {
    //        FieldEntryView fieldEntryView = new FieldEntryView(new FieldEntryViewModel(houses[i]));
    //        FieldsTable.Children.Add(fieldEntryView);
    //        fieldEntryView.SelectForEdit += OnSelectForEdit;
    //    }
    //}

    private void PageArrow_PointerEnter(object sender, PointerEventArgs e) {
        ((Label)sender).TextColor = tvm.ThemeColor2;
    }

    private void PageArrow_PointerExit(object sender, PointerEventArgs e) {
        ((Label)sender).TextColor = tvm.ThemeColor;
    }

    private void PageLeftArrow_Tapped(object sender, TappedEventArgs e) {
        if (RightArrow.IsVisible == false) {
            RightArrow.IsVisible = true;
        }
        ChangeHousesTable(-1);
    }

    private void PageRightArrow_Tapped(object sender, TappedEventArgs e) {
        if (LeftArrow.IsVisible == false) {
            LeftArrow.IsVisible = true;
        }
        ChangeHousesTable(1);
    }

     /*
     * ================================
     * Directions:
     * 1 - Forward, -1 - Backwards, 0 - Initialize, 2 - BanMember, 3 - AddMember
     * ================================
     */

    private void ChangeHousesTable(int dir) {
        if (dir != 3) FieldsTable.Children.Clear();
        //if (users.Count == 0) {
        //    SetNoMemberLayout();
        //    return;
        //}

        int startLength;
        int endLength;

        if (dir == 0 && tableIndex * 9 + 9 >= houses.Count) RightArrow.IsVisible = false;
        if (dir == 1) {
            tableIndex++;
            if (tableIndex * 9 + 9 >= houses.Count) RightArrow.IsVisible = false;
        } else if (dir == -1) {
            tableIndex--;
            if (tableIndex == 0) LeftArrow.IsVisible = false;
        }

        if (tableIndex * 9 + 9 >= houses.Count) {
            endLength = houses.Count;
            startLength = tableIndex * 9;
        } else {
            endLength = tableIndex * 9 + 9;
            startLength = tableIndex * 9;
        }

        if (dir == 2) {
            if (tableIndex * 9 + 9 >= houses.Count) RightArrow.IsVisible = false;
            if (LeftArrow.IsVisible && startLength == endLength) {
                ChangeHousesTable(-1);
                return;
            }
        }

        if (dir == 3 && !RightArrow.IsVisible) {
            if (FieldsTable.Children.Count == 9) {
                RightArrow.IsVisible = true;
                return;
            }
            startLength = houses.Count - 1;
            endLength = houses.Count;
        } else if (dir == 3) {
            return;
        }

        for (int i = startLength; i < endLength; i++) {
            FieldEntryView fieldEntryView = new FieldEntryView(new FieldEntryViewModel(houses[i]));
            fieldEntryView.OnEdit += OnEditEvent;
            fieldEntryView.DeleteTapEvent += OnFieldRemove;
            FieldsTable.Children.Add(fieldEntryView);
        }
    }

    private void OnEditEvent(object sender, DbHouse house) {
        houses[house.GetId()] = house;
        ManagementHandler.SaveHouses(houses);
    }

    private void OnFieldRemove(object sender, int id) {
        int index = houses.FindIndex(x => x.GetId() == id);
        houses.RemoveAt(index);
        ChangeHousesTable(2);
        ManagementHandler.SaveHouses(houses);
        PopulateNextEntry();
    }

    private void OrderTapped(object sender, TappedEventArgs e) {
        string methodId = ((HorizontalStackLayout)sender).StyleId;
        if (orderedById != "" && methodId != orderedById) ((Border)((Grid)((HorizontalStackLayout)FindByName(orderedById)).Parent).Parent).BackgroundColor = Colors.Transparent;
        if (orderedById != methodId) {
            orderedDir = -1;
        }

        if (methodId == "GetBoughtBy" || methodId == "GetAddress") {
            if (orderedDir == 1) {
                houses = houses.OrderByDescending(house => ((List<string>)DbHouse.GetMethodValue(house, methodId))[0]).ToList();
                orderedById = methodId;
                orderedDir = -1;
            } else {
                houses = houses.OrderBy(house => ((List<string>)DbHouse.GetMethodValue(house, methodId))[0]).ToList();
                orderedById = methodId;
                orderedDir = 1;
            }
        } else {
            if (orderedDir == 1) {
                houses = houses.OrderByDescending(house => DbHouse.GetMethodValue(house, methodId)).ToList();
                orderedById = methodId;
                orderedDir = -1;
            } else {
                houses = houses.OrderBy(house => DbHouse.GetMethodValue(house, methodId)).ToList();
                orderedById = methodId;
                orderedDir = 1;
            }
        }

        ((Border)((Grid)((HorizontalStackLayout)sender).Parent).Parent).BackgroundColor = Color.FromArgb("1F1F1F");
        ChangeHousesTable(0);
    }

    private void NewSaveTapped(object sender, TappedEventArgs e) {
        houses.Add(new DbHouse(int.Parse(NextEntryId.Text), DateTime.Parse(NextAdded.Text), DateTime.Parse(NextSold.Text), NextBoughtBy.Text.Split(",").ToList(), int.Parse(NextPrice.Text), int.Parse(NextSize.Text), NextCounty.Text, NextCity.Text, NextAddress.Text.Split(",").ToList(), AddedBy.Text));
        ManagementHandler.SaveHouses(houses);
        ChangeHousesTable(3);

        ClearNewEntryRow();
    }

    private void ClearNewEntryRow() {
        PopulateNextEntry();
        NextAdded.Text = "";
        NextSold.Text = "";
        NextBoughtBy.Text = "";
        NextPrice.Text = "";
        NextSize.Text = "";
        NextCounty.Text = "";
        NextCity.Text = "";
        NextAddress.Text = "";
    }
}