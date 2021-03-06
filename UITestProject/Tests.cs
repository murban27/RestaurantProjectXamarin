﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITestProject
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;
        public int Cena = 0;
        public int FinalPrice = 0;
        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [OneTimeSetUp]
        public void BeforeTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test,Order(1)]
        public void A_LoginPage()
        {
           var item = app.WaitForElement(c => c.Marked("UserLabel"));

            app.EnterText("UserLabel","cisnik");
            System.Threading.Thread.Sleep(200);
            app.EnterText("PassLabel","cisnik");
            app.Tap(x => x.Marked("Loginbtn"));

        }
        [Test, Order(2)]
        public void B_TablePage()
        {
           
            var items = app.WaitForElement(x => x.Marked("SyncfusionGrid"));
            app.DoubleTap("SyncfusionGrid Row3");


        }



        [Test, Order(3)]
        public void AddItemToOrder()

        {

            System.Threading.Thread.Sleep(2500);
            var TabView = app.WaitForElement("TabView").FirstOrDefault();//Najde TabView,vybere první element
            app.Tap(TabView.Id);//Klikne
            var resultString = Regex.Match(app.WaitForElement(x => x.Marked("LabelCena")).
                FirstOrDefault().Text, @"\d+").Value; //Načte cenu
            Cena = int.Parse(resultString);
            var items = app.WaitForElement(x => x.Marked("ImageClick"));//Element přidání do objednávky
            var item = items.FirstOrDefault();
            app.Tap(item.Id);
        }

        
        [Test, Order(4)]
        public void CheckTotalPrice()

        {
            System.Threading.Thread.Sleep(2600);//Uspi
            var resultString = Regex.Match(app.WaitForElement(x => x.Marked("LabelCena"))
                .FirstOrDefault().Text, @"\d+").Value;//Načti cenu
            FinalPrice = int.Parse(resultString);
            Assert.AreNotEqual(FinalPrice, Cena,"Cena se nezměnila!");//Pokud je cena stále stejná, vyhoď chybu

        }



        [Test, Order(5)]
        public void CommitOrder()

        {
            var item = app.WaitForElement(x => x.Marked("ConfirmOrder")).FirstOrDefault();
            app.Tap(item.Id);
        }

        [Test, Order(6)]
        public void ConfirmPayment()

        {
            var item = app.WaitForElement(x => x.Marked("Ano")).FirstOrDefault();
            app.Tap(item.Id);
        }

    }
}
