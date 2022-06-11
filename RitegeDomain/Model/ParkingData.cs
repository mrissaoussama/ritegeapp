namespace RitegeDomain.Model
{
    public class ParkingData
    {public int IdParking { get; set; }
        public string ParkingName { get; set; }
        public ParkingData() { }
        public ParkingData(string parkingName) { ParkingName = parkingName; }

        public ParkingData(int idParking, string parkingName)
        {
            IdParking = idParking;
            ParkingName = parkingName;
        }
    }
}
