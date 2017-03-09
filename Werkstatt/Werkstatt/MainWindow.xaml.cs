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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WerkstattBL.Controller;
using WerkstattBL.Model;

namespace Werkstatt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IEnumerable<KundenRechnungsHilfe> aufagabenHeute = null;
        private IEnumerable<Reparaturart> repArten = null;
        private string currentStandort = "";

        public MainWindow()
        {
            
            InitializeComponent();
            WerkstattBL.Configuration.ConfigManager.Initialize();
            this.btnFertig.Visibility = Visibility.Hidden;
            this.aufagabenHeute = WerkstattBL.Controller.WerkstattManager.GetMessagesForToday(DateTime.Today);
            this.dgMessages.ItemsSource = this.aufagabenHeute;
            this.cmbStandorte.ItemsSource = WerkstattManager.GetAlleStandort();

        }

        private void btnFertig_Click(object sender, RoutedEventArgs e)
        {
            if(this.dgMessages.SelectedItem != null)
            {
                KundenRechnungsHilfe toDelet = this.dgMessages.SelectedItem as KundenRechnungsHilfe;
                WerkstattBL.Controller.WerkstattManager.DeleteFromHelp(toDelet.Rechnungsnummer, toDelet.KundenID);
                this.dgMessages.ItemsSource = null;
                this.aufagabenHeute = WerkstattManager.GetMessagesForToday(DateTime.Today);               
                this.dgMessages.ItemsSource = this.aufagabenHeute;
            }
           
        }

        private void dgMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as DataGrid).SelectedItem != null )
            {
                this.btnFertig.Visibility = Visibility.Visible;
                this.lblKundenId.Content = ((sender as DataGrid).SelectedItem as KundenRechnungsHilfe).KundenID.ToString();
            }
        }

        private void cmbStandorte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ComboBox).SelectedItem != null) {
                try
                {
                    this.currentStandort = (sender as ComboBox).SelectedItem.ToString();
                    this.repArten = WerkstattManager.GetAlleFürStandort(this.currentStandort);
                    if (this.repArten != null)
                    {
                        this.cmbReparturArten.ItemsSource = this.repArten.Select(item => item.Bezeichnung);
                    }
                }
                catch ( Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRepErstellen_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(this.lblKundenId.Content.ToString()))
            {
                if(string.IsNullOrEmpty(this.currentStandort))
                {
                    try
                    {
                        WerkstattManager.CreateReparatur(this.repArten.First(item => item.Bezeichnung.Equals(this.cmbReparturArten.SelectedItem.ToString())).ReparaturArtId,
                                                                   (this.dgMessages.SelectedItem as KundenRechnungsHilfe).Rechnungsnummer,
                                                                    (this.dgMessages.SelectedItem as KundenRechnungsHilfe).Datum,
                                                                    this.currentStandort);
                        MessageBox.Show("Füge nun bitte weitere Reparaturen zu dieser Aufgabe hinzu oder entferne sie. ");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Oje etwas ist schief gelaufen!\n" + ex.Message);         
                    }
                }
                else
                {
                    MessageBox.Show("Bitte wähle deinen Standort aus!");
                }
            }
            else
            {
                MessageBox.Show("Bitte wähle eine Aufgabe aus!");
            }
        }
    }
}
