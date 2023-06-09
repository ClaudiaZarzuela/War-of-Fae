﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarOfFae
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Inventory : Page, INotifyPropertyChanged
    {
        public ObservableCollection<ViewPowerUp> ListaPowerUps { get; } = new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPowerUp> ListaPowerUpsElem { get; } = new ObservableCollection<ViewPowerUp>();
        public string Info = " ";

        public event PropertyChangedEventHandler PropertyChanged;

        public Inventory()
        {
            this.InitializeComponent();
            Info = "Info sobre el powerup";
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaPowerUps != null)
            {
                foreach (PowerUps dron in Model.GetAllDrones())
                {
                    ViewPowerUp VMitem = new ViewPowerUp(dron);
                    ListaPowerUps.Add(VMitem);
                }
            }
            if (ListaPowerUps != null)
            {
                foreach (PowerUps dron in Model.GetAllDrones2())
                {
                    ViewPowerUp VMitem = new ViewPowerUp(dron);
                    ListaPowerUpsElem.Add(VMitem);
                }
            }
            base.OnNavigatedTo(e);
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

            //ViewPowerups o = e.ClickedItem as ViewPersonajes;
            ViewPowerUp p = e.ClickedItem as ViewPowerUp;
            Info = p.Explicacion;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));

        }
    }
}
