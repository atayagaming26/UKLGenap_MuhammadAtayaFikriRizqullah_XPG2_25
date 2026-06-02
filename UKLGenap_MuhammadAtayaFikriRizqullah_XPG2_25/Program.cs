using System;
using System.Collections.Generic;
using System.Linq;

namespace ManajemenStandBazzar
{
    class Stand
    {
        protected string _namaStand;
        protected double _hargaSewaPerhari;
        protected bool _isAvailable;

        public Stand(string namaStand, double hargaSewaperhari)
        {
            NamaStand = namaStand;
            HargaSewaPerhari = hargaSewaperhari;
            _isAvailable = true;

        }

        public string NamaStand
        {
            get { return _namaStand; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _namaStand = value;
            }
        }

        public double HargaSewaPerhari
        {
            get { return _hargaSewaPerhari; }

            set
            {
                if (value > 0)
                    _hargaSewaPerhari = value;
            }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"{NamaStand,-12} | Rp {HargaSewaPerhari,10:N0} / hari | {(IsAvailable ? "Tersedia" : "Tidak tersedia")}");

        }

        public void UbahStatus()
        {
            _isAvailable = !_isAvailable;
        }

        public virtual double HitungTotal(int jumlahHari)
        {
            return HargaSewaPerhari * jumlahHari;
        }
    }

    class StandOutdoor : Stand
    {
        protected double _biayaTenda = 75000;

        public StandOutdoor(string namaStand, double harga)
            : base(namaStand, harga)
        {

        }

        public double BiayaTenda
        {
            get { return _biayaTenda; }
        }

        public override double HitungTotal(int jumlahHari)
        {
            return (HargaSewaPerhari * jumlahHari) + (BiayaTenda * jumlahHari);

        }


    }

    class StandIndoor : Stand
    {
        protected double _biayaListrik = 100000;

        public StandIndoor(string namaStand, double harga)
            : base(namaStand, harga)
        {

        }

        public double BiayaListrik
        {
            get { return _biayaListrik; }
        }

        public override double HitungTotal(int jumlahHari)
        {
            return (HargaSewaPerhari * jumlahHari) + (BiayaListrik * jumlahHari);
        }
    }

    class StandPremium : Stand
    {
        protected double _biayaKeamanan = 300000;

        public StandPremium(string namaStand, double harga)
            : base(namaStand, harga)
        {

        }

        public double BiayaKeamanan
        {
            get { return _biayaKeamanan; }
        }

        public override double HitungTotal(int jumlahHari)
        {
            return (HargaSewaPerhari * jumlahHari) + BiayaKeamanan;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Stand> daftarStand = new List<Stand>()
            {
                new StandOutdoor("Outdoor-1", 400000),
                new StandOutdoor("Outdoor-2", 500000),

                new StandIndoor("Indoor-1", 700000),
                new StandIndoor("Indoor-2", 800000),

                new StandPremium("Premium-1", 1800000),
                new StandPremium("Premium-2", 2000000)
            };


            int pilihan;

            do
            {
                Console.Clear();

                Console.WriteLine("=== Moklet Expo Management Center ===");
                Console.WriteLine("Daftar Stand Tersedia\n");

                foreach (Stand stand in daftarStand)
                {
                    if (stand.IsAvailable)
                    {
                        stand.DisplayInfo();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("1. Sewa Stand");
                Console.WriteLine("2. Akhiri Sewa Stand");
                Console.WriteLine("3. Keluar");


                Console.Write("\nPilih Menu : ");
                pilihan = int.Parse(Console.ReadLine());

                switch (pilihan)
                {
                    case 1:
                        SewaStand(daftarStand);
                        break;

                    case 2:
                        AkhirSewaStand(daftarStand);
                        break;

                    case 3:
                        Console.WriteLine("\nTerima kasih...");
                        break;

                    default:
                        Console.WriteLine("Menu tidak valid!");
                        break;
                }
                if (pilihan != 3)
                {
                    Console.WriteLine("\nTekan ENTER...");
                    Console.ReadLine();
                }

            } while (pilihan != 3);
        }

        static void SewaStand(List<Stand> daftarStand)
        {
            Console.Write("\nMasukkan nama stand :");
            string nama = Console.ReadLine();

            Stand stand = daftarStand.FirstOrDefault(s => s.NamaStand.ToLower() == nama.ToLower());

            if (stand == null)
            {
                Console.WriteLine("Stand tidak ditemukan.");
                return;
            }

            if (!stand.IsAvailable)
            {
                Console.WriteLine("Stand sedang tidak tersedia.");
                return;
            }

            Console.WriteLine(
                $"Stand ditemukan: {stand.NamaStand} | Rp {stand.HargaSewaPerhari:N0}/hari");

            Console.Write("Masukkan jumlah hari : ");
            int jumlahHari = int.Parse(Console.ReadLine());

            double total = stand.HitungTotal(jumlahHari);

            Console.WriteLine($"\nTotal Biaya : Rp {total:N0}");

            stand.UbahStatus();

            Console.WriteLine(
                $"Stand {stand.NamaStand} berhasil disewakan selama {jumlahHari} hari");
        }

        static void AkhirSewaStand(List<Stand> daftarStand)
        {
            Console.WriteLine("\nDaftar Stand Yang Sedang Disewakan\n");

            foreach (Stand s in daftarStand)
            {
                if (!s.IsAvailable)
                {
                    s.DisplayInfo();
                }
            }

            Console.Write("\nMasukkan nama stand : ");
            string nama = Console.ReadLine();

            Stand stand = daftarStand.FirstOrDefault(s => s.NamaStand.ToLower() == nama.ToLower());


            if (stand == null)
            {
                Console.WriteLine("Stand Tidak Ditemukan. ");
                return;
            }

            if (stand.IsAvailable)
            {
                Console.WriteLine("Stand Belum Disewa. ");
                return;
            }

            stand.UbahStatus();
            Console.WriteLine("Sewa stand Berhasil Diakiri. ");
        }
    }
}
