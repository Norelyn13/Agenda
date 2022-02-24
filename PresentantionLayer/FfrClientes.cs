using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LayerEntity;
using LayerBusines;


namespace PresentantionLayer
{
    
    public partial class FfrClientes : Form
    {
        CECliente eCliente = new CECliente();
        CNCliente service = new CNCliente();
        public FfrClientes()
        {
            InitializeComponent();
        }
        #region Events
        private void FfrClientes_Load(object sender, EventArgs e)
        {
            load();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            clearForm();
        }
        private void LnkFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            selectedPhoto();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            save();
            load();
        }
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
               txtId.Value = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[2].Value);
                picFoto.ImageLocation = Convert.ToString(dgvClientes.CurrentRow.Cells[3].Value);
            }

        }
        #endregion
        #region Process

        private void load()
        {
            dgvClientes.DataSource = service.load();
            dgvClientes.ClearSelection();
            dgvClientes.Columns[0].Visible = false;

            clearForm();
        }
        public void clearForm()
        {
            txtId.Value = 0;
            txtNombre.Clear();
            txtApellido.Clear();
            picFoto.Image = null;
        }

        public void selectedPhoto()
        {
            if (ofdFoto.ShowDialog() == DialogResult.OK)
            {
                picFoto.Load(ofdFoto.FileName);
            }
            ofdFoto.FileName = string.Empty;
        }


        public void save()
        {
            validation();
        }
        public bool validation()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es necesario para continuar.", "System");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es necesario para continuar.", "System");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(picFoto.ImageLocation))
            {
                MessageBox.Show("La foto es necesario para continuar.", "System");
                return false;
            }
            else
            {
                eCliente = saveClient();

               return eCliente.id != 0 ? Edit(eCliente) : Insert(eCliente);
            }
        }

        
        public CECliente saveClient()
        {
            CECliente cliente = new CECliente();
            {
                cliente.id = (int)txtId.Value;
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Foto = picFoto.ImageLocation;

            }
            return cliente;
        }

        private bool Insert(CECliente cliente)
        {
            if (service.Insert(eCliente))
            {
                MessageBox.Show("El cliente se ha registrado con existo.", "System");
                eCliente = new CECliente();

                return true;
            }
            else
            {
                MessageBox.Show("El cliente no se ha podido registrar.", "System");
                eCliente = new CECliente();

                return false;
            }
        }
        private bool Edit(CECliente cliente)
        {
            if (service.Edit(cliente.id,eCliente))
            {
                MessageBox.Show("El cliente se ha editado con existo.", "System");
                eCliente = new CECliente();

                return true;
            }
            else
            {
                MessageBox.Show("El cliente no se ha podido editar.", "System");
                eCliente = new CECliente();

                return false;
            }
        }

        private void delete(int id)
        {
            if(id == 0)
            {
                MessageBox.Show("Tiene que selecionar algun cliente para poder realizar esta operacion.", "System");
            }
            else
            {       
                DialogResult message;
                message = MessageBox.Show("Estas seguro de que quieres eliminar este cliente?", "System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(message == DialogResult.Yes)
                {
                    if (service.delete(id) == true)
                    {
                        MessageBox.Show("Se ha eliminado correctamente el cliente", "System");
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido eliminar el cliente.", "System");
                    }
                }                
            }
        }

        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            delete((int)txtId.Value);
            load();
        }
    }
}
