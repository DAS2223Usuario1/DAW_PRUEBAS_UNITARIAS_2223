using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GestionBancariaAppNS;
using System.Security.Cryptography.X509Certificates;

namespace GestionBancariaTest
{
    [TestClass]
    public class GestionBancariaTest
    {
        // 1. PRUEBAS REINTEGROS
        [TestMethod]
        public void ValidarReintegroR3ValidoDAS2223()
        {
            double saldoInicial = 1000;
            double reintegro = 250;
            double saldoEsperado = 750;

            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            //Método a probar
            miApp.RealizarReintegro(reintegro);

            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001,
                "Se produjo un error al realizar el reintegro, saldo incorrecto.");
        }

        //Continuación de Prueba R3 (probamos con valores frontera)
        /*Vamos a utilizar un array bidimensional para probar con distintos 
        valores en cada caso de prueba. Pasaremos por cada elemento de la matriz
        mediante el uso de un bucle*/
        [TestMethod]
        public void ValidarReintegroR3ValidoParte2DAS2223()
        {
            double[,] casosPruebaR3 =
            {
                { 1000, 1000, 0 },
                { 1000, 999, 1 },
            };

            for (int i = 0; i < 2; i++)
            {
                double saldoInicial = casosPruebaR3[i, 0];
                double reintegro = casosPruebaR3[i, 1];
                double saldoEsperado = casosPruebaR3[i, 2];

                //Creamos nuevo objeto de Clase GestionBancariaApp para trabajar sobre este
                GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

                miApp.RealizarReintegro(reintegro);

                Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001,
                    "Se produjo un error al realizar el reintegro, saldo incorrecto.");
            }
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidarReintegroR1SaldoInsufDAS2223()
        {
            double[,] casosPruebaR1 =
            {
                { 1000, 1001, 1000 }, //Se espera un saldo final de 1000, ya que tras el error, el saldo queda intacto.
                { 1000, 1500, 1000 }, //Se espera un saldo final de 1000, ya que tras el error, el saldo queda intacto.
            };

            for (int i = 0; i < 2; i++)
            {
                double saldoInicial = casosPruebaR1[i, 0];
                double reintegro = casosPruebaR1[i, 1];
                double saldoEsperado = casosPruebaR1[i, 2];

                //Creamos nuevo objeto de Clase GestionBancariaApp para trabajar sobre este
                GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
                try
                {
                    miApp.RealizarReintegro(reintegro);
                }
                catch(ArgumentOutOfRangeException exception)
                {
                    StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_SALDO_INSUFICIENTE);
                    return;
                }

                Assert.Fail("Error. Se debería haber producido una excepción");
            }
        }
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidarReintegroR2CantidadInsufDAS2223()
        {
            double saldoInicial = 1000;
            double reintegro = -250;
            double saldoFinal = saldoInicial - reintegro;

                //Creamos nuevo objeto de Clase GestionBancariaApp para trabajar sobre este
                GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);
            try
            {
                miApp.RealizarReintegro(reintegro);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                //assert
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                return;
            }
            Assert.Fail("Error. Se debía haber producido una excepción.");
        }
        // 2. PRUEBAS INGRESOS
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidarIngresoIN1CantidadInsufDAS2223()
        {
            double[,] casosPruebaIN1 =
            {
                { 1000, 0, 1000 }, //Se espera un saldo final de 1000, ya que tras el error, el saldo queda intacto.
                { 1000, -1, 1000 }, //Se espera un saldo final de 1000, ya que tras el error, el saldo queda intacto.
                { 1000, -500, 1000 }, //Se espera un saldo final de 1000, ya que tras el error, el saldo queda intacto.
            };

            for (int i = 0; i < 3; i++)
            {
                double saldoInicial = casosPruebaIN1[i, 0];
                double ingreso = casosPruebaIN1[i, 1];
                double saldoEsperado = casosPruebaIN1[i, 2];

                //Creamos nuevo objeto de Clase GestionBancariaApp para trabajar sobre este
                GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

                try
                {
                    miApp.RealizarIngreso(ingreso);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    //assert
                    StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
                    return;
                }
                Assert.Fail("Error. Se debía haber producido una excepción.");
            }
        }
        [TestMethod]
        public void ValidarIngresoIN2ValidoDAS2223()
        {
            double[,] casosPruebaIN2 =
            {
                { 1000, 1, 1001 },
                { 1000, 500, 1500 },
            };

            for (int i = 0; i < 2; i++)
            {
                double saldoInicial = casosPruebaIN2[i, 0];
                double ingreso = casosPruebaIN2[i, 1];
                double saldoEsperado = casosPruebaIN2[i, 2];

                //Creamos nuevo objeto de Clase GestionBancariaApp para trabajar sobre este
                GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

                miApp.RealizarIngreso(ingreso);

                Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001,
                    "Se produjo un error al realizar el ingreso, saldo incorrecto.");
            }
        }
    }
}
