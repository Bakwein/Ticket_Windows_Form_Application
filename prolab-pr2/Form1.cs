using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Drawing.Text;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace prolab_pr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //private Admin admin = new Admin();
        private void buttonAdminGirisi_Click(object sender, EventArgs e)
        {
            comboBoxVarOlanFirmalar.Items.Clear();

            Admin admin = new Admin();
            if (admin.getLoginable() == false)
            {
                MessageBox.Show("Admin giri�i ba�ar�s�z oldu");
                return;
            }
            string Username = textBoxKullaniciAdi.Text;
            string Password = textBoxSifre.Text;
            if (Username == "admin" && Password == "admin")
            {
                admin.userName = Username;
                admin.password = Password;

                panelFirma.Visible = false;
                panelAdmin.Visible = true;

            }
            else
            {
                MessageBox.Show("Kullan�c� ad� veya �ifre yanl��");
            }

            foreach (Company c in company.companyList)
            {
                comboBoxVarOlanFirmalar.Items.Add(c.getUserName());
            }
            panel1.Visible = false;
        }

        private Company company = new Company();
        private void buttonFirmaGirisi_Click(object sender, EventArgs e)
        {
            comboAracId.Items.Clear();
            comboBoxSeferNo.Items.Clear();
            comboBoxSeferNo2.Items.Clear();
            comboBoxYakitTuru.Items.Clear();
            string Username = textBoxKullaniciAdi.Text;
            string Password = textBoxSifre.Text;

            int flag = 0;
            foreach (Company c in company.companyList)
            {
                if (Username == c.userName && Password == c.password)
                {
                    flag = 1;
                    if (c.getLoginable() == false)
                    {
                        MessageBox.Show("Firma giri�i ba�ar�s�z oldu");
                        return;
                    }
                    break;
                }
            }

            //yeni aray�ze buradan devam(if icinden)
            if (flag == 1)
            {
                panelAdmin.Visible = false;
                panelFirma.Visible = true;
                labelFirmaAdi.Text = Username + " Firmasi";
            }
            else
            {
                MessageBox.Show("Kullan�c� ad� veya �ifre yanl��");

            }

            List<Vehicle> v1 = new List<Vehicle>();

            foreach (Company c in company.companyList)
            {
                if (c.userName == Username)
                {
                    foreach (Vehicle v in c.vehicleList)
                    {
                        int var_mi = 0;
                        foreach (Vehicle v2 in v1)
                        {
                            if (v.getArac_id() == v2.getArac_id())
                            {
                                var_mi = 1;
                            }
                        }
                        if (var_mi == 0)
                        {
                            v1.Add(v);
                        }
                    }
                }
            }

            foreach (Vehicle v in v1)
            {
                comboAracId.Items.Add(v.getArac_id());
            }

            //comboAracId.Items.Clear(); eklenecek
            //string Username = textBoxKullaniciAdi.Text;

            foreach (Company c in company.companyList)
            {
                if (c.userName == Username)
                {
                    foreach (string s in c.yakitList)
                    {
                        comboBoxYakitTuru.Items.Add(s);
                    }
                }
            }

            foreach (Company c in company.companyList)
            {
                if (c.userName == Username)
                {
                    if (Username == "A" || Username == "B" || Username == "C" || Username == "D" || Username == "F")
                    {
                        foreach (string s in c.seferList)
                        {
                            comboBoxSeferNo.Items.Add(s);
                            comboBoxSeferNo2.Items.Add(s);
                        }
                    }
                    else
                    {
                        comboBoxSeferNo.Items.Add("1. Sefer");
                        comboBoxSeferNo.Items.Add("2. Sefer");
                        comboBoxSeferNo.Items.Add("3. Sefer");
                        comboBoxSeferNo.Items.Add("4. Sefer");
                        comboBoxSeferNo.Items.Add("5. Sefer");
                        comboBoxSeferNo.Items.Add("6. Sefer");
                        comboBoxSeferNo2.Items.Add("1. Sefer");
                        comboBoxSeferNo2.Items.Add("2. Sefer");
                        comboBoxSeferNo2.Items.Add("3. Sefer");
                        comboBoxSeferNo2.Items.Add("4. Sefer");
                        comboBoxSeferNo2.Items.Add("5. Sefer");
                        comboBoxSeferNo2.Items.Add("6. Sefer");
                    }

                }
            }
            panel1.Visible = false;
        }

        private void comboBoxKalkisNoktasi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxVarisNoktasi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxKullaniciAdi_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelDeneme_Click(object sender, EventArgs e)
        {

        }

        private void buttonAra_Click(object sender, EventArgs e)
        {
            checkedListBoxSeferler.Items.Clear();
            panelSeferler.Visible = true;
            panelAdmin.Visible = false;
            panelFirma.Visible = false;


            if (textBoxKullaniciAdi.Text == "" || textBoxSifre.Text == "")
            {
                MessageBox.Show("L�tfen kullan�c� ad� veya �ifre k�sm�n� bo� b�rakmay�n�z.");
                return;
            }
            if (textBoxKullaniciAdi.Text.Length == 1)
            {
                MessageBox.Show("Kullan�c� ad� uzunlu�u 1 olamaz. L�tfen tekrar deneyin."); // sirketle ayni kullanci olmasin
                return;
            }
            else if (textBoxKullaniciAdi.Text == "admin")
            {
                MessageBox.Show("Kullan�c� ad� admin olamaz. L�tfen tekrar deneyin."); // sirketle ayni kullanci olmasin
                return;

            }
            for (int i = 0; i < textBoxKullaniciAdi.Text.Length; i++)
            {
                if (!((textBoxKullaniciAdi.Text[i] >= 'a' && textBoxKullaniciAdi.Text[i] <= 'z') || (textBoxKullaniciAdi.Text[i] >= 'A' && textBoxKullaniciAdi.Text[i] <= 'Z')
                    || textBoxKullaniciAdi.Text[i] == ' ' || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�'
                    || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�'
                    || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�'
                    || textBoxKullaniciAdi.Text[i] == '�' || textBoxKullaniciAdi.Text[i] == '�'))
                {
                    MessageBox.Show("Kullan�c� ad� k�sm�nda istenilmeyen karakter girdiniz. L�tfen tekrar deneyin.");
                    return;
                }
            }

            string[] kontrol = textBoxKullaniciAdi.Text.Split(' ');
            if (kontrol.Length <= 1)
            {
                MessageBox.Show("M��teri isim k�sm�n� l�tfen do�ru �ekilde giriniz.\n�rne�in-> Ardahan Aytan.");
                return;
            }


            string kalkis_ = comboBoxKalkisNoktasi.Text;
            string varis_ = comboBoxVarisNoktasi.Text;
            if (kalkis_ == "" || varis_ == "")
            {
                MessageBox.Show("Kalk�� noktas� veya var�� noktas� se�meyi unutmay�n.");
                return;
            }


            if (kalkis_ == varis_)
            {
                MessageBox.Show("Kalk�� ve var�� noktas� ayn� olamaz.");
                return;
            }

            Customer newCust = new Customer(textBoxKullaniciAdi.Text, textBoxSifre.Text);
            MessageBox.Show("Ho� geldin," + newCust.getAd() + " " + newCust.getSoyad() + "!");

            string gun = dateTimePickerTarih.Value.ToString("dddd");
            Reservation r = new Reservation();
            Trip t = new Trip();

            List<string> yazdirmak_icin = new List<string>();
            Company c = new Company();

            foreach (Vehicle v in c.getStaticVehicleList())
            {
                if (v.getGun_eng() == gun || v.getGun_tr() == gun)
                {

                    foreach (Sefer s in t.getSeferList())
                    {

                        string sefer_ismi = s.getSeferIsmi();
                        if (v.getSefer_no() == sefer_ismi)
                        {
                            //mm += v.getArac_id() + " " + s.getSeferIsmi() + "\n";

                            foreach (List<string> inner in s.getIceIce())
                            {
                                int inner_flag = 0;
                                if (inner.Contains(kalkis_) && inner.Contains(varis_) && inner.IndexOf(kalkis_) < inner.IndexOf(varis_))
                                {
                                    string kalkis = comboBoxKalkisNoktasi.Text;
                                    int kalkis_index = v.getSehirler().IndexOf(kalkis);
                                    string varis = comboBoxVarisNoktasi.Text;
                                    int varis_index = v.getSehirler().IndexOf(varis);
                                    if (kalkis_index > varis_index)
                                    {
                                        kalkis_index = v.getSehirler().LastIndexOf(kalkis);
                                        varis_index = v.getSehirler().LastIndexOf(varis);
                                    }
                                    List<Koltuk> k = new List<Koltuk>();
                                    k = v.getKoltuk_ic_ice()[kalkis_index];
                                    int dolu_koltuk_sayisi = 0;
                                    foreach (Koltuk k1 in k)
                                    {
                                        if (k1.getDoluluk_durumu() == 1)
                                        {
                                            dolu_koltuk_sayisi++;
                                        }
                                    }

                                    foreach (List<Koltuk> k1 in v.getKoltuk_ic_ice())
                                    {
                                        int dolu_koltuk_sayisi1 = 0;
                                        foreach (Koltuk k2 in k1)
                                        {
                                            if (k2.getDoluluk_durumu() == 1)
                                            {
                                                dolu_koltuk_sayisi1++;
                                            }
                                        }
                                        if (dolu_koltuk_sayisi1 > dolu_koltuk_sayisi)
                                        {
                                            dolu_koltuk_sayisi = dolu_koltuk_sayisi1;
                                        }

                                    }

                                    foreach (string s1 in yazdirmak_icin)
                                    {

                                        //if (s1 == String.Format("{0,-20} {1,-20} {2,-20}", v.getSirketAdi(), v.getArac_id(), v.getDoluKoltuk().ToString() + "/" + v.getKoltuk_sayisi()))
                                        if (s1.Contains(String.Format("{0,-20} {1,-20}", v.getSirketAdi(), v.getArac_id(), StringComparison.OrdinalIgnoreCase)))
                                        {
                                            inner_flag = 1;
                                            break;
                                        }
                                    }
                                    if (inner_flag == 1)
                                    {
                                        break;
                                    }

                                    //checkedListBoxSeferler.Items.Add(String.Format("{0,-20} {1,-20} {2,-20}", v.getSirketAdi(), v.getArac_id(), v.getKoltuk_sayisi()));
                                    yazdirmak_icin.Add(String.Format("{0,-20} {1,-20} {2,-20}", v.getSirketAdi(), v.getArac_id(), dolu_koltuk_sayisi.ToString() + "/" + v.getKoltuk_sayisi()));
                                    //MessageBox.Show(v.getGun_tr());
                                }
                            }

                        }
                    }
                }
            }

            foreach (string s in yazdirmak_icin)
            {
                checkedListBoxSeferler.Items.Add(s);
            }

        }

        public void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = comboBoxVarOlanFirmalar.SelectedIndex;
            string str = comboBoxVarOlanFirmalar.Text;
            comboBoxVarOlanFirmalar.Items.RemoveAt(a);
            company.companyList.Remove(company.companyList[a]);

            MessageBox.Show(str + " isimli firma silindi.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxYeniKullaniciAdi.Text == "" || textBoxYeniSifre.Text == "")
            {
                MessageBox.Show("L�tfen firma girerken bo� alan b�rakmay�n�z.");
                return;
            }
            else if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Firma se�erken en az bir yak�t t�r� se�mek zorundas�n�z. L�tfen tekrar deneyin.");
                return;
            }

            if (textBoxHizmetBedeli.Text == "")
            {
                MessageBox.Show("Hizmet bedeli bo� olamaz.");
                return;
            }

            for (int i = 0; i < textBoxHizmetBedeli.Text.Length; i++)
            {
                if (textBoxHizmetBedeli.Text[i] < '0' || textBoxHizmetBedeli.Text[i] > '9')
                {
                    MessageBox.Show("Hizmet bedeli k�sm�na sadece say� giriniz.");
                    return;
                }
            }

            if (Convert.ToInt32(textBoxHizmetBedeli.Text) < 0)
            {
                MessageBox.Show("Hizmet bedeli negatif olamaz.");
                return;
            }

            int hizmet_bedeli = Convert.ToInt32(textBoxHizmetBedeli.Text);


            string Username = textBoxYeniKullaniciAdi.Text;
            string Password = textBoxYeniSifre.Text;

            //comboBoxVarOlanFirmalar icindekileri gezme
            foreach (string s in comboBoxVarOlanFirmalar.Items)
            {
                if (s == Username)
                {
                    MessageBox.Show("Bu kullan�c� ad� zaten var. Ba�ka bir kullan�c� ad� deneyin.");
                    return;
                }
            }

            Company c = new Company(Username, Password, hizmet_bedeli);
            comboBoxVarOlanFirmalar.Items.Add(c.getUserName());
            company.companyList.Add(c);

            /*
             comboBoxVarOlanFirmalar.Items.RemoveAt(a);
            company.companyList.Remove(company.companyList[a]); 
             */

            //s�k�nt� olabilir!!??!!!
            foreach (string s in checkedListBox1.CheckedItems)
            {
                foreach (Company x in company.companyList)
                {
                    if (x.userName == Username)
                    {
                        x.yakitList.Add(s);

                        if (s == "Benzin")
                        {
                            if (textBoxBenzinMaliyet.Text == "")
                            {
                                MessageBox.Show("L�tfen benzin maliyetini giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            else if (Convert.ToInt32(textBoxBenzinMaliyet.Text) < 0)
                            {
                                MessageBox.Show("Benzin maliyeti negatif olamaz.L�tfen benzin maliyetini do�ru giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            x.yakitMaliyetList.Add(Convert.ToInt32(textBoxBenzinMaliyet.Text));
                        }
                        else if (s == "Motorin")
                        {
                            if (textBoxMotorinMaliyet.Text == "")
                            {
                                MessageBox.Show("L�tfen benzin maliyetini giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            else if (Convert.ToInt32(textBoxMotorinMaliyet.Text) < 0)
                            {
                                MessageBox.Show("Benzin maliyeti negatif olamaz.L�tfen benzin maliyetini do�ru giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            x.yakitMaliyetList.Add(Convert.ToInt32(textBoxMotorinMaliyet.Text));
                        }
                        else if (s == "Gaz")
                        {
                            if (textBoxGazMaliyet.Text == "")
                            {
                                MessageBox.Show("L�tfen benzin maliyetini giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            else if (Convert.ToInt32(textBoxGazMaliyet.Text) < 0)
                            {
                                MessageBox.Show("Benzin maliyeti negatif olamaz.L�tfen benzin maliyetini do�ru giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            x.yakitMaliyetList.Add(Convert.ToInt32(textBoxGazMaliyet.Text));
                        }
                        else if (s == "Elektrik")
                        {
                            if (textBoxElektrikMaliyet.Text == "")
                            {
                                MessageBox.Show("L�tfen benzin maliyetini giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            else if (Convert.ToInt32(textBoxElektrikMaliyet.Text) < 0)
                            {
                                MessageBox.Show("Benzin maliyeti negatif olamaz.L�tfen benzin maliyetini do�ru giriniz.");
                                comboBoxVarOlanFirmalar.Items.Remove(x.getUserName());
                                company.companyList.Remove(x);
                                return;
                            }
                            x.yakitMaliyetList.Add(Convert.ToInt32(textBoxElektrikMaliyet.Text));
                        }
                    }
                }
            }

            MessageBox.Show(Username + " isimli firma eklendi.");

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelAdmin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxVarOlanFirmalar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxYeniKullaniciAdi_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelFirmaAdi_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void buttonAracEkle_Click(object sender, EventArgs e)
        {
            string company_name = textBoxKullaniciAdi.Text;
            //MessageBox.Show(company_name);

            Company temp;

            int control_flag = 0;
            foreach (Company x in company.companyList)
            {
                if (x.userName == company_name)
                {
                    temp = x;
                    //bos mu kontrol
                    if (textBoxAracId.Text == "" || comboBoxYakitTuru.Text == "" || textBoxKapasite.Text == "" || comboBoxSeferNo.Text == "")
                    {
                        MessageBox.Show("Ara� olu�turuken l�tfen bo� alan b�rakmay�n.");
                        return;
                    }
                    string arac_id = textBoxAracId.Text;
                    // comboAracId icinde gezme
                    foreach (string s in comboAracId.Items)
                    {
                        if (s == arac_id)
                        {
                            MessageBox.Show("Bu ara� id zaten var. Ba�ka bir ara� id deneyin.");
                            return;
                        }
                    }

                    string yakit_turu = comboBoxYakitTuru.Text;
                    int koltuk_no = Convert.ToInt32(textBoxKapasite.Text);
                    string sefer_no = comboBoxSeferNo.Text;

                    string sefer_turu = "";
                    Trip t = new Trip();
                    foreach (Sefer s in t.getSeferList())
                    {

                        if (sefer_no == s.getSeferIsmi())
                        {
                            sefer_turu = s.getSeferCesidi();
                        }

                    }
                    //MessageBox.Show(sefer_turu);

                    string[] str = arac_id.Split(' ');
                    string arac_turu = str[0];
                    if (arac_turu == "Otob�s")
                    {
                        if (yakit_turu != "Benzin" && yakit_turu != "Motorin")
                        {
                            MessageBox.Show("Yanl�� yak�t t�r� tercihi yapt�n�z. Tekrar deneyin.");
                            return;
                        }
                        if (sefer_turu != "Karayolu")
                        {
                            MessageBox.Show("Yanl�� sefer t�r� tercihi yapt�n�z. Ara� t�r�n�ze uygun �ekilde tekrar deneyin.");
                        }
                    }
                    else if (arac_turu == "Tren")
                    {
                        if (yakit_turu != "Elektrik")
                        {
                            MessageBox.Show("Yanl�� yak�t t�r� tercihi yapt�n�z. Tekrar deneyin.");
                            return;
                        }
                        if (sefer_turu != "Demiryolu")
                        {
                            MessageBox.Show("Yanl�� sefer t�r� tercihi yapt�n�z. Ara� t�r�n�ze uygun �ekilde tekrar deneyin.");
                        }
                    }
                    else if (arac_turu == "U�ak")
                    {
                        if (yakit_turu != "Gaz")
                        {
                            MessageBox.Show("Yanl�� yak�t t�r� tercihi yapt�n�z. Tekrar deneyin.");
                            return;
                        }
                        if (sefer_turu != "Havayolu")
                        {
                            MessageBox.Show("Yanl�� sefer t�r� tercihi yapt�n�z. Ara� t�r�n�ze uygun �ekilde tekrar deneyin.");
                        }
                    }
                    else
                    {
                        return;
                    }

                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "Pazartesi", "Monday");
                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "Sal�", "Tuesday");
                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "�ar�amba", "Wednesday");
                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "Per�embe", "Thursday");
                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "Cuma", "Friday");
                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "Cumartesi", "Saturday");
                    temp.arac_olustur(x.getUserName(), arac_id, yakit_turu, koltuk_no, 0, sefer_no, "Pazar", "Sunday");

                    comboAracId.Items.Add(arac_id);
                    control_flag = 1;
                    break;
                }
            }

            if (control_flag == 0)
            {
                MessageBox.Show("Firma bulunamadi!");
                return;
            }

            MessageBox.Show(textBoxAracId.Text + " isimli ara� eklendi.");
        }

        private void comboAracId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAracSil_Click(object sender, EventArgs e)
        {
            string str = comboAracId.Text;
            if (comboAracId.Text == "")
            {
                MessageBox.Show("L�tfen silenecek eleman� se�ip ondan sonra silme butonuna bas�n�z");
                return;
            }
            string company_name = textBoxKullaniciAdi.Text;
            int control_flag = 0;
            foreach (Company x in company.companyList)
            {
                if (x.userName == company_name)
                {
                    int silinecek = comboAracId.SelectedIndex;
                    string silinecek_str = comboAracId.Text;
                    comboAracId.Items.RemoveAt(silinecek);
                    /*
                    foreach (string s in comboBoxSeferAraci.Items)
                    {
                        if (s == silinecek_str)
                        {
                            comboBoxSeferAraci.Items.Remove(s);
                        }
                    }
                    */

                    List<Vehicle> vehiclesToRemove = new List<Vehicle>();

                    foreach (Vehicle v in x.vehicleList)
                    {
                        if (v.Arac_id == silinecek_str && v.getSirketAdi() == company_name)
                        {
                            vehiclesToRemove.Add(v);
                        }
                    }

                    foreach (Vehicle v in vehiclesToRemove)
                    {
                        x.getVehicleList().Remove(v);
                    }

                    List<Vehicle> vehiclesToRemove2 = new List<Vehicle>();


                    foreach (Vehicle v in x.getStaticVehicleList())
                    {
                        if (v.Arac_id == silinecek_str && v.getSirketAdi() == company_name)
                        {
                            vehiclesToRemove2.Add(v);
                        }
                    }

                    foreach (Vehicle v in vehiclesToRemove)
                    {
                        x.getStaticVehicleList().Remove(v);
                    }

                    /*
                    foreach (Vehicle v in x.getStaticVehicleList())
                    {
                        if (v.Arac_id == silinecek_str && v.getSirketAdi() == company_name)
                        {
                            x.getStaticVehicleList().Remove(v);
                        }
                    }
                    */
                    MessageBox.Show("sorun2");
                    control_flag = 1;
                }
            }
            if (control_flag == 0)
            {
                MessageBox.Show("Firma bulunamadi!");
                return;
            }

            MessageBox.Show(str + " isimli ara� silindi.");
        }

        private void comboBoxYakitTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboAracId.Items.Clear(); eklenecek
            /*string Username = textBoxKullaniciAdi.Text;

            foreach (Company c in company.companyList)
            {
                if (c.userName == Username)
                {
                    MessageBox.Show("adf");
                    foreach (string s in c.yakitList)
                    {
                        comboBoxYakitTuru.Items.Add(s);
                    }
                }
            }*/
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxElektrikMaliyet_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string str = comboBoxSeferNo2.Text;
            if (comboBoxSeferNo2.Text == "")
            {
                MessageBox.Show("L�tfen silenecek eleman� se�ip ondan sonra silme butonuna bas�n�z");
                return;
            }
            string company_name = textBoxKullaniciAdi.Text;
            int control_flag = 0;
            Trip T = new Trip();
            foreach (Company x in company.companyList)
            {
                if (x.userName == company_name)
                {
                    int silinecek = comboBoxSeferNo2.SelectedIndex;
                    comboBoxSeferNo.Items.RemoveAt(silinecek);
                    comboBoxSeferNo2.Items.RemoveAt(silinecek);
                    x.seferList.Remove(x.seferList[silinecek]);
                    T.getSeferList().Remove(T.getSeferList()[silinecek]);

                    control_flag = 1;
                }
            }
            if (control_flag == 0)
            {
                MessageBox.Show("Firma bulunamadi!");
                return;
            }

            MessageBox.Show(str + " isimli sefer silindi.");
        }

        private void buttonYolEkle_Click(object sender, EventArgs e)
        {
            //bos mu kontrol
            if (comboBoxYollar.Text == "" || checkedListBoxSehirler.CheckedItems.Count == 0)
            {
                MessageBox.Show("L�tfen sefer olu�tururken bo� alan b�rakmay�n�z!");
                return;
            }
            else if (checkedListBoxSehirler.CheckedItems.Count == 1)
            {
                MessageBox.Show("L�tfen birden fazla �ehir se�iniz.");
                return;
            }

            if (comboBoxYollar.Text == "Karayolu") //karayolu
            {
                foreach (string s in checkedListBoxSehirler.CheckedItems)
                {
                    if (s == "Bilecik")
                    {
                        MessageBox.Show("Bilecik i�in karayolu ula��m� yoktur.");
                        return;
                    }
                }

            }
            else if (comboBoxYollar.Text == "Demiryolu")
            {

            }
            else if (comboBoxYollar.Text == "Havayolu")
            {
                foreach (string s in checkedListBoxSehirler.CheckedItems)
                {
                    if (s != "Istanbul" && s != "Ankara" && s != "Konya")
                    {
                        MessageBox.Show("Istanbul-Ankara-Konya harici havayolu ula��m� se�ilemez.");
                        return;
                    }
                }
            }
            string yollanacak = comboBoxYollar.Text + ":";
            int ilk_flag = 0;
            int count = checkedListBoxSehirler.CheckedItems.Count;
            string[] stringDizisi = new string[count];
            int sayan = 0;
            foreach (string s in checkedListBoxSehirler.CheckedItems)
            {
                if (ilk_flag == 0)
                {
                    stringDizisi[sayan] = s;
                    sayan++;
                    yollanacak += s;
                    ilk_flag = 1;
                    continue;
                }

                yollanacak += " - ";
                yollanacak += s;
                stringDizisi[sayan] = s;
                sayan++;
            }
            for (int i = count - 2; i >= 0; i--)
            {
                yollanacak += " - ";
                yollanacak += stringDizisi[i];
            }
            Trip t = new Trip();
            t.seferEkle(yollanacak);
            //MessageBox.Show(s1);

            int x = t.getSefer_sayisi();
            //MessageBox.Show(x.ToString());

            //c�k�p girince gidecek bu

            comboBoxSeferNo.Items.Add(x + ". Sefer");
            comboBoxSeferNo2.Items.Add(x + ". Sefer");

            string str = x + ". Sefer";

            //company sinifinin sefer listesine de eklenmeli
            string company_name = textBoxKullaniciAdi.Text;
            foreach (Company c in company.companyList)
            {
                if (c.userName == company_name)
                {
                    c.seferList.Add(x + ". Sefer");
                }
            }

            MessageBox.Show(str + " isimli sefer eklendi.");

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxSeferNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSeferAraci_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePickerTarih.MinDate = new DateTime(2023, 12, 4);
            dateTimePickerTarih.MaxDate = new DateTime(2023, 12, 10);

            dateTimePickerTarih.Format = DateTimePickerFormat.Custom;
            dateTimePickerTarih.CustomFormat = "dddd dd MMMM yyyy";

            dateTimePickerOdeme.Format = DateTimePickerFormat.Custom;
            dateTimePickerOdeme.CustomFormat = "dd MMMM yyyy";
        }

        private void checkedListBoxSeferler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("fonk");

            Transport t = new Transport();
            int i = 0;
            string z = "";

            foreach (Vehicle v in t.getStaticTumSeferler())
            {
                z += v.getArac_id() + " " + v.getSirketAdi() + " " + v.getSefer_no() + " ";
                i++;

            }
            MessageBox.Show(i.ToString());
        }

        private void buttonSeferiSec_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSeferler.CheckedItems.Count != 1)
            {
                MessageBox.Show("L�tfen sadece 1 tane sefer se�iniz.");
                return;
            }

            string str = "";

            foreach (string selecteditem in checkedListBoxSeferler.CheckedItems)
            {
                str = selecteditem;
            }


            string ardahan = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    while (i < str.Length && (i + 1 < str.Length) && str[i + 1] == ' ')
                    {
                        i++;
                    }
                    ardahan += ' ';
                }
                else
                {
                    ardahan += str[i];
                }
            }
            string[] for_split = ardahan.Split(' ');
            string sirket = for_split[0];
            string arac = for_split[1] + " " + for_split[2];

            Company c = new Company();

            string gun = dateTimePickerTarih.Value.ToString("dddd");
            foreach (Vehicle v in c.getStaticVehicleList())
            {
                if (v.getArac_id() == arac && v.getSirketAdi() == sirket && (v.getGun_eng() == gun || v.getGun_tr() == gun))
                {
                    //MessageBox.Show(gun);
                    //String.Format("{0,-20} {1,-20} {2,-20}", v.getSirketAdi(), v.getArac_id(), v.getDoluKoltuk().ToString() + "/" + v.getKoltuk_sayisi())
                    string yazdirmak_icin2 = String.Format("�irket Ad� | Ara� �smi | Dolu Koltuk | Toplam Koltuk | Sefer Numaras�");
                    labelSehirler.Text = yazdirmak_icin2;
                    string yazdirmak_icin = String.Format("{0,-20} {1,-20} {2,-20} {3,-20} {4,-20}", v.getSirketAdi(), v.getArac_id(), v.getDoluKoltuk(), v.getKoltuk_sayisi(), v.getSefer_no());
                    labelSeferBilgi.Text = yazdirmak_icin;

                    string kalkis = comboBoxKalkisNoktasi.Text;
                    string varis = comboBoxVarisNoktasi.Text;


                    int kalkis_index = v.getSehirler().IndexOf(kalkis);
                    int varis_index = v.getSehirler().IndexOf(varis);
                    if (kalkis_index > varis_index)
                    {
                        kalkis_index = v.getSehirler().LastIndexOf(kalkis);
                        varis_index = v.getSehirler().LastIndexOf(varis);
                    }

                    List<Koltuk> kol_list = new List<Koltuk>();
                    kol_list = v.getKoltuk_ic_ice()[kalkis_index];
                    //MessageBox.Show(kalkis_index.ToString() + " " + varis_index.ToString());
                    int sayi1 = 0;
                    for (int i = kalkis_index; i < varis_index; i++)
                    {
                        int sayici = 0;
                        foreach (Koltuk k in v.getKoltuk_ic_ice()[i])
                        {
                            if (k.getDoluluk_durumu() == 1)
                            {
                                sayici++;
                            }
                        }
                        //MessageBox.Show(sayici.ToString());
                        if (sayici > sayi1)
                        {
                            sayi1 = sayici;
                            kol_list = v.getKoltuk_ic_ice()[i];
                        }
                    }

                    int sayi = 1;
                    foreach (Koltuk k in kol_list)
                    {
                        if (k.getDoluluk_durumu() == 1)
                        {
                            string koltuk_name = sayi.ToString() + ". Koltuk";
                            comboBoxDoluKoltuklar.Items.Add(koltuk_name);
                        }
                        sayi++;
                    }

                    sayi = 1;
                    foreach (Koltuk k in kol_list)
                    {
                        if (k.getDoluluk_durumu() == 0)
                        {
                            string koltuk_name = sayi.ToString() + ". Koltuk";
                            checkedListBoxBosKoltuklar.Items.Add(koltuk_name);
                        }
                        sayi++;
                    }
                }
            }

            labelKalkisa.Text = comboBoxKalkisNoktasi.Text;
            labelVarisb.Text = comboBoxVarisNoktasi.Text;
            panelBilet.Visible = true;
            panel1.Visible = false;
            panelSeferler.Visible = false;
        }

        private void buttonKoltukSec_Click(object sender, EventArgs e)
        {
            int up_down_sayi = ((int)numericUpDownBiletSayisi.Value);
            int secili_koltuk_sayisi = checkedListBoxBosKoltuklar.CheckedItems.Count;

            if (up_down_sayi != secili_koltuk_sayisi)
            {
                MessageBox.Show("Bilet say�s� ve se�ili koltuk say�s� e�it olmal�d�r.");
                return;
            }

            if (up_down_sayi == 1)
            {
                Trip T = new Trip();

                int bastirilacak_fiyat = -1;
                string str = "";
                foreach (string selecteditem in checkedListBoxSeferler.CheckedItems)
                {
                    str = selecteditem;
                }
                string ardahan = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        while (i < str.Length && (i + 1 < str.Length) && str[i + 1] == ' ')
                        {
                            i++;
                        }
                        ardahan += ' ';
                    }
                    else
                    {
                        ardahan += str[i];
                    }
                }

                string[] for_split = ardahan.Split(' ');
                string sirket = for_split[0]; //silebilirsin
                string arac = for_split[1] + " " + for_split[2];//silebilirsin

                string kalkis = comboBoxKalkisNoktasi.Text;
                int kalkis_index = -1;
                string varis = comboBoxVarisNoktasi.Text;
                int varis_index = -1;

                int fiyat = -1;

                if (for_split[1] == "Otob�s")
                {
                    for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                    {
                        if (T.karayol_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.karayol_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }

                    fiyat = T.karayol_fiyatlar[kalkis_index][varis_index];


                }
                else if (for_split[1] == "U�ak")
                {
                    for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                    {
                        if (T.havayolu_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.havayolu_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }
                    fiyat = T.havayolu_fiyatlar[kalkis_index][varis_index];
                }
                else if (for_split[1] == "Tren")
                {
                    for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                    {
                        if (T.demiryolu_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.demiryolu_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }
                    fiyat = T.demiryolu_fiyatlar[kalkis_index][varis_index];
                }

                bastirilacak_fiyat = fiyat * ((int)numericUpDownBiletSayisi.Value);
                labelOdemeTutar.Text = bastirilacak_fiyat.ToString();

                button3.Visible = false;
                buttonRandomBilet.Visible = false;
                buttonOncekiBilet.Visible = false;
                button4.Visible = true;
                labelOdemeTutar.Visible = true;
            }
            else
            {
                button3.Visible = true;
                buttonRandomBilet.Visible = true;
                buttonOncekiBilet.Visible = false;
                button4.Visible = false;
            }

            labelKacinciBilet.Text = "1. Bilet";
            panelOdeme.Visible = true;
            panelBilet.Visible = false;
        }

        private void buttonBiletGeriDon_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panelSeferler.Visible = true;
            panelBilet.Visible = false;
            numericUpDownBiletSayisi.Value = 0;
            comboBoxDoluKoltuklar.SelectedItem = null;
            comboBoxDoluKoltuklar.Items.Clear();
            checkedListBoxBosKoltuklar.Items.Clear();

        }

        private void panelOdeme_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();

            List<string> secililer = new List<string>();
            foreach (string s3 in checkedListBoxBosKoltuklar.CheckedItems)
            {
                secililer.Add(s3);
            }
            Passenger p = new Passenger(textBoxAd.Text, textBoxSoyad.Text, textBoxTCKimlik.Text, dateTimePickerOdeme.Text.ToString(), secililer[s.getSayi()]);

            labelOnayTutar.Text = labelOdemeTutar.Text;
            panelOdeme.Visible = false;
            panelSON.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //bilet no
            int bilet_sayisi = ((int)numericUpDownBiletSayisi.Value);
            int bastirilacak_fiyat = -1;
            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();
            //listeler

            //kontroller
            string tc_temp = textBoxTCKimlik.Text;
            for (int i = 0; i < tc_temp.Length; i++)
            {
                if (!(tc_temp[i] >= '0' && tc_temp[i] <= '9'))
                {
                    MessageBox.Show("Lutfen TC Kimlik Numaras� k�sm�na rakam harici karaker girmeyin.");
                    return;
                }
            }

            if (tc_temp.Length != 11)
            {
                MessageBox.Show("TC Kimlik Numaras� 11 hane olmal�d�r.");
                return;
            }

            List<string> secililer = new List<string>();
            foreach (string s3 in checkedListBoxBosKoltuklar.CheckedItems)
            {
                secililer.Add(s3);
            }

            if (s.getSayi() >= 0 && s.getSayi() < bilet_sayisi)
            {

                Passenger p2 = new Passenger(textBoxAd.Text, textBoxSoyad.Text, textBoxTCKimlik.Text, dateTimePickerOdeme.Text.ToString(), secililer[s.getSayi()]);
                s.arttir();
            }
            if (s.getSayi() == bilet_sayisi - 1)
            {

                Trip T = new Trip();

                string str = "";
                foreach (string selecteditem in checkedListBoxSeferler.CheckedItems)
                {
                    str = selecteditem;
                }
                string ardahan = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        while (i < str.Length && (i + 1 < str.Length) && str[i + 1] == ' ')
                        {
                            i++;
                        }
                        ardahan += ' ';
                    }
                    else
                    {
                        ardahan += str[i];
                    }
                }

                string[] for_split = ardahan.Split(' ');
                string sirket = for_split[0]; //silebilirsin
                string arac = for_split[1] + " " + for_split[2];//silebilirsin

                string kalkis = comboBoxKalkisNoktasi.Text;
                int kalkis_index = -1;
                string varis = comboBoxVarisNoktasi.Text;
                int varis_index = -1;

                int fiyat = -1;

                if (for_split[1] == "Otob�s")
                {
                    for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                    {
                        if (T.karayol_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.karayol_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }

                    fiyat = T.karayol_fiyatlar[kalkis_index][varis_index];


                }
                else if (for_split[1] == "U�ak")
                {
                    for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                    {
                        if (T.havayolu_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.havayolu_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }
                    fiyat = T.havayolu_fiyatlar[kalkis_index][varis_index];
                }
                else if (for_split[1] == "Tren")
                {
                    for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                    {
                        if (T.demiryolu_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.demiryolu_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }
                    fiyat = T.demiryolu_fiyatlar[kalkis_index][varis_index];
                }

                bastirilacak_fiyat = fiyat * ((int)numericUpDownBiletSayisi.Value);
                labelOdemeTutar.Visible = true;
                button3.Visible = false;
                buttonRandomBilet.Visible = false;
                button4.Visible = true;
            }
            labelOdemeTutar.Text = bastirilacak_fiyat.ToString();

            buttonOncekiBilet.Visible = true;
            //aMessageBox.Show(textBoxAdSoyad.Text + " " + textBoxTCKimlik.Text + " " + dateTimePickerOdeme.Text.ToString());
            labelKacinciBilet.Text = String.Format("{0}. Bilet", s.getSayi() + 1);
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxTCKimlik.Clear();
            dateTimePickerOdeme.Value = DateTime.Now;
        }

        private void buttonOdemeGeriDon_Click(object sender, EventArgs e)
        {
            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();
            s.setSayi(0);
            panelOdeme.Visible = false;
            panelBilet.Visible = true;
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxTCKimlik.Clear();
            dateTimePickerOdeme.Value = DateTime.Now;
        }

        private void buttonOncekiBilet_Click(object sender, EventArgs e)
        {
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxTCKimlik.Clear();
            dateTimePickerOdeme.Value = DateTime.Now;

            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();
            int bilet_sayisi = ((int)numericUpDownBiletSayisi.Value);

            if (s.getSayi() >= 0 && s.getSayi() < bilet_sayisi)
            {
                s.azalt();
            }

            if (s.getSayi() == 0)
            {
                buttonOncekiBilet.Visible = false;
            }
            labelKacinciBilet.Text = String.Format("{0}. Bilet", s.getSayi() + 1);
            button3.Visible = true;
            buttonRandomBilet.Visible = true;

            button4.Visible = false;
            labelOdemeTutar.Visible = false;
            label24.Visible = false;

            Passenger p = new Passenger();
            p.getStaticPassangerList().RemoveAt(p.getStaticPassangerList().Count - 1);
        }

        private void buttonSONGeriDon_Click(object sender, EventArgs e)
        {
            //passanger son eleman silinir
            Passenger p = new Passenger();
            p.getStaticPassangerList().RemoveAt(p.getStaticPassangerList().Count - 1);

            panelSON.Visible = false;
            panelOdeme.Visible = true;
            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();
            s.setSayi(0);
            textBoxKartNumaras�.Clear();
            textBoxKartCVV.Clear();
            comboBoxKartAy.SelectedItem = null;
            comboBoxKartY�l.SelectedItem = null;
        }

        private void labelKacinciBilet_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBoxKartNumaras�.Text.Length != 16)
            {
                MessageBox.Show("Kart numaras� 16 hane olmal�d�r.");
                return;
            }

            if (textBoxKartCVV.Text.Length != 3)
            {
                MessageBox.Show("CVV 3 hane olmal�d�r.");
                return;
            }

            string str = "";
            foreach (string selecteditem in checkedListBoxSeferler.CheckedItems)
            {
                str = selecteditem;
            }
            string ardahan = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    while (i < str.Length && (i + 1 < str.Length) && str[i + 1] == ' ')
                    {
                        i++;
                    }
                    ardahan += ' ';
                }
                else
                {
                    ardahan += str[i];
                }
            }

            string[] for_split = ardahan.Split(' ');
            string sirket = for_split[0]; //silebilirsin
            string arac = for_split[1] + " " + for_split[2];//silebilirsin

            List<int> secilmis_koltuklar = new List<int>();
            string bos = "";
            foreach (string str1 in checkedListBoxBosKoltuklar.CheckedItems)
            {
                string[] strArr = str1.Split('.');
                int no = Convert.ToInt32(strArr[0]);
                bos += no.ToString() + "\n";
                secilmis_koltuklar.Add(no);
            }

            string gun = dateTimePickerTarih.Value.ToString("dddd");
            Company C = new Company();
            foreach (Vehicle v in C.getStaticVehicleList())
            {
                if (v.getArac_id() == arac && v.getSirketAdi() == sirket && (v.getGun_eng() == gun || v.getGun_tr() == gun))
                {
                    string kalkis = comboBoxKalkisNoktasi.Text;
                    int kalkis_index = v.getSehirler().IndexOf(kalkis);
                    string varis = comboBoxVarisNoktasi.Text;
                    int varis_index = v.getSehirler().IndexOf(varis);
                    if (kalkis_index > varis_index)
                    {
                        kalkis_index = v.getSehirler().LastIndexOf(kalkis);
                        varis_index = v.getSehirler().LastIndexOf(varis);
                    }
                    for (int i = kalkis_index; i < varis_index; i++)
                    {
                        foreach (int koltuk_no in secilmis_koltuklar)
                        {
                            v.getKoltuk_ic_ice()[i][koltuk_no - 1].setDoluluk_durumu(1);
                            //v.set
                        }
                    }
                }
            }

            Passenger p = new Passenger();
            //labelBiletBilgi1
            foreach (Passenger p1 in p.getStaticPassangerList())
            {
                listBoxBiletler.Items.Add(p1.getAd() + " " + p1.getSoyad() + " " + p1.getKoltuk() + "\n");
                //labelBiletBilgi1 += p1.getAd() + " " + p1.getSoyad() + " " + p1.getKoltuk() + "\n";
            }
            //labelOnayTutar.Text
            foreach (Vehicle v in C.getStaticVehicleList())
            {
                if (v.getArac_id() == arac && v.getSirketAdi() == sirket && (v.getGun_eng() == gun || v.getGun_tr() == gun))
                {
                    v.setSatilan_bilet_gelir(v.getSatilan_bilet_gelir() + Convert.ToInt32(labelOnayTutar.Text));
                }
            }

            labelBiletBilgi2.Text = comboBoxKalkisNoktasi.Text + " - " + comboBoxVarisNoktasi.Text;
            //labelBiletBilgi3.Text = comboBoxVarisNoktasi.Text;

            panelSON.Visible = false;
            panelOnay.Visible = true;
        }

        private void buttonAnaMen�_Click(object sender, EventArgs e)
        {
            panelOnay.Visible = false;
            panel1.Visible = true;
            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();
            s.setSayi(0);


            textBoxKullaniciAdi.Clear();
            textBoxSifre.Clear();
            comboBoxKalkisNoktasi.SelectedItem = null;
            comboBoxVarisNoktasi.SelectedItem = null;
            //dateTimePickerTarih.Value = DateTime.MinValue;
            comboBoxVarOlanFirmalar.SelectedItem = null;
            comboBoxVarOlanFirmalar.Items.Clear();
            textBoxYeniKullaniciAdi.Clear();
            textBoxYeniSifre.Clear();
            checkedListBox1.Items.Clear();
            textBoxBenzinMaliyet.Clear();
            textBoxMotorinMaliyet.Clear();
            textBoxGazMaliyet.Clear();
            textBoxElektrikMaliyet.Clear();
            numericUpDownBiletSayisi.Value = 0;
            comboBoxDoluKoltuklar.SelectedItem = null;
            comboBoxDoluKoltuklar.Items.Clear();
            checkedListBoxBosKoltuklar.Items.Clear();
            checkedListBoxSeferler.Items.Clear();
            comboAracId.SelectedItem = null;
            comboAracId.Items.Clear();
            textBoxAracId.Clear();
            comboBoxYakitTuru.SelectedItem = null;
            comboBoxYakitTuru.Items.Clear();
            textBoxKapasite.Clear();
            comboBoxSeferNo.SelectedItem = null;
            comboBoxSeferNo.Items.Clear();
            comboBoxSeferNo2.SelectedItem = null;
            comboBoxSeferNo2.Items.Clear();
            comboBoxYollar.SelectedItem = null;
            comboBoxYollar.Items.Clear();
            checkedListBoxSehirler.Items.Clear();
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxTCKimlik.Clear();
            dateTimePickerOdeme.Value = DateTime.Now;
            textBoxKartNumaras�.Clear();
            textBoxKartCVV.Clear();
            comboBoxKartAy.SelectedItem = null;
            comboBoxKartY�l.SelectedItem = null;
            labelBiletBilgi1.Text = "";

            Passenger p = new Passenger();
            p.getStaticPassangerList().Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonKarHesaplama_Click(object sender, EventArgs e)
        {
            if (comboKarGunler.Text == "")
            {
                MessageBox.Show("G�n se�imi yapmay� unutmay�n.");
                return;
            }

            string secili_gun = comboKarGunler.Text;
            string secili_sirket = textBoxKullaniciAdi.Text;

            //labelGun
            //labelHesapSonucu
            labelGun.Text = "";
            labelHesapSonucu.Text = "";

            labelGun.Text = secili_gun + ": ";

            int giderler = -1;
            int gelirler = -1;

            string gun = comboKarGunler.Text;
            Company c = new Company();
            foreach (Company c1 in c.getStaticCompany())
            {
                if (c1.getUserName() == secili_sirket)
                {
                    giderler = c1.gunluk_gider_hesapla(gun);
                    gelirler = c1.gunluk_gelir_hesapla(gun);
                }
            }

            int kar = gelirler - giderler;
            MessageBox.Show(kar.ToString() + " = " + gelirler.ToString() + " - " + giderler.ToString());

            //�U AN SADECE G�DERLER YAP�L�
            labelHesapSonucu.Text = kar.ToString();
        }

        private void labelGun_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAdminGeriDon_Click(object sender, EventArgs e)
        {
            comboBoxVarOlanFirmalar.SelectedItem = null;
            textBoxYeniKullaniciAdi.Clear();
            textBoxYeniSifre.Clear();
            textBoxHizmetBedeli.Clear();
            textBoxBenzinMaliyet.Clear();
            textBoxMotorinMaliyet.Clear();
            textBoxGazMaliyet.Clear();
            textBoxElektrikMaliyet.Clear();

            panelAdmin.Visible = false;
            panel1.Visible = true;
        }

        private void buttonFirmaGeriDon_Click(object sender, EventArgs e)
        {
            comboKarGunler.SelectedItem = null;
            comboAracId.SelectedItem = null;
            textBoxAracId.Clear();
            comboBoxYakitTuru.SelectedItem = null;
            textBoxKapasite.Clear();
            comboBoxSeferNo.SelectedItem = null;
            comboBoxSeferNo2.SelectedItem = null;
            comboBoxYollar.SelectedItem = null;

            panelFirma.Visible = false;
            panel1.Visible = true;
        }

        private void labelOdemeTutar_Click(object sender, EventArgs e)
        {

        }

        private void buttonRandomBilet_Click(object sender, EventArgs e)
        {
            int bilet_sayisi = ((int)numericUpDownBiletSayisi.Value);
            int bastirilacak_fiyat = -1;
            staticKisiSayisiTutucu s = new staticKisiSayisiTutucu();


            List<string> secililer = new List<string>();
            foreach (string s3 in checkedListBoxBosKoltuklar.CheckedItems)
            {
                secililer.Add(s3);
            }

            if (s.getSayi() >= 0 && s.getSayi() < bilet_sayisi)
            {

                Passenger p2 = new Passenger("PersonName" + s.getSayi().ToString(), "PersonSurname" + s.getSayi().ToString(), (12345678901 + s.getSayi()).ToString(), dateTimePickerOdeme.Text.ToString(), secililer[s.getSayi()]);
                s.arttir();
            }
            if (s.getSayi() == bilet_sayisi - 1)
            {

                Trip T = new Trip();

                string str = "";
                foreach (string selecteditem in checkedListBoxSeferler.CheckedItems)
                {
                    str = selecteditem;
                }
                string ardahan = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ')
                    {
                        while (i < str.Length && (i + 1 < str.Length) && str[i + 1] == ' ')
                        {
                            i++;
                        }
                        ardahan += ' ';
                    }
                    else
                    {
                        ardahan += str[i];
                    }
                }

                string[] for_split = ardahan.Split(' ');
                string sirket = for_split[0]; //silebilirsin
                string arac = for_split[1] + " " + for_split[2];//silebilirsin

                string kalkis = comboBoxKalkisNoktasi.Text;
                int kalkis_index = -1;
                string varis = comboBoxVarisNoktasi.Text;
                int varis_index = -1;

                int fiyat = -1;

                if (for_split[1] == "Otob�s")
                {
                    for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                    {
                        if (T.karayol_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.karayol_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }

                    fiyat = T.karayol_fiyatlar[kalkis_index][varis_index];


                }
                else if (for_split[1] == "U�ak")
                {
                    for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                    {
                        if (T.havayolu_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.havayolu_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }
                    fiyat = T.havayolu_fiyatlar[kalkis_index][varis_index];
                }
                else if (for_split[1] == "Tren")
                {
                    for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                    {
                        if (T.demiryolu_sehirleri[i] == varis)
                        {
                            varis_index = i;
                        }
                        if (T.demiryolu_sehirleri[i] == kalkis)
                        {
                            kalkis_index = i;
                        }
                    }
                    fiyat = T.demiryolu_fiyatlar[kalkis_index][varis_index];
                }
                bastirilacak_fiyat = fiyat * ((int)numericUpDownBiletSayisi.Value);
                labelOdemeTutar.Visible = true;
                button3.Visible = false;
                buttonRandomBilet.Visible = false;
                button4.Visible = true;
            }
            labelOdemeTutar.Text = bastirilacak_fiyat.ToString();

            buttonOncekiBilet.Visible = true;
            //aMessageBox.Show(textBoxAdSoyad.Text + " " + textBoxTCKimlik.Text + " " + dateTimePickerOdeme.Text.ToString());
            labelKacinciBilet.Text = String.Format("{0}. Bilet", s.getSayi() + 1);
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxTCKimlik.Clear();
            dateTimePickerOdeme.Value = DateTime.Now;
        }

        private void panelOnay_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBoxBiletler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class staticKisiSayisiTutucu
    {

        static int sayi;
        public staticKisiSayisiTutucu()
        {

        }

        public void azalt()
        {
            sayi--;
        }

        public void arttir()
        {
            sayi++;
        }

        public int getSayi()
        {
            return sayi;
        }

        public void setSayi(int v)
        {
            sayi = v;
        }
    }

    public abstract class User : ILoginable
    {
        public string userName = "";
        public string password = "";
        public bool loginable = false;

        public virtual void setToLoginable()
        {
            loginable = true;
        }

        public virtual string getUserName()
        {
            return userName;
        }
        public virtual void setUserName(string userName)
        {
            this.userName = userName;
        }

        public virtual string getPassword()
        {
            return password;
        }
        public virtual void setPassword(string password)
        {
            this.password = password;
        }

        public virtual bool getLoginable()
        {
            return this.loginable;
        }
        public virtual void setLoginable(bool b)
        {
            this.loginable = b;
        }
    }
    public class Admin : User
    {
        public Admin()
        {
            this.setToLoginable();
        }
        public override void setToLoginable()
        {
            loginable = true;
        }

        public override string getUserName()
        {
            return userName;
        }
        public override void setUserName(string userName)
        {
            this.userName = userName;
        }
        public override string getPassword()
        {
            return password;
        }
        public override void setPassword(string password)
        {
            this.password = password;
        }
        public override bool getLoginable()
        {
            return this.loginable;
        }
        public override void setLoginable(bool b)
        {
            this.loginable = b;
        }
    }

    public class Company : User, IProfitable
    {
        static int ilk_sefer = 0;
        public List<Company> companyList = new List<Company>();
        public static List<Company> static_companyList = new List<Company>();
        public List<Vehicle> vehicleList = new List<Vehicle>();
        public List<string> yakitList = new List<string>();
        public List<int> yakitMaliyetList = new List<int>();
        public List<string> seferList = new List<string>();
        public static List<Vehicle> static_vehicle = new List<Vehicle>();
        public List<int> karayolu_personel_fiyat = new List<int>(); //hepsi 2 boyutlu
        public List<int> havayolu_personel_fiyat = new List<int>();
        public List<int> demiryolu_personel_fiyat = new List<int>();
        public int hizmet_bedeli;
        public int gunluk_gider;

        public Company()
        {
            this.setToLoginable();
            if (ilk_sefer == 0)
            {
                this.ilk_atama_personel_fiyat();
                Company A = new Company("A", "sifreA", 1000);
                Company B = new Company("B", "sifreB", 1000);
                Company C = new Company("C", "sifreC", 1000);
                Company D = new Company("D", "sifreD", 1000);
                //Company E = new Company("E", "sifreE");
                Company F = new Company("F", "sifreF", 1000);

                companyList.Add(A);
                companyList.Add(B);
                companyList.Add(C);
                companyList.Add(D);
                //companyList.Add(E);
                companyList.Add(F);



                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "Pazartesi", "Monday");
                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "Sal�", "Tuesday");
                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "�ar�amba", "Wednesday");
                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "Per�embe", "Thursday");
                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "Cuma", "Friday");
                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "Cumartesi", "Saturday");
                A.arac_olustur("A", "Otob�s 1", "Benzin", 20, 18, "3. Sefer", "Pazar", "Sunday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "Pazartesi", "Monday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "Sal�", "Tuesday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "�ar�amba", "Wednesday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "Per�embe", "Thursday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "Cuma", "Friday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "Cumartesi", "Saturday");
                A.arac_olustur("A", "Otob�s 2", "Benzin", 15, 10, "3. Sefer", "Pazar", "Sunday");
                A.yakitList.Add("Benzin");
                A.yakitMaliyetList.Add(10);
                A.seferList.Add("3. Sefer");
                A.getKarayolu_personel_fiyat()[0] = 5000 * 2;
                A.getKarayolu_personel_fiyat()[1] = 2000 * 2;


                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "Pazartesi", "Monday");
                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "Sal�", "Tuesday");
                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "�ar�amba", "Wednesday");
                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "Per�embe", "Thursday");
                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "Cuma", "Friday");
                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "Cumartesi", "Saturday");
                B.arac_olustur("B", "Otob�s 1", "Motorin", 15, 7, "3. Sefer", "Pazar", "Sunday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "Pazartesi", "Monday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "Sal�", "Tuesday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "�ar�amba", "Wednesday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "Per�embe", "Thursday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "Cuma", "Friday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "Cumartesi", "Saturday");
                B.arac_olustur("B", "Otob�s 2", "Motorin", 20, 9, "4. Sefer", "Pazar", "Sunday");
                B.yakitList.Add("Motorin");
                B.yakitMaliyetList.Add(5);
                B.seferList.Add("3. Sefer");
                B.seferList.Add("4. Sefer");
                B.getKarayolu_personel_fiyat()[0] = 3000 * 2;
                B.getKarayolu_personel_fiyat()[1] = 1500 * 2;


                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "Pazartesi", "Monday");
                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "Sal�", "Tuesday");
                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "�ar�amba", "Wednesday");
                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "Per�embe", "Thursday");
                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "Cuma", "Friday");
                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "Cumartesi", "Saturday");
                C.arac_olustur("C", "Otob�s 1", "Motorin", 20, 19, "4. Sefer", "Pazar", "Sunday");

                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "Pazartesi", "Monday");
                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "Sal�", "Tuesday");
                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "�ar�amba", "Wednesday");
                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "Per�embe", "Thursday");
                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "Cuma", "Friday");
                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "Cumartesi", "Saturday");
                C.arac_olustur("C", "U�ak 1", "Gaz", 30, 12, "5. Sefer", "Pazar", "Sunday");

                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "Pazartesi", "Monday");
                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "Sal�", "Tuesday");
                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "�ar�amba", "Wednesday");
                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "Per�embe", "Thursday");
                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "Cuma", "Friday");
                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "Cumartesi", "Saturday");
                C.arac_olustur("C", "U�ak 2", "Gaz", 30, 9, "5. Sefer", "Pazar", "Sunday");
                C.yakitList.Add("Motorin");
                C.yakitMaliyetList.Add(6);
                C.yakitList.Add("Gaz");
                C.yakitMaliyetList.Add(25);
                C.seferList.Add("4. Sefer");
                C.seferList.Add("5. Sefer");
                C.getKarayolu_personel_fiyat()[0] = 4000 * 2;
                C.getKarayolu_personel_fiyat()[1] = 2000 * 2;

                C.getHavayolu_personel_fiyat()[0] = 10000 * 2;
                C.getHavayolu_personel_fiyat()[0] = 6000 * 2;


                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "Pazartesi", "Monday");
                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "Sal�", "Tuesday");
                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "�ar�amba", "Wednesday");
                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "Per�embe", "Thursday");
                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "Cuma", "Friday");
                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "Cumartesi", "Saturday");
                D.arac_olustur("D", "Tren 1", "Elektrik", 25, 12, "1. Sefer", "Pazar", "Sunday");

                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "Pazartesi", "Monday");
                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "Sal�", "Tuesday");
                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "�ar�amba", "Wednesday");
                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "Per�embe", "Thursday");
                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "Cuma", "Friday");
                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "Cumartesi", "Saturday");
                D.arac_olustur("D", "Tren 2", "Elektrik", 25, 11, "2. Sefer", "Pazar", "Sunday");

                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "Pazartesi", "Monday");
                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "Sal�", "Tuesday");
                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "�ar�amba", "Wednesday");
                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "Per�embe", "Thursday");
                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "Cuma", "Friday");
                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "Cumartesi", "Saturday");
                D.arac_olustur("D", "Tren 3", "Elektrik", 25, 9, "2. Sefer", "Pazar", "Sunday");

                D.yakitList.Add("Elektrik");
                D.yakitMaliyetList.Add(3);
                D.seferList.Add("1. Sefer");
                D.seferList.Add("2. Sefer");
                D.getDemiryolu_personel_fiyat()[0] = 2000 * 2;
                D.getDemiryolu_personel_fiyat()[1] = 1000 * 2;


                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "Pazartesi", "Monday");
                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "Sal�", "Tuesday");
                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "�ar�amba", "Wednesday");
                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "Per�embe", "Thursday");
                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "Cuma", "Friday");
                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "Cumartesi", "Saturday");
                F.arac_olustur("F", "U�ak 1", "Gaz", 30, 19, "6. Sefer", "Pazar", "Sunday");

                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "Pazartesi", "Monday");
                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "Sal�", "Tuesday");
                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "�ar�amba", "Wednesday");
                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "Per�embe", "Thursday");
                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "Cuma", "Friday");
                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "Cumartesi", "Saturday");
                F.arac_olustur("F", "U�ak 2", "Gaz", 30, 7, "6. Sefer", "Pazar", "Sunday");
                F.yakitList.Add("Gaz");
                F.yakitMaliyetList.Add(20);
                F.seferList.Add("6. Sefer");
                F.getHavayolu_personel_fiyat()[0] = 7500 * 2;
                F.getHavayolu_personel_fiyat()[1] = 4000 * 2;

                this.standart_personel_olusturma();

                ilk_sefer = 1;
            }

        }

        public Company(string userName, string password, int hizmet_bedeli)
        {
            this.setToLoginable();
            this.userName = userName;
            this.password = password;
            this.hizmet_bedeli = hizmet_bedeli;
            static_companyList.Add(this);
            this.ilk_atama_personel_fiyat();
            this.ilk_atama_yeni_personel_fiyat();
        }

        public void ilk_atama_personel_fiyat()
        {
            karayolu_personel_fiyat.Add(-1);
            karayolu_personel_fiyat.Add(-1);

            havayolu_personel_fiyat.Add(-1);
            havayolu_personel_fiyat.Add(-1);

            demiryolu_personel_fiyat.Add(-1);
            demiryolu_personel_fiyat.Add(-1);
        }

        public override void setToLoginable()
        {
            loginable = true;
        }
        public override bool getLoginable()
        {
            return this.loginable;
        }
        public override void setLoginable(bool b)
        {
            this.loginable = b;
        }

        public void ilk_atama_yeni_personel_fiyat()
        {

            this.getKarayolu_personel_fiyat()[0] = 2500 * 2;
            this.getKarayolu_personel_fiyat()[1] = 1000 * 2;

            this.getHavayolu_personel_fiyat()[0] = 10000 * 2;
            this.getHavayolu_personel_fiyat()[1] = 4000 * 2;

            this.getDemiryolu_personel_fiyat()[0] = 2000 * 2;
            this.getDemiryolu_personel_fiyat()[1] = 1000 * 2;
        }

        public void arac_olustur(string sirket_adi, string arac_id, string yakit_turu, int koltuk_no, int dolu_koltuk_sayisi, string sefer_no, string gun_tr, string gun_eng)
        {
            int carpici = 1; //3. seferde birden fazla gidis-gelis olayi var - karayolu->otobuse eklemek yeterli
            if (sefer_no == "3. Sefer")
            {
                carpici = 2;
            }

            string[] str = arac_id.Split(' ');
            string arac_turu = str[0];
            if (arac_turu == "Otob�s")
            {
                Bus new_bus = new Bus(sirket_adi, arac_id, yakit_turu, koltuk_no, dolu_koltuk_sayisi, sefer_no, gun_tr, gun_eng);
                vehicleList.Add(new_bus);
                static_vehicle.Add(new_bus);

                if (dolu_koltuk_sayisi == 0)
                {
                    new_bus.personel_olustur("OtobosPersonel1Ad", "OtobosPersonel1Soyad", sirket_adi, this.getKarayolu_personel_fiyat()[0] * carpici);
                    new_bus.personel_olustur("OtobosPersonel2Ad", "OtobosPersonel2Soyad", sirket_adi, this.getKarayolu_personel_fiyat()[0] * carpici);
                    new_bus.personel_olustur("OtobosPersonel3Ad", "OtobosPersonel3Soyad", sirket_adi, this.getKarayolu_personel_fiyat()[1] * carpici);
                    new_bus.personel_olustur("OtobosPersonel4Ad", "OtobosPersonel4Soyad", sirket_adi, this.getKarayolu_personel_fiyat()[1] * carpici);
                }

            }
            else if (arac_turu == "U�ak")
            {
                Airplane new_airplane = new Airplane(sirket_adi, arac_id, yakit_turu, koltuk_no, dolu_koltuk_sayisi, sefer_no, gun_tr, gun_eng);
                vehicleList.Add(new_airplane);
                static_vehicle.Add(new_airplane);

                if (dolu_koltuk_sayisi == 0)
                {
                    new_airplane.personel_olustur("U�akPersonel1Ad", "U�akPersonel1Soyad", sirket_adi, this.getHavayolu_personel_fiyat()[0]);
                    new_airplane.personel_olustur("U�akPersonel2Ad", "U�akPersonel2Soyad", sirket_adi, this.getHavayolu_personel_fiyat()[0]);
                    new_airplane.personel_olustur("U�akPersonel3Ad", "U�akPersonel3Soyad", sirket_adi, this.getHavayolu_personel_fiyat()[1]);
                    new_airplane.personel_olustur("U�akPersonel4Ad", "U�akPersonel4Soyad", sirket_adi, this.getHavayolu_personel_fiyat()[1]);
                }
            }
            else if (arac_turu == "Tren")
            {
                Train new_train = new Train(sirket_adi, arac_id, yakit_turu, koltuk_no, dolu_koltuk_sayisi, sefer_no, gun_tr, gun_eng);
                vehicleList.Add(new_train);
                static_vehicle.Add(new_train);

                if (dolu_koltuk_sayisi == 0)
                {
                    new_train.personel_olustur("TrenPersonel1Ad", "TrenPersonel1Soyad", sirket_adi, this.getDemiryolu_personel_fiyat()[0]);
                    new_train.personel_olustur("TrenPersonel2Ad", "TrenPersonel2Soyad", sirket_adi, this.getDemiryolu_personel_fiyat()[0]);
                    new_train.personel_olustur("TrenPersonel3Ad", "TrenPersonel3Soyad", sirket_adi, this.getDemiryolu_personel_fiyat()[1]);
                    new_train.personel_olustur("TrenPersonel4Ad", "TrenPersonel4Soyad", sirket_adi, this.getDemiryolu_personel_fiyat()[1]);
                }
            }
            else
            {
                MessageBox.Show("Program�n daha iyi �al��abilmesi i�in ara� id'lerinde \"Otob�s\", \"U�ak\" ve \"Tren\" s�zc�klerini do�ru yaz�n ve bo�luk b�rak�n.");
            }
        }
        public void standart_personel_olusturma()
        {
            foreach (Company c in this.getCompanyList())
            {
                foreach (Vehicle v in this.getStaticVehicleList())
                {

                    if (v.getArac_id() == "Otob�s 1" && c.getUserName() == "A")
                    {
                        v.personel_olustur("Ali", "Ayd�n", c.getUserName(), c.getKarayolu_personel_fiyat()[0] * 2);
                        v.personel_olustur("Veli", "K�l��", c.getUserName(), c.getKarayolu_personel_fiyat()[0] * 2);
                        v.personel_olustur("Mehmet", "Y�lmaz", c.getUserName(), c.getKarayolu_personel_fiyat()[1] * 2);
                        v.personel_olustur("Kaz�m", "Mumcu", c.getUserName(), c.getKarayolu_personel_fiyat()[1] * 2);
                    }
                    else if (v.getArac_id() == "Otob�s 2" && c.getUserName() == "A")
                    {
                        v.personel_olustur("Benhur", "Keser", c.getUserName(), c.getKarayolu_personel_fiyat()[0] * 2);
                        v.personel_olustur("Tayfun", "Aydo�an", c.getUserName(), c.getKarayolu_personel_fiyat()[0] * 2);
                        v.personel_olustur("Metin", "Tekin", c.getUserName(), c.getKarayolu_personel_fiyat()[1] * 2);
                        v.personel_olustur("Mehmet", "�zdilek", c.getUserName(), c.getKarayolu_personel_fiyat()[1] * 2);
                    }
                    else if (v.getArac_id() == "Otob�s 1" && c.getUserName() == "B")
                    {
                        v.personel_olustur("Ayta�", "Kara", c.getUserName(), c.getKarayolu_personel_fiyat()[0] * 2);
                        v.personel_olustur("G�ray", "Vural", c.getUserName(), c.getKarayolu_personel_fiyat()[0] * 2);
                        v.personel_olustur("Kamil Ahmet", "��rek�i", c.getUserName(), c.getKarayolu_personel_fiyat()[1] * 2);
                        v.personel_olustur("Mevl�t", "Ekelik", c.getUserName(), c.getKarayolu_personel_fiyat()[1] * 2);
                    }
                    else if (v.getArac_id() == "Otob�s 2" && c.getUserName() == "B")
                    {
                        v.personel_olustur("Serdar", "Dursun", c.getUserName(), c.getKarayolu_personel_fiyat()[0]);
                        v.personel_olustur("U�ur", "�ift�i", c.getUserName(), c.getKarayolu_personel_fiyat()[0]);
                        v.personel_olustur("Caner", "Osmanpa�a", c.getUserName(), c.getKarayolu_personel_fiyat()[1]);
                        v.personel_olustur("Alaaddin", "Okumu�", c.getUserName(), c.getKarayolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "Otob�s 1" && c.getUserName() == "C")
                    {
                        v.personel_olustur("Murat", "Paluli", c.getUserName(), c.getKarayolu_personel_fiyat()[0]);
                        v.personel_olustur("Emir", "Ortakaya", c.getUserName(), c.getKarayolu_personel_fiyat()[0]);
                        v.personel_olustur("Emrah", "Ba�san", c.getUserName(), c.getKarayolu_personel_fiyat()[1]);
                        v.personel_olustur("Mehmet", "Kuzucu", c.getUserName(), c.getKarayolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "U�ak 1" && c.getUserName() == "C")
                    {
                        v.personel_olustur("Furkan", "Bay�r", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Cenk", "Tosun", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Samet", "Akaydin", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                        v.personel_olustur("Emre", "Mor", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "U�ak 2" && c.getUserName() == "C")
                    {
                        v.personel_olustur("Atilla", "Turan", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("G�khan", "De�irmenci", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Bar��", "Memi�", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                        v.personel_olustur("Mehmet", "Y�lmaz", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "Tren 1" && c.getUserName() == "D")
                    {
                        v.personel_olustur("M�cahit", "Albayrak", c.getUserName(), c.getDemiryolu_personel_fiyat()[0]);
                        v.personel_olustur("Rahmetullah", "Beri�bek", c.getUserName(), c.getDemiryolu_personel_fiyat()[0]);
                        v.personel_olustur("Alihan", "Kubalas", c.getUserName(), c.getDemiryolu_personel_fiyat()[1]);
                        v.personel_olustur("Atila", "Turan", c.getUserName(), c.getDemiryolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "Tren 2" && c.getUserName() == "D")
                    {
                        v.personel_olustur("Recep", "Niyaz", c.getUserName(), c.getDemiryolu_personel_fiyat()[0]);
                        v.personel_olustur("Melih", "Kabasakal", c.getUserName(), c.getDemiryolu_personel_fiyat()[0]);
                        v.personel_olustur("Fethi", "�zer", c.getUserName(), c.getDemiryolu_personel_fiyat()[1]);
                        v.personel_olustur("Ferhat", "�ztorun", c.getUserName(), c.getDemiryolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "Tren 3" && c.getUserName() == "D")
                    {
                        v.personel_olustur("Burak", "���r", c.getUserName(), c.getDemiryolu_personel_fiyat()[0]);
                        v.personel_olustur("Hamza", "G�reler", c.getUserName(), c.getDemiryolu_personel_fiyat()[0]);
                        v.personel_olustur("Muhammet", "�zbask�c�", c.getUserName(), c.getDemiryolu_personel_fiyat()[1]);
                        v.personel_olustur("Osman", "�elik", c.getUserName(), c.getDemiryolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "U�ak 1" && c.getUserName() == "F")
                    {
                        v.personel_olustur("Zeki", "Yavru", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Fatih", "Aksoy", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Kubilay", "K�yl�", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                        v.personel_olustur("�zzet", "Topatar", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                    }
                    else if (v.getArac_id() == "U�ak 2" && c.getUserName() == "F")
                    {
                        v.personel_olustur("Vefa", "Temel", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Muammer", "Sar�kaya", c.getUserName(), c.getHavayolu_personel_fiyat()[0]);
                        v.personel_olustur("Ali", "Ya�ar", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                        v.personel_olustur("Mehmet", "Ye�il", c.getUserName(), c.getHavayolu_personel_fiyat()[1]);
                    }

                }
            }

        }
        public int gunluk_gelir_hesapla(string gun)
        {
            List<Vehicle> vehLi = new List<Vehicle>();
            int toplam = 0;
            foreach (Company c in this.getStaticCompany())
            {
                if (c.getUserName() == this.getUserName())
                {
                    foreach (Vehicle v in c.getStaticVehicleList())
                    {
                        if (v.getSirketAdi() == this.getUserName() && (gun == v.getGun_tr() || gun == v.getGun_eng()))
                        {
                            //MessageBox.Show(c.getUserName() + " " + v.getArac_id());
                            int var_mi = 0;
                            foreach (Vehicle v2 in vehLi)
                            {
                                if (v2.getArac_id() == v.getArac_id() && v2.getSirketAdi() == v.getSirketAdi())
                                {
                                    var_mi = 1;
                                }
                            }
                            if (var_mi == 0)
                            {
                                vehLi.Add(v);
                            }

                        }
                    }
                }

            }
            foreach (Vehicle v in vehLi)
            {

                toplam += v.calculateDefaultGelir();
                toplam += v.getSatilan_bilet_gelir();
            }
            return toplam;
        }
        public int gunluk_gider_hesapla(string gun)
        {
            List<Vehicle> vehLi = new List<Vehicle>();
            int toplam = 0;
            foreach (Company c in this.getStaticCompany())
            {
                if (c.getUserName() == this.getUserName())
                {
                    foreach (Vehicle v in c.getStaticVehicleList())
                    {
                        if (v.getSirketAdi() == this.getUserName() && (gun == v.getGun_tr() || gun == v.getGun_eng()))
                        {
                            //MessageBox.Show(c.getUserName() + " " + v.getArac_id());
                            int var_mi = 0;
                            foreach (Vehicle v2 in vehLi)
                            {
                                if (v2.getArac_id() == v.getArac_id() && v2.getSirketAdi() == v.getSirketAdi())
                                {
                                    var_mi = 1;
                                }
                            }
                            if (var_mi == 0)
                            {
                                vehLi.Add(v);
                            }

                        }
                    }
                }

            }
            foreach (Vehicle v in vehLi)
            {
                toplam += v.calculateTotalCostPerDay();
            }
            //MessageBox.Show("Hizmet bedeli �ncesi: " + toplam.ToString());
            toplam += this.getHizmetBedeli();
            return toplam;
        }

        public List<Company> getStaticCompany()
        {
            return static_companyList;
        }

        public void setStaticCompany(List<Company> l)
        {
            static_companyList = l;
        }

        public int getIlk_sefer()
        {
            return ilk_sefer;
        }

        public void setIlk_sefer(int val)
        {
            ilk_sefer = val;
        }

        public override string getUserName()
        {
            return userName;
        }
        public override void setUserName(string userName)
        {
            this.userName = userName;
        }

        public override string getPassword()
        {
            return password;
        }
        public override void setPassword(string password)
        {
            this.password = password;
        }

        public List<Company> getCompanyList()
        {
            return companyList;
        }

        public void setCompanyList(List<Company> companyList)
        {
            this.companyList = companyList;
        }
        public List<Vehicle> getVehicleList()
        {
            return vehicleList;
        }

        public void setVehicleList(List<Vehicle> vehicleList)
        {
            this.vehicleList = vehicleList;
        }

        public List<string> getYakitList()
        {
            return yakitList;
        }

        public void setYakitList(List<string> yakitList)
        {
            this.yakitList = yakitList;
        }

        public List<int> getYakitMaliyetList()
        {
            return yakitMaliyetList;
        }

        public void setYakitMaliyetList(List<int> yakitMaliyetList)
        {
            this.yakitMaliyetList = yakitMaliyetList;
        }

        public List<string> getSeferList()
        {
            return seferList;
        }

        public void setSeferList(List<string> seferList)
        {
            this.seferList = seferList;
        }

        public List<Vehicle> getStaticVehicleList()
        {
            return static_vehicle;
        }

        public void setStaticVehicleList(List<Vehicle> l)
        {
            static_vehicle = l;
        }

        public List<int> getKarayolu_personel_fiyat()
        {
            return this.karayolu_personel_fiyat;
        }

        public void setKarayolu_personel_fiyat(List<int> l)
        {
            this.karayolu_personel_fiyat = l;
        }

        public List<int> getHavayolu_personel_fiyat()
        {
            return this.havayolu_personel_fiyat;
        }

        public void setHavayolu_personel_fiyat(List<int> l)
        {
            this.havayolu_personel_fiyat = l;
        }

        public List<int> getDemiryolu_personel_fiyat()
        {
            return this.demiryolu_personel_fiyat;
        }

        public void setDemiryolu_personel_fiyat(List<int> l)
        {
            this.demiryolu_personel_fiyat = l;
        }

        public void staticVehYazdir()
        {
            string s = "";
            foreach (Vehicle v in getStaticVehicleList())
            {
                s += v.getArac_id();
                s += "\n";
            }
            MessageBox.Show(s);
        }

        public int getHizmetBedeli()
        {
            return this.hizmet_bedeli;
        }
        public void setHizmetBedeli(int val)
        {
            this.hizmet_bedeli = val;
        }
        public int getGunlukGider()
        {
            return this.gunluk_gider;
        }
        public void setGunlukGider(int val)
        {
            this.gunluk_gider = val;
        }

    }
    public class Koltuk
    {
        public int doluluk_durumu;
        public static List<Koltuk> static_koltuk = new List<Koltuk>();

        public Koltuk()
        {

        }

        public Koltuk(int durum)
        {
            this.doluluk_durumu = durum;
        }

        public int getDoluluk_durumu()
        {
            return doluluk_durumu;
        }

        public void setDoluluk_durumu(int val)
        {
            this.doluluk_durumu = val;
        }

        public List<Koltuk> getStatic_koltuk()
        {
            return static_koltuk;
        }
        public void setStatic_koltuk(List<Koltuk> l)
        {
            static_koltuk = l;
        }
    }

    public abstract class Vehicle
    {
        public string sirket_adi = "";
        public string Arac_id = "";
        public string Yakit_turu = "";
        public int Koltuk_sayisi;
        public string Sefer_no = "";
        public string gun_tr = "";
        public string gun_eng = "";
        public int dolu_koltuk;
        public int satilan_bilet_gelir;
        public List<Koltuk> koltukList = new List<Koltuk>();
        public List<Personel> personelList = new List<Personel>();
        public List<string> sehirler = new List<string>();
        public List<List<Koltuk>> koltuk_ic_ice = new List<List<Koltuk>>();

        public Vehicle()
        {

        }

        public virtual string getSirketAdi()
        {
            return this.sirket_adi;
        }
        public virtual void setSirketAdi(string value)
        {
            this.sirket_adi = value;
        }

        public virtual string getArac_id()
        {
            return this.Arac_id;
        }
        public virtual void setArac_id(string value)
        {
            this.Arac_id = value;
        }

        public virtual string getYakit_turu()
        {
            return this.Yakit_turu;
        }
        public virtual void setYakit_turu(string value)
        {
            this.Yakit_turu = value;
        }

        public virtual int getKoltuk_sayisi()
        {
            return this.Koltuk_sayisi;
        }
        public virtual void setKoltukSayisi(int value)
        {
            this.Koltuk_sayisi = value;
        }
        public virtual string getSefer_no()
        {
            return this.Sefer_no;
        }
        public virtual void setSefer_no(string value)
        {
            this.Sefer_no = value;
        }

        public virtual int getDoluKoltuk()
        {
            return dolu_koltuk;
        }

        public virtual void setDoluKoltuk(int value)
        {
            this.dolu_koltuk = value;
        }

        public virtual void bos_koltuk_doldurma()
        {
            int koltuk_say = this.Koltuk_sayisi;
            for (int i = 0; i < koltuk_say; i++)
            {
                koltukList.Add(new Koltuk(0));
            }
        }

        public virtual void setKoltukArrayNum(int index, int value)
        {
            koltukList[index].setDoluluk_durumu(value);
        }

        public virtual void random_koltuk_doldurma()
        {
            int dolu = this.dolu_koltuk;
            List<int> ints = new List<int>();
            Random random = new Random();
            for (int i = 0; i < dolu; i++)
            {
                int rastgeleSayi = random.Next(0, this.Koltuk_sayisi);
                int var_mi_flag = 0;
                foreach (int i1 in ints)
                {
                    if (i1 == rastgeleSayi)
                    {
                        var_mi_flag = 1;
                    }
                }
                if (var_mi_flag == 1)
                {
                    i--;
                    continue;
                }
                ints.Add(rastgeleSayi);
                setKoltukArrayNum(rastgeleSayi, 1);

            }
        }

        public virtual void sehir_doldurma()
        {
            Trip t = new Trip();
            foreach (Sefer s in t.getSeferList())
            {
                if (s.getSeferIsmi() == this.getSefer_no())
                {
                    foreach (string str in s.getSeferSehirList())
                    {
                        this.getSehirler().Add(str);
                    }
                    //this.sehirler.Add(s.getIceIce()[s.getIceIce().Count - 1][s.getIceIce()[s.getIceIce().Count - 1].Count - 1]);
                }
            }
        }

        public virtual void ic_ice_koltuk_doldurma()
        {
            int uzunluk = this.getSehirler().Count;
            for (int i = 0; i < uzunluk; i++)
            {
                this.getKoltuk_ic_ice().Add(this.getKoltukList());
            }
        }

        //public List<Koltuk> koltukList = new List<Koltuk>();
        public virtual List<Koltuk> getKoltukList()
        {
            return koltukList;
        }

        public virtual void setKoltukList(List<Koltuk> k)
        {
            this.koltukList = k;
        }

        public virtual List<Personel> getPersonelList()
        {
            return this.personelList;
        }

        public virtual void setPersonelList(List<Personel> l)
        {
            this.personelList = l;
        }

        public virtual void personel_olustur(string ad, string soyad, string sirket_ismi, int sefer_basi_maas)
        {
            Personel p = new Personel(ad, soyad, sirket_ismi, sefer_basi_maas);
            this.getPersonelList().Add(p);

        }
        public virtual int calculateFuelCost()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.karayol_sehirleri.Length; i++)
                            {
                                if (r.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.havayolu_sehirleri.Length; i++)
                            {
                                if (r.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.demiryolu_sehirleri.Length; i++)
                            {
                                if (r.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                        }
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            int mesafe = sonuc;
            //MessageBox.Show("mesafe ->" + mesafe.ToString());
            sonuc = 0;
            string yakit_turu = this.getYakit_turu();
            foreach (Company c1 in c.getStaticCompany())
            {
                //MessageBox.Show(c1.getUserName() + " - " + this.getSirketAdi());
                if (c1.getUserName() == this.getSirketAdi())
                {
                    int index = -1;
                    foreach (string s in c1.getYakitList())
                    {
                        if (s == yakit_turu)
                        {
                            index = c1.getYakitList().IndexOf(s);
                        }
                        //MessageBox.Show("index ->" + index.ToString());
                    }
                    //MessageBox.Show("yakit maliyeti ->" + c1.getYakitMaliyetList()[index].ToString());
                    sonuc = mesafe * c1.getYakitMaliyetList()[index];
                    break; //gezmesine gerek yok baska yok zaten
                }
            }
            return sonuc;
        }

        public virtual int calculatePersonelCost()
        {
            int sonuc = 0;
            foreach (Personel p in this.getPersonelList())
            {
                if (p.getSirketIsmi() == this.getSirketAdi())
                {
                    //MessageBox.Show(p.getAd() + " " + p.getSoyad() + " " + p.getMaas().ToString());
                    sonuc += p.getMaas();
                }
            }
            //MessageBox.Show("sonuc ->" + sonuc.ToString());
            return sonuc;
        }
        public virtual int calculateTotalCostPerDay()
        {
            int sonuc = 0;
            sonuc += this.calculateFuelCost();
            //MessageBox.Show("fuel sonuc ->" + sonuc.ToString());
            sonuc += this.calculatePersonelCost();
            //MessageBox.Show("personel+fuel sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public virtual int calculateDefaultGelir()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    //MessageBox.Show(sefer_ismi);
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                            {
                                if (T.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                            sonuc += T.karayol_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                            {
                                if (T.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.havayolu_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                            {
                                if (T.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.demiryolu_fiyatlar[kalkis_index][varis_index];
                        }
                        //MessageBox.Show("sonuc ->" + sonuc.ToString());
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            //MessageBox.Show("en sondan onceki sonuc ->" + sonuc.ToString() + "kisi ->" + this.getDoluKoltuk());
            sonuc = sonuc * this.getDoluKoltuk();
            //MessageBox.Show("son sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public virtual string getGun_tr()
        {
            return this.gun_tr;
        }

        public virtual void setGun_tr(string value)
        {
            this.gun_tr = value;
        }

        public virtual string getGun_eng()
        {
            return this.gun_eng;
        }

        public virtual void setGun_eng(string value)
        {
            this.gun_eng = value;
        }

        public virtual List<string> getSehirler()
        {
            return this.sehirler;
        }

        public virtual void setSehirler(List<string> l)
        {
            this.sehirler = l;
        }

        public virtual List<List<Koltuk>> getKoltuk_ic_ice()
        {
            return this.koltuk_ic_ice;
        }

        public virtual void setKoltuk_ic_ice(List<List<Koltuk>> l)
        {
            this.koltuk_ic_ice = l;
        }

        //public int satilan_bilet_gelir;
        public virtual int getSatilan_bilet_gelir()
        {
            return this.satilan_bilet_gelir;
        }

        public virtual void setSatilan_bilet_gelir(int value)
        {
            this.satilan_bilet_gelir = value;
        }
    }

    public class Bus : Vehicle
    {
        public Bus(string sirket_adi, string arac_id, string yakit_turu, int koltuk_sayisi, int dolu_koltuk, string sefer_no, string gun_tr, string gun_eng)
        {
            if (koltuk_sayisi < dolu_koltuk)
            {
                MessageBox.Show("Dolu koltuk sayisi, koltuk sayisinden b�y�k olamaz.");
                return;
            }

            this.sirket_adi = sirket_adi;
            this.Arac_id = arac_id;
            this.Yakit_turu = yakit_turu;
            this.Koltuk_sayisi = koltuk_sayisi;
            this.dolu_koltuk = dolu_koltuk;
            this.Sefer_no = sefer_no;
            this.gun_tr = gun_tr;
            this.gun_eng = gun_eng;
            this.satilan_bilet_gelir = 0;

            this.bos_koltuk_doldurma();
            this.random_koltuk_doldurma();
            this.sehir_doldurma();
            this.ic_ice_koltuk_doldurma();
        }


        public override string getSirketAdi()
        {
            return this.sirket_adi;
        }
        public override void setSirketAdi(string value)
        {
            this.sirket_adi = value;
        }
        public override string getArac_id()
        {
            return this.Arac_id;
        }
        public override void setArac_id(string value)
        {
            this.Arac_id = value;
        }

        public override string getYakit_turu()
        {
            return this.Yakit_turu;
        }
        public override void setYakit_turu(string value)
        {
            this.Yakit_turu = value;
        }

        public override int getKoltuk_sayisi()
        {
            return this.Koltuk_sayisi;
        }
        public override void setKoltukSayisi(int value)
        {
            this.Koltuk_sayisi = value;
        }

        public override string getSefer_no()
        {
            return this.Sefer_no;
        }
        public override void setSefer_no(string value)
        {
            this.Sefer_no = value;
        }

        public override int getDoluKoltuk()
        {
            return dolu_koltuk;
        }

        public override void setDoluKoltuk(int value)
        {
            this.dolu_koltuk = value;
        }

        public override void bos_koltuk_doldurma()
        {
            int koltuk_say = this.Koltuk_sayisi;
            for (int i = 0; i < koltuk_say; i++)
            {
                koltukList.Add(new Koltuk(0));
            }
        }

        public override void setKoltukArrayNum(int index, int value)
        {
            koltukList[index].setDoluluk_durumu(value);
        }

        public override void random_koltuk_doldurma()
        {
            int dolu = this.dolu_koltuk;
            List<int> ints = new List<int>();
            Random random = new Random();
            for (int i = 0; i < dolu; i++)
            {
                int rastgeleSayi = random.Next(0, this.Koltuk_sayisi);
                int var_mi_flag = 0;
                foreach (int i1 in ints)
                {
                    if (i1 == rastgeleSayi)
                    {
                        var_mi_flag = 1;
                    }
                }
                if (var_mi_flag == 1)
                {
                    i--;
                    continue;
                }
                ints.Add(rastgeleSayi);
                setKoltukArrayNum(rastgeleSayi, 1);

            }
        }

        public override List<Koltuk> getKoltukList()
        {
            return koltukList;
        }

        public override void setKoltukList(List<Koltuk> k)
        {
            this.koltukList = k;
        }

        public override List<Personel> getPersonelList()
        {
            return this.personelList;
        }

        public override void setPersonelList(List<Personel> l)
        {
            this.personelList = l;
        }

        public override void personel_olustur(string ad, string soyad, string sirket_ismi, int sefer_basi_maas)
        {
            Personel p = new Personel(ad, soyad, sirket_ismi, sefer_basi_maas);
            this.getPersonelList().Add(p);

        }

        public override int calculateFuelCost()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.karayol_sehirleri.Length; i++)
                            {
                                if (r.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.havayolu_sehirleri.Length; i++)
                            {
                                if (r.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.demiryolu_sehirleri.Length; i++)
                            {
                                if (r.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                        }
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            int mesafe = sonuc;
            //MessageBox.Show("mesafe ->" + mesafe.ToString());
            sonuc = 0;
            string yakit_turu = this.getYakit_turu();
            foreach (Company c1 in c.getStaticCompany())
            {
                //MessageBox.Show(c1.getUserName() + " - " + this.getSirketAdi());
                if (c1.getUserName() == this.getSirketAdi())
                {
                    int index = -1;
                    foreach (string s in c1.getYakitList())
                    {
                        if (s == yakit_turu)
                        {
                            index = c1.getYakitList().IndexOf(s);
                        }
                        //MessageBox.Show("index ->" + index.ToString());
                    }
                    //MessageBox.Show("yakit maliyeti ->" + c1.getYakitMaliyetList()[index].ToString());
                    sonuc = mesafe * c1.getYakitMaliyetList()[index];
                    break; //gezmesine gerek yok baska yok zaten
                }
            }
            return sonuc;
        }
        public override int calculateDefaultGelir()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    //MessageBox.Show(sefer_ismi);
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                            {
                                if (T.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                            sonuc += T.karayol_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                            {
                                if (T.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.havayolu_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                            {
                                if (T.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.demiryolu_fiyatlar[kalkis_index][varis_index];
                        }
                        //MessageBox.Show("sonuc ->" + sonuc.ToString());
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            //MessageBox.Show("en sondan onceki sonuc ->" + sonuc.ToString() + "kisi ->" + this.getDoluKoltuk());
            sonuc = sonuc * this.getDoluKoltuk();
            //MessageBox.Show("son sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public override int calculatePersonelCost()
        {
            int sonuc = 0;
            foreach (Personel p in this.getPersonelList())
            {
                if (p.getSirketIsmi() == this.getSirketAdi())
                {
                    //MessageBox.Show(p.getAd() + " " + p.getSoyad() + " " + p.getMaas().ToString());
                    sonuc += p.getMaas();
                }
            }
            //MessageBox.Show("sonuc ->" + sonuc.ToString());
            return sonuc;
        }
        public override int calculateTotalCostPerDay()
        {
            int sonuc = 0;
            sonuc += this.calculateFuelCost();
            //MessageBox.Show("fuel sonuc ->" + sonuc.ToString());
            sonuc += this.calculatePersonelCost();
            //MessageBox.Show("personel+fuel sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        /*
         this.sehir_doldurma();
            this.ic_ice_koltuk_doldurma(); 
         */

        public override void sehir_doldurma()
        {
            Trip t = new Trip();
            foreach (Sefer s in t.getSeferList())
            {
                if (s.getSeferIsmi() == this.getSefer_no())
                {
                    foreach (string str in s.getSeferSehirList())
                    {
                        this.getSehirler().Add(str);
                    }
                    //this.sehirler.Add(s.getIceIce()[s.getIceIce().Count - 1][s.getIceIce()[s.getIceIce().Count - 1].Count - 1]);
                }
            }
        }

        public override void ic_ice_koltuk_doldurma()
        {
            int uzunluk = this.getSehirler().Count;
            for (int i = 0; i < uzunluk; i++)
            {
                this.getKoltuk_ic_ice().Add(this.getKoltukList());
            }
        }

        public override string getGun_tr()
        {
            return this.gun_tr;
        }

        public override void setGun_tr(string value)
        {
            this.gun_tr = value;
        }

        public override string getGun_eng()
        {
            return this.gun_eng;
        }

        public override void setGun_eng(string value)
        {
            this.gun_eng = value;
        }

        public override List<string> getSehirler()
        {
            return this.sehirler;
        }

        public override void setSehirler(List<string> l)
        {
            this.sehirler = l;
        }

        public override List<List<Koltuk>> getKoltuk_ic_ice()
        {
            return this.koltuk_ic_ice;
        }

        public override void setKoltuk_ic_ice(List<List<Koltuk>> l)
        {
            this.koltuk_ic_ice = l;
        }

        public override int getSatilan_bilet_gelir()
        {
            return this.satilan_bilet_gelir;
        }

        public override void setSatilan_bilet_gelir(int value)
        {
            this.satilan_bilet_gelir = value;
        }
    }

    public class Train : Vehicle
    {
        public Train(string sirket_adi, string arac_id, string yakit_turu, int koltuk_sayisi, int dolu_koltuk, string sefer_no, string gun_tr, string gun_eng)
        {
            if (koltuk_sayisi < dolu_koltuk)
            {
                MessageBox.Show("Dolu koltuk sayisi, koltuk sayisinden b�y�k olamaz.");
                return;
            }

            this.sirket_adi = sirket_adi;
            this.Arac_id = arac_id;
            this.Yakit_turu = yakit_turu;
            this.Koltuk_sayisi = koltuk_sayisi;
            this.dolu_koltuk = dolu_koltuk;
            this.Sefer_no = sefer_no;
            this.gun_tr = gun_tr;
            this.gun_eng = gun_eng;
            this.satilan_bilet_gelir = 0;

            this.bos_koltuk_doldurma();
            this.random_koltuk_doldurma();
            this.sehir_doldurma();
            this.ic_ice_koltuk_doldurma();
        }

        public override string getSirketAdi()
        {
            return this.sirket_adi;
        }
        public override void setSirketAdi(string value)
        {
            this.sirket_adi = value;
        }
        public override string getArac_id()
        {
            return this.Arac_id;
        }
        public override void setArac_id(string value)
        {
            this.Arac_id = value;
        }

        public override string getYakit_turu()
        {
            return this.Yakit_turu;
        }
        public override void setYakit_turu(string value)
        {
            this.Yakit_turu = value;
        }

        public override int getKoltuk_sayisi()
        {
            return this.Koltuk_sayisi;
        }
        public override void setKoltukSayisi(int value)
        {
            this.Koltuk_sayisi = value;
        }

        public override string getSefer_no()
        {
            return this.Sefer_no;
        }
        public override void setSefer_no(string value)
        {
            this.Sefer_no = value;
        }

        public override int getDoluKoltuk()
        {
            return dolu_koltuk;
        }

        public override void setDoluKoltuk(int value)
        {
            this.dolu_koltuk = value;
        }

        public override void bos_koltuk_doldurma()
        {
            int koltuk_say = this.Koltuk_sayisi;
            for (int i = 0; i < koltuk_say; i++)
            {
                koltukList.Add(new Koltuk(0));
            }
        }

        public override void setKoltukArrayNum(int index, int value)
        {
            koltukList[index].setDoluluk_durumu(value);
        }

        public override void random_koltuk_doldurma()
        {
            int dolu = this.dolu_koltuk;
            List<int> ints = new List<int>();
            Random random = new Random();
            for (int i = 0; i < dolu; i++)
            {
                int rastgeleSayi = random.Next(0, this.Koltuk_sayisi);
                int var_mi_flag = 0;
                foreach (int i1 in ints)
                {
                    if (i1 == rastgeleSayi)
                    {
                        var_mi_flag = 1;
                    }
                }
                if (var_mi_flag == 1)
                {
                    i--;
                    continue;
                }
                ints.Add(rastgeleSayi);
                setKoltukArrayNum(rastgeleSayi, 1);

            }
        }

        public override List<Koltuk> getKoltukList()
        {
            return koltukList;
        }

        public override void setKoltukList(List<Koltuk> k)
        {
            this.koltukList = k;
        }

        public override List<Personel> getPersonelList()
        {
            return this.personelList;
        }

        public override void setPersonelList(List<Personel> l)
        {
            this.personelList = l;
        }

        public override void personel_olustur(string ad, string soyad, string sirket_ismi, int sefer_basi_maas)
        {
            Personel p = new Personel(ad, soyad, sirket_ismi, sefer_basi_maas);
            this.getPersonelList().Add(p);

        }

        public override int calculateDefaultGelir()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    //MessageBox.Show(sefer_ismi);
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                            {
                                if (T.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                            sonuc += T.karayol_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                            {
                                if (T.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.havayolu_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                            {
                                if (T.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.demiryolu_fiyatlar[kalkis_index][varis_index];
                        }
                        //MessageBox.Show("sonuc ->" + sonuc.ToString());
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            //MessageBox.Show("en sondan onceki sonuc ->" + sonuc.ToString() + "kisi ->" + this.getDoluKoltuk());
            sonuc = sonuc * this.getDoluKoltuk();
            //MessageBox.Show("son sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public override int calculateFuelCost()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.karayol_sehirleri.Length; i++)
                            {
                                if (r.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.havayolu_sehirleri.Length; i++)
                            {
                                if (r.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.demiryolu_sehirleri.Length; i++)
                            {
                                if (r.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                        }
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            int mesafe = sonuc;
            //MessageBox.Show("mesafe ->" + mesafe.ToString());
            sonuc = 0;
            string yakit_turu = this.getYakit_turu();
            foreach (Company c1 in c.getStaticCompany())
            {
                //MessageBox.Show(c1.getUserName() + " - " + this.getSirketAdi());
                if (c1.getUserName() == this.getSirketAdi())
                {
                    int index = -1;
                    foreach (string s in c1.getYakitList())
                    {
                        if (s == yakit_turu)
                        {
                            index = c1.getYakitList().IndexOf(s);
                        }
                        //MessageBox.Show("index ->" + index.ToString());
                    }
                    //MessageBox.Show("yakit maliyeti ->" + c1.getYakitMaliyetList()[index].ToString());
                    sonuc = mesafe * c1.getYakitMaliyetList()[index];
                    break; //gezmesine gerek yok baska yok zaten
                }
            }
            return sonuc;
        }

        public override int calculatePersonelCost()
        {
            int sonuc = 0;
            foreach (Personel p in this.getPersonelList())
            {
                if (p.getSirketIsmi() == this.getSirketAdi())
                {
                    //MessageBox.Show(p.getAd() + " " + p.getSoyad() + " " + p.getMaas().ToString());
                    sonuc += p.getMaas();
                }
            }
            //MessageBox.Show("sonuc ->" + sonuc.ToString());
            return sonuc;
        }
        public override int calculateTotalCostPerDay()
        {
            int sonuc = 0;
            sonuc += this.calculateFuelCost();
            //MessageBox.Show("fuel sonuc ->" + sonuc.ToString());
            sonuc += this.calculatePersonelCost();
            //MessageBox.Show("personel+fuel sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public override void sehir_doldurma()
        {
            Trip t = new Trip();
            foreach (Sefer s in t.getSeferList())
            {
                if (s.getSeferIsmi() == this.getSefer_no())
                {
                    foreach (string str in s.getSeferSehirList())
                    {
                        this.getSehirler().Add(str);
                    }
                    //this.sehirler.Add(s.getIceIce()[s.getIceIce().Count - 1][s.getIceIce()[s.getIceIce().Count - 1].Count - 1]);
                }
            }
        }

        public override void ic_ice_koltuk_doldurma()
        {
            int uzunluk = this.getSehirler().Count;
            for (int i = 0; i < uzunluk; i++)
            {
                this.getKoltuk_ic_ice().Add(this.getKoltukList());
            }
        }
        public override string getGun_tr()
        {
            return this.gun_tr;
        }

        public override void setGun_tr(string value)
        {
            this.gun_tr = value;
        }

        public override string getGun_eng()
        {
            return this.gun_eng;
        }

        public override void setGun_eng(string value)
        {
            this.gun_eng = value;
        }

        public override List<string> getSehirler()
        {
            return this.sehirler;
        }

        public override void setSehirler(List<string> l)
        {
            this.sehirler = l;
        }

        public override List<List<Koltuk>> getKoltuk_ic_ice()
        {
            return this.koltuk_ic_ice;
        }

        public override void setKoltuk_ic_ice(List<List<Koltuk>> l)
        {
            this.koltuk_ic_ice = l;
        }
        public override int getSatilan_bilet_gelir()
        {
            return this.satilan_bilet_gelir;
        }

        public override void setSatilan_bilet_gelir(int value)
        {
            this.satilan_bilet_gelir = value;
        }
    }

    public class Airplane : Vehicle
    {

        public Airplane(string sirket_adi, string arac_id, string yakit_turu, int koltuk_sayisi, int dolu_koltuk, string sefer_no, string gun_tr, string gun_eng)
        {
            if (koltuk_sayisi < dolu_koltuk)
            {
                MessageBox.Show("Dolu koltuk sayisi, koltuk sayisinden b�y�k olamaz.");
                return;
            }

            this.sirket_adi = sirket_adi;
            this.Arac_id = arac_id;
            this.Yakit_turu = yakit_turu;
            this.Koltuk_sayisi = koltuk_sayisi;
            this.dolu_koltuk = dolu_koltuk;
            this.Sefer_no = sefer_no;
            this.gun_tr = gun_tr;
            this.gun_eng = gun_eng;
            this.satilan_bilet_gelir = 0;

            this.bos_koltuk_doldurma();
            this.random_koltuk_doldurma();
            this.sehir_doldurma();
            this.ic_ice_koltuk_doldurma();
        }


        public override string getArac_id()
        {
            return this.Arac_id;
        }
        public override void setArac_id(string value)
        {
            this.Arac_id = value;
        }

        public override string getYakit_turu()
        {
            return this.Yakit_turu;
        }
        public override void setYakit_turu(string value)
        {
            this.Yakit_turu = value;
        }

        public override int getKoltuk_sayisi()
        {
            return this.Koltuk_sayisi;
        }
        public override void setKoltukSayisi(int value)
        {
            this.Koltuk_sayisi = value;
        }

        public override string getSefer_no()
        {
            return this.Sefer_no;
        }
        public override void setSefer_no(string value)
        {
            this.Sefer_no = value;
        }
        public override string getSirketAdi()
        {
            return this.sirket_adi;
        }
        public override void setSirketAdi(string value)
        {
            this.sirket_adi = value;
        }

        public override int getDoluKoltuk()
        {
            return dolu_koltuk;
        }

        public override void setDoluKoltuk(int value)
        {
            this.dolu_koltuk = value;
        }

        public override void bos_koltuk_doldurma()
        {
            int koltuk_say = this.Koltuk_sayisi;
            for (int i = 0; i < koltuk_say; i++)
            {
                koltukList.Add(new Koltuk(0));
            }
        }

        public override void setKoltukArrayNum(int index, int value)
        {
            koltukList[index].setDoluluk_durumu(value);
        }

        public override void random_koltuk_doldurma()
        {
            int dolu = this.dolu_koltuk;
            List<int> ints = new List<int>();
            Random random = new Random();
            for (int i = 0; i < dolu; i++)
            {
                int rastgeleSayi = random.Next(0, this.Koltuk_sayisi);
                int var_mi_flag = 0;
                foreach (int i1 in ints)
                {
                    if (i1 == rastgeleSayi)
                    {
                        var_mi_flag = 1;
                    }
                }
                if (var_mi_flag == 1)
                {
                    i--;
                    continue;
                }
                ints.Add(rastgeleSayi);
                setKoltukArrayNum(rastgeleSayi, 1);

            }
        }

        public override List<Koltuk> getKoltukList()
        {
            return koltukList;
        }

        public override void setKoltukList(List<Koltuk> k)
        {
            this.koltukList = k;
        }

        public override List<Personel> getPersonelList()
        {
            return this.personelList;
        }

        public override void setPersonelList(List<Personel> l)
        {
            this.personelList = l;
        }

        public override void personel_olustur(string ad, string soyad, string sirket_ismi, int sefer_basi_maas)
        {
            Personel p = new Personel(ad, soyad, sirket_ismi, sefer_basi_maas);
            this.getPersonelList().Add(p);
        }

        public override int calculateDefaultGelir()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    //MessageBox.Show(sefer_ismi);
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.karayol_sehirleri.Length; i++)
                            {
                                if (T.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                            sonuc += T.karayol_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.havayolu_sehirleri.Length; i++)
                            {
                                if (T.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.havayolu_fiyatlar[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < T.demiryolu_sehirleri.Length; i++)
                            {
                                if (T.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (T.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            //sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                            sonuc += T.demiryolu_fiyatlar[kalkis_index][varis_index];
                        }
                        //MessageBox.Show("sonuc ->" + sonuc.ToString());
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            //MessageBox.Show("en sondan onceki sonuc ->" + sonuc.ToString() + "kisi ->" + this.getDoluKoltuk());
            sonuc = sonuc * this.getDoluKoltuk();
            //MessageBox.Show("son sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public override int calculateFuelCost()
        {
            int sonuc = 0;
            string sefer_ismi = this.getSefer_no();
            Trip T = new Trip();
            Route r = new Route();
            Company c = new Company();
            foreach (Sefer s in T.getSeferList())
            {

                string sefer_turu = s.getSeferCesidi();
                if (s.getSeferIsmi() == sefer_ismi)
                {
                    int uzunluk = s.getIceIce().Count();
                    int iciceindex = 0;
                    while (iciceindex < uzunluk)
                    {
                        int iciceuzunluk2 = s.getIceIce()[iciceindex].Count;

                        string kalkis = s.getIceIce()[iciceindex][0];
                        string varis = s.getIceIce()[iciceindex][s.getIceIce()[iciceindex].Count - 1];
                        //MessageBox.Show(kalkis + " " + varis + " " + sonuc.ToString());
                        if (sefer_turu == "Karayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.karayol_sehirleri.Length; i++)
                            {
                                if (r.karayol_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.karayol_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.karayol_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Havayolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.havayolu_sehirleri.Length; i++)
                            {
                                if (r.havayolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.havayolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.havayolu_mesafe[kalkis_index][varis_index];
                        }
                        else if (sefer_turu == "Demiryolu")
                        {
                            int kalkis_index = -1;
                            int varis_index = -1;
                            for (int i = 0; i < r.demiryolu_sehirleri.Length; i++)
                            {
                                if (r.demiryolu_sehirleri[i] == kalkis)
                                {
                                    kalkis_index = i;
                                }
                                else if (r.demiryolu_sehirleri[i] == varis)
                                {
                                    varis_index = i;
                                }
                            }
                            sonuc += r.demiryolu_mesafe[kalkis_index][varis_index];
                        }
                        iciceindex += s.getIceIce()[iciceindex].Count - 1;
                    }
                }
            }
            int mesafe = sonuc;
            //MessageBox.Show("mesafe ->" + mesafe.ToString());
            sonuc = 0;
            string yakit_turu = this.getYakit_turu();
            foreach (Company c1 in c.getStaticCompany())
            {
                //MessageBox.Show(c1.getUserName() + " - " + this.getSirketAdi());
                if (c1.getUserName() == this.getSirketAdi())
                {
                    int index = -1;
                    foreach (string s in c1.getYakitList())
                    {
                        if (s == yakit_turu)
                        {
                            index = c1.getYakitList().IndexOf(s);
                        }
                        //MessageBox.Show("index ->" + index.ToString());
                    }
                    //MessageBox.Show("yakit maliyeti ->" + c1.getYakitMaliyetList()[index].ToString());
                    sonuc = mesafe * c1.getYakitMaliyetList()[index];
                    break; //gezmesine gerek yok baska yok zaten
                }
            }
            return sonuc;
        }

        public override int calculatePersonelCost()
        {
            int sonuc = 0;
            foreach (Personel p in this.getPersonelList())
            {
                if (p.getSirketIsmi() == this.getSirketAdi())
                {
                    //MessageBox.Show(p.getAd() + " " + p.getSoyad() + " " + p.getMaas().ToString());
                    sonuc += p.getMaas();
                }
            }
            //MessageBox.Show("sonuc ->" + sonuc.ToString());
            return sonuc;
        }
        public override int calculateTotalCostPerDay()
        {
            int sonuc = 0;
            sonuc += this.calculateFuelCost();
            //MessageBox.Show("fuel sonuc ->" + sonuc.ToString());
            sonuc += this.calculatePersonelCost();
            //MessageBox.Show("personel+fuel sonuc ->" + sonuc.ToString());
            return sonuc;
        }

        public override void sehir_doldurma()
        {
            Trip t = new Trip();
            foreach (Sefer s in t.getSeferList())
            {
                if (s.getSeferIsmi() == this.getSefer_no())
                {
                    foreach (string str in s.getSeferSehirList())
                    {
                        this.getSehirler().Add(str);
                    }
                    //this.sehirler.Add(s.getIceIce()[s.getIceIce().Count - 1][s.getIceIce()[s.getIceIce().Count - 1].Count - 1]);
                }
            }
        }

        public override void ic_ice_koltuk_doldurma()
        {
            int uzunluk = this.getSehirler().Count;
            for (int i = 0; i < uzunluk; i++)
            {
                this.getKoltuk_ic_ice().Add(this.getKoltukList());
            }
        }
        public override string getGun_tr()
        {
            return this.gun_tr;
        }

        public override void setGun_tr(string value)
        {
            this.gun_tr = value;
        }

        public override string getGun_eng()
        {
            return this.gun_eng;
        }

        public override void setGun_eng(string value)
        {
            this.gun_eng = value;
        }

        public override List<string> getSehirler()
        {
            return this.sehirler;
        }

        public override void setSehirler(List<string> l)
        {
            this.sehirler = l;
        }

        public override List<List<Koltuk>> getKoltuk_ic_ice()
        {
            return this.koltuk_ic_ice;
        }

        public override void setKoltuk_ic_ice(List<List<Koltuk>> l)
        {
            this.koltuk_ic_ice = l;
        }
        public override int getSatilan_bilet_gelir()
        {
            return this.satilan_bilet_gelir;
        }

        public override void setSatilan_bilet_gelir(int value)
        {
            this.satilan_bilet_gelir = value;
        }
    }

    public class Trip
    {

        public static List<Sefer> seferList = new List<Sefer>();
        static int ilk_elemanlar = 1;
        static int sefer_sayisi = 0;

        //fiyat
        public string[] karayol_sehirleri;
        public int[][] karayol_fiyatlar = new int[5][];

        public string[] demiryolu_sehirleri;
        public int[][] demiryolu_fiyatlar = new int[6][];

        public string[] havayolu_sehirleri;
        public int[][] havayolu_fiyatlar = new int[3][];

        public Trip()
        {
            karayol_sehirleri = new string[] { "Istanbul", "Kocaeli", "Ankara", "Eski�ehir", "Konya" };
            karayol_fiyatlar[0] = new int[] { -1, 50, 300, 150, 300 };
            karayol_fiyatlar[1] = new int[] { 50, -1, 400, 100, 250 };
            karayol_fiyatlar[2] = new int[] { 300, 400, -1, -1, -1 };
            karayol_fiyatlar[3] = new int[] { 150, 100, -1, -1, 150 };
            karayol_fiyatlar[4] = new int[] { 300, 250, -1, 150, -1 };

            demiryolu_sehirleri = new string[] { "Istanbul", "Kocaeli", "Bilecik", "Ankara", "Eski�ehir", "Konya" };
            demiryolu_fiyatlar[0] = new int[] { -1, 50, 150, 250, 200, 300 };
            demiryolu_fiyatlar[1] = new int[] { 50, -1, 50, 200, 100, 250 };
            demiryolu_fiyatlar[2] = new int[] { 150, 50, -1, 150, 50, 200 };
            demiryolu_fiyatlar[3] = new int[] { 250, 200, 150, -1, 100, -1 };
            demiryolu_fiyatlar[4] = new int[] { 200, 100, 50, 100, -1, 150 };
            demiryolu_fiyatlar[5] = new int[] { 300, 250, 200, -1, 150, -1 };


            havayolu_sehirleri = new string[] { "Istanbul", "Ankara", "Konya" };
            havayolu_fiyatlar[0] = new int[] { -1, 1000, 1200 };
            havayolu_fiyatlar[1] = new int[] { 1000, -1, -1 };
            havayolu_fiyatlar[2] = new int[] { 1200, -1, -1 };

            this.staticListDoldurma1();
        }

        public void staticListDoldurma1()
        {
            if (ilk_elemanlar == 1)
            {
                ilk_elemanlar = -1;
                Sefer sef1 = new Sefer("1. Sefer", "Demiryolu:Istanbul - Kocaeli - Bilecik - Eski�ehir - Ankara - Eskisehir - Bilecik - Kocaeli - Istanbul");
                Sefer sef2 = new Sefer("2. Sefer", "Demiryolu:Istanbul - Kocaeli - Bilecik - Eski�ehir - Konya - Eskisehir - Bilecik - Kocaeli - Istanbul");
                Sefer sef3 = new Sefer("3. Sefer", "Karayolu:Istanbul - Kocaeli - Ankara - Kocaeli - Istanbul - Kocaeli - Ankara - Kocaeli - Istanbul");
                Sefer sef4 = new Sefer("4. Sefer", "Karayolu:Istanbul - Kocaeli - Eski�ehir - Konya - Eski�ehir - Kocaeli - Istanbul");
                Sefer sef5 = new Sefer("5. Sefer", "Havayolu:Istanbul - Konya - Istanbul");
                Sefer sef6 = new Sefer("6. Sefer", "Havayolu:Istanbul - Ankara - Istanbul");
                sefer_sayisi += 6;

                seferList.Add(sef1);
                seferList.Add(sef2);
                seferList.Add(sef3);
                seferList.Add(sef4);
                seferList.Add(sef5);
                seferList.Add(sef6);
            }
        }

        public void seferEkle(string s_str)
        {
            sefer_sayisi++;
            string s1 = sefer_sayisi.ToString() + ". Sefer";
            //MessageBox.Show(s1);
            Sefer s = new Sefer(s1, s_str);

            seferList.Add(s);
        }

        public List<Sefer> getSeferList()
        {
            return seferList;
        }

        public void setSeferList(List<Sefer> l)
        {
            seferList = l;
        }


        public int getSefer_sayisi()
        {
            return sefer_sayisi;
        }


        public string[] getKarayol_sehirleri()
        {
            return this.karayol_sehirleri;
        }

        public void setKarayol_sehirleri(string[] s)
        {
            this.karayol_sehirleri = s;
        }

        public int[][] getKarayol_fiyatlar()
        {
            return this.karayol_fiyatlar;
        }

        public void setKarayol_fiyatlar(int[][] i)
        {
            this.karayol_fiyatlar = i;
        }

        public string[] getDemiryolu_sehirleri()
        {
            return this.demiryolu_sehirleri;
        }

        public void setDemiryolu_sehirleri(string[] s)
        {
            this.demiryolu_sehirleri = s;
        }

        public int[][] getDemiryolu_fiyatlar()
        {
            return this.demiryolu_fiyatlar;
        }

        public void setDemiryolu_fiyatlar(int[][] i)
        {
            this.demiryolu_fiyatlar = i;
        }

        public string[] getHavayolu_sehirleri()
        {
            return this.havayolu_sehirleri;
        }
        public void setHavayolu_sehirleri(string[] s)
        {
            this.havayolu_sehirleri = s;
        }
        public int[][] getHavayolu_fiyatlar()
        {
            return this.havayolu_fiyatlar;
        }
        public void setHavayolu_fiyatlar(int[][] i)
        {
            this.havayolu_fiyatlar = i;
        }
    }

    public class Route
    {
        public string kalkis = "";
        public string varis = "";
        public int fiyat;
        public int mesafe;


        public string[] karayol_sehirleri;
        public int[][] karayol_mesafe = new int[5][];

        public string[] demiryolu_sehirleri;
        public int[][] demiryolu_mesafe = new int[6][];

        public string[] havayolu_sehirleri;
        public int[][] havayolu_mesafe = new int[3][];


        public Route()
        {
            karayol_sehirleri = new string[] { "Istanbul", "Kocaeli", "Ankara", "Eski�ehir", "Konya" };
            demiryolu_sehirleri = new string[] { "Istanbul", "Kocaeli", "Bilecik", "Ankara", "Eski�ehir", "Konya" };
            havayolu_sehirleri = new string[] { "Istanbul", "Ankara", "Konya" };

            karayol_mesafe[0] = new int[] { -1, 100, 500, 300, 600 };
            karayol_mesafe[1] = new int[] { 100, -1, 400, 200, 500 };
            karayol_mesafe[2] = new int[] { 500, 400, -1, -1, -1 };
            karayol_mesafe[3] = new int[] { 300, 200, -1, -1, 300 };
            karayol_mesafe[4] = new int[] { 600, 500, -1, 300, -1 };

            demiryolu_mesafe[0] = new int[] { -1, 75, 225, 375, 300, 450 };
            demiryolu_mesafe[1] = new int[] { 75, -1, 75, 300, 150, 350 };
            demiryolu_mesafe[2] = new int[] { 225, 75, -1, 225, 75, 300 };
            demiryolu_mesafe[3] = new int[] { 375, 300, 225, -1, 150, -1 };
            demiryolu_mesafe[4] = new int[] { 300, 150, 75, 150, -1, 225 };
            demiryolu_mesafe[5] = new int[] { 450, 350, 300, -1, 225, -1 };

            havayolu_mesafe[0] = new int[] { -1, 250, 300 };
            havayolu_mesafe[1] = new int[] { 250, -1, -1 };
            havayolu_mesafe[2] = new int[] { 300, -1, -1 };
        }



        public string getKalkis()
        {
            return this.kalkis;
        }
        public void setKalkis(string kalkis)
        {
            this.kalkis = kalkis;
        }
        public string getVaris()
        {
            return this.varis;
        }
        public void setVaris(string varis)
        {
            this.varis = varis;
        }
        public int getFiyat()
        {
            return this.fiyat;
        }
        public void setFiyat(int fiyat)
        {
            this.fiyat = fiyat;
        }
        public int getMesafe()
        {
            return this.mesafe;
        }
        public void setMesafe(int mesafe)
        {
            this.mesafe = mesafe;
        }

        public string[] getKarayol_sehirleri()
        {
            return this.karayol_sehirleri;
        }
        public void setKarayol_sehirleri(string[] s)
        {
            this.karayol_sehirleri = s;
        }
        public int[][] getKarayol_mesafe()
        {
            return this.karayol_mesafe;
        }
        public void setKarayol_mesafe(int[][] i)
        {
            this.karayol_mesafe = i;
        }
        public string[] getDemiryolu_sehirleri()
        {
            return this.demiryolu_sehirleri;
        }
        public void setDemiryolu_sehirleri(string[] s)
        {
            this.demiryolu_sehirleri = s;
        }
        public int[][] getDemiryolu_mesafe()
        {
            return this.demiryolu_mesafe;
        }
        public void setDemiryolu_mesafe(int[][] i)
        {
            this.demiryolu_mesafe = i;
        }
        public string[] getHavayolu_sehirleri()
        {
            return this.havayolu_sehirleri;
        }
        public void setHavayolu_sehirleri(string[] s)
        {
            this.havayolu_sehirleri = s;
        }
        public int[][] getHavayolu_mesafe()
        {
            return this.havayolu_mesafe;
        }
        public void setHavayolu_mesafe(int[][] i)
        {
            this.havayolu_mesafe = i;
        }

    }

    public class Sefer
    {
        public string sefer_ismi = "";
        public string guzergah = "";
        public string sefer_cesidi = "";
        public List<string> seferSehirList = new List<string>();
        public List<List<string>> seferSehirIcIce = new List<List<string>>();

        Trip t = new Trip();
        public Sefer()
        {

        }

        public Sefer(string isim, string guzergah)
        {
            this.sefer_ismi = isim;
            this.guzergah = guzergah;
            string[] str = guzergah.Split(":");
            this.sefer_cesidi = str[0];
            string[] str2 = str[1].Split(" - ");
            foreach (string s in str2)
            {
                seferSehirList.Add(s);
            }
            this.sehir_ic_ice_olusturma(seferSehirList.Count);
        }
        public void sehir_ic_ice_olusturma(int boyut)
        {
            //int s_temp = 0;
            for (int s = 0; s < boyut - 1; s++)
            {
                List<string> temp = new List<string>();
                temp.Add(seferSehirList[s]);
                int t1 = 1;
                while (t1 < boyut / 2 + 1 && s + t1 < boyut)
                {
                    //check if the city is already in the list if so break the loop
                    string eklenenSehir = seferSehirList[s + t1];
                    int flag = 0;
                    for (int i = 0; i < t1; i++)
                    {
                        if (temp[i] == eklenenSehir)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                    {
                        break;
                    }
                    temp.Add(seferSehirList[s + t1]);
                    t1++;
                }
                seferSehirIcIce.Add(temp);
            }
        }

        public string getSeferIsmi()
        {
            return this.sefer_ismi;
        }
        public void setSeferIsmi(string sefer_ismi)
        {
            this.sefer_ismi = sefer_ismi;
        }
        public string getGuzergah()
        {
            return this.guzergah;
        }
        public void setGuzergah(string guzergah)
        {
            this.guzergah = guzergah;
        }
        public string getSeferCesidi()
        {
            return this.sefer_cesidi;
        }
        public void setSeferCesidi(string sefer_cesidi)
        {
            this.sefer_cesidi = sefer_cesidi;
        }
        public List<string> getSeferSehirList()
        {
            return this.seferSehirList;
        }
        //public static List<List<string>> seferSehirIcIce = new List<List<string>>();
        public List<List<string>> getIceIce()
        {
            return seferSehirIcIce;
        }
        public void setIceIce(List<List<string>> l)
        {
            this.seferSehirIcIce = l;
        }
        public void yazdir()
        {
            string s = "";

            foreach (List<string> innerList in seferSehirIcIce)
            {
                foreach (string item in innerList)
                {
                    s += item + " ";
                }
                s += "\n"; // �� i�e d�ng�den sonra yeni sat�ra ge�
            }
            MessageBox.Show(s);
        }
    }

    public class Customer
    {
        public string ad_soyad = "";
        public string ad = "";
        public string soyad = "";
        public string sifre = "";
        public static List<Customer> customerList = new List<Customer>();

        public Customer()
        {

        }
        public Customer(string ad_soyad, string sifre)
        {
            this.ad_soyad = ad_soyad;
            this.sifre = sifre;

            string[] str = ad_soyad.Split(" ");
            this.soyad = str[str.Length - 1];
            for (int i = 0; i < str.Length - 1; i++)
            {
                this.ad += str[i] + " ";
            }
            this.ad = this.ad.TrimEnd();
            customerList.Add(this);
        }

        public string getAd_soyad()
        {
            return this.ad_soyad;
        }
        public void setAd_soyad(string ad_soyad)
        {
            this.ad_soyad = ad_soyad;
        }
        public string getAd()
        {
            return this.ad;
        }
        public void setAd(string ad)
        {
            this.ad = ad;
        }
        public string getSoyad()
        {
            return this.soyad;
        }
        public void setSoyad(string soyad)
        {
            this.soyad = soyad;
        }
        public string getSifre()
        {
            return this.sifre;
        }
        public void setSifre(string sifre)
        {
            this.sifre = sifre;
        }
        public List<Customer> getCustomerList()
        {
            return customerList;
        }
        public void setCustomerList(List<Customer> l)
        {
            customerList = l;
        }
    }

    public abstract class Person
    {
        public string ad = "";
        public string soyad = "";

        public virtual string getAd()
        {
            return this.ad;
        }

        public virtual void setAd(string str)
        {
            this.ad = str;
        }

        public virtual string getSoyad()
        {
            return this.soyad;
        }

        public virtual void setSoyad(string str)
        {
            this.soyad = str;
        }
    }

    public class Personel : Person
    {
        public string sirket_ismi = "";
        public int sefer_basi_maas = -1;
        public static List<Personel> staticPersonelList = new List<Personel>();
        public Personel()
        {

        }
        public Personel(string ad, string soyad, string sirket_ismi, int sefer_basi_maas)
        {
            this.ad = ad;
            this.soyad = soyad;
            this.sirket_ismi = sirket_ismi;
            this.sefer_basi_maas = sefer_basi_maas;
            staticPersonelList.Add(this);
        }
        public override string getAd()
        {
            return this.ad;
        }

        public override void setAd(string str)
        {
            this.ad = str;
        }

        public override string getSoyad()
        {
            return this.soyad;
        }

        public override void setSoyad(string str)
        {
            this.soyad = str;
        }

        public string getSirketIsmi()
        {
            return this.sirket_ismi;
        }

        public int getMaas()
        {
            return this.sefer_basi_maas;
        }

        public void setMaas(int x)
        {
            this.sefer_basi_maas = x;
        }

        public void setSirketIsmi(string str)
        {
            this.sirket_ismi = str;
        }

        public List<Personel> getStaticPersonelList()
        {
            return staticPersonelList;
        }
        public void setStaticPersonelList(List<Personel> l)
        {
            staticPersonelList = l;
        }
    }

    public class Passenger : Person
    {
        public string tc_kimlik_no = "";
        public string dogum_tarihi = "";
        public string koltuk = "";
        public static int passanger_ismi;
        public static List<Passenger> passenger_static_list = new List<Passenger>();

        public Passenger()
        {

        }

        public Passenger(string ad, string soyad, string tc_kimlik_no, string dogum_tarihi, string koltuk)
        {
            this.ad = ad;
            this.soyad = soyad;
            this.tc_kimlik_no = tc_kimlik_no;
            this.dogum_tarihi = dogum_tarihi;
            this.koltuk = koltuk;
            passenger_static_list.Add(this);
            passanger_ismi++;
        }

        public override string getAd()
        {
            return this.ad;
        }

        public override void setAd(string str)
        {
            this.ad = str;
        }

        public override string getSoyad()
        {
            return this.soyad;
        }

        public override void setSoyad(string str)
        {
            this.soyad = str;
        }

        public List<Passenger> getStaticPassangerList()
        {
            return passenger_static_list;
        }
        public void setStaticPassangerList(List<Passenger> l)
        {
            passenger_static_list = l;
        }

        public string getTc_kimlik_no()
        {
            return this.tc_kimlik_no;
        }

        public void setTc_kimlik_no(string str)
        {
            this.tc_kimlik_no = str;
        }

        public string getDogum_tarihi()
        {
            return this.dogum_tarihi;
        }
        public void setDogum_tarihi(string str)
        {
            this.dogum_tarihi = str;
        }
        public string getKoltuk()
        {
            return this.koltuk;
        }

        public void setKoltuk(string str)
        {
            this.koltuk = str;
        }
        public int getPassanger_ismi()
        {
            return passanger_ismi;
        }
        public void setPassanger_ismi(int x)
        {
            passanger_ismi = x;
        }
    }

    public class Days
    {
        public string name = "";
        public string eng_name = "";
        public List<Vehicle> vehList = new List<Vehicle>();
        public static List<Days> staticDaysList = new List<Days>();
        //public static List<Vehicle> static_vehicle = new List<Vehicle>();

        public Days()
        {

        }
        public Days(string name, string eng_name, List<Vehicle> v)
        {
            this.name = name;
            this.eng_name = eng_name;
            foreach (Vehicle v1 in v)
            {
                vehList.Add(v1);
            }
            staticDaysList.Add(this);
        }

        public string getName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }

        public string getEngName()
        {
            return eng_name;
        }

        public void setEngName(string eng_name)
        {
            this.eng_name = eng_name;
        }

        public List<Vehicle> getVehList()
        {
            return this.vehList;
        }

        public void setVehList(List<Vehicle> v)
        {
            this.vehList = v;
        }

        public List<Days> getDays()
        {
            return staticDaysList;
        }

        public void setDays(List<Days> d)
        {
            staticDaysList = d;
        }
    }

    public class Reservation
    {
        public static List<Days> d = new List<Days>();
        public Company c = new Company();
        //burada days const tanimlanacak
        public static int ilk_static = 0;

        public Reservation()
        {

            if (ilk_static == 0)
            {
                static_doldurma();
                ilk_static = 1;
            }
        }
        public void static_doldurma()
        {
            Days dort_aralik = new Days("Pazartesi", "Monday", c.getStaticVehicleList());
            Days bes_aralik = new Days("Sal�", "Tuesday", c.getStaticVehicleList());
            Days alti_aralik = new Days("�ar�amba", "Wednesday", c.getStaticVehicleList());
            Days yedi_aralik = new Days("Per�embe", "Thursday", c.getStaticVehicleList());
            Days sekiz_aralik = new Days("Cuma", "Friday", c.getStaticVehicleList());
            Days dokuz_aralik = new Days("Cumartesi", "Saturday", c.getStaticVehicleList());
            Days on_aralik = new Days("Pazar", "Sunday", c.getStaticVehicleList());

            d.Add(dort_aralik);
            d.Add(bes_aralik);
            d.Add(alti_aralik);
            d.Add(yedi_aralik);
            d.Add(sekiz_aralik);
            d.Add(dokuz_aralik);
            d.Add(on_aralik);
        }

        public List<Days> getDays()
        {
            return d;
        }
        public void setDays(List<Days> l)
        {
            d = l;
        }
        public int getStatic_ilk()
        {
            return ilk_static;
        }
        public void setStatic_ilk(int x)
        {
            ilk_static = x;
        }
    }

    public class Transport : IReservable
    {

        public static List<Company> sirketListesi = new List<Company>();
        public static int ilk = 0;
        //arac static list
        public static List<Vehicle> aracListesi = new List<Vehicle>();

        //seyehat static bilgileri ? -> daysdeki static vehicle
        public static List<Vehicle> tumSeferler = new List<Vehicle>();

        //koltuk static list
        public static List<Koltuk> tumKoltuklar = new List<Koltuk>();

        public static List<List<List<Koltuk>>> ic_ice_tum_koltuklar = new List<List<List<Koltuk>>>();

        public Transport()
        {
            if (ilk == 0)
            {
                Company c = new Company();
                Days d = new Days();
                Koltuk k = new Koltuk();
                foreach (Company c1 in c.getStaticCompany())
                {
                    sirketListesi.Add(c1);
                }

                foreach (Vehicle v in c.getStaticVehicleList())
                {
                    aracListesi.Add(v);
                }

                foreach (Days d1 in d.getDays())
                {
                    foreach (Vehicle v in d1.getVehList())
                    {
                        tumSeferler.Add(v);
                    }
                }

                foreach (Koltuk k1 in k.getStatic_koltuk())
                {
                    tumKoltuklar.Add(k1);
                }

                foreach (Vehicle v in c.getStaticVehicleList())
                {
                    ic_ice_tum_koltuklar.Add(v.getKoltuk_ic_ice());
                }
                ilk = 1;
            }
        }

        public List<Company> getStaticSirketList()
        {
            return sirketListesi;
        }
        public void setStaticSirketList(List<Company> s)
        {
            sirketListesi = s;
        }
        public List<Vehicle> getStaticAracList()
        {
            return aracListesi;
        }
        public void setStaticAracList(List<Vehicle> a)
        {
            aracListesi = a;
        }
        public List<Vehicle> getStaticTumSeferler()
        {
            return tumSeferler;
        }
        public void setStaticTumSeferler(List<Vehicle> t)
        {
            tumSeferler = t;
        }
        public List<Koltuk> getStaticTumKoltuklar()
        {
            return tumKoltuklar;
        }
        public void setStaticTumKoltuklar(List<Koltuk> t)
        {
            tumKoltuklar = t;
        }

        public List<List<List<Koltuk>>> getStaticIc_ice_tum_koltuklar()
        {
            return ic_ice_tum_koltuklar;
        }
        public void setStaticIc_ice_tum_koltuklar(List<List<List<Koltuk>>> t)
        {
            ic_ice_tum_koltuklar = t;
        }
    }

    public interface ILoginable
    {
        void setToLoginable();
    }

    public interface IReservable
    {
        List<Company> getStaticSirketList();
        void setStaticSirketList(List<Company> s);
        List<Vehicle> getStaticAracList();
        void setStaticAracList(List<Vehicle> a);
        List<Vehicle> getStaticTumSeferler();
        void setStaticTumSeferler(List<Vehicle> t);
        List<Koltuk> getStaticTumKoltuklar();
        void setStaticTumKoltuklar(List<Koltuk> t);
        List<List<List<Koltuk>>> getStaticIc_ice_tum_koltuklar();
        void setStaticIc_ice_tum_koltuklar(List<List<List<Koltuk>>> t);
    }

    public interface IProfitable
    {
        int gunluk_gider_hesapla(string gun);
        int gunluk_gelir_hesapla(string gun);
    }
}