using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StokTakipWeb.Models;
using System.Text.Json;
using System.IO; // Dosya işlemleri için

namespace StokTakipWeb.Controllers;

public class HomeController : Controller
{
    // --- VERİTABANI SİMÜLASYONU (Aynı Mantık) ---
    static List<Ayakkabi> envanter = new List<Ayakkabi>();
    static string dosyaYolu = "envanter.json";

    public HomeController()
    {
        // Controller her çalıştığında verileri kontrol etsin
        if (envanter.Count == 0) VerileriYukle();
    }

    // 1. ANA SAYFA (LİSTELEME)
    public IActionResult Index()
    {
        // Kullanıcı siteye girince listeyi View'a (Ekrana) gönder
        return View(envanter);
    }

    // 2. YENİ EKLEME SAYFASINI GÖSTER
    public IActionResult YeniEkle()
    {
        return View();
    }

    // 3. YENİ AYAKKABIYI KAYDET (Formdan gelen veri)
    [HttpPost]
    public IActionResult YeniEkle(Ayakkabi yeniUrun)
    {
        // Formdan gelen veriyi listeye ekle
        envanter.Add(yeniUrun);
        VerileriKaydet();
        
        // İş bitince ana sayfaya yönlendir
        return RedirectToAction("Index");
    }

    // --- SENİN ESKİ YARDIMCI METOTLARIN (Aynen Buradalar) ---
    void VerileriYukle()
    {
        if (System.IO.File.Exists(dosyaYolu))
        {
            try
            {
                string json = System.IO.File.ReadAllText(dosyaYolu);
                if (!string.IsNullOrEmpty(json))
                    envanter = JsonSerializer.Deserialize<List<Ayakkabi>>(json);
            }
            catch { envanter = new List<Ayakkabi>(); }
        }
    }

    void VerileriKaydet()
    {
        var ayarlar = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(envanter, ayarlar);
        System.IO.File.WriteAllText(dosyaYolu, json);
    }
}