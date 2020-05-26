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

namespace CreadorUsuariosTest
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            LibreriaClases.EjecutarQuery.insertSQL(txtNombre.Text, txtApellido.Text);
        }

        private void btnBuscarID_Click(object sender, RoutedEventArgs e)
        {
            LibreriaClases.usuario usuario = new LibreriaClases.usuario();
            if(LibreriaClases.EjecutarQuery.selectSQL(int.Parse(txtID.Text)).Any())
            {
                usuario = LibreriaClases.EjecutarQuery.selectSQL(int.Parse(txtID.Text))[0];
                txtNombre.Text = usuario.nombre;
                txtApellido.Text = usuario.apellido;
            }
            else
            {
                MessageBox.Show("Usuario no encontrado", "Alerta", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LibreriaClases.EjecutarQuery.selectAllUsuariosSQL().Any())
            {
                dgUsuario.ItemsSource = LibreriaClases.EjecutarQuery.selectAllUsuariosSQL();
            }
            else
            {
                MessageBox.Show("No se encontro ningun usuario", "Alerta", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
