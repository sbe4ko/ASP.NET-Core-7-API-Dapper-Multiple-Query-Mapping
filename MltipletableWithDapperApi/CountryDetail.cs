namespace MltipletableWithDapperApi
{
    public class CountryDetail
    {
        public IEnumerable<States> States{ get; set; }
        public IEnumerable<City> City { get; set; }
    }
    public class States 
    {
        public string NameOfState { get; set; }
        public int stateID { get; set; }
    }
    public class City
    {
        public string NameOfCity { get; set; }
        public int CityID { get; set; }
        public int stateID { get; set; }
    }
}
