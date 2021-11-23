using AppComensal.Shared;
using System.Collections.Generic;

namespace AppComensal.Client.Servicios
{
    public class PedidoService
    {
        private List<Item> _items;

        public IEnumerable<Item> Items => _items;

        public PedidoService()
        {
            _items = new List<Item>();
        }
        public void AgregarConsumible(Item item) {
            _items.Add(item);
        }

        public void QuitarConsumible(Item item) {
            _items.Remove(item);
        }

        public void Vaciar() {
            _items.Clear();
        }
    }
}
