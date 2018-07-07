using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.Remote;
namespace ChromeRemoto
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;

        struct Combinacion
        {
            public string Name { get; set; }
            public Action Callback { get; set; }
        }

        // DLL libraries used to manage hotkeys
        globalKeyboardHook gkh = new globalKeyboardHook();        

        [DllImport("user32.dll")] //Used for sending keystrokes to new window.

        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Ansi)] //Used to find the window to send keystrokes to.

        public static extern IntPtr FindWindow(string className, string windowName);

        private string presionadas = "";

        public List<string> temp_keys = new List<string>();

        private bool called = false;

        private bool reproduciendo = false;

        //IDictionary<Combinacion> COMBINACIONES = new Dictionary<Combinacion>();
        Dictionary<string, Action> COMBINACIONES = new Dictionary<string, Action>();

        //public 

        public Form1()
        {
            InitializeComponent();
        }

        public void IniciarChrome()
        {
            if (called)
            {
                return;
            }

            ChromeOptions options = new ChromeOptions();
            options.AddExtension("ubloc.crx");

            driver = new ChromeDriver(options);
            
            driver.Navigate().GoToUrl("https://open.spotify.com");


            //Thread.Sleep(4000);

            var task = Task.Run(() => setCookie(driver));
            if (task.Wait(TimeSpan.FromSeconds(10)))
                driver = task.Result;
            else
                throw new Exception("Timed out");


            called = true;
        }

        public IWebDriver setCookie(IWebDriver driver)
        {
            DateTime t = DateTime.Now;
            t = t.AddHours(1);
            Cookie c_spo = new Cookie("sp_dc", "AQCSAKQj-mmsxjhGds1OoB7VzMkN2TgLuBh1CHSvv5M7IoNNpPNiRN37ILO0zK5FcREGuC8TRy44PfSuGCZiQOjg", ".spotify.com", "/" , t);
            driver.Manage().Cookies.AddCookie(c_spo);

            driver.Navigate().Refresh();

            return driver;
        }

        public void reproducirSpotify()
        {
            if(driver == null)
            {
                return;
            }

            // Obtener el pause / play button and click()
            try
            {
                By byXpath = By.XPath("//button[contains(@title, 'Reproducir')]");

                var a = driver.FindElement(byXpath);
                Console.WriteLine("Elemento reproducir encontrado");
                if (a != null)
                {
                    a.Click();
                }
                else
                {
                    Console.WriteLine("Elemento reproducir es nulo");
                }
            }
            catch (NoSuchElementException)
            {
                MessageBox.Show("Elemento reproducir no existe");
            }
        }

        public void pausarSpotify()
        {
            if (driver == null)
            {
                return;
            }

            // Obtener el pause / play button and click()
            try
            {
                By byXpath = By.XPath("//button[contains(@title, 'Pausar')]");

                var a = driver.FindElement(byXpath);
                Console.WriteLine("Elemento pausar encontrado");
                if (a != null)
                {
                    a.Click();
                }
                else
                {
                    Console.WriteLine("Elemento pausar es nulo");
                }
            }
            catch (NoSuchElementException)
            {
                MessageBox.Show("Elemento pausar no existe");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var _gkh = gkh.getInstance();

            _gkh.setKeys(System.Windows.Forms.Keys.S);//This collects the A Key.

            _gkh.setKeys(System.Windows.Forms.Keys.P);//This collects the B Key.

            _gkh.setKeys(System.Windows.Forms.Keys.O);//This collects the C Key.

            _gkh.KeyDown += new KeyEventHandler(gkh_KeyDown); //Event for pressing the key down.

            _gkh.KeyUp += new KeyEventHandler(gkh_KeyUp); //Event for the release of key.

            _gkh.hook();

            COMBINACIONES.Add("SPO", IniciarChrome);
            COMBINACIONES.Add("POS", reproducirSpotify);
            COMBINACIONES.Add("PSO", pausarSpotify);
        }

        //What happens on key release.
        void gkh_KeyUp(object sender, KeyEventArgs e){
            
            // Eliminar la letra anterior si es la misma a esta
            /*if(presionadas.Length > 1)
            {
                var i = presionadas.IndexOf(e.KeyCode.ToString());
                if (i >= 0 && i == presionadas.Length - 1)
                {
                    presionadas.Remove(i);
                }
            }*/

            // Una vez encontrado eliminar esa key de presionada
            if (!temp_keys.Contains(e.KeyCode.ToString()))
            {                
                presionadas += e.KeyCode.ToString();

                lstLog.Items.Add(e.KeyCode.ToString());

                temp_keys.Add(e.KeyCode.ToString());

                foreach (KeyValuePair<string, Action> entry in COMBINACIONES)
                {
                    Console.WriteLine("indice de "+ entry.Key + " en "+ presionadas + " es -> " + presionadas.IndexOf(entry.Key)); 
                    if (presionadas.IndexOf(entry.Key) >= 0)
                    {
                        Console.WriteLine("Encontra2 " + entry.Key);
                        entry.Value.Invoke();
                    }
                }

                /*if (comprobarIniciar())
                {
                    MessageBox.Show("LLAMAR AHORA");

                    
                }
                else if (comprobarReproducir())
                {
                    Console.WriteLine("Llamando reproducir");
                    boton_control(driver, true);
                }else if (comprobarPausar())
                {
                    Console.WriteLine("Llamando pausa");
                    boton_control(driver, false);
                }*/


                if (presionadas.Length > 10) presionadas = "";

                if (temp_keys.Count == 3)
                {
                    temp_keys.Clear();
                    lstLog.Items.Clear();
                }
            }

            e.Handled = false; //Setting this to true will cause the global hotkeys to block all outgoing keystrokes.
        }

        //What happens on key press.
        void gkh_KeyDown(object sender, KeyEventArgs e){
            //lstLog.Items.Add("Down\t" + e.KeyCode.ToString());
            //MessageBox.Show(e.KeyCode.ToString());            

            e.Handled = false;
        }

        public bool comprobarIniciar()
        {
            //bool ini = false;
            foreach (KeyValuePair<string, Action> entry in COMBINACIONES)
            {
                if(presionadas.IndexOf(entry.Key) > 0)
                {
                    Console.WriteLine("Encontra2 "+ entry.Key);
                    entry.Value.Invoke();
                }
            }

            
            bool s = false;
            bool o = false;
            bool p = false;

            if (temp_keys.Count >= 3)
            {
                s = temp_keys[0] == "S";
                p = temp_keys[1] == "P";
                o = temp_keys[2] == "O";
            }

            return s && o && p;

        }

        public bool comprobarPausar()
        {
            bool s = false;
            bool o = false;
            bool p = false;

            if (temp_keys.Count >= 3)
            {
                s = temp_keys[0] == "P";
                p = temp_keys[1] == "S";
                o = temp_keys[2] == "O";
            }

            return s && o && p;
        }

        public bool comprobarReproducir()
        {
            bool s = false;
            bool o = false;
            bool p = false;

            if (temp_keys.Count >= 3)
            {
                s = temp_keys[0] == "P";
                p = temp_keys[1] == "O";
                o = temp_keys[2] == "S";
            }

            return s && o && p;
        }
    }
}
