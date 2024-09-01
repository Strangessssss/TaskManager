using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskManagaerBeta.UserControls;

public partial class TaskBar : UserControl
{
    public TaskBar()
    {
        InitializeComponent();
    }
    
    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            ToggleWindowState();
        }
        else
        {
            var app = Application.Current.MainWindow;
            app?.DragMove();
        }
    }

    private void MinimizeWindow(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow != null) 
            Application.Current.MainWindow.Close();
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow != null) 
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }
    
    private void ToggleWindowState()
    {
        var app = Application.Current.MainWindow;
        if (app is { WindowState: WindowState.Maximized })
        {
            app.WindowState = WindowState.Normal;
        }
        else
        {
            if (app != null) app.WindowState = WindowState.Maximized;
        }
    }
    
}