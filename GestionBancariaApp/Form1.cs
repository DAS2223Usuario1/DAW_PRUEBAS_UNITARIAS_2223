using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBancariaAppNS
{
    public partial class GestionBancariaApp : Form
    {
        private double saldo;
        //const int ERR_CANTIDAD_NO_VALIDA = 1;
        public const string ERR_CANTIDAD_NO_VALIDA = "Cantidad No Válida";
        //const int ERR_SALDO_INSUFICIENTE = 2;
        public const string ERR_SALDO_INSUFICIENTE = "Saldo Insuficiente";

        public GestionBancariaApp(double saldo = 0)
        {
            InitializeComponent();
            if (saldo > 0)
                this.saldo = saldo;
            else
                this.saldo = 0;
            txtSaldo.Text = ObtenerSaldo().ToString();
            txtCantidad.Text = "0";
        }

        public double ObtenerSaldo() { return saldo; }

        public int RealizarReintegro(double cantidad) 
        {
            if (cantidad <= 0)
                //return ERR_CANTIDAD_NO_VALIDA;
                throw new ArgumentOutOfRangeException(/*"Cantidad No Válida"*/ERR_CANTIDAD_NO_VALIDA);
            if (saldo < cantidad)
                //return ERR_SALDO_INSUFICIENTE;
                throw new ArgumentOutOfRangeException(/*"Saldo Insuficiente"*/ERR_SALDO_INSUFICIENTE);
            saldo -= cantidad;
            return 0;
        }

        public int RealizarIngreso(double cantidad)
        {
            if (cantidad <= 0)
                //return ERR_CANTIDAD_NO_VALIDA;
                throw new ArgumentOutOfRangeException(ERR_CANTIDAD_NO_VALIDA);
            saldo += cantidad;
            return 0;
        }

        private void btOperar_Click(object sender, EventArgs e)
        {
            double cantidad = Convert.ToDouble(txtCantidad.Text); // Cogemos la cantidad del TextBox y la pasamos a número
            if (rbReintegro.Checked)
            {
                //    int respuesta = RealizarReintegro(cantidad);
                //    if (respuesta == ERR_SALDO_INSUFICIENTE)
                //        MessageBox.Show("No se ha podido realizar la operación (¿Saldo insuficiente?)");
                //    else
                //    if (respuesta == ERR_CANTIDAD_NO_VALIDA)
                //        MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                //    else
                //        MessageBox.Show("Transacción realizada.");

                //}
                //else {
                //    if (RealizarIngreso(cantidad) == ERR_CANTIDAD_NO_VALIDA)
                //        MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                //    else
                //        MessageBox.Show("Transacción realizada.");

                try
                {
                    RealizarReintegro(cantidad);
                    MessageBox.Show("Transacción Realizada.");
                }
                catch (Exception error)
                {
                    if (error.Message.Contains(ERR_SALDO_INSUFICIENTE))
                        MessageBox.Show("No se ha podido realizar la operación (¿Saldo insuficiente?)");
                    else
                        if (error.Message.Contains(ERR_CANTIDAD_NO_VALIDA))
                            MessageBox.Show("Cantidad no Válida, sólo se admiten cantidades positivas.");
                }
            }
            else
            {
                try
                {
                    RealizarIngreso(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch (Exception error)
                {
                    if (error.Message.Contains(ERR_CANTIDAD_NO_VALIDA))
                        MessageBox.Show("Cantidad no válidad sólo se admiten cantidades positivas");
                }
            }
            txtSaldo.Text = ObtenerSaldo().ToString();
        }
    }
}
