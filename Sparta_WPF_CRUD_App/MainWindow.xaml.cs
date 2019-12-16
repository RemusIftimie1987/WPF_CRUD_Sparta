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

namespace Sparta_WPF_CRUD_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Spartan> spartans = new List<Spartan>();
        //just added - not using yet:
        List<Client> clients = new List<Client>();
        List<Job> jobs = new List<Job>();
        Spartan spartan = new Spartan();
        BrushConverter brush = new BrushConverter();

        public MainWindow()
        {
            InitializeComponent();
            Initialise();
        }

        void Initialise()
        {
            using (var db = new SpartaDBEntities())
            {
                spartans = db.Spartans.ToList();
                //just added - not using yet:
                clients = db.Clients.ToList();
                jobs = db.Jobs.ToList();
                // clients      //jobs
            }
            ListViewSpartans.ItemsSource = spartans;
            
            
        }

        #region Read/View
        private void ListViewSpartans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // print out details of selected items
            spartan = (Spartan)ListViewSpartans.SelectedItem;
            if (spartan != null)
            {
                TextBoxId.Text = spartan.SpartanID.ToString();
                TextBoxName.Text = spartan.SpartanName;
                TextBoxCourse.Text = spartan.Course;
                TextBoxIsPlaced.Text = spartan.IsPlaced.ToString();
                ButtonEdit.IsEnabled = true;
                ButtonDelete.IsEnabled = true;
                ButtonDelete.Content = "Delete";
                ButtonAdd.Content = "Add";
                ButtonEdit.Content = "Edit";
                TextBoxId.IsReadOnly = true;
                TextBoxName.IsReadOnly = true;
                TextBoxCourse.IsReadOnly = true;
                TextBoxIsPlaced.IsReadOnly = true;
            }
            
        }
        #endregion

        #region Add_Spartan
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAdd.Content.ToString() == "Add")
            {
                ButtonAdd.Content = "Sure ?";
                TextBoxName.Background = Brushes.White;
                TextBoxCourse.Background = Brushes.White;
                TextBoxIsPlaced.Background = Brushes.White;
                TextBoxName.IsReadOnly = false;
                TextBoxCourse.IsReadOnly = false;
                TextBoxIsPlaced.IsReadOnly = false;
                //clear the boxes
                TextBoxId.Text = "";
                TextBoxName.Text = "";
                TextBoxCourse.Text = "";
                TextBoxIsPlaced.Text = "";
            }
            else
            {
                ButtonAdd.Content = "Add";
                //set boxes back to readonly
                TextBoxName.IsReadOnly = true;
                TextBoxCourse.IsReadOnly = true;
                TextBoxIsPlaced.IsReadOnly = true;
                TextBoxName.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                TextBoxCourse.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                TextBoxIsPlaced.Background = (Brush)brush.ConvertFrom("#C3FFB6");

                //add record to database
                Boolean.TryParse(TextBoxIsPlaced.Text, out bool placed);
                var spartanToAdd = new Spartan()
                {
                    SpartanName = TextBoxName.Text,
                    Course = TextBoxCourse.Text,
                    IsPlaced = placed
                };
                using (var db = new SpartaDBEntities())
                {
                    db.Spartans.Add(spartanToAdd);
                    db.SaveChanges();
                    ListViewSpartans.ItemsSource = null;
                    spartans = db.Spartans.ToList();
                    ListViewSpartans.ItemsSource = spartans;
                }
            }

        }
        #endregion

        #region Edit_Spartan_Details
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonEdit.Content.ToString() == "Edit")
            {
                TextBoxName.IsReadOnly = false;
                TextBoxName.Background = Brushes.White;
                TextBoxCourse.IsReadOnly = false;
                TextBoxCourse.Background = Brushes.White;
                TextBoxIsPlaced.IsReadOnly = false;
                TextBoxIsPlaced.Background = Brushes.White;
                ButtonEdit.IsEnabled = true;
                ButtonEdit.Content = "Save";
            }
            else if (ButtonEdit.Content.ToString() == "Save")
            {
                using (var db = new SpartaDBEntities())
                {
                    var spartanToEdit = db.Spartans.Find(spartan.SpartanID);
                    spartanToEdit.SpartanName = TextBoxName.Text;
                    spartanToEdit.Course = TextBoxCourse.Text;
                    Boolean.TryParse(TextBoxIsPlaced.Text, out bool placed);
                    spartanToEdit.IsPlaced = placed;
                    //save changes to database
                    db.SaveChanges();
                    ListViewSpartans.ItemsSource = null; //reset listview
                    TextBoxId.Text = "Record Updated";
                    TextBoxName.Text = null;
                    TextBoxCourse.Text = null;
                    TextBoxIsPlaced.Text = null;
                    TextBoxName.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                    TextBoxCourse.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                    TextBoxIsPlaced.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                    spartans = db.Spartans.ToList();
                    ListViewSpartans.ItemsSource = spartans;
                }
            }
        }
        #endregion

        #region Delete_Spartan
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonDelete.Content.ToString() == "Delete")
            {
                ButtonDelete.Content = "Confirm";
                TextBoxName.Background = Brushes.LightSalmon;
                TextBoxCourse.Background = Brushes.LightSalmon;
                TextBoxIsPlaced.Background = Brushes.LightSalmon;
            }
            else if (ButtonDelete.Content.ToString() == "Confirm")
            {
                using (var db = new SpartaDBEntities())
                {
                    var removeRecord = db.Spartans.Find(spartan.SpartanID);
                    db.Spartans.Remove(removeRecord);
                    db.SaveChanges();
                    //update list
                    ListViewSpartans.ItemsSource = null;
                    spartans = db.Spartans.ToList();
                    ListViewSpartans.ItemsSource = spartans;
                }
                ButtonDelete.Content = "Delete";
                ButtonDelete.IsEnabled = false;
                ButtonEdit.IsEnabled = false;
                TextBoxName.Text = "Record Deleted";
                TextBoxCourse.Text = null;
                TextBoxIsPlaced.Text = null;
                TextBoxName.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                TextBoxCourse.Background = (Brush)brush.ConvertFrom("#C3FFB6");
                TextBoxIsPlaced.Background = (Brush)brush.ConvertFrom("#C3FFB6");
            }
        }
        #endregion

        #region ClientJob_Queries

        private void ButtonClients_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonViewClients.Content.ToString() == "View Clients")
            {
                ListViewQueries.ItemsSource = clients;
                ButtonViewClients.Content = "Clear";

            }
            else if (ButtonViewClients.Content.ToString() == "Clear")
            {
                ListViewQueries.ItemsSource = null;
                ButtonViewClients.Content = "View Clients";
            }
            
        }

        private void ButtonJobs_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonViewJobs.Content.ToString() == "Available Jobs")
            {
                ListViewQueries.ItemsSource = jobs;
                ButtonViewJobs.Content = "Clear";
            }
            else if (ButtonViewJobs.Content.ToString() == "Clear")
            {
                ListViewQueries.ItemsSource = clients;
                ButtonViewJobs.Content = "Available Jobs";
            }
        }
        #endregion
    }
}
