using System;
using MySql.Data.MySqlClient;
using LayerEntity;
using System.Data;

namespace LayerDAL
{
    public class Repo
    {
        string cadena = "Server=Localhost;User=root;Password=1218023015Fb;Port=3306;database=first_crud";
        MySqlConnection connection;

        public Repo()
        {
            connection = new MySqlConnection(cadena);
        }

        public bool Prueba()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                return false;
            }
            connection.Close();
            return true;
        }

        public DataTable loadClient()
        {            
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SELECT * FROM `clientes` LIMIT 1000;", connection);
            return LoadData(dataAdapter);              
        }
        public bool Insert(CECliente cliente)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `clientes` (`Nombre`, `Apellido`, `Foto`) VALUES (@nombre, @apellido, @foto); ", connection);
            command.Parameters.AddWithValue("@nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@apellido", cliente.Apellido);
            command.Parameters.AddWithValue("@foto", MySql.Data.MySqlClient.MySqlHelper.EscapeString(cliente.Foto));

            return Execute(command);
        }
        public bool edit(int id,CECliente cliente)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `clientes` SET `Nombre`= @nombre , `Apellido` = @apellido, `Foto` = @foto WHERE `Id` = @id; ", connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nombre", cliente.Nombre);
            command.Parameters.AddWithValue("@apellido", cliente.Apellido);
            command.Parameters.AddWithValue("@foto", MySql.Data.MySqlClient.MySqlHelper.EscapeString(cliente.Foto));

            return Execute(command);
        }

        public bool delete(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `clientes` WHERE `Id` = @id; ", connection);

            command.Parameters.AddWithValue("@id", id);

            return Execute(command);
        }

        private bool Execute(MySqlCommand command)
        {
            connection.Open();

            try
            {
                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        private DataTable LoadData(MySqlDataAdapter dataAdapter)
        {
            try
            {
                connection.Close();

                DataTable data = new DataTable();

                connection.Open();

                dataAdapter.Fill(data);

                connection.Close();

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
