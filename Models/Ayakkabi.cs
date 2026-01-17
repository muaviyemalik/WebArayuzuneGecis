namespace StokTakipWeb.Models
{
    public class Ayakkabi
    {
        // Her ayakkabının benzersiz bir kimliği olsun (Web'de işimize yarayacak)
        public int Id { get; set; } = new Random().Next(1000, 99999); 
        
        public string Ad { get; set; }
        public int Numara { get; set; }
        public string Renk { get; set; }
        public decimal Fiyat { get; set; }
        public int StokAdedi { get; set; }
    }
}