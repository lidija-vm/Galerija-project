using LidijaDjakSport;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galerija
{
    class Album
    {
        #region Polja

        private ChromeDriver driver;
        private string adresa;
        private Helper helper;
        private List<string> imena; 
        private List<string> putanje;
        #endregion

        #region Svojstva

        public List<string> Imena { get { return imena; } set { imena = value; } }
        public List<string> Putanje { get { return putanje; } set { putanje = value; } }

        #endregion
        #region Konstruktori
        public Album()
        {
            driver = new ChromeDriver();
            adresa = "https://unsplash.com/wallpapers/nature";
            helper = new Helper();
            imena = new List<string>();
            putanje = new List<string>();

        }
        public void otvoriStranicu()
        {
            driver.Url = adresa;
        }
        public void nasumicneFotografije()
        {

            IWebElement listaFotografija = driver.FindElement(By.XPath("//*[@id='app']/div/div[4]"));
            IWebElement[] fotografije = listaFotografija.FindElements(By.ClassName("_1tO5-")).ToArray();

            int fotografijeDuzina = fotografije.Length;

            Random random = new Random();

            int[] nasumicneSlike = new int[3];

            for (int i = 0; i < 3; i++)
            {
                nasumicneSlike[i] = random.Next(0, fotografijeDuzina);
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var prvaSlika = fotografije[nasumicneSlike[0]];
            var drugaSlika = fotografije[nasumicneSlike[1]];
            var trecaSlika = fotografije[nasumicneSlike[2]];

            List<IWebElement> lista = new List<IWebElement>() {prvaSlika, drugaSlika, trecaSlika};
           
            for (int i = 0; i < lista.Count; i++)
            {
                helper.actionsClick(driver, lista[i]);
               
                wait.Until(driver => lista[i].Displayed);
                Thread.Sleep(3000);
                IWebElement divSlika = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[4]/div/div/div[1]/div[1]/header/div[1]/span/div[2]/span/div/a"));
                string slikaIme = divSlika.Text;

                IWebElement divAdresa = driver.FindElement(By.XPath("/html/body/div[4]/div/div/div[4]/div/div/div[1]/div[3]/div/div/button/div[2]/div[2]/div"));
                string poslednjaAdresa = divAdresa.FindElement(By.TagName("img")).GetAttribute("srcset");
                string ispisiAdresu;
                if (poslednjaAdresa != "undefined")
                {
                    ispisiAdresu = poslednjaAdresa.Split(',').Last();              
                }
                else
                {
                   ispisiAdresu = divAdresa.FindElement(By.TagName("img")).GetAttribute("src");
                    
                }
                Imena.Add(slikaIme);
                Putanje.Add(ispisiAdresu);

                Console.WriteLine(slikaIme);
                Thread.Sleep(4000);
                driver.Navigate().Back();
            }
            
        }

        #endregion
    }

}
      