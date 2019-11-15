namespace InventariosApi.Entity
{
    public class Inventarios
    {
        public int IdInventario { get; set; }
        public string Name { get; set; }
        public string TipoProducto { get; set; }
        public double Precio { get; set; }
        public string Anaquel { get; set; }
        public bool Activo { get; set; }
    }
}
