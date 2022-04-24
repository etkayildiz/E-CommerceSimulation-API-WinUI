using MagazaWinUI.DesignPatterns.SingletonPattern;
using MagazaWinUI.DTOClasses;
using MagazaWinUI.Models.Context;
using MagazaWinUI.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagazaWinUI
{
    public partial class Form1 : Form
    {
        MyContext _db;
        public Form1()
        {
            InitializeComponent();
            _db = DBTool.DBInstance;
        }
        
        private async void Form1_Load(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44339/api/");
                Task<HttpResponseMessage> getTask = client.GetAsync("home/getstock");

                HttpResponseMessage result = new HttpResponseMessage();

                try
                {
                    result = getTask.Result;
                }
                catch (Exception)
                {
                    MessageBox.Show("DepoAPI bağlantıyı reddetti");
                    return;
                }

                if (result.IsSuccessStatusCode)
                {
                    string contentString = await getTask.Result.Content.ReadAsStringAsync();

                    StockDTO[] resultContent = JsonConvert.DeserializeObject<StockDTO[]>(contentString);


                    string[] lstViewItems = new string[resultContent.Length];


                    foreach (StockDTO element in resultContent)
                    {
                        lstViewItems = new string[] { element.ID.ToString(), element.UnitPrice.ToString(), element.UnitInStock.ToString(), element.ProductName };
                        ListViewItem listItem = new ListViewItem(lstViewItems);
                        lstView.Items.Add(listItem);
                    }

                    label2.Text = lstView.Items.Count.ToString();


                }
                else
                {
                    MessageBox.Show("DepoAPI ile ilgili bir sorun oluştu");
                }
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            //Flat null geçilebilir müstakil daireler için
            if (listStock.Count > 0 && (!string.IsNullOrEmpty(txtFullName.Text) && (!string.IsNullOrEmpty(txtPhone.Text)) && (!string.IsNullOrEmpty(txtCountry.Text) && (!string.IsNullOrEmpty(txtCity.Text)) && (!string.IsNullOrEmpty(txtNeighborhood.Text)) && (!string.IsNullOrEmpty(txtStreet.Text)) && (!string.IsNullOrEmpty(txtAptNo.Text)) && (!string.IsNullOrEmpty(txtDistrcit.Text)) && !(txtFullName.Text == "İsim Soyisim") && !(txtPhone.Text == "TelNo") && !(txtCountry.Text == "Ülke") && !(txtCity.Text == "Şehir") && !(txtDistrcit.Text == "İlçe") && !(txtNeighborhood.Text == "Mahalle") && !(txtStreet.Text == "Sokak") && !(txtAptNo.Text == "AptNo") && !(txtFlat.Text == "KapıNo (Yok ise 0 giriniz)"))))
            {
                //AptNo ve FlatNo prop'ları byte tipinde olduğu için try ile kontrolü sağlandı
                byte aptNo;
                byte flatNo;

                try
                {
                    aptNo = Convert.ToByte(txtAptNo.Text);
                    flatNo = Convert.ToByte(txtFlat.Text);

                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen AptNo ve KapıNo alanlarına sadece sayı giriniz");
                    return;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44339/api/");
                    Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Home/StockDrop", listStock);

                    HttpResponseMessage result;

                    try
                    {
                        result = postTask.Result;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DepoAPI bağlantıyı reddetti");
                        return;
                    }

                    if (result.IsSuccessStatusCode)
                    {

                        Order myOrder = new Order
                        {
                            FullName = txtFullName.Text,
                            Phone = txtPhone.Text,
                            Country = txtCountry.Text,
                            City = txtCity.Text,
                            District = txtDistrcit.Text,
                            Neighborhood = txtNeighborhood.Text,
                            Street = txtStreet.Text,
                            AptNo = aptNo,
                            Flat = flatNo
                        };

                        _db.Orders.Add(myOrder);
                        _db.SaveChanges(); //Order'ın ID'si oluşması için önceden SaveChanges yaptık

                        Product myProduct;
                        OrderDetail myOrderDetail;

                        foreach (StockDropDTO item in listStock)
                        {
                            myProduct = new Product { PurchasedID = item.ID };
                            _db.Products.Add(myProduct);
                            _db.SaveChanges();
                            myOrderDetail = new OrderDetail { OrderID = myOrder.ID, ProductID = myProduct.ID, Quantity = item.Quantity };
                            _db.Details.Add(myOrderDetail);
                        }

                        _db.SaveChanges();

                        listStock.Clear();//List içindeki ürünler temizlendi
                        lstBox.Items.Clear();//ListBox'taki gösterge temizlendi

                        MessageBox.Show("Sipariş tamamlandı");
                    }
                    else MessageBox.Show("DepoAPI ile ilgili bir sorun oluştu");
                }
            }
            else MessageBox.Show("Lütfen sipariş edilecek ürünlerin ve teslimat bilgilerinin eksiksiz olduğundan emin olun");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lstView.Items.Clear();
            Form1_Load(sender, e);
        }

        //DepoAPI'daki StockDrop action'ı List<StockDropDTO> tipinde argüman istiyor.
        List<StockDropDTO> listStock = new List<StockDropDTO>();

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txtID.Text) && !string.IsNullOrEmpty(txtAmount.Text)) && (!(txtID.Text == "Satın alınacak ürün ID") && !(txtAmount.Text == "Miktar")))
            {
                int id;
                short amount;

                try
                {
                    id = Convert.ToInt32(txtID.Text);
                    amount = Convert.ToInt16(txtAmount.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen ID ve Miktar kısımlarına sadece sayı giriniz");
                    return;
                }

                listStock.Add(new StockDropDTO { ID = id, Quantity = amount });

                lstBox.Items.Add(listStock.LastOrDefault());

                txtID.Clear();
                txtAmount.Clear();
            }
            else MessageBox.Show("Lütfen gerekli alanları boş bırakmayınız");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstBox.SelectedIndex > -1)
            {
                StockDropDTO toBeDeleted = (lstBox.SelectedItem as StockDropDTO);
                listStock.Remove(toBeDeleted);
                lstBox.Items.Remove(lstBox.SelectedItem);
            }
            else MessageBox.Show("Sipariş Listesinden silmek istediğiniz ürünü seçin");
        }

        private void txtID_Click(object sender, EventArgs e)
        {
            InputClear(txtID);
        }

        private void txtAmount_Click(object sender, EventArgs e)
        {
            InputClear(txtAmount);
        }

        private void txtFullName_Click(object sender, EventArgs e)
        {
            InputClear(txtFullName);
        }

        private void txtPhone_Click(object sender, EventArgs e)
        {
            txtPhone.Clear();
            InputClear(txtFlat);
        }

        private void txtCountry_Click(object sender, EventArgs e)
        {
            InputClear(txtCountry);
        }

        private void txtCity_Click(object sender, EventArgs e)
        {
            InputClear(txtCity);
        }

        private void txtDistrcit_Click(object sender, EventArgs e)
        {
            InputClear(txtDistrcit);
        }

        private void txtNeighborhood_Click(object sender, EventArgs e)
        {
            InputClear(txtNeighborhood);
        }

        private void txtStreet_Click(object sender, EventArgs e)
        {
            InputClear(txtStreet);
        }

        private void txtAptNo_Click(object sender, EventArgs e)
        {
            InputClear(txtAptNo);
        }

        private void txtFlat_Click(object sender, EventArgs e)
        {
            InputClear(txtFlat);
        }

        public void InputClear(TextBox inputName)
        {
            inputName.Clear();
        }
    }
}
