using System;
using LayerEntity;
using LayerDAL;
using System.Data;

namespace LayerBusines
{
    public class CNCliente
    {
        Repo repo = new Repo();

        public bool prueba()
        {
            return repo.Prueba();
        }

        public DataTable load()
        {
            return repo.loadClient();
        }
        public bool Insert(CECliente cliente)
        {
            return repo.Insert(cliente);
        }
        public bool Edit(int Id,CECliente cliente)
        {
            return repo.edit(Id, cliente);
        }
        public bool delete(int Id)
        {
            return repo.delete(Id);
        }

    }
}
