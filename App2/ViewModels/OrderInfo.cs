﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Models;
using App2.Services;
using App2.Views;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Stoly = App2.Models.Stoly;

namespace App2.ViewModels
{
   public class OrderInfo:BaseViewModel
    {
        public Xamarin.Forms.Command LoadItemsCommand { get; set; }
        public ObservableCollection<Models.Stoly> Stolies { get; set; }
        public ObservableCollection<Models.Polozka> Polozkas { get; set; }
        public Order Order { get; set; }

        public Stoly stoly { get; set; }
        public OrderInfo(Stoly table=null)

        {
            Order = table.Orders.FirstOrDefault();
             
                LoadItemsCommand = new Xamarin.Forms.Command(async () => await GetTask());
        }
             async Task GetTask()
        {
            if (IsBusy)
                return;

            OrdersForTablesDataStorecs ordersForTablesDataStorec = new OrdersForTablesDataStorecs();
            var s = await ordersForTablesDataStorec.GetPolozkasAsync(Order);

            foreach (var item in s)
            {
                Polozkas.Add(item);
            }
            IsBusy = false;
      


        }




    }
}
