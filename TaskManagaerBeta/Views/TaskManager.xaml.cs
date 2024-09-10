using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagaerBeta.Messages;

namespace TaskManagaerBeta.Views;


public partial class TaskManager : Window
{
    public TaskManager()
    {
        InitializeComponent();
    }

    private void HideShow(object sender, RoutedEventArgs e)
    {
        if (StackPanel.HorizontalAlignment == HorizontalAlignment.Center)
        {
            LeftPanel.Width = new GridLength(0.4, GridUnitType.Star);
            StackPanel.HorizontalAlignment = HorizontalAlignment.Right;
            return;
        }
        LeftPanel.Width = new GridLength(0.1, GridUnitType.Star);
        StackPanel.HorizontalAlignment = HorizontalAlignment.Center;
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new SearchChanged());
    }
}