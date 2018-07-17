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
using Microsoft.WindowsAPICodePack.Taskbar;

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
        private Utilities.Utilidades utilidades;

        private bool iniciado = false;

        Dictionary<string, Action> COMBINACIONES = new Dictionary<string, Action>();

        //Boton play Taskbar
        ThumbnailToolBarButton play_button;
        bool play_button_enabled = true;

        // Cookies set up
        Stack<Cookie> cookies = new Stack<Cookie>();
        static int count_cook = 0;
        string cookie_url;

        public Form1()
        {
            InitializeComponent();
        }

        #region in-browser methods
        public void IniciarChrome()
        {
            if (iniciado) return;
            
            ChromeOptions options = new ChromeOptions();

            if (chb_adblock.Checked){
                options.AddExtension("ubloc.crx");
            }

            driver = new ChromeDriver(options);

            try
            {
                driver.Navigate().GoToUrl("https://open.spotify.com");
            }catch(WebDriverException e)
            {
                MessageBox.Show("Hubo un error al conectar a la pagina web...");
            }


            //Thread.Sleep(4000);

            var task = Task.Run(() => setCookies(driver));
            if (task.Wait(TimeSpan.FromSeconds(10))){
                driver = task.Result;
                executeScript_txt();
            }
            else{
                throw new Exception("Timed out");
            }


            iniciado = true;
        }

        public IWebDriver setCookies(IWebDriver driver)
        {
            DateTime t = DateTime.Now;
            t = t.AddHours(1);

            foreach (Cookie c in cookies) {
                Cookie c_spo = new Cookie(c.Name, c.Value, cookie_url, "/", t);
                driver.Manage().Cookies.AddCookie(c_spo);
            }

            driver.Navigate().Refresh();

            return driver;
        }

        public void interactuarSpotify(string action, bool pausaRepr = false)
        {
            if (driver == null || action == String.Empty) return;

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
                else
                {
                    Console.WriteLine("Elemento {0} no existe", titulo);
                }
            }

        }

        #endregion

        #region key events
        private void SetKeysListener()
        {
            var _gkh = gkh.getInstance();

            var a = System.Windows.Forms.Keys.MediaNextTrack;

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
            // Controlling the volume is almost impossible, the extensions can not take the events produced in js. 
            // Chrome either have something to control this.
            // Spotify verify if the event to handle the volume has the property "isTrusted" as true, 
            // each mouse event generated by script is marked as false.
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

        #endregion

        #region form methods
        private void Form1_Load(object sender, EventArgs e)
        {
            SetKeysListener();

            utilidades = new Utilities.Utilidades();

            play_button = AgregarBotonTaskBar("play-track.ico", btn_play_pause_taskbar_click, "Reproducir");
            var next = AgregarBotonTaskBar("next-track.ico", (o, s) => { interactuarSpotify("Siguiente"); }, "Siguiente");
            var prev = AgregarBotonTaskBar("prev-track.ico", (o, s) => { interactuarSpotify("Anterior"); }, "Anterior");

            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(this.Handle, prev, play_button, next);
        }

        void btn_play_pause_taskbar_click(object sender, ThumbnailButtonClickedEventArgs e)
        {
            play_button_enabled = !play_button_enabled;

            string p = string.Format(@"{0}\Assets\Icons\{1}", Application.StartupPath, play_button_enabled ? "play-track.ico" : "pause-track.ico");
            play_button.Icon = new Icon(p);
            play_button.Tooltip = play_button_enabled ? "Reproducir" : "Pausar";

            interactuarSpotify("PausarReproducir");
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            if (iniciado)
            {
                driver.Quit();
                iniciado = false;
            }

            IniciarChrome();

            btn_iniciar.Text = iniciado ? "Reiniciar" : "Iniciar";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(driver != null)
            {
                var res = MessageBox.Show("¿Cerrar el navegador también?", "Cerrar", MessageBoxButtons.YesNo);
                if(res == DialogResult.Yes)
                {
                    driver.Quit();
                }
            }
        }
        #endregion

        ThumbnailToolBarButton AgregarBotonTaskBar(string path_icon, EventHandler<ThumbnailButtonClickedEventArgs> handler_event, 
            string title, bool enabled = true)
        {
            string path = string.Format(@"{0}\Assets\Icons\{1}", Application.StartupPath, path_icon);

            Icon i = new Icon(path);

            ThumbnailToolBarButton new_button = new ThumbnailToolBarButton(i, title);
            new_button.Enabled = enabled;
            new_button.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(handler_event);
            
            return new_button;
        }

        public void executeScript_txt()
        {
            if (txt_script.Text != String.Empty && txt_script.Text != "Ejecutar este script despues de iniciada la pagina")
            {
                IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)driver;
                var a = scriptExecutor.ExecuteScript(txt_script.Text);
                Console.WriteLine(a);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool continuar = true;
            foreach(Control a in panel1.Controls)
            {
                if(a is TextBox){
                    if (a.Text == "Nombre" || a.Text == "Valor" || a.Text == String.Empty)
                    {
                        continuar = false;
                    }
                }
            }

            if (!continuar) return;

            TextBox name = new TextBox
            {
                Text = "Nombre",
                Name = "txt_name_" + count_cook,
                Location = new Point(0, 30 * count_cook)
            };

            TextBox value = new TextBox
            {
                Text = "Valor",
                Name = "txt_value_" + count_cook,
                Location = new Point(name.Width + 15, 30 * count_cook)
            };

            name.Enter += (o, s) => { if (o is TextBox obj) obj.Text = ""; }; 
            value.Enter += (o, s) => { if (o is TextBox obj) obj.Text = ""; };

            Button btn_guardar = new Button();

            btn_guardar.Click += new EventHandler((o, s) => {
                if((name.Text != String.Empty && name.Text != "Nombre") && 
                (value.Text != String.Empty && value.Text != "Valor")) { 
                    cookies.Push(new Cookie(name.Text, value.Text));
                    name.Enabled = value.Enabled = btn_guardar.Enabled = false;
                }
            });
            btn_guardar.Text = "Guardar";
            btn_guardar.Location = new Point((name.Width + value.Width) + btn_guardar.Width / 3, 30 * count_cook);

            panel1.Controls.Add(name);
            panel1.Controls.Add(value);
            panel1.Controls.Add(btn_guardar);

            count_cook++;
        }

        private void txt_url_Leave(object sender, EventArgs e)
        {
            Uri uriResult;
            bool valid_url = Uri.TryCreate(txt_url.Text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (valid_url)
            {
                cookie_url = "." + utilidades.GetDomainFromUrl(txt_url.Text);
                Console.WriteLine("Url para cookies: " + cookie_url);
                groupBox1.Enabled = btn_iniciar.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = btn_iniciar.Enabled = false;
            }
        }

        private void txt_script_Enter(object sender, EventArgs e)
        {
            txt_script.Text = "";
        }

        //isAd = false;
        /* */
    }
}
