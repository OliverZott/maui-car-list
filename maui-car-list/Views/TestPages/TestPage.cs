namespace maui_car_list.Views.TestPages;

public class TestPage : ContentPage
{
    int count = 0;
    Label lblH1 = new Label();
    Label lblCounter = new Label();

    public TestPage()
    {
        var scrollView = new ScrollView();
        var stackLayout = new StackLayout();
        scrollView.Content = stackLayout;

        lblH1 = new Label
        {
            Text = "TestPage C# Version :)",
            Style = (Style)Application.Current.Resources["Headline"],
        };
        SemanticProperties.SetHeadingLevel(lblH1, SemanticHeadingLevel.Level1);
        stackLayout.Children.Add(lblH1);

        lblCounter = new Label
        {
            Text = "Count: 0",
            FontSize = 22,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
        };

        stackLayout.Children.Add(lblCounter);

        var btnCounter = new Button
        {
            Text = "Click to count",
            HorizontalOptions = LayoutOptions.Center,
        };

        stackLayout.Children.Add(btnCounter);

        this.Content = scrollView;

        btnCounter.Clicked += OnClickedEvent;
    }

    private void OnClickedEvent(object? sender, EventArgs e)
    {
        count++;
        lblCounter.Text = $"Click Count: {count}";

        SemanticScreenReader.Announce(lblCounter.Text);
    }
}