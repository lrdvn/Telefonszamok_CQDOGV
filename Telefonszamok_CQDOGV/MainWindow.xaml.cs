using System;
using System.Collections.ObjectModel;
using System.ComponentModel;            // ICollectionView
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;              // CollectionViewSource
using cnTelefonkonyv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;                  // SaveFileDialog, OpenFileDialog
using Models;

namespace Telefonszamok_CQDOGV
{
    public partial class MainWindow : Window
    {
        private readonly TelefonkonyvContext _context = new();
        private ObservableCollection<enSzemely> _szemelyek = new();
        private ObservableCollection<enTelefonszam> _telefonszamok = new();
        private ICollectionView _szemelyView;
        private ICollectionView _telefonView;
        private bool _suppressAspect;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Helységek a ComboBox-hoz
            var helyek = _context.enHelysegek.ToList();
            colHelyseg.ItemsSource = helyek;

            // Személyek + filter
            var sList = _context.enSzemelyek
                                .Include(s => s.enHelyseg)
                                .Include(s => s.enTelefonszamok)
                                .ToList();
            _szemelyek = new ObservableCollection<enSzemely>(sList);
            _szemelyView = CollectionViewSource.GetDefaultView(_szemelyek);
            dgSzemelyek.ItemsSource = _szemelyView;

            // Telefonszámok + filter
            _telefonszamok = new ObservableCollection<enTelefonszam>();
            _telefonView = CollectionViewSource.GetDefaultView(_telefonszamok);
            dgTelefonszamok.ItemsSource = _telefonView;
        }

        // ===== Személy szűrés =====
        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = tbFilter.Text.Trim().ToLower();
            _szemelyView.Filter = obj =>
            {
                if (obj is enSzemely s)
                {
                    var full = $"{s.Vezeteknev} {s.Utonev}".ToLower();
                    return string.IsNullOrEmpty(txt) || full.Contains(txt);
                }
                return false;
            };
        }

        // ===== Telefonszám szűrés =====
        private void tbPhoneFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = tbPhoneFilter.Text.Trim().ToLower();
            _telefonView.Filter = obj =>
            {
                if (obj is enTelefonszam t)
                    return string.IsNullOrEmpty(txt) || (t.Szam?.ToLower().Contains(txt) ?? false);
                return false;
            };
        }

        // ===== Személy =====
        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            var uj = new enSzemely
            {
                Vezeteknev = "(vezetéknév)",
                Utonev = "(utónév)",
                Lakcim = "",
                enHelysegid = _context.enHelysegek.FirstOrDefault()?.id ?? 0
            };
            _szemelyek.Add(uj);
            dgSzemelyek.SelectedItem = uj;
            dgSzemelyek.ScrollIntoView(uj);
            dgSzemelyek.BeginEdit();
        }

        void SavePerson()
        {
            try
            {
                foreach (var p in _szemelyek)
                    if (p.id == 0) _context.enSzemelyek.Add(p);
                _context.SaveChanges();
                MessageBox.Show("Személyek mentve.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a személyek mentésekor:\n" + ex.Message, "Mentési hiba",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSavePerson_Click(object sender, RoutedEventArgs e)
        {
            SavePerson();
        }

        private void btnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (dgSzemelyek.SelectedItem is enSzemely sel)
            {
                var teljesNev = $"{sel.Vezeteknev} {sel.Utonev}".Trim();
                if (MessageBox.Show($"Törlöd: {teljesNev}?", "Megerősítés",
                                    MessageBoxButton.YesNo, MessageBoxImage.Warning)
                    == MessageBoxResult.Yes)
                {
                    _context.enSzemelyek.Remove(sel);
                    _context.SaveChanges();
                    _szemelyek.Remove(sel);
                    _telefonszamok.Clear();
                }
            }
        }

        private void dgSzemelyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSzemelyek.SelectedItem is enSzemely sel)
                _telefonszamok = new ObservableCollection<enTelefonszam>(sel.enTelefonszamok);
            else
                _telefonszamok.Clear();
            // újratelepítjük a filter-view-t, hogy a filter is üres legyen
            _telefonView = CollectionViewSource.GetDefaultView(_telefonszamok);
            dgTelefonszamok.ItemsSource = _telefonView;
        }

        // ===== Telefonszám =====
        private void btnAddPhone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var p in _szemelyek)
                    if (p.id == 0) _context.enSzemelyek.Add(p);
                _context.SaveChanges();
            }
            catch 
            {}

            try
            {
                // új telefonszám hozzáadása
                if (dgSzemelyek.SelectedItem is enSzemely sel)
                {
                    var uj = new enTelefonszam
                    {
                        Szam = "(új szám)",
                        enSzemelyid = sel.id
                    };
                    _telefonszamok.Add(uj);
                    dgTelefonszamok.SelectedItem = uj;
                    dgTelefonszamok.ScrollIntoView(uj);
                    dgTelefonszamok.BeginEdit();
                }
                else
                {
                    MessageBox.Show("Előbb válassz személyt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a telefonszám hozzáadásakor:\n" + ex.Message, "Hiba",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSavePhone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var t in _telefonszamok)
                    if (t.id == 0) _context.enTelefonszamok.Add(t);
                _context.SaveChanges();
                MessageBox.Show("Telefonszámok mentve.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a telefonszámok mentésekor:\n" + ex.Message, "Mentési hiba",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeletePhone_Click(object sender, RoutedEventArgs e)
        {
            if (dgTelefonszamok.SelectedItem is enTelefonszam sel)
            {
                if (MessageBox.Show($"Törlöd a {sel.Szam} számot?", "Megerősítés",
                                    MessageBoxButton.YesNo, MessageBoxImage.Warning)
                    == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.enTelefonszamok.Remove(sel);
                        _context.SaveChanges();
                        _telefonszamok.Remove(sel);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba a telefonszám törlésekor:\n" + ex.Message,
                                        "Törlési hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        // ===== CSV Export/Import =====
        private void ExportPersons_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "CSV fájl|*.csv", FileName = "szemelyek.csv" };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("id;Vezeteknev;Utonev;Lakcim;Helyseg");
                foreach (var p in _context.enSzemelyek.Include(x => x.enHelyseg))
                    sb.AppendLine($"{p.id};{p.Vezeteknev};{p.Utonev};{p.Lakcim};{p.enHelyseg?.Nev}");
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Export kész.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ImportPersons_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "CSV fájl|*.csv" };
            if (dlg.ShowDialog() == true)
            {
                var lines = File.ReadAllLines(dlg.FileName, Encoding.UTF8).Skip(1);
                foreach (var line in lines)
                {
                    var cols = line.Split(';');
                    if (cols.Length >= 5)
                    {
                        var p = new enSzemely
                        {
                            Vezeteknev = cols[1],
                            Utonev = cols[2],
                            Lakcim = cols[3],
                            enHelysegid = _context.enHelysegek.FirstOrDefault(h => h.Nev == cols[4])?.id ?? 0
                        };
                        _context.enSzemelyek.Add(p);
                    }
                }
                _context.SaveChanges();
                LoadData();
                MessageBox.Show("Import kész.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportPhones_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog { Filter = "CSV fájl|*.csv", FileName = "telefonszamok.csv" };
            if (dlg.ShowDialog() == true)
            {
                var sb = new StringBuilder();
                sb.AppendLine("id;Szam;SzemelyID");
                foreach (var t in _context.enTelefonszamok)
                    sb.AppendLine($"{t.id};{t.Szam};{t.enSzemelyid}");
                File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Export kész.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ImportPhones_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "CSV fájl|*.csv" };
            if (dlg.ShowDialog() == true)
            {
                var lines = File.ReadAllLines(dlg.FileName, Encoding.UTF8).Skip(1);
                foreach (var line in lines)
                {
                    var cols = line.Split(';');
                    if (cols.Length >= 3 && int.TryParse(cols[2], out var sid))
                    {
                        var t = new enTelefonszam
                        {
                            Szam = cols[1],
                            enSzemelyid = sid
                        };
                        _context.enTelefonszamok.Add(t);
                    }
                }
                _context.SaveChanges();
                LoadData();
                MessageBox.Show("Import kész.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // ===== Aspect ratio event =====
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_suppressAspect)
            {
                _suppressAspect = false;
                return;
            }

            const double ratio = 16.0 / 9.0;
            _suppressAspect = true;

            if (e.WidthChanged)
                this.Height = this.ActualWidth / ratio;
            else if (e.HeightChanged)
                this.Width = this.ActualHeight * ratio;
        }
    }
}
