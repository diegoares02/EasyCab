using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WinForms.Internal.Soap.ReportingServices2005.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyCab.Reportes
{
    public partial class ReporteIngresos : Form
    {
        public ReporteIngresos()
        {
            InitializeComponent();
        }

        private void ReporteIngresos_Load(object sender, EventArgs e)
        {
            Cargar();
            // TODO: esta línea de código carga datos en la tabla 'easyCabBDDataSet1.Servicio' Puede moverla o quitarla según sea necesario.
            //this.servicioTableAdapter1.Fill(this.easyCabDatos1.Servicio);
            EasyCabDatosTableAdapters.ServicioTableAdapter adapter = new EasyCabDatosTableAdapters.ServicioTableAdapter(); //con este conjunto de codigos se logra instanciar el dataset          
            EasyCabDatos.ServicioDataTable table = new EasyCabDatos.ServicioDataTable();
            adapter.Fill(table);
            ReportDataSource data = new ReportDataSource("ServicioDatos", (DataTable)table);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(data);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
        private void Cargar()
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var conductor = from d in db.Conductor
                                select d.Nombre;
                var operador = from c in db.Usuario
                               where c.IdPerfil == 3
                               select c.Nombre;
                var cliente = from t in db.Cliente
                              select t.Nombre;
                cmbConductor.DataSource = conductor;
                cmbOperador.DataSource = operador;
                cmbCliente.DataSource = cliente;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //las consultas definidas en el dataset representan los parametros del sistema para el reporte de datos
                EasyCabDatosTableAdapters.ServicioTableAdapter adapter = new EasyCabDatosTableAdapters.ServicioTableAdapter();
                EasyCabDatos.ServicioDataTable table = new EasyCabDatos.ServicioDataTable();
                if (cmbOperador.Text != string.Empty)
                {
                    adapter.FillByConductor(table, BuscarU(cmbOperador.Text));
                }
                if (cmbConductor.Text != string.Empty)
                {
                    adapter.FillByConductor(table, BuscarCon(cmbConductor.Text));
                }
                if (cmbCliente.Text != string.Empty)
                {
                    adapter.FillByCliente(table, BuscarCli(cmbCliente.Text));
                }
                ReportDataSource data = new ReportDataSource("ServicioDatos", (DataTable)table);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(data);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private int BuscarU(string nombre)//busca usuarios
        { 
            using(var db =new DCEasyCabBDDataContext())
            {
                var res = (from b in db.Usuario
                          where b.Nombre == nombre && b.IdPerfil == 3
                          select b.IdUsuario).FirstOrDefault();
                return res;
            }
        }
        private int BuscarCon(string nombre)//busca conductores
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var res = (from b in db.Conductor
                           where b.Nombre == nombre
                           select b.IdConductor).FirstOrDefault();
                return res;
            }
        }
        private int BuscarCli(string nombre) //busca clientes
        {
            using (var db = new DCEasyCabBDDataContext())
            {
                var res = (from b in db.Cliente
                           where b.Nombre == nombre
                           select b.IdCliente).FirstOrDefault();
                return res;
            }
        }
    }
}
