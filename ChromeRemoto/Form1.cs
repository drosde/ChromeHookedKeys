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

        // DLL libraries used to manage hotkeys
        globalKeyboardHook gkh = new globalKeyboardHook();        

        [DllImport("user32.dll")] //Used for sending keystrokes to new window.

        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Ansi)] //Used to find the window to send keystrokes to.

        public static extern IntPtr FindWindow(string className, string windowName);

        // Utilidades
        private string presionadas = "";

        private bool called, cooldownKeys = false;

        Dictionary<string, Action> COMBINACIONES = new Dictionary<string, Action>();        

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

        public void interactuarSpotify(string action, bool pausaRepr = false)
        {
            if (driver == null)
            {
                return;
            }

            string titulo = action;

            if(action == "PausarReproducir")
            {
                //bool encontrado = opt_title == null ? false : true;

                titulo = pausaRepr ? "Pausar" : "Reproducir";
                
            }else if (action == "Siguiente"){
                //
            }


            try
            {
                By byXpath = By.XPath("//button[contains(@title, '" + titulo + "')]");

                IWebElement a = driver.FindElement(byXpath);
                Console.WriteLine("Elemento {0} encontrad.", titulo);

                if (a != null)
                {
                    a.Click();
                }
            }
            catch (NoSuchElementException)
            {
                if (!pausaRepr && action == "PausarReproducir")
                {
                    Console.WriteLine("Llamando de nuevo a la fx pausarRepr -> {0}", pausaRepr);

                    interactuarSpotify(action, true);
                }

                Console.WriteLine("Elemento {0} no existe", titulo);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var _gkh = gkh.getInstance();

            _gkh.setKeys(System.Windows.Forms.Keys.MediaNextTrack); //This collects the MediaNextTrack Key.

            _gkh.setKeys(System.Windows.Forms.Keys.MediaPreviousTrack); //This collects the ... Key.

            _gkh.setKeys(System.Windows.Forms.Keys.MediaPlayPause); //This collects the ... Key.

            _gkh.setKeys(System.Windows.Forms.Keys.F2); // Volume down

            _gkh.setKeys(System.Windows.Forms.Keys.F3); // Volume up

            _gkh.KeyDown += new KeyEventHandler(gkh_KeyDown); //Event for pressing the key down.

            _gkh.KeyUp += new KeyEventHandler(gkh_KeyUp); //Event for the release of key.

            _gkh.hook();

            // ... Media controls keys
            COMBINACIONES.Add("MediaPlayPause", () => { interactuarSpotify("PausarReproducir"); });
            COMBINACIONES.Add("MediaNextTrack", () => { interactuarSpotify("Siguiente"); });
            COMBINACIONES.Add("MediaPreviousTrack", () => { interactuarSpotify("Anterior"); });

            // ... Volume controls keys
        }

        //What happens on key release.
        void gkh_KeyUp(object sender, KeyEventArgs e){
            presionadas += e.KeyCode.ToString();

            lstLog.Items.Add(e.KeyCode.ToString());

            foreach (KeyValuePair<string, Action> entry in COMBINACIONES)
            {
                Console.WriteLine("indice de {0} en {1} es -> {2}", entry.Key, presionadas, presionadas.IndexOf(entry.Key)); 
                if (presionadas.IndexOf(entry.Key) >= 0)
                {
                    entry.Value.Invoke();

                    presionadas = presionadas.Remove(presionadas.IndexOf(entry.Key), entry.Key.Length);
                }
            }
    
            e.Handled = false; //Setting this to true will cause the global hotkeys to block all outgoing keystrokes.
        }

        //What happens on key press.
        void gkh_KeyDown(object sender, KeyEventArgs e){

            e.Handled = false;
        }
        
        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            IniciarChrome();
        }
    }
}
