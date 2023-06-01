using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CografiBilgiSistemiProjesi1
{
    public partial class Form1 : Form
    {
        GMapOverlay katman1;
        List<Arac> list;

        public Form1()
        {
            InitializeComponent();
            initializeMap();
            aracListesiniOlustur();
        }

        private void aracListesiniOlustur()
        {
            list = new List<Arac>();
            list.Add(new Arac("34AC01", "Tır", "Ankara", "İstanbul", new PointLatLng(40.05, 32.22)));
            list.Add(new Arac("06AC02", "Kamyon", "İzmir", "İstanbul", new PointLatLng(39.22, 27.67)));
            list.Add(new Arac("35AC03", "Hafif Ticari", "Ankara", "İstanbul", new PointLatLng(40.67, 30.24)));
            list.Add(new Arac("07AC04", "Tır", "İstanbul", "Ankara", new PointLatLng(40.30, 32.47)));
            list.Add(new Arac("34AC05", "Kamyon", "Ankara", "İzmir", new PointLatLng(38.75, 30.43)));

        }

        private void initializeMap()
        {
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            map.Position = new GMap.NET.PointLatLng(36.0, 42.0);
            map.Zoom = 4;
            map.MinZoom = 3;
            map.MaxZoom = 25;
            katman1 = new GMapOverlay();
            // Bir Overlay (katman) oluşturmamız lazım
            // Harita üzerinde görüntülenecek tüm componentleri bu katman(overlay) eklememiz gerekmekte            
            // İlk olarak da yeni oluşturduğumuz katmanı harita nesnemize eklemeliyiz
            map.Overlays.Add(katman1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointLatLng lokasyon1 = new PointLatLng(Convert.ToDouble(textBoxEnlem.Text),
                                                    Convert.ToDouble(textBoxBoylam.Text));
            GMarkerGoogle marker = new GMarkerGoogle(lokasyon1, GMarkerGoogleType.blue_dot);
            marker.ToolTipText = "\nLokasyon1\nTır\nFrom: Ankara\nTo:İstanbul\n";
            marker.ToolTip.Fill = Brushes.Black;
            marker.ToolTip.Foreground = Brushes.White;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(5, 5);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.Tag = 101;           

            // Daha sonra marker(lar)ı eklemelisiniz. 
            // Dikkat! 
            // Markerı önce eklerseniz yanlış yere koyabilir
            katman1.Markers.Add(marker);            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            map.Dispose();
            Application.Exit();
        }

        private void map_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            // int markerId = (int)item.Tag;
            // Console.WriteLine("id: " + markerId + " olan Markera tıklandı..");

            string secilenAracinPlakasi = (string)item.Tag;

            foreach(Arac arac in list)
            {
                if (secilenAracinPlakasi.Equals(arac.Plaka))
                {
                    textBox3.Text = secilenAracinPlakasi;
                    textBox4.Text = arac.Tipi;
                    textBox5.Text = arac.From;
                    textBox6.Text = arac.To;
                    break;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointLatLng lokasyon2 = new PointLatLng(Convert.ToDouble(textBox2.Text),
                                                    Convert.ToDouble(textBox1.Text));
            GMarkerGoogle marker2 = new GMarkerGoogle(lokasyon2, GMarkerGoogleType.red_dot);
            marker2.Tag = 102;
            katman1.Markers.Add(marker2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Arac arac in list)
            {
                GMarkerGoogle markerTmp = new GMarkerGoogle(arac.Konum, GMarkerGoogleType.green_dot);
                markerTmp.Tag = arac.Plaka;
                markerTmp.ToolTipText = arac.ToString();
                katman1.Markers.Add(markerTmp);
                markerTmp.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                Console.WriteLine(arac.ToString());                
            }
        }
    }
}
