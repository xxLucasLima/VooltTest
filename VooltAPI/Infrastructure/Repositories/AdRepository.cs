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
            try
            {
                if (!File.Exists(_filePath))
                {
                    File.WriteAllText(_filePath, "[]");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize data file at path: {_filePath}", ex);
            }
        }

        public Ad Create(Ad ad)
        {
            try
            {
                var ads = GetAll();

                if (ad.AdId > 0)
                {
                    var adIndex = ads.FindIndex(existingAd => existingAd.AdId == ad.AdId);

                    if (adIndex != -1)
                    {
                        ads[adIndex] = ad;
                        SaveAdsToFile(ads).Wait();
                    }
                    else
                    {
                        throw new KeyNotFoundException("Ad not found");
                    }
                }
                else
                {
                    ad.AdId = ads.Count > 0 ? ads.Max(existingAd => existingAd.AdId) + 1 : 1;
                    ads.Add(ad);
                    SaveAdsToFile(ads).Wait();
                }

                return ad;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("An error occurred while processing the JSON data.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while creating the ad.", ex);
            }
        }

        public List<Ad> GetAll()
        {
            try
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<Ad>>(jsonData) ?? new List<Ad>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("An error occurred while deserializing the JSON data.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while retrieving ads.", ex);
            }
        }

        private async Task SaveAdsToFile(List<Ad> ads)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(ads, Formatting.Indented);
                await File.WriteAllTextAsync(_filePath, jsonData);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("An error occurred while serializing the ad data.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while saving ads.", ex);
            }
        }
    }
}
