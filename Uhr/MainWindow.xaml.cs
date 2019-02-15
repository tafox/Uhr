using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Media;
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
using System.Windows.Threading;
using System.Threading;

namespace Uhr
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer timer;

        private DispatcherTimer Sekundentimer;
        private DispatcherTimer Minutentimer;
        private DispatcherTimer Viertelstundentimer;
        private DispatcherTimer Stundentimer;

        private MediaPlayer Sekundenabspieler;
        private MediaPlayer Minutenabspieler;
        private MediaPlayer Viertelstundenabspieler;
        private MediaPlayer Stundenabspieler;

        private string zwei_Sekundenton = @"C:\Users\foxthom\Documents\Visual Studio 2017\Projects\Uhr\Uhr\Töne\ticktock.wav";
        private string Minutenton = @"C:\Users\foxthom\Documents\Visual Studio 2017\Projects\Uhr\Uhr\Töne\click.wav";
        private string Viertelstundenton = @"C:\Users\foxthom\Documents\Visual Studio 2017\Projects\Uhr\Uhr\Töne\3-bells.wav";
        private string Stundenton = @"C:\Users\foxthom\Documents\Visual Studio 2017\Projects\Uhr\Uhr\Töne\churchbell2.wav";

        public MainWindow()
        {
            InitializeComponent();


            Sekundenabspieler = new MediaPlayer();
            Sekundenabspieler.Open(new Uri(zwei_Sekundenton));
            Sekundenabspieler.MediaEnded += Zwei_Sekunden_sind_um;

            Minutenabspieler = new MediaPlayer();
            Minutenabspieler.Open(new Uri(Minutenton));

            Viertelstundenabspieler = new MediaPlayer();
            Viertelstundenabspieler.Open(new Uri(Viertelstundenton));

            Stundenabspieler = new MediaPlayer();
            Stundenabspieler.Open(new Uri(Stundenton));

            Sekundentimer = new DispatcherTimer(DispatcherPriority.Render);
            Sekundentimer.Tick += Uhr_aktualisieren;
            Sekundentimer.Interval = new TimeSpan(0, 0, 1);

            Minutentimer = new DispatcherTimer(DispatcherPriority.Render);
            Minutentimer.Tick += Minute_abspielen;
            Minutentimer.Interval = new TimeSpan(0, 1, 0);

            Viertelstundentimer = new DispatcherTimer(DispatcherPriority.Render);
            Viertelstundentimer.Tick += Viertelstunde_abspielen;
            Viertelstundentimer.Interval = new TimeSpan(0, 15, 0);

            Stundentimer = new DispatcherTimer(DispatcherPriority.Render);
            Stundentimer.Tick += Stunde_abspielen;
            Stundentimer.Interval = new TimeSpan(1, 0, 0);

            timer = new System.Timers.Timer(1);
            timer.Elapsed += Timer_starten;
            timer.Start();
        }

        private void Timer_starten(object absender, EventArgs e)
        {
            if (DateTime.Now.Millisecond == 0)
            {
                Sekundenabspieler.Play();
                Minutentimer.Start();
                Viertelstundentimer.Start();
                Stundentimer.Start();
                Sekundentimer.Start();
                timer.Stop();
            }
        }

        private void Zwei_Sekunden_sind_um(object absender, EventArgs e)
        {
            Sekundenabspieler.Position = TimeSpan.Zero;
            Sekundenabspieler.Play();
        }

        private void Stunde_abspielen(object absender, EventArgs e)
        {
            int Schläge = DateTime.Now.Hour;
            for (int i = 0; i < Schläge; i++)
            {
                Stundenabspieler.Play();
                Thread.Sleep(5000);
            }
        }

        private void Minute_abspielen(object absender, EventArgs e)
        {
            Minutenabspieler.Play();
        }

        private void Viertelstunde_abspielen(object absender, EventArgs e)
        {
            if (DateTime.Now.Minute % 15 == 0)
            {
                int Schläge = DateTime.Now.Minute / 15;
                for (int i = 0; i < Schläge; i++)
                {
                    Viertelstundenabspieler.Play();
                    Thread.Sleep(5000);
                }
            }
        }

        private void Uhr_aktualisieren(object absender, EventArgs e)
        {
            Zeit.Content = DateTime.Now.ToString();
        }
    }
}
