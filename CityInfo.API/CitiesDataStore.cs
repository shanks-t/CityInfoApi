using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }
    //public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public CitiesDataStore()
    {
        // init dummy data
        Cities = new List<CityDto>()
        {
            new CityDto()
            {

                Id = 1,
                Name = "New York City",
                Description = "The greatest city on Earth.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {   
                new PointOfInterestDto() {
                Id = 1,
                Name = "Cathedral of Our Lady",
                Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
            new PointOfInterestDto() {
                Id = 2,
                Name = "Antwerp Central Station",
                Description = "The the finest example of railway architecture in Belgium." },
        }
            },
            new CityDto()
            {

                Id = 2,
                Name = "Nashville",
                Description = "Music City baby!",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                new PointOfInterestDto() {
                Id = 1,
                Name = "Nissan Stadium",
                Description = "Go Titans." },
            new PointOfInterestDto() {
                Id = 2,
                Name = "Ryman auditorium",
                Description = "Honky tonk!" },
            }
            },
            new CityDto()
            {

                Id = 3,
                Name = "Seattle",
                Description = "Lots of clouds, but we have the best coffee.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                new PointOfInterestDto() {
                Id = 1,
                Name = "GasWorks Park",
                Description = "Steam punk with great city view." },
            new PointOfInterestDto() {
                Id = 2,
                Name = "Golden Gardens",
                Description = "Best Sunsets in the whole city" },
            }
            }
        };

    }
    
}

