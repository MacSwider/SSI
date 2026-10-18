using System.Text;
using lab02.Clustering;

namespace lab02
{
    public partial class Form1 : Form
    {
        // kolory grup na wykresie (czerwony, niebieski, zielony, ...)
        Color[] kolory = { Color.Red, Color.Blue, Color.Green, Color.Magenta, Color.Orange, Color.Teal, Color.Purple, Color.Brown };

        double[][]? probki;   // dane z csv
        WynikKMeans? wynik;   // po uruchomieniu algorytmu

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            cmbMiara.Items.Add("Euklidesowa");
            cmbMiara.Items.Add("Manhattan");
            cmbMiara.Items.Add("Czebyszewa");
            cmbMiara.Items.Add("Minkowskiego");
            cmbMiara.Items.Add("Tylko |x1|"); // do 2. raportu z pdf
            cmbMiara.SelectedIndex = 0;
            OdswiezMink();

            // spiralka.csv jest kopiowany do folderu z exe
            string sciezka = Path.Combine(AppContext.BaseDirectory, "spiralka.csv");
            if (!File.Exists(sciezka))
            {
                // jak odpalamy z VS to czasem jest w katalogu projektu
                sciezka = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\spiralka.csv"));
            }

            if (File.Exists(sciezka))
                WczytajCsv(sciezka);
            else
                txtInfo.Text = "Nie znalazlem spiralka.csv — wybierz plik przyciskiem.";
        }

        private void btnWybierz_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pliki CSV (*.csv)|*.csv|Wszystkie pliki (*.*)|*.*";
            ofd.Title = "Wybierz plik z probekami";

            if (ofd.ShowDialog() == DialogResult.OK)
                WczytajCsv(ofd.FileName);
        }

        private void cmbMiara_SelectedIndexChanged(object? sender, EventArgs e)
        {
            OdswiezMink();
        }

        private void OdswiezMink()
        {
            // parametr mink potrzebny tylko przy Minkowskiego
            bool wlaczone = cmbMiara.SelectedIndex == (int)RodzajOdleglosci.Minkowskiego;
            nudMink.Enabled = wlaczone;
            lblMink.Enabled = wlaczone;
        }

        // proste wczytanie csv: kazdy wiersz = x1,x2
        private void WczytajCsv(string sciezka)
        {
            try
            {
                string[] linie = File.ReadAllLines(sciezka);
                List<double[]> lista = new List<double[]>();

                foreach (string linia in linie)
                {
                    if (string.IsNullOrWhiteSpace(linia))
                        continue;

                    string[] czesci = linia.Split(',');
                    double x1 = double.Parse(czesci[0].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    double x2 = double.Parse(czesci[1].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    lista.Add(new double[] { x1, x2 });
                }

                probki = lista.ToArray();
                wynik = null;
                lblPlik.Text = Path.GetFileName(sciezka) + "  (" + probki.Length + " probek)";
                txtInfo.Text = "Wczytano " + probki.Length + " probek z:\r\n" + sciezka + "\r\n\r\nUstaw parametry i kliknij Uruchom.";
                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Blad przy wczytywaniu csv:\n" + ex.Message);
            }
        }

        private void btnStart_Click(object? sender, EventArgs e)
        {
            if (probki == null)
            {
                MessageBox.Show("Brak probek.");
                return;
            }

            int m = (int)nudGrup.Value;
            int iters = (int)nudIter.Value;
            RodzajOdleglosci miara = (RodzajOdleglosci)cmbMiara.SelectedIndex;
            double mink = (double)nudMink.Value;

            KMeans km = new KMeans();

            // jak jest >= 4 / 10 iteracji to liczymy tez stany posrednie do raportu
            WynikKMeans? po4 = null;
            WynikKMeans? po10 = null;

            if (iters >= 4)
                po4 = new KMeans().Uruchom(probki, m, 4, miara, mink);
            if (iters >= 10)
                po10 = new KMeans().Uruchom(probki, m, 10, miara, mink);

            wynik = km.Uruchom(probki, m, iters, miara, mink);

            // wypisanie rzeczy potrzebnych do raportu
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("miara: " + cmbMiara.Text);
            sb.AppendLine("grupy: " + m + ", iteracje: " + iters);
            sb.AppendLine();

            if (po4 != null && iters != 4)
            {
                sb.AppendLine("--- po 4 iteracjach ---");
                DopiszInfoOGrupach(sb, po4);
                sb.AppendLine();
            }

            if (po10 != null && iters != 10)
            {
                sb.AppendLine("--- po 10 iteracjach ---");
                DopiszInfoOGrupach(sb, po10);
                sb.AppendLine();
            }

            sb.AppendLine("--- wynik koncowy (" + iters + " iter.) ---");
            DopiszInfoOGrupach(sb, wynik);

            txtInfo.Text = sb.ToString();
            pictureBox1.Invalidate();
        }

        // polozenie srodkow, ile probek, min/max x1 i x2 - jak w wymaganiach raportu
        private void DopiszInfoOGrupach(StringBuilder sb, WynikKMeans w)
        {
            for (int j = 0; j < w.V.Length; j++)
            {
                List<double[]> zGrupu = new List<double[]>();
                for (int s = 0; s < probki!.Length; s++)
                {
                    if (w.U[s] == j)
                        zGrupu.Add(probki[s]);
                }

                sb.AppendLine("grupa " + (j + 1) + ":");
                sb.AppendLine("  V = (" + w.V[j][0].ToString("0.####") + ", " + w.V[j][1].ToString("0.####") + ")");
                sb.AppendLine("  liczba probek = " + zGrupu.Count);

                if (zGrupu.Count > 0)
                {
                    double minX1 = zGrupu.Min(p => p[0]);
                    double maxX1 = zGrupu.Max(p => p[0]);
                    double minX2 = zGrupu.Min(p => p[1]);
                    double maxX2 = zGrupu.Max(p => p[1]);
                    sb.AppendLine("  x1 min/max: " + minX1.ToString("0.####") + " / " + maxX1.ToString("0.####"));
                    sb.AppendLine("  x2 min/max: " + minX2.ToString("0.####") + " / " + maxX2.ToString("0.####"));
                }
            }
        }

        private void pictureBox1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            if (probki == null || probki.Length == 0)
                return;

            // zakres danych + troche marginesu
            double minX = probki.Min(p => p[0]) - 0.2;
            double maxX = probki.Max(p => p[0]) + 0.2;
            double minY = probki.Min(p => p[1]) - 0.2;
            double maxY = probki.Max(p => p[1]) + 0.2;

            int margin = 30;
            int w = pictureBox1.Width - 2 * margin;
            int h = pictureBox1.Height - 2 * margin;

            // zamiana wspolrzednych danych na piksele
            PointF DoEkranu(double x, double y)
            {
                float px = margin + (float)((x - minX) / (maxX - minX) * w);
                float py = margin + (float)((maxY - y) / (maxY - minY) * h); // y odwrocony
                return new PointF(px, py);
            }

            // rysujemy probki
            for (int s = 0; s < probki.Length; s++)
            {
                PointF p = DoEkranu(probki[s][0], probki[s][1]);
                Color c = Color.Black;
                if (wynik != null)
                    c = kolory[wynik.U[s] % kolory.Length];

                using (Brush b = new SolidBrush(c))
                    g.FillEllipse(b, p.X - 3, p.Y - 3, 6, 6);
            }

            // srodki grup (czarne krzyzyki)
            if (wynik != null)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    for (int j = 0; j < wynik.V.Length; j++)
                    {
                        PointF p = DoEkranu(wynik.V[j][0], wynik.V[j][1]);
                        g.DrawLine(pen, p.X - 7, p.Y, p.X + 7, p.Y);
                        g.DrawLine(pen, p.X, p.Y - 7, p.X, p.Y + 7);
                    }
                }
            }
        }
    }
}
