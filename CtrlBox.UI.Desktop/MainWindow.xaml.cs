using CtrlBox.Application.ViewModel;
using CtrlBox.UI.Desktop.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CtrlBox.UI.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WebApiMobile _apiMobile = null;

        public MainWindow()
        {
            InitializeComponent();

            try
            {

                _apiMobile = new WebApiMobile("http://localhost:53929", "Mobile");
                var routes = _apiMobile.GetRoutesAvailable(Guid.NewGuid());

                cboRoutes.ItemsSource = routes;
                cboRoutes.DisplayMemberPath = "Name";
                cboRoutes.SelectedValuePath = "DT_RowId";

            }
            catch
            {

            }
        }

        private void CboRoutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RouteVM route = (sender as ComboBox).SelectedItem as RouteVM;
            var boxes = _apiMobile.GetBoxesStockParents(new Guid(route.DT_RowId));

            grdTagsAvailable.ItemsSource = boxes;
            cboBoxTypes.ItemsSource = boxes.Select(x=>x.BoxType.Name).Distinct().ToList();   
        }
    }
}
