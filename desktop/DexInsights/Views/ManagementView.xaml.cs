using DexInsights.Database;
using DexInsights.DataModels;
using DexInsights.ViewModels;

namespace DexInsights.Views;

public partial class ManagementView : ContentView
{
    private ThemeViewModel tvm;
    private List<DbUser> users;
    private int tableIndex = 0;
    private bool membersExtended = false;
    private bool addMemberAnimationIsFinished = true;
	public ManagementView(ThemeViewModel model)
	{
        InitializeComponent();
        BindingContext = model;
        this.tvm = model;

        AnimateAppearing();
        GetUsers();
        ChangeMembersTable(0);
    }

    private void AnimateAppearing() {
        var animation = new Animation {
        { 0, 1, new Animation(v => ((Frame)ContentFrame).TranslationY = v, 200, 0) }
    };
        animation.Commit(((Frame)ContentFrame), "AnimateTranslation", length: 200);
    }

    private void GetUsers() {
        users = ManagementHandler.GetUsers();
    }

    private void AddMember_PointerEnter(object sender, PointerEventArgs e) {
        Button button = (Button)sender;
        var animation = new Animation {
            { 0, 1, new Animation(v => button.BackgroundColor = tvm.Lighten(button.BackgroundColor, 0.07), start: 0, end: 10) },
        };

        animation.Commit(button, "AnimateColor", length: 50);
    }

    private void AddMember_PointerExit(object sender, PointerEventArgs e) {
        Button button = (Button)sender;
        var animation = new Animation {
        { 0, 1, new Animation(v => button.BackgroundColor = tvm.ThemeColor2, 0.3, 0) },
    };

        animation.Commit(button, "AnimateColor", length: 150);
    }

    private void AddMember_Tapped(object sender, TappedEventArgs e) {
        if (!addMemberAnimationIsFinished) return;
        if (membersExtended) {
            CollapseMembersLayout();
            AddMemberSpace.Children.Clear();
            return;
        }
        ExpandMembersLayout();
        AddNewMemberView();
    }

    private async void ExpandMembersLayout() {
        addMemberAnimationIsFinished = false;
        var animation = new Animation {
        { 0, 1, new Animation(v => MemberLayout.HeightRequest = v, MemberLayout.HeightRequest, MemberLayout.HeightRequest + 80) }
    };

        animation.Commit(MemberLayout, "AnimateHeight", length: 150);
        await Task.Delay(150);
        addMemberAnimationIsFinished = true;
        membersExtended = true;
    }

    private async void CollapseMembersLayout() {
        addMemberAnimationIsFinished = false;
        var animation = new Animation {
        { 0, 1, new Animation(v => MemberLayout.HeightRequest = v, MemberLayout.HeightRequest, MemberLayout.HeightRequest - 80) }
    };

        animation.Commit(MemberLayout, "AnimateHeight", length: 150);
        await Task.Delay(150);
        addMemberAnimationIsFinished = true;
        membersExtended = false;
    }

    private void AddNewMemberView() {
        NewMemberView view = new NewMemberView(new ManagerMemberViewModel("", "User"));
        AddMemberSpace.Children.Add(view);
        view.NewMemberEvent += OnNewMemberAddEvent;
    }

    private void OnNewMemberAddEvent(object sender, string data) {
        string[] newUserData = data.Split(",");
        DbUser user = new DbUser(newUserData[0], newUserData[1], DateTime.Now);
        users.Add(user);

        if (users.Count == 1) SetMemberLayout();
        ChangeMembersTable(3);
        ManagementHandler.SaveUsers(users);
    }

    //private void PopulateMembersTable() {
    //    MembersList.Children.Clear();
    //    if (users.Count == 0) {
    //        SetNoMemberLayout();
    //        return;
    //    }

    //    if (tableIndex * 5 + 5 >= users.Count) RightArrow.IsVisible = false;

    //    for (int i = 0; i < 5; i++) {
    //        ManagerMemberView view = new ManagerMemberView(new ManagerMemberViewModel(users[i].GetName(), users[i].GetRole()));
    //        view.BanClickEvent += OnBanClickEvent;
    //        MembersList.Children.Add(view);
    //    }
    //}

    //private void RenderTable() {
    //    MembersList.Children.Clear();
    //    if (users.Count == 0) {
    //        SetNoMemberLayout();
    //        return;
    //    }

    //    int startLength = 0;
    //    int endLength = 5;

    //    for (int i = startLength; i < endLength; i++) {
    //        ManagerMemberView view = new ManagerMemberView(new ManagerMemberViewModel(users[i].GetName(), users[i].GetRole()));
    //        view.BanClickEvent += OnBanClickEvent;
    //        MembersList.Children.Add(view);
    //    }
    //}

    /*
     * ================================
     * Directions:
     * 1 - Forward, -1 - Backwards, 0 - Initialize, 2 - BanMember, 3 - AddMember
     * ================================
     */

    private void ChangeMembersTable(int dir) {
        if (dir != 3) MembersList.Children.Clear();
        if (users.Count == 0) {
            SetNoMemberLayout();
            return;
        }

        int startLength;
        int endLength;

        if (dir == 0 && tableIndex * 5 + 5 >= users.Count) RightArrow.IsVisible = false;
        if (dir == 1) {
            tableIndex++;
            if (tableIndex * 5 + 5 >= users.Count) RightArrow.IsVisible = false;
        } else if (dir == -1){
            tableIndex--;
            if (tableIndex == 0) LeftArrow.IsVisible = false;
        }

        if (tableIndex * 5 + 5 >= users.Count) {
            endLength = users.Count;
            startLength = tableIndex * 5;
        } else {
            endLength = tableIndex * 5 + 5;
            startLength = tableIndex * 5;
        }

        if (dir == 2) {
            if (tableIndex * 5 + 5 >= users.Count) RightArrow.IsVisible = false;
            if (LeftArrow.IsVisible && startLength == endLength) {
                ChangeMembersTable(-1);
                return;
            }
        }

        if (dir == 3 && !RightArrow.IsVisible) {
            if (MembersList.Children.Count == 5) {
                RightArrow.IsVisible = true;
                return;
            }
            startLength = users.Count - 1;
            endLength = users.Count;
        } else if (dir == 3) {
            return;
        }

        for (int i = startLength; i < endLength; i++) {
            ManagerMemberView view = new ManagerMemberView(new ManagerMemberViewModel(users[i].GetName(), users[i].GetRole()));
            view.BanClickEvent += OnBanClickEvent;
            view.SaveClickEvent += OnSaveClickEvent;
            MembersList.Children.Add(view);
        }
    }

    private void OnBanClickEvent(object sender, string name) {
        int index = users.FindIndex(x => x.GetName() == name);
        users.RemoveAt(index);
        ChangeMembersTable(2);
        ManagementHandler.SaveUsers(users);
    }

    private void OnSaveClickEvent(object sender, string data) {
        string[] editedUserData = data.Split(",");
        int index = users.FindIndex(x => x.GetName() == editedUserData[0]);
        DbUser editedUser = new DbUser(editedUserData[0], editedUserData[1], users[index].GetDate());
        users[index] = editedUser;
        ManagementHandler.SaveUsers(users);
    }

    private void SetNoMemberLayout() {
        NoMembersYet.IsVisible = true;
        MembersList.IsVisible = false;
    }

    private void SetMemberLayout() {
        NoMembersYet.IsVisible = false;
        MembersList.IsVisible = true;
    }

    private void MemberArrow_PointerEnter(object sender, PointerEventArgs e) {
        ((Label)sender).TextColor = tvm.ThemeColor2;
    }

    private void MemberArrow_PointerExit(object sender, PointerEventArgs e) {
        ((Label)sender).TextColor = tvm.ThemeColor;
    }

    private void MemberLeftArrow_Tapped(object sender, TappedEventArgs e) {
        if (RightArrow.IsVisible == false) {
            RightArrow.IsVisible = true;
        }
        ChangeMembersTable(-1);
    }

    private void MemberRightArrow_Tapped(object sender, TappedEventArgs e) {
        if (LeftArrow.IsVisible == false) {
            LeftArrow.IsVisible = true;
        }
        ChangeMembersTable(1);
    }
}