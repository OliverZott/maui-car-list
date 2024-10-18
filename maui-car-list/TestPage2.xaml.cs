namespace maui_car_list;

public partial class TestPage2 : ContentPage
{
    private int count = 0;

    public TestPage2()
    {
        InitializeComponent();

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        count++;
        lblCounter.Text = $"Click Count: {count}";

        SemanticScreenReader.Announce(lblCounter.Text);
    }
}