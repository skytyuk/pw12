using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pw12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            date_button.SelectedDate = DateTime.Now;
            delete_entry_button.IsEnabled = false;
            save_button.IsEnabled = false;

        }

        private void delete_entry_button_Click(object sender, RoutedEventArgs e)
        {
            List<Records> li_records = De_Serialize.Deserialize();
            li_records.RemoveAt(records_list.SelectedIndex);
            De_Serialize.Serialize(li_records);
            gg();
            name_text.Clear();
            description_text.Clear();
        }

        private void create_button_Click(object sender, RoutedEventArgs e)
        {

            List<Records> li_records = De_Serialize.Deserialize();
            Records records = new Records();
            bool cheak_Li = true;
            if (string.IsNullOrEmpty(name_text.Text) || string.IsNullOrWhiteSpace(name_text.Text))
            {
                MessageBox.Show("Название записи не должно быть пусто!");
            }
            else
            {
                if (li_records == null)
                {
                    li_records = new List<Records>();
                }
                foreach (Records record in li_records)
                {
                    if (record.Name == name_text.Text && record.Date == date_button.SelectedDate)
                    {
                        MessageBox.Show("Такое название уже есть!");
                        cheak_Li = false;
                        break;
                    }
                    else
                    {
                        cheak_Li = true;
                    }
                }
                if (cheak_Li)
                {
                    records.Name = name_text.Text;
                    records.Description = description_text.Text;
                    records.Date = DateTime.Parse(date_button.Text);
                    li_records.Add(records);
                    De_Serialize.Serialize(li_records);
                    gg();
                    records_list.SelectedIndex = records_list.Items.Count - 1;
                }
            }
        }



        private void Description_Text_Changed(object sender, TextChangedEventArgs e)
        {
            List<Records> li_records = De_Serialize.Deserialize();
            if (records_list.SelectedItem != null)
            {
                foreach (var item in li_records)
                {
                    if (item.Date == date_button.SelectedDate)
                    {
                        if (item.Name == records_list.SelectedItem.ToString())
                        {
                            if (item.Description != description_text.Text)
                            {
                                save_button.IsEnabled = true;
                            }
                            else
                            {
                                save_button.IsEnabled = false;
                            }
                        }
                    }
                }
            }

        }

        private void Name_Text_Changed(object sender, TextChangedEventArgs e)
        {
            List<Records> li_records = De_Serialize.Deserialize();
            if (records_list.SelectedItem != null)
            {
                foreach (var item in li_records)
                {
                    if (item.Date == date_button.SelectedDate)
                    {
                        if (item.Name == records_list.SelectedItem.ToString())
                        {
                            if (item.Name != name_text.Text)
                            {
                                save_button.IsEnabled = true;
                            }
                            else
                            {
                                save_button.IsEnabled = false;
                            }
                        }
                    }
                }
            }
        }

        private void Records_List_Changed(object sender, SelectionChangedEventArgs e)
        {
            gg2();

        }
        public void gg2()
        {
            List<Records> li_records = De_Serialize.Deserialize();

            if (records_list.SelectedItem != null)
            {
                foreach (var item in li_records)
                {
                    if (item.Date == date_button.SelectedDate)
                    {
                        if (item.Name == records_list.SelectedItem.ToString())
                        {
                            name_text.Text = item.Name;
                            description_text.Text = item.Description;
                            delete_entry_button.IsEnabled = true;

                        }

                    }
                }
            }
        }
        private void gg()
        {
            List<Records> li_records = De_Serialize.Deserialize();
            List<string> list = new List<string>();
            if (li_records == null)
            {
                li_records = new List<Records>();
            }
            records_list.Items.Clear();
            foreach (var i in li_records)
            {
                if (i.Date == date_button.SelectedDate)
                {
                    records_list.Items.Add(i.Name);
                }
            }
            delete_entry_button.IsEnabled = false;
            save_button.IsEnabled = false;
        }


        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            List<Records> li_records = De_Serialize.Deserialize();
            Records records = new Records();
            bool cheak_Li = true;
            if (string.IsNullOrEmpty(name_text.Text) || string.IsNullOrWhiteSpace(name_text.Text))
            {
                MessageBox.Show("Название записи не должно быть пусто!");
            }
            else
            {
                if (li_records == null)
                {
                    li_records = new List<Records>();
                }
                foreach (Records record in li_records)
                {
                    if (name_text.Text == records_list.SelectedItem.ToString())
                    {

                        cheak_Li = true;


                    }
                    else
                    {
                        if (record.Name == name_text.Text && record.Date == date_button.SelectedDate)
                        {

                            MessageBox.Show("Такое название уже есть!");
                            cheak_Li = false;
                            break;




                        }
                        else
                        {
                            cheak_Li = true;
                        }
                    }
                }
                if (cheak_Li)
                {
                    if (records_list.SelectedItem != null)
                    {
                        foreach (var item in li_records)
                        {
                            if (item.Date == date_button.SelectedDate)
                            {
                                if (item.Name == records_list.SelectedItem.ToString())
                                {
                                    item.Name = name_text.Text;
                                    item.Description = description_text.Text;
                                    int i = records_list.SelectedIndex;
                                    De_Serialize.Serialize(li_records);
                                    gg();
                                    records_list.SelectedIndex = i;
                                }
                            }
                        }
                    }

                }
            }



        }

        private void date_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (date_button.SelectedDate == null)
            {
                records_list.Items.Clear();
                name_text.Clear();
                description_text.Clear();
                MessageBox.Show("Дата не выбрана");
            }
            else
            {
                name_text.Clear();
                description_text.Clear();
                gg();
            }
        }
    }
}
