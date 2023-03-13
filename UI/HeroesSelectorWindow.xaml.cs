using HellHades.ArtifactExtractor.Models;
using RaidCatalog.Logic.ViewModels;
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
using System.Windows.Shapes;

namespace RaidCatalog {
    public partial class HeroesSelectorWindow : Window {

        public HeroesViewModel Model {
            get => (HeroesViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public HeroesSelectorWindow(List<HeroWrapper> heroes, HeroWrapper hero) {
            InitializeComponent();

            this.Model = new HeroesViewModel(heroes) { 
                SelectedHero = hero
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
