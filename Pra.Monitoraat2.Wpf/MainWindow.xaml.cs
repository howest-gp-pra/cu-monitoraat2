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
using Pra.Monitoraat2.Core.Entities;
using Pra.Monitoraat2.Core.Services;

namespace Pra.Monitoraat2.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool isNew;
        LawnMowerService lawnMowerService;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lawnMowerService = new LawnMowerService();
            ViewDefault();
            ClearDetails();
            PopulateLawnMowers();
        }
        private void ViewDefault()
        {
            grpDetails.IsEnabled = false;
            grpMowers.IsEnabled = true;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        private void ViewNewEdit()
        {
            grpDetails.IsEnabled = true;
            grpMowers.IsEnabled = false;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }
        private void ClearDetails()
        {
            txtBrand.Text = "";
            txtSeries.Text = "";
            chkHasEngine.IsChecked = false;
            txtBladeWidth.Text = "";
            txtSalesPrice.Text = "";
        }
        private void PopulateDetails(LawnMower lawnMower)
        {
            txtBrand.Text = lawnMower.Brand;
            txtSeries.Text = lawnMower.Series;
            chkHasEngine.IsChecked = lawnMower.HasEngine;
            txtBladeWidth.Text = lawnMower.BladeWidth.ToString();
            txtSalesPrice.Text = lawnMower.SalesPrice.ToString("0.00");
        }
        private void PopulateLawnMowers()
        {
            lstMowers.ItemsSource = null;
            lstMowers.ItemsSource = lawnMowerService.LawnMowers;
        }

        private void lstMowers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearDetails();
            if (lstMowers.SelectedItem == null)
                return;
            else
                PopulateDetails((LawnMower)lstMowers.SelectedItem);

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDetails();
            ViewNewEdit();
            isNew = true;
            txtBrand.Focus();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstMowers.SelectedItem == null)
                return;
            ViewNewEdit();
            isNew = false;
            txtBrand.Focus();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstMowers.SelectedItem == null)
                return;
            if(lawnMowerService.DeleteLawnMower((LawnMower)lstMowers.SelectedItem))
            {
                ClearDetails();
                PopulateLawnMowers();
                lstMowers.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Something went terribly wrong ...");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LawnMower lawnMower;
            if(isNew)
            {
                lawnMower = new LawnMower();
            }
            else
            {
                lawnMower = (LawnMower)lstMowers.SelectedItem;
            }
            try
            {
                lawnMower.Brand = txtBrand.Text;
                lawnMower.Series = txtSeries.Text;
                lawnMower.HasEngine = (bool)chkHasEngine.IsChecked;
                int.TryParse(txtBladeWidth.Text, out int bladewidth);
                lawnMower.BladeWidth = bladewidth;
                // oppassen met decimals ! decimaalsymbool moet , zijn
                txtSalesPrice.Text = txtSalesPrice.Text.Replace(".", ",");
                decimal.TryParse(txtSalesPrice.Text, out decimal salesPrice);
                lawnMower.SalesPrice = salesPrice;
            }
            catch
            {
                MessageBox.Show("Something terribly wrong with your input");
                return;
            }
            if(isNew)
            {
                if (!lawnMowerService.AddLawnMower(lawnMower))
                {
                    MessageBox.Show("Something went terribly wrong : mower could not be added");
                    return;
                }
            }
            else
            {
                if(!lawnMowerService.UpdateLawnMower(lawnMower))
                {
                    MessageBox.Show("Something went terribly wrong : mower could not be updated");
                    return;
                }
            }
            PopulateLawnMowers();
            ViewDefault();
            lstMowers.SelectedItem = lawnMower;
            lstMowers_SelectionChanged(null, null);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ViewDefault();
            ClearDetails();
            lstMowers_SelectionChanged(null, null);
        }


    }
}
