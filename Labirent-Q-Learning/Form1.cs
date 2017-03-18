using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labirent_Q_Learning
{
    public partial class Form1 : Form
    {
        private string text;
        private int[,] R;
        private double[,] Q;
        private List<int> route;
        private int[,] maze;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            //dosya içeriğini olduğu gibi text e at.
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    text = File.ReadAllText(file);
                    //şuan dosya içeriği text stringinde
                    //baslangicDuzenlemesi
                    baslangicDuzenlemesi();
                    startButton.Visible = true;
                    rMatrixButton.Visible = true;
                    qMatrixButton.Visible = true;
                }
                catch (IOException)
                {
                    MessageBox.Show("Dosya Açılamadı!");
                }
            }
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(startButtonThread);
            thread.Start();
            drawMazeButton.Visible = true;
            progressBar1.Visible = true;
        }
        private void rMatrixButton_Click(object sender, EventArgs e)
        {
            //Çizilmiş bir labirent varsa labirenti siler
            for (int a = 0; a < 10; a++)
                foreach (Control control in this.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    if (picture != null)
                    {
                        this.Controls.Remove(picture);
                    }
                }

            string[] nodes = getNodes();

            int matrixSize = nodes.Length;
            label2.Visible = true;
            label2.Text = "R Matrisi\n";
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    label2.Text += R[i, j] + "   ";
                }
                label2.Text += "\n";
            }
            try
            {
                if (route.Count == 0)
                {
                    label2.Text += "\nİzlenmesi Gereken Yol: Henüz Öğrenilmedi.";
                }
                else
                {
                    string routeStr = string.Join(" --> ", route);
                    label2.Text += "\nİzlenmesi Gereken Yol: " + routeStr;
                }
            }
            catch (Exception)
            {
                label2.Text += "\nİzlenmesi Gereken Yol: Henüz Öğrenilmedi.";
            }
        }

        private void qMatrixButton_Click(object sender, EventArgs e)
        {
            //Çizilmiş bir labirent varsa labirenti siler
            for (int a = 0; a < 10; a++)
                foreach (Control control in this.Controls)
                {
                    PictureBox picture = control as PictureBox;
                    if (picture != null)
                    {
                        this.Controls.Remove(picture);
                    }
                }

            string[] nodes = getNodes();

            int matrixSize = nodes.Length;
            label2.Visible = true;
            label2.Text = "Q Matrisi\n";
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    label2.Text += String.Format("{0:0.##}", Q[i, j]) + "   ";
                }
                label2.Text += "\n";
            }
            try
            {
                if (route.Count == 0)
                {
                    label2.Text += "\nİzlenmesi Gereken Yol: Henüz Öğrenilmedi.";
                }
                else
                {
                    string routeStr = string.Join(" --> ", route);
                    label2.Text += "\nİzlenmesi Gereken Yol: " + routeStr;
                }
            }
            catch (Exception)
            {
                label2.Text += "\nİzlenmesi Gereken Yol: Henüz Öğrenilmedi.";
            }

        }
        private void drawMazeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (route.Count != 0)
                {
                    //labirenti çözümüyle birlikte ekrana çizdiren kod.
                    string[] nodes = getNodes();
                    int nodeCount = nodes.Length;
                    int matrixSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(nodeCount)));

                    label2.Visible = false;

                    for (int a = 0; a < 10; a++)
                        foreach (Control control in this.Controls)
                        {
                            PictureBox picture = control as PictureBox;
                            if (picture != null)
                            {
                                this.Controls.Remove(picture);
                            }
                        }



                    for (int i = 0; i < matrixSize * 2 + 1; i++)
                    {
                        for (int j = 0; j < matrixSize * 2 + 1; j++)
                        {
                            PictureBox blackPicBox = new PictureBox();
                            blackPicBox.Size = new Size(20, 20);
                            blackPicBox.Location = new Point(j * 20 + 124, i * 20 + 17);
                            blackPicBox.BackColor = Color.Black;

                            PictureBox redPicBox = new PictureBox();
                            redPicBox.Size = new Size(10,10);
                            redPicBox.Location = new Point(j * 20 + 129, i * 20 + 22);
                            redPicBox.BackColor = Color.Red;

                            if (maze[i, j] == 1)
                            {
                                this.Controls.Add(blackPicBox);
                            }
                            else if (maze[i, j] == 2)
                            {
                                this.Controls.Add(redPicBox);
                            }

                        }
                    }
                }else
                {
                    MessageBox.Show("Önce Labirent Çözümünü Yapınız!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Önce Labirent Çözümünü Yapınız!");
            }
        }
        
        private void startButtonThread()
        {
            try
            {
                dizileriDuzenle();
                getRMatrix();
                getQMatrix();
                getRoute();
                getMaze();
                writeToFile();
                MessageBox.Show("İşlem tamamlandı.");
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Girdiğiniz Bilgileri Kontrol Ediniz!");
            }

        }
        private void baslangicDuzenlemesi()
        {
            try
            {
                route = new List<int>();
                startComboBox.Items.Clear();
                targetComboBox.Items.Clear();

                string[] nodes = getNodes();

                startComboBox.Items.AddRange(nodes);
                targetComboBox.Items.AddRange(nodes);
                startComboBox.Text = "Başlangıç";
                targetComboBox.Text = "Hedef";
                int matrixSize = nodes.Length;
                R = new int[matrixSize, matrixSize];
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        R[i, j] = -1;
                    }
                }
                Q = new double[matrixSize, matrixSize];
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        Q[i, j] = 0;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Dosya!");
            }
        }
        private void dizileriDuzenle()
        {
            route = new List<int>();
            string[] nodes = getNodes();
            int matrixSize = nodes.Length;

            R = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    R[i, j] = -1;
                }
            }
            Q = new double[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    Q[i, j] = 0;
                }
            }

        }
        
        private string[] getNodes()
        {
            //input dosyasını okuyarak nodeların listesini döndüren fonksiyon.
            string str = text;
            str = str.Replace("\r\n", ",").Replace("\r", ",").Replace("\n", ",");
            string[] s = str.Trim().TrimStart(',').TrimEnd(',').Split(',');
            s = s.Distinct().ToArray();
            int[] nodesInt = Array.ConvertAll(s, int.Parse);
            Array.Sort(nodesInt);
            string[] nodes = Array.ConvertAll(nodesInt, q => q.ToString());
            return nodes;
        }
        private void getRMatrix()
        {
            //R matrisini oluşturan fonksiyon
            string str = text;
            int matrixSize = getNodes().Length;
            //dosya içeriği text stringi
            //alt satıra geçmek için macOsda linuxta kullanılan "\r" ve "\r\n"
            //karakterlerinin "\n" e dönüştürülmüş hali str stringi
            //str stringinin başındaki ve sonundaki "," ve "\n" karakterlerinin temizlenmiş hali s stringi
            //s2 stringi str stringinin bir satırından oluşan string
            //s3 dizisi s2 nin int e çevrilmiş hali
            //yani dosyayı alıp, düzenliyoruz daha sonra satır satır int e çevirip inceleyerek R matrisine atıyoruz.
            str = str.Replace("\r\n", "\n").Replace("\r", "\n");
            string[] s = str.Trim().TrimStart(',', '\n').TrimEnd(',', '\n').Split('\n');
            string[] s2;
            int[] s3;
            for (int i = 0; i < matrixSize; i++)
            {
                s2 = s[i].Split(',');
                s3 = Array.ConvertAll(s2, int.Parse);
                for (int x = 0; x < s3.Length; x++)
                {
                    if (s3[x] == int.Parse(targetComboBox.SelectedItem.ToString()))
                    {
                        R[i, s3[x]] = 100;
                    }
                    else
                    {
                        R[i, s3[x]] = 0;
                    }
                }
            }
            R[int.Parse(targetComboBox.SelectedItem.ToString()), int.Parse(targetComboBox.SelectedItem.ToString())] = 100;

        }
        private void getQMatrix()
        {
            //Q matrisini oluşturan fonksiyon

            //Form ekranından iterasyon için gerekli veriler alınıyor.
            int baslangic = int.Parse(startComboBox.SelectedItem.ToString());
            int hedef = int.Parse(targetComboBox.SelectedItem.ToString());
            int iteration;
            try
            {
                iteration = int.Parse(iterationTextBox.Text.ToString());
            }
            catch (Exception)
            {
                iteration = 3000;
            }
            

            //iterasyon için basşlangıç değerleri
            int state = baslangic;
            int action = 0;
            
            progressBar1.Maximum = iteration;

            //iterasyon başlangıcı
            for (int x = 0; x < iteration; x++)
            {
                action = getRandomNextAction(state);

                //Console.WriteLine(x + " " + state + "->" + action);
                progressBar1.Value = x;

                Q[state, action] = R[state, action] + 0.8 * getMaxOfActions(action);
                //Eğer hedefe ulaşıldıktan sonra tekrar başlangıç noktasına dönülmesini istiyorsan
                //aşağıdaki açıklama satırlarını düzelt
                //if (action == hedef)
                //{
                //    state = baslangic;
                //}
                //else
                //{
                state = action;
                //}
            }
            //iterasyon bitişi. artık Q matrisi belirlendi.
        }

        private int getRandomNextAction(int state)
        {
            //bir sonraki adımda gidilebilecek yollardan birini random olarak döndüren fonksiyon.
            int matrixSize = getNodes().Length;
            List<int> actions = new List<int>();
            
            for (int x = 0; x < matrixSize; x++)
            {
                //hedef noktaya geldiğinde kendi içinde tekrar etmemesi istenirse
                //"&& x != state" kodu if koşuluna eklenir.
                //(Hedef nokta başlangıçta 100 olduğu için >=0 kuralını sağlıyor).
                if (R[state, x] >= 0)
                {
                    actions.Add(x); //bir sonraki adımda gidilebilecek düğümler.
                }
            }
            Random random = new Random(Guid.NewGuid().GetHashCode());

            return actions[random.Next(0, actions.Count)];
        }
        private double getMaxOfActions(int state)
        {
            //Q learning algoritmasındaki max değeri hesaplayan fonksiyon.
            int matrixSize = getNodes().Length;
            List<double> actions = new List<double>();

            for (int x = 0; x < matrixSize; x++)
            {
                //hedef noktaya geldiğinde kendi içinde tekrar etmemesi istenirse
                //"&& x != state" kodu if koşuluna eklenir.
                //(Hedef nokta başlangıçta 100 olduğu için >=0 kuralını sağlıyor).
                if (R[state, x] >= 0)
                {
                    actions.Add(Q[state, x]); //bir sonraki adımda gidilebilecek düğümlerin Q matrisindeki değerleri.
                }
            }

            return actions.Max();
        }

        private void getRoute()
        {
            //Çözüm yolunu bulan fonksiyon
            route = new List<int>();
            int baslangic = int.Parse(startComboBox.SelectedItem.ToString());
            int hedef = int.Parse(targetComboBox.SelectedItem.ToString());
            int matrixSize = getNodes().Length;

            route.Add(baslangic);

            while (route.LastOrDefault() != hedef)
            {
                for (int x = 0; x < matrixSize; x++)
                {
                    if (Q[route.LastOrDefault(), x]== getMaxOfActions(route.LastOrDefault()))
                    {
                        route.Add(x); //bir sonraki adımda gidilebilecek en mantıklı node
                    }
                }
            }
        }

        private void writeToFile()
        {
            string[] nodes = getNodes();
            int matrixSize = nodes.Length;

            String str;
            str = "R Matrisi\n";
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    str += R[i, j] + " ";
                }
                str += "\n";
            }
            StreamWriter outR = new StreamWriter("outR.txt");
            outR.Write(str);
            outR.Close();
            
            str = "Q Matrisi\n";
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    str += String.Format("{0:0.##}", Q[i, j]) + " ";
                }
                str += "\n";
            }
            StreamWriter outQ = new StreamWriter("outQ.txt");
            outQ.Write(str);
            outQ.Close();

            str = "Takip Edilecek Yol\n";
            str += string.Join(" --> ", route);
            StreamWriter outPath = new StreamWriter("outPath.txt");
            outPath.Write(str);
            outPath.Close();
        }
        private List<int> getPossibleActions(int state)
        {
            //Gidilebilecek düğümleri döndüren fonksiyon
            int matrixSize = getNodes().Length;
            List<int> actions = new List<int>();

            for (int x = 0; x < matrixSize; x++)
            {
                if (R[state, x] >= 0 && x != state)
                {
                    actions.Add(x); //bir sonraki adımda gidilebilecek düğümler.
                }
            }

            return actions;
        }
        
        private void getMaze()
        {
            //Labirenti matris şeklinde oluşturan fonksiyon.
            string[] nodes = getNodes();
            int nodeCount = nodes.Length;
            int matrixSize = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(nodeCount)));

            int baslangic = int.Parse(startComboBox.SelectedItem.ToString());
            int hedef = int.Parse(targetComboBox.SelectedItem.ToString());
            maze = new int[matrixSize * 2 + 1, matrixSize * 2 + 1];
            
            //0 lar yol, 1 ler duvar
            //her yer duvar olarak belirlendi.
            for (int x = 0; x < matrixSize * 2 + 1; x++)
            {
                for (int y = 0; y < matrixSize * 2 + 1; y++)
                {
                    maze[x, y] = 1;
                }
            }
            int i = 0;
            int j = 0;
            int state = 0;
            //yollar açılıyor.
            for (state = 0; state < nodeCount; state++)
            {
                i = (state / matrixSize) * 2 + 1;
                j = (state % matrixSize) * 2 + 1;

                //üsttündeki node a geçiş varsa üst tarafı 0 yap
                if (getPossibleActions(state).Contains(state - matrixSize))
                    maze[i - 1, j] = 0;
                //soldaki node a geçiş varsa sol tarafı 0 yap
                if (getPossibleActions(state).Contains(state - 1))
                    maze[i, j - 1] = 0;
                //merkezi 0 yap
                maze[i, j] = 0;

                if (state == baslangic || state == hedef)
                {
                    //giriş çıkış noktaları üstte ise
                    if (state < matrixSize)
                        maze[i - 1, j] = 0;
                    //giriş çıkış noktaları altta ise
                    else if (state >= nodeCount - matrixSize)
                        maze[i + 1, j] = 0;
                    //giriş çıkış noktaları solda ise
                    else if (state % matrixSize == 0)
                        maze[i, j - 1] = 0;
                    //giriş çıkış noktaları sağda ise
                    else if (state % matrixSize == matrixSize - 1)
                        maze[i, j + 1] = 0;
                }
            }

            i = 0;
            j = 0;
            state = 0;

            //0 lar yol, 1 ler duvar, 2 ler çözüm yolu.
            for (int x = 0; x < route.Count; x++)
            {
                state = route[x];
                i = (state / matrixSize) * 2 + 1;
                j = (state % matrixSize) * 2 + 1;

                //merkezleri 2 yap
                maze[i, j] = 2;
                
                if (state == baslangic)
                {
                    //giriş noktası üstte ise
                    if (state < matrixSize)
                        maze[i - 1, j] = 2;
                    //giriş noktası altta ise
                    else if (state >= nodeCount - matrixSize)
                        maze[i + 1, j] = 2;
                    //giriş noktası solda ise
                    else if (state % matrixSize == 0)
                        maze[i, j - 1] = 2;
                    //giriş noktası sağda ise
                    else if (state % matrixSize == matrixSize - 1)
                        maze[i, j + 1] = 2;

                    //bir sonraki adım üste gidiyorsa
                    if (route[x + 1] - state == -matrixSize)
                        maze[i - 1, j] = 2;
                    //bir sonraki adım sağa gidiyorsa
                    else if (route[x + 1] - state == 1)
                        maze[i, j + 1] = 2;
                    //bir sonraki adım alta gidiyorsa
                    else if (route[x + 1] - state == matrixSize)
                        maze[i + 1, j] = 2;
                    //bir sonraki adım sola gidiyorsa
                    else if (route[x + 1] - state == -1)
                        maze[i, j - 1] = 2;

                }
                else if (state != hedef)
                {
                    //bir sonraki adım üste gidiyorsa
                    if (route[x + 1] - state == - matrixSize)
                        maze[i - 1, j] = 2;
                    //bir sonraki adım sağa gidiyorsa
                    else if (route[x + 1] - state == 1)
                        maze[i, j + 1] = 2;
                    //bir sonraki adım alta gidiyorsa
                    else if (route[x + 1] - state ==matrixSize)
                        maze[i + 1, j] = 2;
                    //bir sonraki adım sola gidiyorsa
                    else if (route[x + 1] - state == -1)
                        maze[i, j - 1] = 2;
                }
                else if (state == hedef)
                {
                    //çıkış noktası üstte ise
                    if (state < matrixSize)
                        maze[i - 1, j] = 2;
                    //çıkış noktası altta ise
                    else if (state >= nodeCount - matrixSize)
                        maze[i + 1, j] = 2;
                    //çıkış noktası solda ise
                    else if (state % matrixSize == 0)
                        maze[i, j - 1] = 2;
                    //çıkış noktası sağda ise
                    else if (state % matrixSize == matrixSize - 1)
                        maze[i, j + 1] = 2;
                }
                
            }
            
        }
    }
}
