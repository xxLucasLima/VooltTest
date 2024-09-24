using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VooltAPI.Application.Interfaces;
using VooltAPI.Domain.Entities;

namespace VooltAPI.Infrastructure.Repositories
{
    public class AdRepository : IAdRepository
    {
        private readonly string _filePath = "./Infrastructure/Data/Data.json";

        public AdRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public  Ad Create(Ad ad)
        {
            var ads =  GetAll();

            ad.AdId = ads.Count > 0 ? ads.Max(ad => ad.AdId) + 1 : 1;

            ads.Add(ad);
            SaveAdsToFile(ads);
            return ad;
        }

        public List<Ad> GetAll()
        {
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Ad>>(jsonData) ?? new List<Ad>();
        }

        public Ad Update(Ad ad)
        {
            var ads = GetAll();
            var adIndex = ads.FindIndex(ad => ad.AdId == ad.AdId);

            if (adIndex != -1)
            {
                ads[adIndex] = ad;
                SaveAdsToFile(ads);
            }
            else
            {
                throw new Exception("Ad not found");
            }

            return ad;

        }

        private async Task SaveAdsToFile(List<Ad> ads)
        {
            var jsonData = JsonConvert.SerializeObject(ads, Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }
    }
}
