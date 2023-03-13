using RaidCatalog.Logic.ViewModels;
using System;
using System.Windows;

namespace RaidCatalog {
    public partial class SetSelectorWindow : Window {

        public SetsViewModel Model {
            get => (SetsViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public SetSelectorWindow(SetWrapper set) {
            InitializeComponent();

            this.Model = new SetsViewModel(MainViewModel.Instance.Sets) { 
                SelectedSet = set
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        private void Window_Closed(object sender, EventArgs e) {

        }

        private void Ok_OnClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
