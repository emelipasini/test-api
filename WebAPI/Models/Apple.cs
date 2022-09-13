namespace WebAPI.Models
{
    public class Apple
    {
        public int id { get; set; }
        public string fecha_modif { get; set; }
        public float latitud_mza { get; set; }
        public float longitud_mza { get; set; }

        public Apple(int id, string fecha_modif, float latitud_mza, float longitud_mza)
        {
            this.id = id;
            this.fecha_modif = fecha_modif;
            this.latitud_mza = latitud_mza;
            this.longitud_mza = longitud_mza;
        }
    }
}
