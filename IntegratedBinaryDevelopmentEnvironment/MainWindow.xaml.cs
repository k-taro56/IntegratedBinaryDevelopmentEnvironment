using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IntegratedBinaryDevelopmentEnvironment;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private bool _isNavigationViewItemChangedFromCode;

    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
    }

    private void NavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (_isNavigationViewItemChangedFromCode)
        {
            _isNavigationViewItemChangedFromCode = false;
            return;
        }

        if (args.IsSettingsSelected)
        {

        }
        else
        {
            NavigationViewItem selectedItem = (NavigationViewItem)args.SelectedItem;

            if (selectedItem.Tag is Type type)
            {
                ContentFrame.Navigate(type);
            }
        }
    }

    private void NavigationViewBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (ContentFrame.CanGoBack)
        {
            _isNavigationViewItemChangedFromCode = true;
            ContentFrame.GoBack();

            foreach (object item in NavigationViewControl.MenuItems)
            {
                if (item is NavigationViewItem navigationViewItem && navigationViewItem.Tag is Type type && type == ContentFrame.SourcePageType)
                {
                    NavigationViewControl.SelectedItem = item;
                    return;
                }
            }
        }
    }
}
